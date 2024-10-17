using NUnit.Framework;
using ProjectManagementSystem.BusinessLayer.Repository;
using ProjectManagementSystem.Entity;
using ProjectManagementSystem.BusinessLayer.Service;
using System.Configuration;

namespace ProjectManagement.Tests
{
    [TestFixture]
    public class EmployeeCreationTests
    {
        private IProjectService _projectSystem;

        [SetUp]
        public void Setup()
        {
            // Initialize ProjectManagement with a test connection string
            string connectionString = ConfigurationManager.ConnectionStrings["ProjectManagementDB"].ConnectionString;
            _projectSystem = new ProjectRepositoryImpl(connectionString);
        }

        [Test]
        public void CreateEmployee_ShouldCreateEmployeeSuccessfully()
        {
            var employee = new Employee
            {
                Name = "Alice Johnson",
                Designation = "Software Engineer",
                Gender = "M", 
                Salary = 90000,
                ProjectId = 2  // Assuming project ID 2 exists
            };

            var result = _projectSystem.CreateEmployee(employee);
            Assert.IsTrue(result, "Employee should be created successfully.");
        }
    }
}
