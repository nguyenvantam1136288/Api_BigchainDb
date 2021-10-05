using Api_BigchainDb.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api_BigchainDb.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public IContactsRepository ContactsRepo { get; set; }
        private readonly ILogger _logger;
        private IHttpContextAccessor _httpContextAccessor;

        public HomeController(IContactsRepository _repo, ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            ContactsRepo = _repo;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var context = _httpContextAccessor.HttpContext;
            _logger.LogInformation("Log message in the Index() method" + context);
            return Ok("successfully");
        }
    }
}
