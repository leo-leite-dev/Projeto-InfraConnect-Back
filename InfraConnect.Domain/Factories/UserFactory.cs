using InfraConnect.Domain.Entities.Users;
using InfraConnect.Domain.Enums;

namespace InfraConnect.Domain.Factories
{
    public static class UserFactory
    {
        public static User Create(
              string email,
            string rawPassword,
            UserRole role,
            UserProfile profile,
            string? username,
            Func<string, string> hashFunction)
        {
            var user = new User(
                email: email,
                role: role,
                profile: profile,
                username: username
            );

            user.SetPassword(rawPassword, hashFunction);
            return user;
        }
    }
}