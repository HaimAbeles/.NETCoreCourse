
Create TABLE [USER] (
    Id INT PRIMARY KEY IDENTITY,
    UserName VARCHAR(100),
    Password VARCHAR(100),
    Email VARCHAR(100),
    Phone VARCHAR(100),
    ClassId SMALLINT,
);