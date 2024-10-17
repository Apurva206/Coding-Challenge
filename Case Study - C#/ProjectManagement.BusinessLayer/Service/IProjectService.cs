using System;
using System.Collections.Generic;
using ProjectManagementSystem.Entity;

namespace ProjectManagementSystem.BusinessLayer.Service
{
    public interface IProjectService
    {
        bool CreateEmployee(Employee emp);
        bool CreateProject(Project pj);
        bool CreateTask(Task task);
        bool AssignProjectToEmployee(int projectId, int employeeId);
        bool AssignTaskToEmployee(int taskId, int projectId, int employeeId);
        bool DeleteEmployee(int employeeId);
        bool DeleteProject(int projectId);
        List<Task> GetAllTasksAssignedToEmployee(int employeeId, int projectId);
    }
}
