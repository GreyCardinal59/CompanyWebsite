-- Set formatting parameters
SET NOCOUNT ON;

PRINT '======= 1. LIST OF ALL EMPLOYEES =======';
-- 1. Script for selecting all employees
SELECT 
    SUBSTRING(CONVERT(VARCHAR(36), e.Id), 1, 8) AS ID, 
    e.FullName AS FullName, 
    CONVERT(VARCHAR(10), e.BirthDate, 104) AS BirthDate, 
    CONVERT(VARCHAR(10), e.HireDate, 104) AS HireDate, 
    CAST(e.Salary AS VARCHAR) AS Salary, 
    d.Name AS Department
FROM Employees e
JOIN Departments d ON e.DepartmentId = d.Id
ORDER BY e.FullName;

PRINT '';
PRINT '======= 2. EMPLOYEES WITH SALARY ABOVE 10000 =======';
-- 2. Script for selecting employees with salary above 10000
SELECT 
    SUBSTRING(CONVERT(VARCHAR(36), e.Id), 1, 8) AS ID, 
    e.FullName AS FullName, 
    CONVERT(VARCHAR(10), e.BirthDate, 104) AS BirthDate, 
    CONVERT(VARCHAR(10), e.HireDate, 104) AS HireDate, 
    CAST(e.Salary AS VARCHAR) AS Salary, 
    d.Name AS Department
FROM Employees e
JOIN Departments d ON e.DepartmentId = d.Id
WHERE e.Salary > 10000
ORDER BY e.Salary DESC;

PRINT '';
PRINT '======= 3. EMPLOYEES OLDER THAN 70 YEARS =======';
-- 3. Script for deleting employees older than 70 years
-- Selection for verification
SELECT 
    SUBSTRING(CONVERT(VARCHAR(36), e.Id), 1, 8) AS ID, 
    e.FullName AS FullName, 
    CONVERT(VARCHAR(10), e.BirthDate, 104) AS BirthDate, 
    DATEDIFF(YEAR, e.BirthDate, GETDATE()) AS Age
FROM Employees e
WHERE DATEDIFF(YEAR, e.BirthDate, GETDATE()) > 70;

-- Deletion
PRINT '';
PRINT 'Deleting employees older than 70 years:';
DECLARE @deleted INT;
BEGIN TRANSACTION;
DELETE FROM Employees
WHERE DATEDIFF(YEAR, BirthDate, GETDATE()) > 70;
SET @deleted = @@ROWCOUNT;
COMMIT;
PRINT 'Employees deleted: ' + CAST(@deleted AS VARCHAR);

PRINT '';
PRINT '======= 4. EMPLOYEES WITH SALARY LESS THAN 15000 =======';
-- 4. Script for updating salary to 15000 for employees with lower salary
-- Selection for verification
SELECT 
    SUBSTRING(CONVERT(VARCHAR(36), e.Id), 1, 8) AS ID, 
    e.FullName AS FullName, 
    CAST(e.Salary AS VARCHAR) AS CurrentSalary, 
    '15000' AS NewSalary
FROM Employees e
WHERE e.Salary < 15000;

-- Salary update
PRINT '';
PRINT 'Updating salary to 15000 for employees with lower salary:';
DECLARE @updated INT;
BEGIN TRANSACTION;
UPDATE Employees
SET Salary = 15000
WHERE Salary < 15000;
SET @updated = @@ROWCOUNT;
COMMIT;
PRINT 'Employees updated: ' + CAST(@updated AS VARCHAR); 