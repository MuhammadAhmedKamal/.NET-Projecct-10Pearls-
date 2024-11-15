IF NOT EXISTS (SELECT * FROM SYS.TABLES T WHERE T.NAME = 'Registration')
BEGIN
    CREATE TABLE Registration (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        FirstName VARCHAR(100),
        LastName VARCHAR(100),
        Email VARCHAR(100) UNIQUE,
        Password VARCHAR(256),
        IsActive INT NOT NULL DEFAULT 1,
		Role VARCHAR(50) NOT NULL DEFAULT 'User' 
    );
END


SELECT * FROM Registration;



IF NOT EXISTS (SELECT  * FROM SYS.TABLES T WHERE T.NAME = 'EmployeeTasks')
BEGIN
CREATE TABLE EmployeeTasks (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Tasks VARCHAR(100),
	tasksDescription VARCHAR(100),
	Taskstatus VARCHAR(20) NOT NULL,
)
END

SELECT * FROM EmployeeTasks;