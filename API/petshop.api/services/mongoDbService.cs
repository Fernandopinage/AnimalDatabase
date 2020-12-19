using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using petshop.api.model;

namespace petshop.api.services
{
    public class MongoDbService
    {
        private IMongoCollection<Animal> AnimalCollection { get; }
        public MongoDbService(string database, string collection, string databaseUrl)
        {
            var mongoClient = new MongoClient(databaseUrl);
            var mongoDatabase = mongoClient.GetDatabase(database);
            AnimalCollection = mongoDatabase.GetCollection<Animal>(collection);
        }

        //operações INSERIR, UPDATE, LER e EXCLUSÃO

        //Recupera do mongo o registro, buscando pelo id
        public Animal Get (string id) => AnimalCollection
            .Find<Animal>(Animal => Animal.Id == id).FirstOrDefault();    

        public async Task Insere(Animal animal) => await AnimalCollection.InsertOneAsync(animal);

        public async Task<List<Animal>> GetAllAnimals(){
            var Animals = new List<Animal>();
            var allDocuments = await AnimalCollection.FindAsync(new BsonDocument());
            await allDocuments.ForEachAsync(doc => Animals.Add(doc));
            return Animals;
        }

        public void Atualizar(string id, Animal AnimalIn) => AnimalCollection.ReplaceOne(Animal => Animal.Id == id, AnimalIn);

        public void Deletar(Animal AnimalIn) => AnimalCollection.DeleteOne(Animal => Animal.Id == AnimalIn.Id);
    }
}