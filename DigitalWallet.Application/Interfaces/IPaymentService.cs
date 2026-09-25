using DigitalWallet.Application.DTOs.Payments;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDTO> MakePayment(PaymentRequestDTO request);
    }
}
