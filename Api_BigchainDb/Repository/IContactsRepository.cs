using Api_BigchainDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BigchainDb.Repository
{
    public interface IContactsRepository
    {
        bool CheckValidUserKey(string reqkey);
    }
}
