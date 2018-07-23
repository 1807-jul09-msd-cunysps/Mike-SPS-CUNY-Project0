﻿-- Create tables
CREATE TABLE products (
    ID INT PRIMARY KEY,
    Name VARCHAR(35),
    Price INT
);

CREATE TABLE customers (
    ID INT PRIMARY KEY,
    Firstname VARCHAR(35),
	Lastname VARCHAR(35),
    CardNumber VARCHAR(35)
);

CREATE TABLE orders (
    ID INT PRIMARY KEY,
    ProductID INT FOREIGN KEY REFERENCES products(ID),
	CustomerID INT FOREIGN KEY REFERENCES customers(ID)
);

-- Add three records into each table
-- Record 1
INSERT INTO products
	VALUES (0, 'Product1', 10);

INSERT INTO customers
	VALUES (0, 'Billy', 'Bob', '1234567890');

INSERT INTO orders
	VALUES (0, 0, 0);
-- Record 2
INSERT INTO products
	VALUES (1, 'Product2', 100);

INSERT INTO customers
	VALUES (1, 'Johnny', 'Cash', '0123456789');

INSERT INTO orders
	VALUES (1, 1, 1);
-- Record 3
INSERT INTO products
	VALUES (2, 'Product3', 12);

INSERT INTO customers
	VALUES (2, 'Don', 'King', '1234567892');

INSERT INTO orders
	VALUES (2, 2, 2);

-- Add iPhone
INSERT INTO products
	VALUES (3, 'iPhone', 200);

-- Add Tina Smith
INSERT INTO customer
	VALUES (3, 'Tina', 'Smith', '59237895837');

-- Add order for Tina Smith and iPhone
INSERT INTO orders
	VALUES (3, 3, 3);

-- Report orders by Tina Smith
SELECT * FROM orders AS o
	WHERE o.CustomerID = 3;

-- Report revenue generated by iPhone
SELECT ID, Name, COUNT(SELECT * FROM orders WHERE ProductID = 3)
	FROM products
	WHERE ProductID = 3

-- Increase price of iPhone to 250
UPDATE products
	SET Price = 250
	WHERE p.ID = 3;