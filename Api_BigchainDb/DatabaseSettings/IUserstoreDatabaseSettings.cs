namespace Api_BigchainDb.DatabaseSettings
{
    public interface IStoreDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string UserCollectionName { get; set; }
        string BookCollectionName { get; set; }

    }
}
