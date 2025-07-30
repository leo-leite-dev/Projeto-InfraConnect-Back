using InfraConnect.Domain.Entities.Commons;
using InfraConnect.Domain.Exceptions;
using InfraConnect.Domain.Validators;

public abstract class UserBase : Base
{
    public string? Username { get; protected set; }
    public string Email { get; protected set; } = string.Empty;
    public string PasswordHash { get; protected set; } = string.Empty;

    public bool IsActive { get; protected set; } = true;
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }

    public string? PasswordResetToken { get; protected set; }
    public DateTime? PasswordResetTokenExpiresAt { get; protected set; }

    protected UserBase() { }

    protected void InitializeBase(string email, string? username = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new UserException("O e-mail é obrigatório.");

        Email = EmailValidator.EnsureValid(email);
        IsActive = true;
        CreatedAt = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(username))
            SetUsername(username, _ => false);
    }

    public void SetUsername(string newUsername, Func<string, bool> isUsernameTaken)
    {
        if (string.IsNullOrWhiteSpace(newUsername))
            throw new UserException("O nome de usuário não pode estar vazio.");

        newUsername = newUsername.Trim();

        if (newUsername.Contains(' '))
            throw new UserException("O nome de usuário não pode conter espaços.");

        if (newUsername.Length < 4 || newUsername.Length > 20)
            throw new UserException("O nome de usuário deve ter entre 4 e 20 caracteres.");

        if (isUsernameTaken.Invoke(newUsername))
            throw new UserException("Este nome de usuário já está em uso.");

        Username = newUsername;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetPassword(string rawPassword, Func<string, string> hashFunction)
    {
        PasswordValidator.EnsureValid(rawPassword);

        PasswordHash = hashFunction.Invoke(rawPassword);
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(string currentPasswordHash, string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new UserException("A nova senha não pode estar vazia.");

        if (newPasswordHash.Length < 8)
            throw new UserException("A nova senha deve conter no mínimo 8 caracteres.");

        if (PasswordHash != currentPasswordHash)
            throw new UserException("A senha atual está incorreta.");

        if (PasswordHash == newPasswordHash)
            throw new UserException("A nova senha deve ser diferente da senha atual.");

        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    public void GeneratePasswordResetToken(Func<string> tokenGenerator, TimeSpan duration)
    {
        PasswordResetToken = tokenGenerator.Invoke();
        PasswordResetTokenExpiresAt = DateTime.UtcNow.Add(duration);
    }

    public void ResetPasswordWithToken(string token, string newPasswordHash)
    {
        if (PasswordResetToken == null || PasswordResetTokenExpiresAt == null)
            throw new UserException("Nenhuma solicitação de recuperação de senha foi feita.");

        if (PasswordResetTokenExpiresAt < DateTime.UtcNow)
            throw new UserException("O token de recuperação de senha expirou.");

        if (PasswordResetToken != token)
            throw new UserException("Token de recuperação de senha inválido.");

        if (string.IsNullOrWhiteSpace(newPasswordHash) || newPasswordHash.Length < 8)
            throw new UserException("A nova senha deve conter no mínimo 8 caracteres.");

        PasswordHash = newPasswordHash;
        PasswordResetToken = null;
        PasswordResetTokenExpiresAt = null;
        UpdatedAt = DateTime.UtcNow;
    }
}