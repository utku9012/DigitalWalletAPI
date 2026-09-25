using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs.Users
{
    public class UserResponseDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

    }
}
