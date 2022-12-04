USE ParkingIOT2;
INSERT INTO ParkingAreas VALUES ('d934def1-82fc-4fd3-a56f-4db4aab8c6eb', 'Parking Area 1', 'Ha Noi', 0);
INSERT INTO ParkingAreas VALUES ('971d28ae-69c5-4aa1-beae-d90d4bd91500', 'Parking Area 2', 'Ha Noi', 0);
INSERT INTO ParkingAreas VALUES ('2f1b3f04-378c-4f59-ae7a-469e06d88105', 'Parking Area 3', 'Ha Noi', 0);


INSERT INTO ParkingSlots VALUES ('fa1e4ba9-8ef6-4c86-8037-96890e3065d7', 'A1', 0, 'd934def1-82fc-4fd3-a56f-4db4aab8c6eb');
INSERT INTO ParkingSlots VALUES ('fa1e4ba9-8ef6-4c86-8037-96890e3065d8', 'A2', 0, 'd934def1-82fc-4fd3-a56f-4db4aab8c6eb');
INSERT INTO ParkingSlots VALUES ('fa1e4ba9-8ef6-4c86-8037-96890e3065q9', 'A3', 0, 'd934def1-82fc-4fd3-a56f-4db4aab8c6eb');
INSERT INTO ParkingSlots VALUES ('fa1e4ba9-8ef6-4c86-8037-96820e3065a9', 'B1', 0, 'd934def1-82fc-4fd3-a56f-4db4aab8c6eb');
INSERT INTO ParkingSlots VALUES ('06445048-bcb9-460c-8e2a-d18899808010', 'B2', 0, 'd934def1-82fc-4fd3-a56f-4db4aab8c6eb');
INSERT INTO ParkingSlots VALUES ('fa1e4ba9-8ef6-4c86-8037-96120e3065b9', 'B3', 0, 'd934def1-82fc-4fd3-a56f-4db4aab8c6eb');

INSERT INTO ParkingSlots VALUES ('6026928f-9430-452f-999f-75268182a1f7', 'E1', 0, '971d28ae-69c5-4aa1-beae-d90d4bd91500');
INSERT INTO ParkingSlots VALUES ('c41585e8-89aa-4958-9b6a-ade6b6e76f8b', 'E2', 0, '971d28ae-69c5-4aa1-beae-d90d4bd91500');
INSERT INTO ParkingSlots VALUES ('dec3f915-cce9-47b8-862d-5362d61ab611', 'E3', 0, '971d28ae-69c5-4aa1-beae-d90d4bd91500');

INSERT INTO Customers (Id, Name, Email, LicensePlate) VALUES ('332db0dd-1500-4ee1-a3f9-5dbd81e1b2dd', 'Nguyen Thanh Cong', 'levels1912@gmail.com', '30M2-8621');
INSERT INTO Customers (Id, Name, Email, LicensePlate) VALUES ('7a6b57c6-2dd3-4ab2-aee0-9caf0bd21750', 'Truong Gia Huy', 'hut@gmail.com', '30B2-7898');
INSERT INTO Customers (Id, Name, Email, LicensePlate) VALUES ('42176f00-3b18-4c60-86dc-0e645b8077ab', 'Nguyen Minh Bao', 'bao@gmail.com', '30B2-7749');
INSERT INTO Customers (Id, Name, Email, LicensePlate) VALUES ('92934549-ec6e-4f6a-b8bc-61cbb60890c4', 'Nguyen Minh Hieu', 'hieu@gmail.com', '29B2-1883');
INSERT INTO Customers (Id, Name, Email, LicensePlate) VALUES ('ade299d6-1ae5-4ee3-849f-2b8d5298039b', 'Chu Viet Anh', 'vietanh@gmail.com', '29B2-3306');
INSERT INTO Customers (Id, Name, Email, LicensePlate) VALUES ('3ac050f6-a62e-4415-ab98-8a18426edb49', 'Nguyen Thanh Tung', 'tung@gmail.com', '29B2-4953');

INSERT INTO RFIDCodes (Id, Code) VALUES ('1aff74b4-cf2a-4fd0-8e09-799e7d60d156', 'A2B3C8D7');
INSERT INTO RFIDCodes (Id, Code) VALUES ('c8c147d9-04b2-4c7e-a770-e118b0df858c', 'E2R3T8V7');
INSERT INTO RFIDCodes (Id, Code) VALUES ('59656ee0-e2f4-4cc5-8094-692c2d4d5bfc', 'F2G3W8Q7');
INSERT INTO RFIDCodes (Id, Code) VALUES ('aedd18c6-e8de-47be-97fb-902695ed448b', 'X2Z3U8L7');






