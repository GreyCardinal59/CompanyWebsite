-- 1. Скрипт для выборки всех сотрудников
SELECT 
    e.Id, 
    e.FullName, 
    e.BirthDate, 
    e.HireDate, 
    e.Salary, 
    d.Name AS DepartmentName
FROM Employees e
JOIN Departments d ON e.DepartmentId = d.Id
ORDER BY e.FullName;

-- 2. Скрипт для выборки сотрудников, у которых зарплата выше 10000
SELECT 
    e.Id, 
    e.FullName, 
    e.BirthDate, 
    e.HireDate, 
    e.Salary, 
    d.Name AS DepartmentName
FROM Employees e
JOIN Departments d ON e.DepartmentId = d.Id
WHERE e.Salary > 10000
ORDER BY e.Salary DESC;

-- 3. Скрипт для удаления сотрудников старше 70 лет
-- Выборка для проверки
SELECT 
    e.Id, 
    e.FullName, 
    e.BirthDate, 
    DATEDIFF(YEAR, e.BirthDate, GETDATE()) AS Age
FROM Employees e
WHERE DATEDIFF(YEAR, e.BirthDate, GETDATE()) > 70;

-- Удаление
DELETE FROM Employees
WHERE DATEDIFF(YEAR, BirthDate, GETDATE()) > 70;

-- 4. Скрипт для обновления зарплаты до 15000 для сотрудников, у которых она меньше
-- Выборка для проверки
SELECT 
    e.Id, 
    e.FullName, 
    e.Salary AS OldSalary, 
    15000 AS NewSalary
FROM Employees e
WHERE e.Salary < 15000;

-- Обновление зп
UPDATE Employees
SET Salary = 15000
WHERE Salary < 15000;