using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Domain.Interfaces
{
    internal interface IEntity
    {
        public DateTime CreatedDate { get; set; }
    }
}
