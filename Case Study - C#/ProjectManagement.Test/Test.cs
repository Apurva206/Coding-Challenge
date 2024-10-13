using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using NUnit.Framework;

public class ProjectManagementSystem
{
    private readonly string _connectionString;
    private object employeeId;

    // Constructor to set the connection string
    public ProjectManagementSystem(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Method to create an employee
    public bool CreateEmployee(Employee emp)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Employee (Name, Designation, Gender, Salary, Project_id) VALUES (@Name, @Designation, @Gender, @Salary, @Project_id)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", emp.Name);
                    cmd.Parameters.AddWithValue("@Designation", emp.Designation);
                    cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                    cmd.Parameters.AddWithValue("@Project_id", emp.ProjectId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    // Method to create a task
    public bool CreateTask(Task task)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Task (Task_name, Project_id, Employee_id, Status) VALUES (@Task_name, @Project_id, @Employee_id, @Status)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Task_name", task.TaskName);
                    cmd.Parameters.AddWithValue("@Project_id", task.ProjectId);
                    cmd.Parameters.AddWithValue("@Employee_id", task.EmployeeId);
                    cmd.Parameters.AddWithValue("@Status", task.Status);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    // Method to get all tasks assigned to an employee in a project
    public List<Task> GetAllTasksAssignedToEmployee(int employeeId, int projectId)
    {
        var taskList = new List<Task>();
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Task WHERE Employee_id = @Employee_id AND Project_id = @Project_id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Employee_id", employeeId);
                    cmd.Parameters.AddWithValue("@Project_id", projectId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var task = new Task
                            {
                                TaskId = (int)reader["Task_id"],
                                TaskName = reader["Task_name"].ToString(),
                                ProjectId = (int)reader["Project_id"],
                                EmployeeId = (int)reader["Employee_id"],
                                Status = reader["Status"].ToString()
                            };
                            taskList.Add(task);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return taskList;
    }

    // Method to delete an employee
    public bool DeleteEmployee(int employeeId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Employee WHERE ID = @Employee_id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Employee_id", employeeId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Error: {ex.Message}");
            throw;
        }
    }

    // Method to delete a project
    public bool DeleteProject(int projectId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Project WHERE ID = @Project_id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Project_id", projectId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
                    }

                    return true;
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Error: {ex.Message}");
            throw;
        }
    }

    // Add more methods if necessary

    // Unit Tests using NUnit
    [TestFixture]
    public class ProjectManagementSystemTests
    {
        private ProjectManagementSystem _projectSystem;

        [SetUp]
        public void Setup()
        {
            _projectSystem = new ProjectManagementSystem("Data Source=DESKTOP-5VEHB15;Initial Catalog=PMSystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        }

        [Test]
        public void CreateEmployee_EmployeeCreatedSuccessfully_ReturnsTrue()
        {
            var employee = new Employee
            {
                Name = "John Doe",
                Designation = "Developer",
                Gender = 'M',
                Salary = 75000,
                ProjectId = 1
            };

            var result = _projectSystem.CreateEmployee(employee);

            Assert.IsTrue(result, "Employee should be created successfully.");
        }

        [Test]
        public void CreateTask_TaskCreatedSuccessfully_ReturnsTrue()
        {
            var task = new Task
            {
                TaskName = "Develop Backend",
                ProjectId = 1,
                EmployeeId = 1,
                Status = "Assigned"
            };

            var result = _projectSystem.CreateTask(task);

            Assert.IsTrue(result, "Task should be created successfully.");
        }

        [Test]
        public void GetAllTasksAssignedToEmployee_TasksRetrievedSuccessfully_ReturnsListOfTasks()
        {
            var result = _projectSystem.GetAllTasksAssignedToEmployee(1, 1);

            Assert.IsNotNull(result, "The task list should not be null.");
            Assert.IsTrue(result.Count > 0, "There should be tasks assigned to the employee.");
        }

        [Test]
        public void DeleteEmployee_NonExistentEmployee_ThrowsException()
        {
            var result = _projectSystem.DeleteEmployee(999);  // Assuming 999 is a non-existent employee ID.
            Assert.IsFalse(result, "Delete should return false if employee does not exist.");

            /*Assert.Throws<SqlException>(() => _projectSystem.DeleteEmployee(999), "Exception should be thrown if employee does not exist.");*/
        }
    }
}

[Serializable]
internal class EmployeeNotFoundException : Exception
{
    public EmployeeNotFoundException()
    {
    }

    public EmployeeNotFoundException(string message) : base(message)
    {
    }

    public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected EmployeeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

// Entity classes
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Designation { get; set; }
    public char Gender { get; set; }
    public decimal Salary { get; set; }
    public int ProjectId { get; set; }
}

public class Task
{
    public int TaskId { get; set; }
    public string TaskName { get; set; }
    public int ProjectId { get; set; }
    public int EmployeeId { get; set; }
    public string Status { get; set; }
}