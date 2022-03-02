# AspNetUserManagement

### CreateDatabase
```
CREATE DATABASE [AspNetUserManagement]
```
### CreateTable
```
USE [AspNetUserManagement]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [user](
	[pk] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[id] [nvarchar](50) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[passwordSalt] [nvarchar](100) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
) ON [PRIMARY]
GO
```

### AddUser
```
IF  EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'AddUser'))
DROP PROCEDURE AddUser
GO
CREATE PROCEDURE AddUser
     @USER_ID                   NVARCHAR(20)
    ,@USER_PW                   NVARCHAR(100)
    ,@USER_SALT                   NVARCHAR(100)
    ,@USER_NM                   NVARCHAR(20)
----WITH ENCRYPTION
AS
BEGIN
    INSERT INTO  [user]
    (
        [id]
        ,[password]
        ,[passwordSalt]
        ,[name]
    )
    VALUES
    (
     @USER_ID
    ,@USER_PW
    ,@USER_SALT
    ,@USER_NM
    )
END
GO
```
### GetUserByUserID
```
IF  EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'GetUserByUserID'))
DROP PROCEDURE GetUserByUserID
GO
CREATE PROCEDURE GetUserByUserID
     @USER_ID                   NVARCHAR(50)
----WITH ENCRYPTION
AS
BEGIN
    SELECT [pk]
        ,[id]
        ,[password]
        ,[passwordSalt]
        ,[name]
    FROM [user]
    WHERE id = @USER_ID
END
GO
```
### GetUserByUserPK
```
IF  EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'GetUserByUserPK'))
DROP PROCEDURE GetUserByUserPK
GO
CREATE PROCEDURE GetUserByUserPK
     @USER_PK                   NVARCHAR(50)
----WITH ENCRYPTION
AS
BEGIN
    SELECT [pk]
        ,[id]
        ,[password]
        ,[passwordSalt]
        ,[name]
    FROM [user]
    WHERE pk = @USER_PK
END
GO
```
