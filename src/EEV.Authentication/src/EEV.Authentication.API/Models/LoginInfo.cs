using System;
using System.Collections.Generic;
using System.Text;

namespace EEV.Authentication.API.Models
{
    public class LoginInfo : IEquatable<LoginInfo>
    {
        public LoginInfo(string name, string password)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public string Name { get; }
        public string Password { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as LoginInfo);
        }

        public bool Equals(LoginInfo other)
        {
            return other != null &&
                   Name == other.Name &&
                   Password == other.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Password);
        }

        public static bool operator ==(LoginInfo left, LoginInfo right)
        {
            return EqualityComparer<LoginInfo>.Default.Equals(left, right);
        }

        public static bool operator !=(LoginInfo left, LoginInfo right)
        {
            return !(left == right);
        }
    }
}
