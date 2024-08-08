using InvoiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Newtonsoft.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace InvoiceSystem.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> Read();
        void Create(Client item);
        void Update(Client item);
        void Delete(decimal id);
        void AcceptInvoice(decimal invoiceId);
        void RejectInvoice(decimal invoiceId);
        Client GetClient(decimal id);
        IEnumerable<Invoice> ShowInvoices(decimal id);
        byte[] GeneratePDFInvoice(decimal invoice);

    }
    public class ClientRepository : IClientRepository
    {
        string ConnectionString = "Data Source=LAPTOP-QJF9QCRR\\SQLEXPRESS;Initial Catalog=INVOICESYSTEM;Integrated Security=True;";

        public Client GetClient(decimal id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT ClientID, ClientName, ClientSurname,TotalInvoiceCount, TotalDebt FROM Clients WHERE ClientID = @ClientId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClientId", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                Client client = new Client
                                {
                                    ClientID = (decimal)reader["ClientID"],
                                    ClientName = (string)reader["ClientName"],
                                    ClientSurname = (string)reader["ClientSurname"],
                                    TotalInvoiceCount = (int)reader["TotalInvoiceCount"],
                                    TotalDebt = (decimal)(reader["TotalDebt"])

                                };

                                return client;
                            }
                            else
                            {
                                Console.WriteLine("No such Client. Try Again..");
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetClient"); return null;
            }
        }
        public void Create(Client item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sqlQuery = "INSERT INTO Clients (ClientID, ClientName, ClientSurname,TotalInvoiceCount, TotalDebt) VALUES (@Code, @Name, @Class, @Tname, @Tsurname);";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Code", item.ClientID);
                        command.Parameters.AddWithValue("@Name", item.ClientName);
                        command.Parameters.AddWithValue("@Class", item.ClientSurname);
                        command.Parameters.AddWithValue("@Tname", item.TotalInvoiceCount);
                        command.Parameters.AddWithValue("@Tsurname", item.TotalDebt);

                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Error Create Client"); }
        }

        public void Delete(decimal id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "DELETE TOP(1) from Clients WHERE ClientID=@CourseCode";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseCode", id);



                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }


        public IEnumerable<Client> Read()
        {
            List<Client> clients = new List<Client>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT ClientID, ClientName, ClientSurname,TotalInvoiceCount, TotalDebt FROM Clients";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Client client = new Client
                                {
                                    ClientID = (decimal)reader["ClientID"],
                                    ClientName = (string)reader["ClientName"],
                                    ClientSurname = (string)reader["ClientSurname"],
                                    TotalInvoiceCount = (int)reader["TotalInvoiceCount"],
                                    TotalDebt = (decimal)reader["TotalDebt"]
                                };
                                clients.Add(client);
                            }
                        }
                    }
                }
                return clients;
            }
            catch { Console.WriteLine("Error"); return clients; }
        }

        public void Update(Client item)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "UPDATE  Clients SET ClientID=@CourseCode, ClientName=@StudentNumber, ClientSurname=@ExamDate, TotalInvoiceCount=@Score, TotalDebt=@Score1 WHERE ClientID=@CourseCode"
                                   ;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseCode", item.ClientID);
                        command.Parameters.AddWithValue("@StudentNumber", item.ClientName);
                        command.Parameters.AddWithValue("@ExamDate", item.ClientSurname);
                        command.Parameters.AddWithValue("@Score", item.TotalInvoiceCount);
                        command.Parameters.AddWithValue("@Score1", item.TotalDebt);

                        command.ExecuteNonQuery();
                    }
                }
            }


            catch (Exception ex) { Console.WriteLine("Error Update"); }
        }
        public void AcceptInvoice(decimal invoiceId)
        {
            Console.WriteLine("invoice accepted.");
            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "UPDATE Invoices SET Status=@Co WHERE InvoiceID=@CourseCode"
                                   ;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        string accepted = "Accepted";
                        command.Parameters.AddWithValue("@CourseCode", invoiceId);
                        command.Parameters.AddWithValue("@Co", accepted);


                        command.ExecuteNonQuery();
                    }
                }
            }


            catch (Exception ex) { Console.WriteLine("Error"); }
        }
        public void RejectInvoice(decimal invoiceId)
        {
            Console.WriteLine("invoice rejected.");
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "UPDATE Invoices SET Status=@Co WHERE InvoiceID=@CourseCode"
                                   ;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        string rejected = "Rejected";
                        command.Parameters.AddWithValue("@CourseCode", invoiceId);
                        command.Parameters.AddWithValue("@Co", rejected);


                        command.ExecuteNonQuery();
                    }
                }
            }


            catch (Exception ex) { Console.WriteLine("Error"); }
        }
        public IEnumerable<Invoice> ShowInvoices(decimal id)
        {
            List<Invoice> invoices = new List<Invoice>();
            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT InvoiceID,ClientID,StartDate,EndDate,Reason,Amount,Status FROM Invoices WHERE ClientID=@ClientID AND Status='Pending' ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClientID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Invoice invoice = new Invoice
                                {

                                    InvoiceID = (decimal)reader["InvoiceID"],
                                    ClientID = (decimal)reader["ClientID"],
                                    StartDate = (DateTime)reader["StartDate"],
                                    EndDate = (DateTime)reader["EndDate"],
                                    Reason = (string)reader["Reason"],
                                    Amount = (decimal)reader["Amount"],
                                    Status = (string)reader["Status"]
                                };
                                invoices.Add(invoice);
                            }
                            return invoices;
                        }
                    }
                }
            }
            catch { Console.WriteLine("Error"); return null; }
        }
        public byte[] GeneratePDFInvoice(decimal invoiceId)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    InvoiceRepository clientRepository = new InvoiceRepository();
                    Invoice invoice = clientRepository.GetInvoice(invoiceId);
                    // Create a new PDF document
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();

                    // Add content to the PDF
                    if (invoice != null)
                    {
                        Paragraph paragraph = new Paragraph(invoice.ToString());
                        document.Add(paragraph);
                        document.Close();
                        writer.Close();

                        return memoryStream.ToArray();
                    }
                    else return null;
                }
            }
            catch { Console.WriteLine("error pdf"); return null; }
        }
    }
}
