using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ProjectManagementSystem.Entity;
using ProjectManagementSystem.BusinessLayer.Service;
using System.Configuration;

namespace ProjectManagementSystem.BusinessLayer.Repository
{

    public class ProjectRepositoryImpl : IProjectService
        {
            private readonly string _connectionString;

            public ProjectRepositoryImpl(string connectionString)
            {
                _connectionString = connectionString;
            }

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

        public bool CreateProject(Project pj)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Project (Project_name, Description, Start_Date, Status) VALUES (@Project_name, @Description, @Start_Date, @Status)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Project_name", pj.ProjectName);
                        cmd.Parameters.AddWithValue("@Description", pj.Description);
                        cmd.Parameters.AddWithValue("@Start_Date", pj.StartDate);
                        cmd.Parameters.AddWithValue("@Status", pj.Status);

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

        public bool AssignProjectToEmployee(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Employee SET Project_id = @Project_id WHERE Id = @Employee_id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Project_id", projectId);
                        cmd.Parameters.AddWithValue("@Employee_id", employeeId);

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

        public bool AssignTaskToEmployee(int taskId, int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Task SET Employee_id = @Employee_id WHERE Task_id = @Task_id AND Project_id = @Project_id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Employee_id", employeeId);
                        cmd.Parameters.AddWithValue("@Task_id", taskId);
                        cmd.Parameters.AddWithValue("@Project_id", projectId);

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


        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Employee WHERE Id = @Employee_id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Employee_id", employeeId);

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


        public bool DeleteProject(int projectId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    // Delete associated tasks first
                    string deleteTasksQuery = "DELETE FROM Task WHERE Project_id = @Project_id";
                    using (SqlCommand cmd = new SqlCommand(deleteTasksQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Project_id", projectId);
                        cmd.ExecuteNonQuery(); // Delete tasks
                    }

                    // Then delete the project
                    string deleteProjectQuery = "DELETE FROM Project WHERE ID = @Project_id";
                    using (SqlCommand cmd = new SqlCommand(deleteProjectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Project_id", projectId);

                        return cmd.ExecuteNonQuery() > 0; // Return true if project is deleted
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }


        public List<Task> GetAllTasksAssignedToEmployee(int employeeId, int projectId)
        {
            List<Task> tasks = new List<Task>();
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
                                Task task = new Task
                                {
                                    TaskId = Convert.ToInt32(reader["Task_id"]),
                                    TaskName = reader["Task_name"].ToString(),
                                    ProjectId = Convert.ToInt32(reader["Project_id"]),
                                    EmployeeId = Convert.ToInt32(reader["Employee_id"]),
                                    Status = reader["Status"].ToString()
                                };
                                tasks.Add(task);
                            }
                        }
                    }
                }
                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return tasks; // Return empty list on failure
            }
        }


    }
}
