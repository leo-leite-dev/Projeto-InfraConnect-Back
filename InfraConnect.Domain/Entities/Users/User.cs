using InfraConnect.Domain.Enums;
using InfraConnect.Domain.Exceptions;

namespace InfraConnect.Domain.Entities.Users
{

    public class User : UserBase
    {
        public Guid ProfileId { get; private set; }
        public UserProfile Profile { get; private set; } = null!;
        public UserRole Role { get; private set; } = UserRole.Trainee;

        private User() { }

        public User(string email, string passwordHash, UserRole role, UserProfile profile, string? username = null)
        {
            Profile = profile ?? throw new UserException("Perfil do usuário é obrigatório.");
            ProfileId = profile.Id;
            Role = role;

            InitializeBase(email, passwordHash, username);
        }
    }
}