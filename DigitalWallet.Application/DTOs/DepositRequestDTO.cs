using System;
using System.Collections.Generic;
using System.Text;

//kullanıcının hesabına para yüklemesi için gerekli olan minimal 2 property
namespace DigitalWallet.Application.DTOs
{
    public class DepositRequestDTO
    {
        public Guid WalletId { get; set; } 
        
        public decimal Amount { get; set; } 
    }
}
