CREATE TABLE [dbo].[Employees]
(
	EmployeeId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(250) NOT NULL,
    Department NVARCHAR(150) NOT NULL,
    JobTitle NVARCHAR(250) NOT NULL,
    Salary DECIMAL(18,2) NOT NULL CHECK (Salary > 0),
    RemoteWorkStatus NVARCHAR(20) NOT NULL
)
