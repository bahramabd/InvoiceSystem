using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Models
{
    public class Client
    {
        public decimal ClientID { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientSurname { get; set; } = string.Empty;
        public int TotalInvoiceCount { get; set; }
        public decimal TotalDebt { get; set; }
        public override string ToString()
        {
            return $"ClientID: {this.ClientID}\n" +
                $"ClientName: {this.ClientName}\n" +
                $"ClientSurname: {this.ClientSurname}\n" +
                $"TotalInvoiceCount: {this.TotalInvoiceCount}\n" +
                $"TotalDebt: {this.TotalDebt}\n";
                
        }
    }
}
