using InvoiceSystem.Models;
using InvoiceSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace InvoiceSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
       

        public InvoicesController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Invoice>> GetInvoice()
        {
            var invoices = _invoiceRepository.Read();
            if (invoices == null)
            {
                return NotFound();
            }

            return Ok(invoices);
        }
        [HttpPost("{invoices}")]
        public IActionResult CreateInvoice([FromBody] Invoice invoice)
        {
            // Implement logic to create a new client
            // Call methods from _clientService or other components
            // Return appropriate status codes and responses
            _invoiceRepository.Create(invoice);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }
        [HttpDelete("{invoiceId}")]
        public IActionResult DeleteClient(decimal invoiceId)
        {
            // Implement logic to delete an existing client
            // Call methods from _clientService or other components
            // Return appropriate status codes and responses
            
            _invoiceRepository.Delete(invoiceId);
            // search if is there client ith this id? you may add method to repository
            return Ok();
        }
        [HttpPut("{invoice}")]
        public IActionResult UpdateClient([FromBody] Invoice invoice)
        {
            try
            {
                // Call your service layer to update the client
                _invoiceRepository.Update(invoice);

                // Return a success response
                return Ok("Invoice updated successfully.");
            }
            catch (Exception ex)
            {
                // Return an error response if something goes wrong
                return BadRequest($"Error updating invoice: {ex.Message}");
            }

        }
        //[HttpGet("invoices/{invoiceId}/print")]
        //public IActionResult PrintInvoice(int invoiceId)
        //{
        //    // Generate the invoice document content
        //    string invoiceContent = GenerateInvoiceContent(invoiceId);

        //    // Return the document content as a response
        //    return File(Encoding.UTF8.GetBytes(invoiceContent), "application/pdf", "invoice.pdf");
        //}

    }
}
