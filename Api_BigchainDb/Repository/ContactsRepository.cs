using System.Collections.Generic;

namespace Api_BigchainDb.Repository
{
    public class ContactsRepository : IContactsRepository
    {
        public bool CheckValidUserKey(string reqkey)
        {
            var userkeyList = new List<string>
            {
                "28236d8ec201df516d0f6472d516d72d",
                "38236d8ec201df516d0f6472d516d72c",
                "48236d8ec201df516d0f6472d516d72b"
            };

            if (userkeyList.Contains(reqkey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
