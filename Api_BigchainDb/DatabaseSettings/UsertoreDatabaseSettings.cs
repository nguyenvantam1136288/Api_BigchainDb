namespace Api_BigchainDb.DatabaseSettings
{
    public class UsertoreDatabaseSettings : IUserstoreDatabaseSettings
    {
        public string UserCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
