-- Table for storing general person information
CREATE TABLE Persons (
    PersonID INT PRIMARY KEY IDENTITY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Username VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    PhoneNumber VARCHAR(20),
    -- Add any other relevant fields
);

-- Table for storing customer information
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY,
    PersonID INT,
    FOREIGN KEY (PersonID) REFERENCES Persons(PersonID)
);

-- Table for storing employee information
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY,
    IsAdmin BIT NOT NULL DEFAULT 0,
    PersonID INT,
    FOREIGN KEY (PersonID) REFERENCES Persons(PersonID)
);

-- Table for storing room information
CREATE TABLE Rooms (
    RoomID INT PRIMARY KEY IDENTITY,
    RoomNumber VARCHAR(10) NOT NULL,
    RoomType VARCHAR(50) NOT NULL,
    Rate DECIMAL(10, 2) NOT NULL,
    Description TEXT,
    IsAvailable BIT NOT NULL DEFAULT 1,
);

-- Room types
-- Standard Room, Single Room, Double Room, Twin Room,
-- Suite Deluxe Room, Family Room, Executive Room,
-- Adjoining Rooms, Accessible Room

-- Table for storing reservations
CREATE TABLE Reservations (
    ReservationID INT PRIMARY KEY IDENTITY,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    IsPaid BIT NOT NULL DEFAULT 0,
    CustomerID INT,
    RoomID INT,
    FOREIGN KEY (CustomerID) REFERENCES Person(PersonID),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    -- Add any other relevant fields
);

-- Table for storing check-ins
CREATE TABLE CheckIns (
    CheckInID INT PRIMARY KEY IDENTITY,
    CheckInDateTime DATETIME NOT NULL,
    CheckOutDateTime DATETIME,
    ReservationID INT,
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID)
);

/*

STORED PROCEDURES

----------------------------------------------------------------------

-- Insert into Persons
CREATE PROCEDURE spInsert_Persons
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Username VARCHAR(50),
    @PasswordHash VARCHAR(50),
    @Email VARCHAR(100),
    @PhoneNumber VARCHAR(20)
AS
BEGIN
    INSERT INTO Persons (FirstName, LastName, Email, Username, PasswordHash, PhoneNumber)
    VALUES (@FirstName, @LastName,@Email, @Username, @PasswordHash, @PhoneNumber);
END
GO

-- Delete from Persons
CREATE PROCEDURE spDelete_Persons
    @PersonID INT
AS
BEGIN
    DELETE FROM Persons WHERE PersonID = @PersonID;
END
GO

-- Update Persons
CREATE PROCEDURE spUpdate_Persons
    @PersonID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Username VARCHAR(50),
    @PasswordHash VARCHAR(50),
    @Email VARCHAR(100),
    @PhoneNumber VARCHAR(20)
AS
BEGIN
    UPDATE Persons
    SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Username = @Username, PasswordHash = @PasswordHash, PhoneNumber = @PhoneNumber
    WHERE PersonID = @PersonID;
END
GO

-- Get all Persons
CREATE PROCEDURE spGetAll_Persons
AS
BEGIN
    SELECT * FROM Persons;
END
GO

-- Get specific Person
CREATE PROCEDURE spGet_Persons
    @PersonID INT
AS
BEGIN
    SELECT * FROM Persons WHERE PersonID = @PersonID;
END
GO

----------------------------------------------------------------------

-- Insert into Customers
CREATE PROCEDURE spInsert_Customers
    @PersonID INT
AS
BEGIN
    INSERT INTO Customers (PersonID)
    VALUES (@PersonID);
END
GO

-- Delete from Customers
CREATE PROCEDURE spDelete_Customers
    @CustomerID INT
AS
BEGIN
    DELETE FROM Customers WHERE CustomerID = @CustomerID;
END
GO

-- Update Customers
CREATE PROCEDURE spUpdate_Customers
    @CustomerID INT,
    @PersonID INT
AS
BEGIN
    UPDATE Customers
    SET PersonID = @PersonID
    WHERE CustomerID = @CustomerID;
END
GO

-- Get all Customers
CREATE PROCEDURE spGetAll_Customers
AS
BEGIN
    SELECT * FROM Customers;
END
GO

-- Get specific Customer
CREATE PROCEDURE spGet_Customers
    @CustomerID INT
AS
BEGIN
    SELECT * FROM Customers WHERE CustomerID = @CustomerID;
END
GO


----------------------------------------------------------------------

-- Insert into Employees
CREATE PROCEDURE spInsert_Employees
    @IsAdmin BIT,
    @PersonID INT
AS
BEGIN
    INSERT INTO Employees (IsAdmin, PersonID)
    VALUES (@IsAdmin, @PersonID);
END
GO

-- Delete from Employees
CREATE PROCEDURE spDelete_Employees
    @EmployeeID INT
AS
BEGIN
    DELETE FROM Employees WHERE EmployeeID = @EmployeeID;
END
GO

-- Update Employees
CREATE PROCEDURE spUpdate_Employees
    @EmployeeID INT,
    @IsAdmin BIT,
    @PersonID INT
AS
BEGIN
    UPDATE Employees
    SET IsAdmin = @IsAdmin, PersonID = @PersonID
    WHERE EmployeeID = @EmployeeID;
END
GO

-- Get all Employees
CREATE PROCEDURE spGetAll_Employees
AS
BEGIN
    SELECT * FROM Employees;
END
GO

-- Get specific Employee
CREATE PROCEDURE spGet_Employees
    @EmployeeID INT
AS
BEGIN
    SELECT * FROM Employees WHERE EmployeeID = @EmployeeID;
END
GO


----------------------------------------------------------------------

-- Insert into Rooms
CREATE PROCEDURE spInsert_Rooms
    @RoomNumber VARCHAR(10),
    @RoomType VARCHAR(50),
    @Rate DECIMAL(10, 2),
    @Description TEXT,
    @IsAvailable BIT
AS
BEGIN
    INSERT INTO Rooms (RoomNumber, RoomType, Rate, Description, IsAvailable)
    VALUES (@RoomNumber, @RoomType, @Rate, @Description, @IsAvailable);
END
GO

-- Delete from Rooms
CREATE PROCEDURE spDelete_Rooms
    @RoomID INT
AS
BEGIN
    DELETE FROM Rooms WHERE RoomID = @RoomID;
END
GO

-- Update Rooms
CREATE PROCEDURE spUpdate_Rooms
    @RoomID INT,
    @RoomNumber VARCHAR(10),
    @RoomType VARCHAR(50),
    @Rate DECIMAL(10, 2),
    @Description TEXT,
    @IsAvailable BIT
AS
BEGIN
    UPDATE Rooms
    SET RoomNumber = @RoomNumber, RoomType = @RoomType, Rate = @Rate, Description = @Description, IsAvailable = @IsAvailable
    WHERE RoomID = @RoomID;
END
GO

-- Get all Rooms
CREATE PROCEDURE spGetAll_Rooms
AS
BEGIN
    SELECT * FROM Rooms;
END
GO

-- Get specific Room
CREATE PROCEDURE spGet_Rooms
    @RoomID INT
AS
BEGIN
    SELECT * FROM Rooms WHERE RoomID = @RoomID;
END
GO


----------------------------------------------------------------------

-- Insert into Reservations
CREATE PROCEDURE spInsert_Reservations
    @TotalAmount DECIMAL(10, 2),
    @IsPaid BIT,
    @CustomerID INT,
    @RoomID INT
AS
BEGIN
    INSERT INTO Reservations (TotalAmount, IsPaid, CustomerID, RoomID)
    VALUES (@TotalAmount, @IsPaid, @CustomerID, @RoomID);
END
GO

-- Delete from Reservations
CREATE PROCEDURE spDelete_Reservations
    @ReservationID INT
AS
BEGIN
    DELETE FROM Reservations WHERE ReservationID = @ReservationID;
END
GO

-- Update Reservations
CREATE PROCEDURE spUpdate_Reservations
    @ReservationID INT
    @TotalAmount DECIMAL(10, 2),
    @IsPaid BIT,
    @CustomerID INT,
    @RoomID INT
AS
BEGIN
    UPDATE Reservations
    SET TotalAmount = @TotalAmount, IsPaid = @IsPaid, CustomerID = @CustomerID, RoomID = @RoomID
    WHERE ReservationID = @ReservationID;
END
GO

-- Get all Reservations
CREATE PROCEDURE spGetAll_Reservations
AS
BEGIN
    SELECT * FROM Reservations;
END
GO

-- Get specific Reservation
CREATE PROCEDURE spGet_Reservations
    @ReservationID INT
AS
BEGIN
    SELECT * FROM Reservations WHERE ReservationID = @ReservationID;
END
GO


----------------------------------------------------------------------

-- Insert into CheckIns
CREATE PROCEDURE spInsert_CheckIns
    @CheckInDateTime DATETIME,
    @CheckOutDateTime DATETIME,
    @ReservationID INT
AS
BEGIN
    INSERT INTO CheckIns (CheckInDateTime, CheckOutDateTime, ReservationID)
    VALUES (@CheckInDateTime, @CheckOutDateTime, @ReservationID);
END
GO

-- Delete from CheckIns
CREATE PROCEDURE spDelete_CheckIns
    @CheckInID INT
AS
BEGIN
    DELETE FROM CheckIns WHERE CheckInID = @CheckInID;
END
GO

-- Update CheckIns
CREATE PROCEDURE spUpdate_CheckIns
    @CheckInID INT,
    @CheckInDateTime DATETIME,
    @CheckOutDateTime DATETIME,
    @ReservationID INT
AS
BEGIN
    UPDATE CheckIns
    SET CheckInDateTime = @CheckInDateTime, CheckOutDateTime = @CheckOutDateTime, ReservationID = @ReservationID
    WHERE CheckInID = @CheckInID;
END
GO

-- Get all CheckIns
CREATE PROCEDURE spGetAll_CheckIns
AS
BEGIN
    SELECT * FROM CheckIns;
END
GO

-- Get specific CheckIn
CREATE PROCEDURE spGet_CheckIns
    @CheckInID INT
AS
BEGIN
    SELECT * FROM CheckIns WHERE CheckInID = @CheckInID;
END
GO


----------------------------------------------------------------------

*/