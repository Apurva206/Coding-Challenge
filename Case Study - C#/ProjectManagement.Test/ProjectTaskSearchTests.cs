using NUnit.Framework;
using ProjectManagementSystem.BusinessLayer.Repository;
using ProjectManagementSystem.Entity;
using ProjectManagementSystem.BusinessLayer.Service;
using System.Collections.Generic;
using System.Configuration;

namespace ProjectManagement.Tests
{
    [TestFixture]
    public class ProjectTaskSearchTests
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
        public void GetAllTasksAssignedToEmployee_ShouldReturnTasksForEmployee()
        {
            int employeeId = 1;
            int projectId = 2; 

            List<Task> tasks = _projectService.GetAllTasksAssignedToEmployee(employeeId, projectId);
            Assert.IsNotNull(tasks, "The task list should not be null.");
            Assert.IsTrue(tasks.Count > 0, "There should be tasks assigned to the employee.");

            // You may want to validate the properties of the tasks
            foreach (var task in tasks)
            {
                Assert.AreEqual(employeeId, task.EmployeeId, "The task should belong to the correct employee.");
                Assert.AreEqual(projectId, task.ProjectId, "The task should belong to the correct project.");
            }
        }

        [TearDown]
        public void Cleanup()
        {
        }
    }
}

