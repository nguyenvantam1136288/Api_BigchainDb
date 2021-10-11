using Api_BigchainDb.Models;
using Api_BigchainDb.Queue;
using Api_BigchainDb.Repository;
using Api_BigchainDb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly BookService _bookService;
        private IConfiguration _iConfig;
        private IServiceProvider _services;
        public UserController(IServiceProvider services,UserService userService, IContactsRepository _repo, IConfiguration iConfig,
        ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor, BookService bookService)
        {
            _userService = userService;
            ContactsRepo = _repo;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _bookService = bookService;
            _iConfig = iConfig;
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var context = _httpContextAccessor.HttpContext;
            _logger.LogInformation("Request:" + context.Request.Path + "Method: " + context.Request.Method);

            var bookAll = await _bookService.GetAllAsync();//Test Join Table Book and User
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
        public async Task<IActionResult> Create([FromBody] List<User> listUer)
        {
            int maxRecord = 0;
            int.TryParse(_iConfig.GetSection("MySettings").GetSection("MaxRecord").Value.ToString(), out maxRecord);

            MonitorLoop monitorLoop = _services.GetRequiredService<MonitorLoop>()!;
            if (listUer.Count() > maxRecord)
            {
                monitorLoop.StartMonitorLoop(listUer);
            }
            else
            {
                await _userService.CreateListAsync(listUer);
            }

            #region
            //int maxRecord = 0;
            //int.TryParse(_iConfig.GetSection("MySettings").GetSection("MaxRecord").Value.ToString(), out maxRecord);


            //if(listUer.Count() > 5)
            //{
            //    //đưa 1000 vào queue
            //    var cqueue = new Queue<object>(listUer.AsEnumerable().Reverse());
            //    foreach (var log in cqueue)
            //    {
            //        var user = new User();
            //        user = (User)log;
            //        //Lưu
            //    }
            //}


            //var qdwueue = new Queue<string>((IEnumerable<string>)listUer);
            //evtLogQueue.Enqueue(listUer.Count());
            //if(evtLogQueue.Count > maxRecord) //5
            //{
            //    foreach (var log in evtLogQueue)
            //    {
            //       //Save Data
            //    }
            //    evtLogQueue.Clear();
            //}

            //MonitorLoop monitorLoop = host.Services.GetRequiredService<MonitorLoop>()!;
            //monitorLoop.StartMonitorLoop();
            #endregion
            return Ok("Success: " + listUer.Count());
        }

        [HttpPost("Create2")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create2(User user)
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
