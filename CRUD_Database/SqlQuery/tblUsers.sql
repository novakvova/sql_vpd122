﻿CREATE TABLE tblUsers(
    ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
    EMAIL NVARCHAR(50) NOT NULL CHECK(EMAIL <> N''),
    FIRSTNAME NVARCHAR(50) NOT NULL CHECK(FIRSTNAME <> N''),
    LASTNAME NVARCHAR(50) NOT NULL CHECK(LASTNAME <> N''));

CREATE TABLE tblRoles(ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
    NAME NVARCHAR(50) NOT NULL CHECK(NAME <> N''));

CREATE TABLE tblUserRoles(ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    USERID INT NOT NULL UNIQUE FOREIGN KEY REFERENCES tblUsers(ID),
    ROLEID INT NOT NULL UNIQUE FOREIGN KEY REFERENCES tblRoles(ID));