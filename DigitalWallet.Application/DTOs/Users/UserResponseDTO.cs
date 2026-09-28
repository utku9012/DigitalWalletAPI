using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs.Users
{
    public class UserResponseDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Email Email { get; set; }

        public string PhoneNumber { get; set; }

        public KYCLevel KYC { get; set; }

        public UserStatus IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
