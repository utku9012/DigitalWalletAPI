using DigitalWallet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.DTOs.Users
{
    public class UpdateKYCRequestDTO
    {
        public string IdentityNumber { get; set; }

        public KYCLevel KYC { get; set; }
    }
}
