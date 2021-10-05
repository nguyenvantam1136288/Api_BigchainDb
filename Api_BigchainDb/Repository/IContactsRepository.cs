namespace Api_BigchainDb.Repository
{
    public interface IContactsRepository
    {
        bool CheckValidUserKey(string reqkey);
    }
}
