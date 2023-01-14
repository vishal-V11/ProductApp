using BookingAPI.Exceptions;
using BookingAPI.Models;
using BookingAPI.Repository;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace BookingAPI.Services
{
    public class BookingService : IBookingService
    {
        readonly IBookingRepository _bookingRepository;
        readonly IConfiguration _configuration;
        public BookingService(IBookingRepository menuRepo, IConfiguration configuration)
        {
            _bookingRepository = menuRepo;
            _configuration = configuration;
        }

        public bool AddItemsToCart(ProductCart addItems)
        {
            ProductCart existingProduct = _bookingRepository.CheckIfItemPresentForParticularUser(addItems.ProductName, addItems.Name);
            if (existingProduct == null)
            {
                return _bookingRepository.AddItems(addItems);           
            }
           else
           {
                return _bookingRepository.UpdateQuantity(addItems);
           }
           
               
        }

        public int GenerateBill(string userName)
        {
            return _bookingRepository.GetTotalAmount(userName);
        }

        public bool PayBill(string userName)
        {
            return _bookingRepository.PayBill(userName);
        }

        public bool RemoveItem(int cartId)
        {
            bool checker = _bookingRepository.GetItemByCartId(cartId);
            if (checker == true)
            {
                return _bookingRepository.RemoveItem(cartId);  
            }
            throw new ProductNotInCartException("Product does not exist in cart");
        }

        public List<ProductCart> ViewCartItems(string userName)
        {
            return _bookingRepository.GetCart(userName);
        }

        public string InVoiceNo()
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string hour = DateTime.Now.Hour.ToString();
            string minute = DateTime.Now.Minute.ToString();
            string second = DateTime.Now.Second.ToString();
            string inVoiceNo = "#" + year + month + day + "-" + hour + minute + second;
            return inVoiceNo;
        }


        public void SendEmail(string userEmail, EmailDetails request)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Invoice Details";
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };
            var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);//host and port
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

    }
}
