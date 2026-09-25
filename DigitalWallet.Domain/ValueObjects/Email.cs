using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("E-posta alanı boş olamaz.");

            if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Geçersiz e-posta formatı.");

            Value = value;
        }

        // private constructor kullanılmayacak olsa bile public olursa dışarıdan new Email yapılabilir, koymazsak da DB'den gelen querylerde EF patlar.
        private Email() { }

        public override string ToString() => Value;
    }
}
