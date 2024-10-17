using NUnit.Framework;
using ProjectManagementSystem.BusinessLayer.Repository;
using ProjectManagementSystem.Entity;
using ProjectManagementSystem.BusinessLayer.Service;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ProjectManagement.Tests
{
    [TestFixture]
    public class ExceptionHandlingTests
    {
        private IProjectService _projectService; // Declare this at the class level

        [SetUp]
        public void Setup()
        {
            // Initialize ProjectManagement with a test connection string
            string connectionString = ConfigurationManager.ConnectionStrings["ProjectManagementDB"].ConnectionString;
            _projectService = new ProjectRepositoryImpl(connectionString);
        }

        [Test]
        public void CreateTask_InvalidEmployeeId_ThrowsException()
        {
            var task = new Task
            {
                TaskName = "Invalid Task",
                ProjectId = 2,  
                EmployeeId = 999, 
                Status = "Assigned"
            };

            var ex = Assert.Throws<ArgumentException>(() => _projectService.CreateTask(task));
            Assert.That(ex.Message, Is.EqualTo("Employee with ID 999 does not exist.")); 
        }

        [Test]
        public void DeleteEmployee_NonExistentEmployeeId_ThrowsException()
        {
            int nonExistentEmployeeId = 999; 

            var ex = Assert.Throws<SqlException>(() => _projectService.DeleteEmployee(nonExistentEmployeeId));
            Assert.That(ex.Message, Does.Contain("No rows affected")); 
        }

        [TearDown]
        public void Cleanup()
        {
           
        }
    }
}

