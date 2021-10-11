namespace Api_BigchainDb.DatabaseSettings
{
    public class StoreDatabaseSettings : IStoreDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UserCollectionName { get; set; }
        public string BookCollectionName { get; set; }

    }
}
