using DigitalWallet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
//kullanıcı 
namespace DigitalWallet.Domain.Entities
{
    public class User
    {
        
        public int Id { get; set; }

        public int IdentityNumber { get; set; }

        public ICollection<Wallet> Wallets { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public string Password { get; set; }

        public KYCLevel KYC { get; set; }

        public bool IsActive { get; set; }

    }
}
