using InvoiceSystem.Repositories;
using InvoiceSystem;
using InvoiceSystem.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using System.Linq.Expressions;

using System.IO;

using System.Net.Http.Headers;


class Program
{
    static async Task Main()
    {

        Client client = new Client(); Client client1 = new Client(); IEnumerable<Client> clients = new List<Client>();
        IClientRepository obj = new ClientRepository();
        //Console.WriteLine("Enter your ID: ");
        //string ID = Console.ReadLine();
        //decimal id = decimal.Parse(ID);
        //client = obj.GetClient(id);
        //client1.TotalDebt = 0; client1.ClientName = "Bahram"; client1.ClientSurname = "Abdullayev"; client1.ClientID = 24809; client1.TotalInvoiceCount = 0;
        //Client client2 = new Client();
        //client2 = obj.GetClient(id);

        //obj.Update(client1);
        //if (client2 != null)
        //{
        //    client1.ShowClient();
        //}
        //Console.WriteLine("Do you want to evaluate your invoices? (yes/no)");
        //string rdl = Console.ReadLine();
        //if (rdl == "yes") obj.ShowInvoices(client.ClientID);
        //else if (rdl == "no") ;
        //else Console.WriteLine("Operation Terminated...");

        Invoice invoice = new Invoice(); Invoice invoice1 = new Invoice(); IEnumerable<Invoice> invoices = new List<Invoice>();
        IInvoiceRepository obj1 = new InvoiceRepository();
        //        invoice1.InvoiceID = 17801; invoice1.ClientID = 24809; invoice1.StartDate = new DateTime(2023, 4, 9); invoice1.EndDate = new DateTime(2023, 12, 10);
        //        invoice1.Amount = 800; invoice1.Reason = " Netflix";
        //        obj1.Create(invoice1);

        bool bln = true;
        Console.WriteLine("Are you Administrator? (yes/no)");
        if (Console.ReadLine() == "yes")
        {
            while (bln)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("if you want to edit invoices, please, press I");
                    Console.WriteLine("if you want to edit clients, please, press C");
                    var rdk = Console.ReadKey().Key;
                    Console.Clear();
                    if (rdk == ConsoleKey.I)
                    {
                        Console.WriteLine("Welcome to Management of Invoice Service App");
                        Console.WriteLine("To create a new invoice, please, press Enter key");
                        Console.WriteLine("To delete an invoice, please, press D key");
                        Console.WriteLine("To view all inovices, please, press V key");
                        Console.WriteLine("To update an invoice, please, press U key");
                        Console.WriteLine("Press a key, please..");
                        var rdkey = Console.ReadKey().Key;
                        Console.Clear();
                        if (rdkey == ConsoleKey.Enter)
                        {
                            Console.WriteLine("Enter Invoice ID: "); invoice.InvoiceID = decimal.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Client ID: "); invoice.ClientID = decimal.Parse(Console.ReadLine());
                            client1 = obj.GetClient(invoice.ClientID);
                            if (client1 != null)
                            {

                                Console.WriteLine("Enter Start Date: "); invoice.StartDate = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Enter End Date: "); invoice.EndDate = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Invoice Reason: "); invoice.Reason = Console.ReadLine();
                                Console.WriteLine("Enter Invoice Amount: "); invoice.Amount = decimal.Parse(Console.ReadLine());

                                using var httpClient = new HttpClient();
                                string deleteUrl = "https://localhost:7033/Invoices/invoice";
                                HttpResponseMessage response = await httpClient.PostAsJsonAsync(deleteUrl, invoice);
                                Console.Clear();
                                if (response.IsSuccessStatusCode)
                                {
                                    Console.WriteLine("Invoice created successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to create invoice. Status code: " + response.StatusCode);
                                }
                            }
                        }
                        if (rdkey == ConsoleKey.U)
                        {
                            Console.WriteLine("Enter Invoice ID: ");
                            invoice = obj1.GetInvoice(decimal.Parse(Console.ReadLine()));
                            if (invoice != null)
                            {
                                Console.WriteLine("Enter Client ID: "); invoice.ClientID = decimal.Parse(Console.ReadLine());
                                client1 = obj.GetClient(invoice.ClientID);
                                if (client1 != null)
                                {
                                    Console.WriteLine("Enter Start Date: "); invoice.StartDate = DateTime.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter End Date: "); invoice.EndDate = DateTime.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter Invoice Reason: "); invoice.Reason = Console.ReadLine();
                                    Console.WriteLine("Enter Invoice Amount: "); invoice.Amount = decimal.Parse(Console.ReadLine());
                                    using var httpClient = new HttpClient();
                                    string deleteUrl = "https://localhost:7033/Invoices/invoice";
                                    HttpResponseMessage response = await httpClient.PutAsJsonAsync(deleteUrl, invoice);
                                    Console.Clear();
                                    if (response.IsSuccessStatusCode)
                                    {
                                        Console.WriteLine("Invoice updated successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to update invoice. Status code: " + response.StatusCode);
                                    }
                                }
                                //else { Console.WriteLine(No )}
                            }
                        }
                        if (rdkey == ConsoleKey.V)
                        {
                            using var httpClient = new HttpClient();
                            string deleteUrl = "https://localhost:7033/Invoices/";
                            HttpResponseMessage response = await httpClient.GetAsync(deleteUrl);
                            if (response.IsSuccessStatusCode)
                            {
                                string jsonResponse = await response.Content.ReadAsStringAsync();

                                invoices = JsonConvert.DeserializeObject<List<Invoice>>(jsonResponse);

                                foreach (Invoice a in invoices)
                                {
                                    Console.WriteLine(a);
                                    // Print other invoice properties as needed
                                }
                            }
                            else
                            {
                                Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                            }
                        }
                        if (rdkey == ConsoleKey.D)
                        {
                            Console.WriteLine("Enter Invoice ID: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal invoiceId))
                            {
                                // Now you have the numeric invoiceId entered by the user
                                string baseUrl = " https://localhost:7033/Invoices/";
                                string deleteUrl = baseUrl + invoiceId;

                                using (var httpClient = new HttpClient())
                                {
                                    HttpResponseMessage response = await httpClient.DeleteAsync(deleteUrl);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        Console.WriteLine("Invoice deleted successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to delete invoice. Status code: " + response.StatusCode);
                                    }
                                }
                            }
                        }
                    }
                    else if (rdk == ConsoleKey.C)
                    {
                        Console.Clear();
                        Console.WriteLine("Welcome to Management of Invoice Service App");
                        Console.WriteLine("To delete a client, please, press D key");
                        Console.WriteLine("To view all clients, please, press V key");
                        Console.WriteLine("To update a client, please, press U key");
                        Console.WriteLine("Press a key, please..");
                        var rdke = Console.ReadKey().Key;
                        Console.Clear();
                        if (rdke == ConsoleKey.U)
                        {
                            Console.WriteLine("Enter Client ID: ");
                            client = obj.GetClient(decimal.Parse(Console.ReadLine()));
                            if (client != null)
                            {
                                Console.Clear();
                                
                                Console.WriteLine("Enter Your Name: "); client.ClientName = (Console.ReadLine());
                                Console.WriteLine("Enter Your Surname: "); client.ClientSurname = (Console.ReadLine());
                                
                                using var httpClient = new HttpClient();
                                string deleteUrl = "https://localhost:7033/Clients/client";
                                HttpResponseMessage response = await httpClient.PutAsJsonAsync(deleteUrl, client);

                                if (response.IsSuccessStatusCode)
                                {
                                    Console.WriteLine("Client updated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to update client. Status code: " + response.StatusCode);
                                }
                            }

                        }
                        if (rdke == ConsoleKey.D)
                        {
                            Console.WriteLine("Enter Client ID: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal clientId))
                            {
                                // Now you have the numeric invoiceId entered by the user
                                string baseUrl = " https://localhost:7033/Clients/";
                                string deleteUrl = baseUrl + clientId;

                                using (var httpClient = new HttpClient())
                                {
                                    HttpResponseMessage response = await httpClient.DeleteAsync(deleteUrl);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        Console.WriteLine("Client deleted successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to delete client. Status code: " + response.StatusCode);
                                    }
                                }
                            }
                        }
                        if (rdke == ConsoleKey.V)
                        {
                            using var httpClient = new HttpClient();
                            string deleteUrl = "https://localhost:7033/Clients/";
                            HttpResponseMessage response = await httpClient.GetAsync(deleteUrl);
                            if (response.IsSuccessStatusCode)
                            {
                                string jsonResponse = await response.Content.ReadAsStringAsync();

                                clients = JsonConvert.DeserializeObject<List<Client>>(jsonResponse);

                                foreach (Client a in clients)
                                {
                                    Console.WriteLine(a);
                                    // Print other invoice properties as needed
                                }
                            }
                            else
                            {
                                Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                            }
                        }

                    }
                    Console.WriteLine("Do you want to continue using the app? (yes/no)");
                    var rd = Console.ReadLine();
                    if (rd == "yes") ; else bln = false;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong..");

                    Console.WriteLine("Do you want to continue using the app? (yes/no)");
                    var rd = Console.ReadLine();
                    Console.Clear();
                    if (rd == "yes") ; else bln = false;
                }
            }

        }
        else
        {
            try {

                Console.Clear();
                //Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome to Invoice Service App.");
                //Console.Beep();
                //Console.ResetColor();
                Console.WriteLine("Do you have account? (yes/no)");

                if (Console.ReadLine() == "no")
                {
                    Console.Clear();
                    Console.WriteLine("Enter Your name: "); client.ClientName = Console.ReadLine();
                    Console.WriteLine("Enter Your surname: "); client.ClientSurname = Console.ReadLine();
                    client.TotalDebt = 0; client.TotalInvoiceCount = 0;
                    Random random = new Random();
                    client.ClientID = random.Next(10000, 100000);
                    Console.WriteLine("Here is your Data: "); Console.WriteLine(client);
                }
                else //(Console.ReadLine()=="yes")
                {
                    Console.Clear();
                    Console.WriteLine("Enter your ID: ");
                    string ID = Console.ReadLine();
                    decimal id = decimal.Parse(ID);
                    Console.Clear();
                    client = obj.GetClient(id);
                    if (client != null)
                    {
                        Console.WriteLine("Here is your Data: "); Console.WriteLine(client);
                    }
                }
                while (bln)
                {
                    if (client!= null)
                    {
                        //manager can delete read update clients || clients can  acc/rej invs 
                        Console.WriteLine("Do you want to evaluate your invoices? (yes/no)");

                        string rdl = Console.ReadLine();
                        Console.Clear();
                        if (rdl == "yes")
                        {
                        using var httpClient = new HttpClient();

                                string baseUrl = " https://localhost:7033/Clients/ShowInvoice/";
                                string deleteUrl = baseUrl + client.ClientID;

                                
                            HttpResponseMessage response = await httpClient.GetAsync(deleteUrl);
                            if (response.IsSuccessStatusCode)
                            {
                                string jsonResponse = await response.Content.ReadAsStringAsync();

                                invoices = JsonConvert.DeserializeObject<List<Invoice>>(jsonResponse);


                                foreach (Invoice a in invoices)
                                {
                                    Console.WriteLine(a);
                                    Console.WriteLine("To download this invoice, please, press D key.." );
                                    if (Console.ReadKey().Key == ConsoleKey.D)
                                    {
                                        Console.Clear();



                                        string basur = " https://localhost:7033/Clients/";
                                        string basUrl = basur + a.InvoiceID;
                                        HttpResponseMessage responsee = await httpClient.GetAsync(basUrl);
                                        if (responsee.IsSuccessStatusCode)
                                        {
                                            // Get the content as bytes
                                            byte[] pdfContent = await responsee.Content.ReadAsByteArrayAsync();

                                            // Get the suggested file name from the Content-Disposition header
                                            string suggestedFileName = responsee.Content.Headers.ContentDisposition.FileName;
                                            string downloadsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                                            // Construct the full path to save the file in the Downloads directory
                                            string fullPath = Path.Combine(downloadsDirectory,"Downloads", suggestedFileName);

                                            // Save the content as a PDF file using the suggested file name
                                            System.IO.File.WriteAllBytes(fullPath, pdfContent);

                                            Console.WriteLine($"PDF invoice downloaded and saved as {suggestedFileName}");
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Error while downloading PDF invoice.");
                                        }
                                    }
                                    Console.WriteLine("Do you accept this invoice? (yes/no)");
                                    if (Console.ReadLine() == "yes")
                                    {
                                        Console.Clear();
                                        string url = "https://localhost:7033/Clients/Accept";
                                        HttpResponseMessage respon = await httpClient.PostAsJsonAsync(url, a.InvoiceID);
                                        
                                        if (respon.IsSuccessStatusCode)
                                        {
                                            Console.WriteLine("Invoice accepted successfully.\n");

                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to accept invoice. Status code: " + response.StatusCode);
                                        }

                                    }
                                    else
                                    {
                                        
                                        string url = "https://localhost:7033/Clients/Reject";
                                        HttpResponseMessage respon = await httpClient.PostAsJsonAsync(url, a.InvoiceID);
                                        Console.Clear();
                                        if (respon.IsSuccessStatusCode)
                                        {
                                            Console.WriteLine("Invoice rejected successfully.\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to reject invoice. Status code: " + response.StatusCode);
                                        }

                                    }

                                }
                                Console.WriteLine("You have no Current Pending Invoice. ");
                            }
                            else
                            {
                                Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                            }
                        }
                    }

                    else
                    {
                        Console.Clear(); Console.WriteLine("Operation Terminated...");
                    }
                    Console.WriteLine("Do you want to continue using the app? (yes/no)");
                    var rd = Console.ReadLine();
                    if (rd == "yes") ; else bln = false;
                }

            }
            catch { Console.Clear(); Console.WriteLine("Something went wrong.."); Main(); }


        }
        
    
    }
}









          
