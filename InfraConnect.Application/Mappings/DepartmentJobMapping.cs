using InfraConnect.Domain.Enums;

public static class DepartmentJobMapping
{
    public static class DepartmentJobTitleMapping
    {
        public static readonly Dictionary<Department, List<JobTitle>> JobTitlesByDepartment = new()
        {
            [Department.Regulation] = new()
        {
            JobTitle.Director,
            JobTitle.Manager,
            JobTitle.Coordinator,
            JobTitle.Analyst
        },
            [Department.Operations] = new()
        {
            JobTitle.Director,
            JobTitle.Manager,
            JobTitle.Coordinator,
            JobTitle.Engineer,
            JobTitle.Analyst
        },
            [Department.Engineering] = new()
        {
            JobTitle.Manager,
            JobTitle.Engineer,
            JobTitle.Technician,
            JobTitle.Intern
        },
            [Department.Planning] = new()
        {
            JobTitle.Manager,
            JobTitle.Analyst,
            JobTitle.Coordinator
        },
            [Department.Maintenance] = new()
        {
            JobTitle.Engineer,
            JobTitle.Technician,
            JobTitle.Supervisor,
            JobTitle.Manager
        },
            [Department.Legal] = new()
        {
            JobTitle.Manager,
            JobTitle.Coordinator,
            JobTitle.Analyst
        },
            [Department.AccessControl] = new()
        {
            JobTitle.Supervisor,
            JobTitle.Technician,
            JobTitle.Manager
        }
        };
    }
}
