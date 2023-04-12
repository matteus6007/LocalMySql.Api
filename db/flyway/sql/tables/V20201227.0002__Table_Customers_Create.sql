USE CloudcallContacts;

CREATE TABLE IF NOT EXISTS Customers (
    Id CHAR(36) NOT NULL UNIQUE,
	FirstName NVARCHAR(100),
	LastName NVARCHAR(100),
    CreatedOn DATETIME NOT NULL
);