

using RazorClient.Tutor.DTOs;
using System.ComponentModel.DataAnnotations;

namespace RazorClient.Tutor.Controllers;
[AllowAnonymous]
public class HomeController : Controller
{
    private readonly IEmailService _emailServices;

    public HomeController(IEmailService emailServices)
    {
        _emailServices = emailServices;
    }

    public ProductViewModel? ProductModal { get; private set; }

    public async Task<IActionResult> Index()
    {
        SMTPEmailRequest request = new SMTPEmailRequest()
        {
            ToEmail = "chechobecho@gmail.com",
            SMTPServerName = SMTPServer.Default,
            EmailSubject = "Email Testing",
            EmailBody = "Testing Email Body"
        };
        var result = await _emailServices.SMTPEmailSenderAsync(request);
        ProductModal = new ProductViewModel();
        return View(ProductModal);
    }
    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Lockout()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorDTO { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public class ProductViewModel
    {
        // We removed ID field so WebApi demo works correctly
        [ScaffoldColumn(false)]
        public int ProductID
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Product name")]
        public string ProductName
        {
            get;
            set;
        }

        [Display(Name = "Unit price")]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue)]
        public decimal UnitPrice
        {
            get;
            set;
        }

        [Display(Name = "Units in stock")]
        [DataType("Integer")]
        [Range(0, int.MaxValue)]
        public int UnitsInStock
        {
            get;
            set;
        }

        public bool Discontinued
        {
            get;
            set;
        }

        [Display(Name = "Last supply")]
        [DataType(DataType.Date)]
        public DateTime LastSupply
        {
            get;
            set;
        }

        [DataType("Integer")]
        public int UnitsOnOrder
        {
            get;
            set;
        }

        public int? CategoryID { get; set; }

        public string QuantityPerUnit { get; set; }
    }
}
