using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNoteService.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DalSmartNote
{
    public class SmartNoteDal : ISmartNoteDal
    {
        protected static IMongoClient _client = new MongoClient();
        protected static IMongoDatabase _database = _client.GetDatabase("SmartNoteDB");

        public bool DeleteNote(Note input)
        {
            var collection = _database.GetCollection<Note>("notes");
            var filter = Builders<Note>.Filter.Eq("Id", input.Id);
            var ret = collection.DeleteOneAsync(filter);

            return true;
        }

        public Task<List<Note>> GetAllNote(User input)
        {
            var collection = _database.GetCollection<Note>("notes");
            var filter = Builders<Note>.Filter.Eq("Author", input);
            var ret = collection.Find(filter).ToListAsync();

            return ret;
        }

        public bool InsertNote(Note input)
        {
            var collection = _database.GetCollection<Note>("notes");
            collection.InsertOneAsync(input);

            return true;
        }

        public bool UpdateNote(Note input)
        {
            var collection = _database.GetCollection<Note>("notes");
            var filter = Builders<Note>.Filter.Eq("Id", input.Id);
            //var update = Builders<Note>.Update.Set("Title", input.Title).Set("Text", input.Text)
            //                                  .Set("ModoficationDate", input.ModoficationDate).Set("Links", input.Links)
            //                                  .Set("CreationDate", input.CreationDate).Set("Author", input.Author)
            //                                  .Set("Attachments", input.Attachments);

            //var ret = collection.UpdateOneAsync(filter, update);

            var ret = collection.ReplaceOneAsync(filter, input);

            return true;
        }

        public bool DeleteAndInsertAll(List<Note> input, User author)
        {
            var collection = _database.GetCollection<Note>("notes");
            var filter = Builders<Note>.Filter.Eq("Author", author);
            var ret = collection.DeleteManyAsync(filter);
            collection.InsertManyAsync(input);

            return true;
        }
    }
}
