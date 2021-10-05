namespace Api_BigchainDb.DatabaseSettings
{
    public interface IUserstoreDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
