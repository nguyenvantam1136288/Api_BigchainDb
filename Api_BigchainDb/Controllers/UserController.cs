using Api_BigchainDb.Models;
using Api_BigchainDb.Repository;
using Api_BigchainDb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api_BigchainDb.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public IContactsRepository ContactsRepo { get; set; }
        private readonly ILogger _logger;
        private IHttpContextAccessor _httpContextAccessor;
        public UserController(UserService userService, IContactsRepository _repo,
            ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            ContactsRepo = _repo;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var context = _httpContextAccessor.HttpContext;
            _logger.LogInformation("Request:" + context.Request.Path + "Method: " + context.Request.Method);

            var userAll = await _userService.GetAllAsync();
            if (userAll == null)
            {
                _logger.LogInformation("Is not found data User: " + StatusCodes.Status404NotFound);
                return NotFound();
            }
            else
            {
                _logger.LogInformation("Success:" + StatusCodes.Status200OK);
                return Ok(userAll);
            }    
        }
        [HttpGet("{User}/{id}")]
        public async Task<IActionResult> FindbyId(string id)
        {
            //615ab0b98b2b3809149c347c
            var book = await _userService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("Create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _userService.CreateAsync(user);
            return Ok(user.Id);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, User booksData)
        {
            var book = await _userService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            await _userService.UpdateAsync(id, booksData);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _userService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            await _userService.DeleteAsync(book.Id);
            return NoContent();
        }
    }
}
