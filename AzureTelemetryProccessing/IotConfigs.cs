using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTelemetryProccessing
{
    public static class IotConfigs
    {
        //AZURE Constants
        public static readonly string SERVICE_CONNECTION_STRING = "HostName=cong-nguyen.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=e4UlCoLTjE187vIbHrQEjZzVPwvhPsKkZRCbKWCsL7U=";
        public static readonly string EVENTHUBS_COMPATIBLE_ENDPOINT = "Endpoint=sb://ihsuprodsgres025dednamespace.servicebus.windows.net/;" +
            "SharedAccessKeyName=iothubowner;" +
            "SharedAccessKey=XbM66VkkH4KBpfKLIoCjosGqlvypW2JDcX9MvL3zw+Y=;" +
            "EntityPath=iothub-ehub-cong-nguye-22561932-78dd6bd706";
        // The path to the Event Hub entity.
        public static readonly string EVENTHUBS_ENTITY_PATH = "iothub-ehub-cong-nguye-22561932-78dd6bd706";
        public static readonly string EVENTHUBS_SERVICE_KEY = "UPJ8J0vUTgbd1nr9pBE9aYRdnAc48lgHP1TEUW+IOds=";
        // The key for the shared access policy rule for the
        // namespace, or entity.
        public static readonly string EVENTHUBS_IOTHUB_SAS_KEYNAME = "service";
        public static readonly string DEVICE_ID = "esp8266";

        //WebServer Constants
        public static readonly string WEBSERVER_HOSTNAME = "https://localhost:5051";
        public static readonly string API_GET_RFID_BYCODE = "api/RFID/code/";

        //Methods from ESP32
        public static readonly string METHOD_RFID_IN = "RFIDIn";
        public static readonly string METHOD_NOTIFY_PARKING = "NotifyParking";
        public static readonly string METHOD_NOTIFY_UNPARKING = "NotifyUnparking";
    }
}
