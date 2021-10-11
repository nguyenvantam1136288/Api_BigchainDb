using Api_BigchainDb.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api_BigchainDb.DatabaseSettings;
using MongoDB.Bson;

namespace Api_BigchainDb.Services
{
    public class UserService: IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UserCollectionName);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(c => true).ToListAsync();
        }
        public async Task<User> GetByIdAsync(string id)
        {
            return await _users.Find<User>(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<User> CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }
        public async Task<List<User>> CreateListAsync(List<User> user)
        {
            await _users.InsertManyAsync(user);
            return user;
        }
        public async Task UpdateAsync(string id, User user)
        {
            await _users.ReplaceOneAsync(c => c.Id == id, user);
        }
        public async Task DeleteAsync(string id)
        {
            await _users.DeleteOneAsync(c => c.Id == id);
        }
    }

    public class BookService : IBookService
    {
        private readonly IMongoCollection<Books> _books;
        private readonly IMongoCollection<User> _users;
        public BookService(IStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<Books>(settings.BookCollectionName);
            _users = database.GetCollection<User>(settings.UserCollectionName);
        }

        public async Task<List<Books>> GetAllAsync()
        {
            var docs = _users.Aggregate()//test join table
                     .Lookup("books", "BookId", "_id", "asbooks")
                     .As<BsonDocument>()
                     .ToList();

            foreach (var doc in docs)
            {
                var cc = doc.ToJson();
            }

            return await _books.Find(c => true).ToListAsync();
        }

    }

}
