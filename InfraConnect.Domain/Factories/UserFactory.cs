using InfraConnect.Domain.Entities.Users;
using InfraConnect.Domain.Enums;

namespace InfraConnect.Domain.Factories
{
    public static class UserFactory
    {
        public static User Create(
            string email,
            string passwordHash,
            UserRole role,
            UserProfile profile,
            string? username = null)
        {
            return new User(
                email: email,
                passwordHash: passwordHash,
                role: role,
                profile: profile,
                username: username
            );
        }
    }
}
