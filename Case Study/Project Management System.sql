Create Database PMSystem;
Use PMSystem;

Create Table Project (
    ID int identity PRIMARY KEY,
    Project_name VARCHAR(100),
    Description TEXT,
    Start_Date DATE,
    Status VARCHAR(20)
);

Create Table Employee (
    ID int identity primary key,
    Name VARCHAR(100),
    Designation VARCHAR(100),
    Gender CHAR(1),
    Salary Decimal(10, 2),
    Project_id int,
    Foreign key (Project_id) REFERENCES Project
);

Create Table Task (
    Task_id int Primary Key,
    Task_name VARCHAR(100),
    Project_id int,
    Employee_id int,
    Status VARCHAR(20),
    FOREIGN KEY (Project_id) REFERENCES Project,
    FOREIGN KEY (Employee_id) REFERENCES Employee
);

Insert into Project Values 
('Project Phoenix', 'Development of AI-based platform', '2023-01-10', 'started'),
('Digital India', 'Nationwide digital infrastructure setup', '2023-02-15', 'dev'),
('Smart City Solutions', 'IoT-based city management', '2023-03-05', 'build'),
('E-commerce Revamp', 'Redesign of e-commerce platform', '2023-04-12', 'test'),
('Banking App', 'Mobile banking solution', '2023-05-20', 'deployed');

Insert into Employee Values
('Amit Sharma', 'Software Engineer', 'M', 75000, 1),
('Pooja Iyer', 'Senior Developer', 'F', 95000, 1),
('Ravi Kumar', 'Project Manager', 'M', 120000, 2),
('Sanya Verma', 'Business Analyst', 'F', 85000, 2),
('Kiran Rao', 'UI/UX Designer', 'M', 70000, 3),
('Neha Singh', 'DevOps Engineer', 'F', 90000, 3),
('Rohan Patil', 'Quality Analyst', 'M', 65000, 4),
('Meera Desai', 'Team Lead', 'F', 110000, 4),
('Arjun Nair', 'Mobile Developer', 'M', 80000, 5),
('Priya Kapoor', 'Database Administrator', 'F', 95000, 5);

Insert into Task Values
(1, 'Develop AI Module', 1, 1, 'Assigned'),
(2, 'Build API Endpoints', 1, 2, 'Started'),
(3, 'Manage Digital Rollout', 2, 3, 'Assigned'),
(4, 'Create Business Models', 2, 4, 'Completed'),
(5, 'Design Interface', 3, 5, 'Assigned'),
(6, 'Setup CI/CD Pipeline', 3, 6, 'Started'),
(7, 'Testing Functionalities', 4, 7, 'Completed'),
(8, 'Lead Team Meetings', 4, 8, 'Started'),
(9, 'Develop Mobile App', 5, 9, 'Completed'),
(10, 'Manage Databases', 5, 10, 'Assigned');

Select * from Project
Select * from Employee
Select * from Task