-- Table for storing common person information
CREATE TABLE Persons (
    PersonID INT PRIMARY KEY IDENTITY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    PhoneNumber VARCHAR(20),
    -- Add any other relevant fields
);

-- Table for associating users with their permissions and login data
CREATE TABLE UserLogin (
    UserID INT PRIMARY KEY IDENTITY,
    PersonID INT NOT NULL,
    IsAdmin BOOLEAN NOT NULL DEFAULT FALSE,
    Username VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    FOREIGN KEY (PersonID) REFERENCES Persons(PersonID)
);

-- Table for storing room information
CREATE TABLE Rooms (
    RoomID INT PRIMARY KEY IDENTITY,
    RoomNumber VARCHAR(10) NOT NULL,
    RoomType VARCHAR(50) NOT NULL,
    Rate DECIMAL(10, 2) NOT NULL,
    Description TEXT,
    IsAvailable BOOLEAN NOT NULL DEFAULT TRUE,
    -- Add any other relevant fields
);

-- Room types
-- Standard Room, Single Room, Double Room, Twin Room,
-- Suite Deluxe Room, Family Room, Executive Room,
-- Adjoining Rooms, Accessible Room

-- Table for storing reservations
CREATE TABLE Reservations (
    ReservationID INT PRIMARY KEY IDENTITY,
    CustomerID INT NOT NULL,
    RoomID INT NOT NULL,
    CheckInDate DATE NOT NULL,
    CheckOutDate DATE NOT NULL,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    IsPaid BOOLEAN NOT NULL DEFAULT FALSE,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    -- Add any other relevant fields
);

-- Table for storing check-ins
CREATE TABLE CheckIns (
    CheckInID INT PRIMARY KEY IDENTITY,
    ReservationID INT NOT NULL,
    CheckInDateTime DATETIME NOT NULL,
    CheckOutDateTime DATETIME,
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
    -- Add any other relevant fields
);