using NUnit.Framework;
using ProjectManagementSystem.BusinessLayer.Repository;
using ProjectManagementSystem.Entity;
using ProjectManagementSystem.BusinessLayer.Service;
using System.Configuration;

namespace ProjectManagement.Tests
{
    [TestFixture]
    public class TaskCreationTests
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
        public void CreateTask_ShouldCreateTaskSuccessfully()
        {
            var task = new Task
            {
                TaskName = "Implement Authentication",
                ProjectId = 2,
                EmployeeId = 1, 
                Status = "Assigned"
            };

            var result = _projectService.CreateTask(task);

            Assert.IsTrue(result, "Task should be created successfully.");
        }

        [TearDown]
        public void Cleanup()
        {
          
        }
    }
}
