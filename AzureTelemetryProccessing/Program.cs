// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// New Features:
//
// * New style of application - a back-end app that can connect to IoT Hub and
//   "listen" for telemetry via the EventHub endpoint.
//
// The app will be used to automate the control of the temperature in the cheese
// cave.

using System;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;

using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using AzureTelemetryProccessing;
using System.Security.Principal;
using System.Net.Http.Headers;
using AzureTelemetryProccessing.Models;
using System.Net.Http.Json;

namespace CheeseCaveOperator
{
    class Program
    {
        private static EventHubConsumerClient consumer;
        private static ServiceClient serviceClient;
        private static RegistryManager registryManager;
        private static DateTimeOffset appStartTime = DateTimeOffset.UtcNow;
        public static async Task Main(string[] args)
        {
            ConsoleHelper.WriteColorMessage("Cheese Cave Operator\n", ConsoleColor.Yellow);
            var connectionString = IotConfigs.EVENTHUBS_COMPATIBLE_ENDPOINT;
            // Assigns the value "$Default"
            var consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
            consumer = new EventHubConsumerClient(consumerGroup, connectionString);
            var d2cPartitions = await consumer.GetPartitionIdsAsync();
            serviceClient = ServiceClient.CreateFromConnectionString(IotConfigs.SERVICE_CONNECTION_STRING);
            //await SendCloudToDeviceMessageAsync();

            // Create receivers to listen for messages.
            // As messages sent from devices to an IoT Hub may be handled by any
            // of the partitions, the app has to retrieve messages from each.
            // The next section of code creates a list of asynchronous tasks -
            // each task will receive messages from a specific partition.
            var tasks = new List<Task>();
            foreach (string partition in d2cPartitions)
            {
                tasks.Add(ReceiveMessagesFromDeviceAsync(partition));
            }

            // The final line will wait for all tasks to complete - as each task
            // is going to be in an infinite loop, this line prevents the
            // application from exiting.
            Task.WaitAll(tasks.ToArray());
        }

        // Reads events from the requested partition as an asynchronous
        // enumerable, allowing events to be iterated as they become available
        // on the partition, waiting as necessary should there be no events available.
        // As you can see, this method is supplied with an argument that defines
        // the target partition. Recall that for the default configuration where
        // 4 partitions are specified, this method is called 4 times, each
        // running asynchronously and in parallel, one for each partition.
        private static async Task ReceiveMessagesFromDeviceAsync(string partition)
        {
            EventPosition startingPosition = EventPosition.Earliest;

            // Reads events from the requested partition as an asynchronous
            // enumerable, allowing events to be iterated as they become available
            // on the partition, waiting as necessary should there be no events
            // available.
            await foreach (PartitionEvent partitionEvent in consumer.ReadEventsFromPartitionAsync(
                partition,
                startingPosition))
            {
                string readFromPartition = partitionEvent.Partition.PartitionId;
                // Each event data body is converted from BinaryData to a byte
                // array, and from there, to a string and written to the
                // console for logging purposes.

                //Only get data from the time application running
                DateTimeOffset dateTimeOffset = partitionEvent.Data.EnqueuedTime;
                if (dateTimeOffset > appStartTime)
                {
                    ReadOnlyMemory<byte> eventBodyBytes = partitionEvent.Data.EventBody.ToMemory();

                    string data = Encoding.UTF8.GetString(eventBodyBytes.ToArray());
                    //Print the message
                    ConsoleHelper.WriteGreenMessage("Telemetry received: " + data);
                    foreach (var prop in partitionEvent.Data.Properties)
                    {
                        if (prop.Value.ToString() == "true")
                        {
                            ConsoleHelper.WriteRedMessage(prop.Key);
                        }
                    }
                    Console.WriteLine();
                    //Processing the message
                    if(!string.IsNullOrEmpty(data))
                    {
                        //Create new HTTP Client
                        using HttpClient client = new();
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                        if (data.Contains(IotConfigs.METHOD_RFID_IN))
                        {
                            RFIDIn rfid = JsonConvert.DeserializeObject<RFIDIn>(data);
                            string read_str = "[+] Data read: " + rfid.Method + " ," + rfid.Gate + " ," + rfid.RFIDCode + " ," + rfid.ParkingAreaId + " ," + rfid.Id;
                            Console.WriteLine(read_str);
                            await ProcessRFIDIn(client, rfid);
                        }
                        else if (data.Contains(IotConfigs.METHOD_NOTIFY_PARKING))
                        {
                            NotifyParking notifyParking = JsonConvert.DeserializeObject<NotifyParking>(data);
                            string read_str = "[+] Data read: " + notifyParking.Method + " ," + notifyParking.ParkingSlotId;
                            Console.WriteLine(read_str);
                            await ProcessNotifyParking(client, notifyParking.ParkingSlotId);
                        } else if (data.Contains(IotConfigs.METHOD_NOTIFY_UNPARKING))
                        {
                            NotifyUnparking notifyUnparking = JsonConvert.DeserializeObject<NotifyUnparking>(data);
                            string read_str = "[+] Data read: " + notifyUnparking.Method + " ," + notifyUnparking.ParkingSlotId;
                            Console.WriteLine(read_str);
                            await ProcessNotifyUnparking(client, notifyUnparking.ParkingSlotId);
                        }
                        else
                        {
                            Console.WriteLine("[+] Inavalid string!");
                        }
                    } else
                    {
                        Console.WriteLine("[+] Empty string!");
                    }
                }
            }
        }
        private static async Task ProcessRFIDIn(HttpClient client, RFIDIn rfid)
        {
            var url = IotConfigs.WEBSERVER_HOSTNAME + "/api/ParkingManagement?parkingAreaId=" + rfid.ParkingAreaId + "&RFIDCode=" + rfid.RFIDCode;
            var json = await client.GetStringAsync(url);
            Console.WriteLine("[+] Response: " + json);
            if (!string.IsNullOrEmpty(json))
            {
                RFIDd2c d2c = new RFIDd2c(false, rfid.Id);
                if (!json.Contains("\"title\": \"Not Found\""))
                {
                    RFIDResponse deserializedObj = JsonConvert.DeserializeObject<RFIDResponse>(json);
                    Console.WriteLine("[+] Response: " + deserializedObj.code);
                    if (deserializedObj.code.Equals(rfid.RFIDCode))
                    {
                        d2c.Result = true;
                    }
                }
                //Generate message
                string serializeJSON = JsonConvert.SerializeObject(d2c);
                var commandMessage = new Message(Encoding.ASCII.GetBytes(serializeJSON));
                Console.Write("[+] Device to cloud message: " + serializeJSON);
                await serviceClient.SendAsync(IotConfigs.DEVICE_ID, commandMessage);
                Console.WriteLine();
            }
        }
        private static async Task ProcessNotifyParking(HttpClient client, string parkingSlotId)
        {
            var url = IotConfigs.WEBSERVER_HOSTNAME + "/api/ParkingSlots/" + parkingSlotId;
            var json = await client.GetStringAsync(url);
            Console.WriteLine("[+] Response: " + json);
            if (!string.IsNullOrEmpty(json))
            {
                if(json.Contains("\"title\": \"Not Found\""))
                {
                    Console.WriteLine("[+] ESP8266 send an inavalid parking slot id!");
                } else
                {
                    Console.WriteLine("[+] Changing data with json: " + json);
                    NotifyParkingResponse notifyParking = JsonConvert.DeserializeObject<NotifyParkingResponse>(json);
                    ParkingSlotUpdateRequest updateRequest = new ParkingSlotUpdateRequest(notifyParking.name, 1);
                    var response = client.PutAsJsonAsync(url, updateRequest).Result;
                    if (response.IsSuccessStatusCode)
                        Console.WriteLine("[+] Update complete!, " + response.Content.ReadAsStringAsync().Result);
                    else
                        Console.WriteLine("[+] Update failed!");
                }
            }
            else
                Console.WriteLine("[+] Reponse from server empty or null!");
        }

        private static async Task ProcessNotifyUnparking(HttpClient client, string parkingSlotId)
        {
            var url = IotConfigs.WEBSERVER_HOSTNAME + "/api/ParkingSlots/" + parkingSlotId;
            var json = await client.GetStringAsync(url);
            Console.WriteLine("[+] Response: " + json);
            if (!string.IsNullOrEmpty(json))
            {
                if (json.Contains("\"title\": \"Not Found\""))
                {
                    Console.WriteLine("[+] ESP8266 send an inavalid parking slot id!");
                }
                else
                {
                    Console.WriteLine("[+] Changing data with json: " + json);
                    NotifyParkingResponse notifyParking = JsonConvert.DeserializeObject<NotifyParkingResponse>(json);
                    ParkingSlotUpdateRequest updateRequest = new ParkingSlotUpdateRequest(notifyParking.name, 0);
                    var response = client.PutAsJsonAsync(url, updateRequest).Result;
                    if (response.IsSuccessStatusCode)
                        Console.WriteLine("[+] Update complete!, " + response.Content.ReadAsStringAsync().Result);
                    else
                        Console.WriteLine("[+] Update failed!");
                }
            }
            else
                Console.WriteLine("[+] Reponse from server empty or null!");
        }

        private async static Task SendCloudToDeviceMessageAsync()
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes("Cloud to device message."));
            await serviceClient.SendAsync(IotConfigs.DEVICE_ID, commandMessage);
        }
    }

    internal static class ConsoleHelper
    {
        internal static void WriteColorMessage(string text, ConsoleColor clr)
        {
            Console.WriteLine();
            Console.ForegroundColor = clr;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        internal static void WriteGreenMessage(string text)
        {
            WriteColorMessage(text, ConsoleColor.Green);
        }

        internal static void WriteRedMessage(string text)
        {
            WriteColorMessage(text, ConsoleColor.Red);
        }
    }
}