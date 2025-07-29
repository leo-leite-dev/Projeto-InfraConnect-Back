namespace InfraConnect.Domain.Enums
{
    public enum UserRole
    {
        Admin,              // Acesso total ao sistema, gestão de usuários, parametrizações
        Supervisor,         // Visualiza tudo e aprova tratativas
        DepartmentManager,  // Responsável por aprovar ou recusar tratativas em seu departamento
        Analyst             // Preenche dados técnicos e acompanha tratativas
    }

    public enum UserExternalRole
    {
        Viewer,
        ExternalAgent
    }
}