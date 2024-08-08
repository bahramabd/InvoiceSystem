using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InvoiceSystem.Models
{
    
        public class Invoice
        {
            public decimal InvoiceID { get; set; }
            public decimal ClientID { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Reason { get; set; }
            public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";

        public override string ToString()
        {
            return $"InvoiceId: {this.InvoiceID}\n" +
                $"ClientId: {this.ClientID}\n" +
                $"Start Date: {this.StartDate.ToString("yy-MM-dd")}\n" +
                $"EndDate: {this.EndDate.ToString("yy-MM-dd")}\n" +
                $"Reason: {this.Reason}\n" +
                $"Amount: {this.Amount}\n" +
                $"Status: {this.Status}\n";
        }
        
    }
   }

