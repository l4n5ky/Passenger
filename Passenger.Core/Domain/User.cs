using System;
using System.Text.RegularExpressions;

namespace Passenger.Core.Domain
{
    public class User
    {
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Username { get; protected set; }
        public string FullName { get; protected set; }
        public string Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdateAt { get; protected set; }

        protected User() { } // will be empty to protect

        public User(Guid userId, string email, string username, string role, string password, string salt)
        {
            Id = userId;
            SetEmail(email);
            SetUsername(username);
            SetRole(role);
            SetPassword(password, salt);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Email address is incorrect.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetUsername(string username)
        {
            if (!NameRegex.IsMatch(username))
            {
                throw new DomainException(ErrorCodes.InvalidUsername, "Username is incorrect.");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new DomainException(ErrorCodes.InvalidUsername, "Username is incorrect.");
            }

            Username = username.ToLowerInvariant();
            UpdateAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new DomainException(ErrorCodes.InvalidRole, "Role can not be empty.");
            }
            if (Role == role)
            {
                return;
            }

            Role = role;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password can not be empty.");
            }
            if (string.IsNullOrEmpty(salt))
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Salt can not be empty.");
            }
            if (password.Length < 6)
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password must contain at least 6 characters.");
            }
            if (password.Length > 20)
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password can not contain more than 20 characters.");
            }
            if (Password == password)
            {
                return;
            }

            Password = password;
            Salt = salt;
            UpdateAt = DateTime.UtcNow;

        }
    }
}
