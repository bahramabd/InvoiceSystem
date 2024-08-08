using InvoiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Repositories
{
    public interface IInvoiceRepository
    {
        IEnumerable<Invoice> Read();
        void Create(Invoice item);
        void Update(Invoice item);
        void Delete(decimal id);
        Invoice GetInvoice(decimal id);
        
    };
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IClientRepository _clientRepository;
        public InvoiceRepository(IClientRepository clientRepository)
        {
            
            _clientRepository = clientRepository;
        }
        public InvoiceRepository()
        {
        }
        
       
        string ConnectionString = "Data Source=LAPTOP-QJF9QCRR\\SQLEXPRESS;Initial Catalog=INVOICESYSTEM;Integrated Security=True;";

        public Invoice GetInvoice(decimal id)
        {
            try
            {
                
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT InvoiceID,ClientID,StartDate,EndDate,Reason,Amount,Status FROM Invoices WHERE InvoiceID = @ClientId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClientId", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                
                                Invoice invoice= new Invoice
                                {
                                    InvoiceID = (decimal)reader["InvoiceID"],
                                    ClientID = (decimal)reader["ClientID"],
                                    StartDate = (DateTime)reader["StartDate"],
                                    EndDate = (DateTime)reader["EndDate"],
                                    Reason = (string)reader["Reason"],
                                    Amount = (decimal)reader["Amount"],
                                    Status = (string)reader["Status"]

                                };

                                return invoice;
                            }
                            else
                            {
                                Console.WriteLine("No such Invoice. Try Againn..");
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

        public void Create(Invoice item)
        {
            try
            {
                Client cl = new Client();
                IClientRepository o = new ClientRepository();
                cl = o.GetClient(item.ClientID);
                
                if (cl != null)
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        string sqlQuery = "INSERT INTO Invoices (InvoiceID,ClientID,StartDate,EndDate,Reason,Amount,Status) VALUES (@Code,@Code1, @Name, @Class, @Tname, @Tsurname, @Code2);";
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Code", item.InvoiceID);
                            command.Parameters.AddWithValue("@Code1", item.ClientID);
                            command.Parameters.AddWithValue("@Name", item.StartDate);
                            command.Parameters.AddWithValue("@Class", item.EndDate);
                            command.Parameters.AddWithValue("@Tname", item.Reason);
                            command.Parameters.AddWithValue("@Tsurname", item.Amount);
                            command.Parameters.AddWithValue("@Code2", item.Status);

                            int rowsAffected = command.ExecuteNonQuery();
                        }
                    }
                    ClientRepository obj = new ClientRepository();
                    Client client = new Client();
                    client = obj.GetClient(item.ClientID);
                    client.TotalInvoiceCount += 1;
                    client.TotalDebt += item.Amount;
                    obj.Update(client);
                }
                else Console.WriteLine("No such Client ID");
            }
            catch  { Console.WriteLine("Errorrr"); }
        }
        
        

        public void Delete(decimal id)
        {
            try
            {
                IInvoiceRepository obj1 = new InvoiceRepository();
                IClientRepository obj = new ClientRepository();
                Client client = new Client();
                Invoice item = new Invoice();
                item = obj1.GetInvoice(id);
                
                if (item != null)
                {
                    client = obj.GetClient(item.ClientID);
                    if (client != null)
                    {
                        client.TotalInvoiceCount -= 1;
                        client.TotalDebt -= item.Amount;
                        obj.Update(client);
                    }
                }
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "DELETE TOP(1) from Invoices WHERE InvoiceID=@CourseCode";

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


        public IEnumerable<Invoice> Read()
        {
            List<Invoice> invoices = new List<Invoice>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    

                    string query = "SELECT InvoiceID,ClientID,StartDate,EndDate,Reason,Amount,Status FROM Invoices";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

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

                        }
                    }
                }
                return invoices;
            }
            catch { Console.WriteLine("Errore"); return invoices; }
        }

        public void Update(Invoice item)
        {
            try
            {
                Invoice invoice=new Invoice();
                invoice = GetInvoice(item.InvoiceID);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "UPDATE  Invoices SET InvoiceID=@CourseCode, ClientID=@StudentNumber, StartDate=@ExamDate, EndDate=@Score, Reason=@Score1, Amount=@Scor, Status=@Sco WHERE InvoiceID=@CourseCode"
                                   ;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourseCode", item.InvoiceID);
                        command.Parameters.AddWithValue("@StudentNumber", item.ClientID);
                        command.Parameters.AddWithValue("@ExamDate", item.StartDate);
                        command.Parameters.AddWithValue("@Score", item.EndDate);
                        command.Parameters.AddWithValue("@Score1", item.Reason);
                        command.Parameters.AddWithValue("@Scor", item.Amount);
                        command.Parameters.AddWithValue("@Sco", item.Status);
                        command.ExecuteNonQuery();
                    }
                }
                //ClientRepository obj = new ClientRepository();
                Client client = new Client();
                client = _clientRepository.GetClient(item.ClientID);
                if (client != null && invoice != null)
                {
                    client.TotalDebt = client.TotalDebt + item.Amount - invoice.Amount;
                    _clientRepository.Update(client);
                }
                else Console.WriteLine("No such client or invoice ID");
            }


            catch (Exception ex) { Console.WriteLine("Error"); }
        }

    }
}
