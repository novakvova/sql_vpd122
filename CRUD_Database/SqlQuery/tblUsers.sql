IF NOT EXISTS (SELECT * FROM sys.tables WHERE object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TABLE tblUsers
(
    Id int IDENTITY PRIMARY KEY NOT NULL,
    Email nvarchar(50) UNIQUE NOT NULL,
    FirstName nvarchar(50),
    LastName nvarchar(50),
);'
