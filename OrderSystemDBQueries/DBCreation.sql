-- CREATE DATABASE OrderSystem;

-- CREATE SCHEMA OS;

--DROP TABLE OS.OBundleItems;
CREATE TABLE OS.OBundleItems (
	BundleID INT NOT NULL,
	Quantity INT NOT NULL,
	ProductID int NOT NULL
);

--DROP TABLE OS.OBundles;
CREATE TABLE OS.OBundles (
	ID INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(100) NOT NULL
);

--DROP TABLE OS.IBundleItems;
CREATE TABLE OS.IBundleItems (
	BundleID INT NOT NULL,
	Quantity INT NOT NULL,
	ProductID int NOT NULL
);

--DROP TABLE OS.IBundles;
CREATE TABLE OS.IBundles (
	ID INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(100) NOT NULL
);

--DROP TABLE OS.Inventory;
CREATE TABLE OS.Inventory (
	StoreID INT NOT NULL,
	Quantity INT NOT NULL,
	ProductID INT NOT NULL

);

--DROP TABLE OS.OrderItems;
CREATE TABLE OS.OrderItems (
	OrderID INT NOT NULL,
	Quantity INT NOT NULL,
	ProductID INT NOT NULL
);

--DROP TABLE OS.OProduct;
CREATE TABLE OS.OProduct (
	ID INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(100) NOT NULL,
	Price DECIMAL(10, 2) NOT NULL
);

--DROP TABLE OS.IProduct;
CREATE TABLE OS.IProduct (
	ID INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(100) NOT NULL,
	Price DECIMAL(10, 2) NOT NULL
);

--DROP TABLE OS.Orders;
CREATE TABLE OS.Orders (
	ID INT PRIMARY KEY IDENTITY,
	StoreID INT NOT NULL,
	CustomerID INT NOT NULL,
	TimePlaced DATETIME2 NOT NULL
);

--DROP TABLE OS.Customer;
CREATE TABLE OS.Customer (
	ID INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	Birthday DATETIME2 NOT NULL,
	StoreID INT NOT NULL 
);

--DROP TABLE OS.Store;
CREATE TABLE OS.Store (
	ID INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(100) NOT NULL,
);



-- Add Composite Primary Key to Inventory
ALTER TABLE OS.Inventory ADD CONSTRAINT PK_Inventory PRIMARY KEY (StoreID, ProductID);

-- Add composite primary key to OrderItems
ALTER TABLE OS.OrderItems ADD CONSTRAINT PK_OrderItems PRIMARY KEY (OrderID, ProductID);

-- Add composite primary key to BundleItems
ALTER TABLE OS.IBundleItems ADD CONSTRAINT PK_IBundleItems PRIMARY KEY (BundleID, ProductID);
ALTER TABLE OS.OBundleItems ADD CONSTRAINT PK_OBundleItems PRIMARY KEY (BundleID, ProductID);

-- Add foreign key constraints to my tables
ALTER TABLE OS.Customer
	ADD CONSTRAINT FK_Customer_To_Store FOREIGN KEY (StoreID) REFERENCES OS.Store (ID)

ALTER TABLE OS.Inventory ADD 
	CONSTRAINT FK_Inventory_To_Store FOREIGN KEY (StoreID) REFERENCES OS.Store (ID),
	CONSTRAINT FK_Inventory_To_Product FOREIGN KEY (ProductID) REFERENCES OS.IProduct (ID);

ALTER TABLE OS.Orders ADD 
	CONSTRAINT FK_Order_To_Store FOREIGN KEY (StoreID) REFERENCES OS.Store (ID),
	CONSTRAINT FK_Order_To_Customer FOREIGN KEY (CustomerID) REFERENCES OS.Customer (ID);

ALTER TABLE OS.OrderItems ADD 
	CONSTRAINT FK_OrderItems_To_Order FOREIGN KEY (OrderID) REFERENCES OS.Orders (ID),
	CONSTRAINT FK_OrderItems_To_Product FOREIGN KEY (ProductID) REFERENCES OS.OProduct (ID);

ALTER TABLE OS.OBundleItems ADD 
	CONSTRAINT FK_BundleItems_To_OBundles FOREIGN KEY (BundleID) REFERENCES OS.OBundles (ID),
	CONSTRAINT FK_BundleItems_To_OProduct FOREIGN KEY (ProductID) REFERENCES OS.OProduct (ID);

ALTER TABLE OS.IBundleItems ADD 
	CONSTRAINT FK_BundleItems_To_IBundles FOREIGN KEY (BundleID) REFERENCES OS.IBundles (ID),
	CONSTRAINT FK_BundleItems_To_IProduct FOREIGN KEY (ProductID) REFERENCES OS.IProduct (ID);

ALTER TABLE OS.OProduct ADD
	InventoryID INT NOT NULL

ALTER TABLE OS.Oproduct ADD
	CONSTRAINT FK_Oproduct_To_Iproduct FOREIGN KEY (InventoryID) REFERENCES OS.IProduct (ID);


SELECT * FROM OS.Inventory;
SELECT * FROM OS.Iproduct

SELECT * FROM OS.OrderItems;
SELECT * FROM OS.Orders;
SELECT * FROM OS.OProduct

DELETE FROM OS.OrderItems 
DELETE FROM OS.Orders
DELETE FROM OS.OProduct

SELECT * FROM OS.

INSERT INTO OS.IProduct (Name, Price) VALUES
	('Playstation 4', 299.99),
	('Controller (PS4)', 59.99),
	('Read Dead Redemption 2 (PS4)', 59.99)

INSERT INTO OS.Inventory (StoreID, Quantity, ProductID) VALUES 
	(1, 2, 1),
	(1, 5, 2),
	(1, 10, 3)

INSERT INTO OS.Inventory (StoreID, Quantity, ProductID) VALUES  (1, 0, 5)
