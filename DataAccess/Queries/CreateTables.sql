CREATE TABLE Person( 
    Id INT PRIMARY KEY IDENTITY(1,1),  
    Firstname VARCHAR(35),  
    Lastname VARCHAR(35)
);

CREATE TABLE Address( 
    Id INT PRIMARY KEY IDENTITY(1,1),  
    FK_Person INT FOREIGN KEY REFERENCES Person(Id), 
    Housenum VARCHAR(25),  
    Street VARCHAR(25),  
    City VARCHAR(25),  
    State VARCHAR(10),  
    Country VARCHAR(25),  
    Zipcode VARCHAR(10)
);

CREATE TABLE Phone( 
    Id INT PRIMARY KEY IDENTITY(1,1),  
	FK_Person INT FOREIGN KEY REFERENCES Person(Id), 
    Country VARCHAR(25),  
    Areacode VARCHAR(10), 
    Number VARCHAR(10), 
    Ext VARCHAR(10)
);

-- Future build

--CREATE TABLE Person( 
--    Id INT PRIMARY KEY IDENTITY(1,1),  
--    Firstname VARCHAR(35),  
--    Lastname VARCHAR(35)
--);

--CREATE TABLE Country(
--	Id INT PRIMARY KEY IDENTITY(1,1),
--	Acronym VARCHAR(10),
--	Name VARCHAR(255),
--	Code VARCHAR(10)
--);

--CREATE TABLE Address( 
--    Id INT PRIMARY KEY IDENTITY(1,1),  
--    FK_Person INT FOREIGN KEY REFERENCES Person(id), 
--    Housenum VARCHAR(25),  
--    Street VARCHAR(25),  
--    City VARCHAR(25),  
--    State VARCHAR(10),  
--    Country INT FOREIGN KEY REFERENCES Country(Id),   
--    Zipcode VARCHAR(10)
--);

--CREATE TABLE Phone( 
--    Id INT PRIMARY KEY IDENTITY(1,1),  
--	FK_Person INT FOREIGN KEY REFERENCES Person(id), 
--    Country INT FOREIGN KEY REFERENCES Country(Id),  
--    Areacode VARCHAR(10), 
--    Number VARCHAR(10), 
--    Ext VARCHAR(10)
--);