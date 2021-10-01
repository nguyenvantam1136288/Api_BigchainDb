using Api_BigchainDb.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BigchainDb.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public IContactsRepository ContactsRepo { get; set; }

        public HomeController(IContactsRepository _repo)
        {
            ContactsRepo = _repo;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok("successfully");
        }

    }
}
