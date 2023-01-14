using BookingAPI.Exceptions;
using BookingAPI.Models;
using BookingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookingAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }


        [Route ("AddToCart")]
        [HttpPost]
        public  ActionResult  AddItemToCart(ProductCart addItems)
        {
            bool check= _bookingService.AddItemsToCart(addItems);
            return Ok(check);
        }

        [Route("ViewCart")]
        [HttpGet]
        public ActionResult ViewCart(string userName)
        {
            List<ProductCart> viewCart = _bookingService.ViewCartItems(userName);
            if (viewCart.Count > 0)
            {
                return Ok(viewCart);
            }
            else return BadRequest("Your Cart is Empty");
        }

        [Route("RemoveItem")]
        [HttpDelete]
        public ActionResult RemoveItem(int cartId)
        {
            try
            {
                bool Message = _bookingService.RemoveItem(cartId);
                return Ok(Message);
            }
            catch(ProductNotInCartException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Route("GenerateBill")]
        [HttpGet]
        public ActionResult GenerateBill(string userName)
        {
            string invoiceNo = _bookingService.InVoiceNo();
            int totalAmount = _bookingService.GenerateBill(userName);
            int gstAmount = (int)(0.18 * totalAmount);
            int grandTotal = totalAmount + gstAmount;
            List<Bill> bill = new List<Bill>() { 
                new Bill { InvoiceNo = invoiceNo, UserName = userName, TotalAmount = totalAmount, GstAmount = gstAmount, GrandTotal = grandTotal }
            }; 
            return Ok(bill);
        }


        [Route("PayBill")]
        [HttpDelete]
        public ActionResult PayBill(string userName)
        {
            bool paybillStatus = _bookingService.PayBill(userName);
            return Ok(paybillStatus);
        }


        [Route("SendEmail")]
        [HttpPost]
        public IActionResult SendEmail(string userEmail, EmailDetails request)
        {
            _bookingService.SendEmail(userEmail, request);
            return Ok();
        }
    }
}
