using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;
using AuthenticationApi.Models;

namespace AuthenticationApi.Servicer
{
    public class MongoDbService
    {
        private readonly MongoDbService _mongoDbService;
      
        public string Hash(string password)
        {
            byte[] data = Encoding.Default.GetBytes(password);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(data);
            password = Convert.ToBase64String(result);
            return password;
        }
        private readonly IMongoCollection<DataBaseModel> _DataBaseCollection;
        public MongoDbService(IOptions<DataBaseSettings> _settings)
        {
            MongoClient client = new MongoClient(_settings.Value.ConnectionUrl);
            IMongoDatabase database = client.GetDatabase(_settings.Value.DataBaseMane);
            _DataBaseCollection = database.GetCollection<DataBaseModel>(_settings.Value.Collection);
        }
        public async Task<DataBaseModel> CreateUserAsync( DataBaseModel _model)
        {
            _model.Password = Hash(_model.Password);
            await _DataBaseCollection.InsertOneAsync(_model);
            return _model;
        }

        public async Task<List<DataBaseModel>> GetAllAsync()
        {
            return await _DataBaseCollection.Find(_ => true).ToListAsync();
        }
    }
}
