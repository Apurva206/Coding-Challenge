using ProjectManagementSystem.BusinessLayer.Repository;
using ProjectManagementSystem.BusinessLayer.Service;
using ProjectManagementSystem.Entity;
using ProjectManagementSystem.Exceptions;
using System;
using System.Configuration;

namespace ProjectManagementSystem
{
    class ProjectApp
    {
        static void Main(string[] args)
        {
            // Connection string to the database
            string connectionString = ConfigurationManager.ConnectionStrings["ProjectManagementDB"].ConnectionString;
            IProjectService projectRepository = new ProjectRepositoryImpl(connectionString);

            bool exit = false;

            while (!exit)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to Project Management System");
                    Console.WriteLine("Select an operation:");
                    Console.WriteLine("1. Add Employee");
                    Console.WriteLine("2. Add Project");
                    Console.WriteLine("3. Add Task to Project");
                    Console.WriteLine("4. Assign Project to Employee");
                    Console.WriteLine("5. Assign Task to Employee in Project");
                    Console.WriteLine("6. Delete Employee");
                    Console.WriteLine("7. Delete Project");
                    Console.WriteLine("8. List all tasks for an employee in a project");
                    Console.WriteLine("9. Exit");

                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddEmployee(projectRepository);
                            break;

                        case "2":
                            AddProject(projectRepository);
                            break;

                        case "3":
                            AddTaskToProject(projectRepository);
                            break;

                        case "4":
                            AssignProjectToEmployee(projectRepository);
                            break;

                        case "5":
                            AssignTaskToEmployeeInProject(projectRepository);
                            break;

                        case "6":
                            DeleteEmployee(projectRepository);
                            break;

                        case "7":
                            DeleteProject(projectRepository);
                            break;

                        case "8":
                            ListTasksForEmployeeInProject(projectRepository);
                            break;

                        case "9":
                            exit = true;
                            Console.WriteLine("Exiting the application. Goodbye!");
                            break;

                        default:
                            Console.WriteLine("Invalid choice! Please try again.");
                            break;
                    }
                }
                catch (EmployeeNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (ProjectNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"General Error: {ex.Message}");
                }

                if (!exit)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void AddEmployee(IProjectService projectRepository)
        {
            try
            {
                Console.WriteLine("\n--- Add Employee ---");
                Console.Write("Enter Employee Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Designation: ");
                string designation = Console.ReadLine();

                Console.Write("Enter Gender (M/F): ");
                string gender = Console.ReadLine();

                Console.Write("Enter Salary: ");
                decimal salary = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter Project ID (0 if not assigned): ");
                int projectId = Convert.ToInt32(Console.ReadLine());

                Employee emp = new Employee
                {
                    Name = name,
                    Designation = designation,
                    Gender = gender,
                    Salary = salary,
                    ProjectId = projectId
                };

                bool result = projectRepository.CreateEmployee(emp);
                Console.WriteLine(result ? "Employee created successfully!" : "Failed to create employee.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void AddProject(IProjectService projectRepository)
        {
            try
            {
                Console.WriteLine("\n--- Add Project ---");
                Console.Write("Enter Project Name: ");
                string projectName = Console.ReadLine();

                Console.Write("Enter Project Description: ");
                string description = Console.ReadLine();

                Console.Write("Enter Start Date (yyyy-MM-dd): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter Status (e.g., started, dev, build, test, deployed): ");
                string status = Console.ReadLine();

                Project proj = new Project
                {
                    ProjectName = projectName,
                    Description = description,
                    StartDate = startDate,
                    Status = status
                };

                bool result = projectRepository.CreateProject(proj);
                Console.WriteLine(result ? "Project created successfully!" : "Failed to create project.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void AddTaskToProject(IProjectService projectRepository)
        {
            try
            {
                Console.WriteLine("\n--- Add Task to Project ---");
                Console.Write("Enter Task Name: ");
                string taskName = Console.ReadLine();

                Console.Write("Enter Project ID: ");
                int projectId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Employee ID: ");
                int employeeId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Task Status (Assigned, Started, Completed): ");
                string status = Console.ReadLine();

                Task task = new Task
                {
                    TaskName = taskName,
                    ProjectId = projectId,
                    EmployeeId = employeeId,
                    Status = status
                };

                bool result = projectRepository.CreateTask(task);
                Console.WriteLine(result ? "Task added to project successfully!" : "Failed to add task.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void AssignProjectToEmployee(IProjectService projectRepository)
        {
            try
            {
                Console.WriteLine("\n--- Assign Project to Employee ---");
                Console.Write("Enter Project ID: ");
                int projectId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Employee ID: ");
                int employeeId = Convert.ToInt32(Console.ReadLine());

                bool result = projectRepository.AssignProjectToEmployee(projectId, employeeId);
                Console.WriteLine(result ? "Project assigned to employee successfully!" : "Failed to assign project to employee.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void AssignTaskToEmployeeInProject(IProjectService projectRepository)
        {
            try
            {
                Console.WriteLine("\n--- Assign Task to Employee in Project ---");
                Console.Write("Enter Task ID: ");
                int taskId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Project ID: ");
                int projectId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Employee ID: ");
                int employeeId = Convert.ToInt32(Console.ReadLine());

                bool result = projectRepository.AssignTaskToEmployee(taskId, projectId, employeeId);
                Console.WriteLine(result ? "Task assigned to employee successfully!" : "Failed to assign task.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void DeleteEmployee(IProjectService projectRepository)
        {
            try
            {
                Console.WriteLine("\n--- Delete Employee ---");
                Console.Write("Enter Employee ID: ");
                int employeeId = Convert.ToInt32(Console.ReadLine());

                bool result = projectRepository.DeleteEmployee(employeeId);
                Console.WriteLine(result ? "Employee deleted successfully!" : "Failed to delete employee.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void DeleteProject(IProjectService projectRepository)
        {
            try
            {
                Console.WriteLine("\n--- Delete Project ---");
                Console.Write("Enter Project ID: ");
                int projectId = Convert.ToInt32(Console.ReadLine());

                bool result = projectRepository.DeleteProject(projectId);
                Console.WriteLine(result ? "Project deleted successfully!" : "Failed to delete project.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void ListTasksForEmployeeInProject(IProjectService projectRepository)
        {
            try
            {
                Console.WriteLine("\n--- List Tasks for Employee in Project ---");
                Console.Write("Enter Employee ID: ");
                int employeeId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Project ID: ");
                int projectId = Convert.ToInt32(Console.ReadLine());

                var tasks = projectRepository.GetAllTasksAssignedToEmployee(employeeId, projectId);
                if (tasks.Count > 0)
                {
                    Console.WriteLine("\nAssigned Tasks:");
                    foreach (var task in tasks)
                    {
                        Console.WriteLine($"Task ID: {task.TaskId}, Task Name: {task.TaskName}, Status: {task.Status}");
                    }
                }
                else
                {
                    Console.WriteLine("No tasks found for this employee in the project.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}