using InvoiceSystem.Models;
using InvoiceSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
       private readonly IInvoiceRepository _invoiceRepository;

        public ClientsController(IClientRepository clientRepository,IInvoiceRepository invoiceRepository)
        {
            _clientRepository = clientRepository;
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClient()
        {
            var clients = _clientRepository.Read();
            if (clients == null)
            {
                return NotFound();
            }

            return Ok(clients);
        }
        [HttpPost("{clients}")]
        public IActionResult CreateClient([FromBody] Client client)
        {
            // Implement logic to create a new client
            // Call methods from _clientService or other components
            // Return appropriate status codes and responses
            _clientRepository.Create(client);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
        [HttpDelete("{clientId}")]
        public IActionResult DeleteClient(int clientId)
        {
            // Implement logic to delete an existing client
            // Call methods from _clientService or other components
            // Return appropriate status codes and responses
            _clientRepository.Delete(clientId);
            // search if is there client ith this id? you may add method to repository
            return Ok();
        }
        [HttpPut("{client}")]
        public IActionResult UpdateClient([FromBody] Client client)
        {
            try
            {
                // Call your service layer to update the client
                _clientRepository.Update(client);

                // Return a success response
                return Ok("Client updated successfully.");
            }
            catch (Exception ex)
            {
                // Return an error response if something goes wrong
                return BadRequest($"Error updating client: {ex.Message}");
            }

        }
        [HttpPost("Accept")]
        public IActionResult AcceptInvoice([FromBody]int invoiceId)
        {
            try
            {
                

                _clientRepository.AcceptInvoice(invoiceId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Reject")]
        public IActionResult RejectInvoice([FromBody]int invoiceId)
        {
            try
            {

                _clientRepository.RejectInvoice(invoiceId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ShowInvoice/{invoiceId}")]
        public ActionResult<IEnumerable<Invoice>> ShowInvoices(decimal InvoiceId)
        {
            var invoices= _clientRepository.ShowInvoices(InvoiceId);
            if (invoices == null)
            {
                return NotFound();
            }
            return Ok(invoices); 
            
        }
        [HttpGet("{invoiceId}")]
        public IActionResult GeneratePDF(decimal invoiceId)
        {
            
            try
            {
                Invoice invoice = new Invoice();
                byte[] pdfBytes = _clientRepository.GeneratePDFInvoice(invoiceId);
              
                invoice=_invoiceRepository.GetInvoice(invoiceId);
                if (invoice != null)
                {
                    
                    Response.Headers.Add("Content-Disposition", $"attachment; filename=Invoice_{invoice.InvoiceID}.pdf");
                    return File(pdfBytes, "application/pdf");
                }
                else { return NotFound(); }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }
    }
}