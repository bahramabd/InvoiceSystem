using InvoiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InvoiceSystem.Models.Invoice;

namespace InvoiceSystem
{
    public class InvoiceFactory
    {
        public Invoice BuildInvoce(decimal invoiceId, decimal clientId, DateTime startDate, DateTime endDate, string reason, decimal amount, string status)
        {
            Invoice invoice = new Invoice()
            {

                InvoiceID = invoiceId,
                ClientID = clientId,
                StartDate = startDate,
                EndDate = endDate,
                Reason = reason,
                Amount = amount,
                Status = status,
            };

            return invoice;
        }
    }
}
