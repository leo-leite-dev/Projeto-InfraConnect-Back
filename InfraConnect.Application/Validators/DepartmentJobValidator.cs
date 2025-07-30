using InfraConnect.Domain.Enums;

namespace InfraConnect.Application.Validators
{
    public static class DepartmentJobValidator
    {
        public static bool IsValidJobTitle(Department department, JobTitle jobTitle)
        {
            return DepartmentJobMapping.DepartmentJobTitleMapping.JobTitlesByDepartment
                .TryGetValue(department, out var validTitles) && validTitles.Contains(jobTitle);
        }
    }
}
