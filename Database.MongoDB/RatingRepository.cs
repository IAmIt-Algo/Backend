using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Configuration.Configuration;
using MongoDB.Driver;

namespace Database.MongoDB
{
    public class RatingRepository : IRatingRepository
    {
        private IMongoCollection<Rating> _ratingItems;
        public RatingRepository(IConfiguration configuration)
        {
            var client = new MongoClient();
            var db = client.GetDatabase(configuration.NameDatabase);
            _ratingItems = db.GetCollection<Rating>(nameof(Rating));
            var field = new StringFieldDefinition<Rating>("StarsCount");
            var options = new CreateIndexOptions() { Unique = false };
            var indexDefinition = new IndexKeysDefinitionBuilder<Rating>().Ascending(field);
            _ratingItems.Indexes.CreateOneAsync(indexDefinition, options);
        }

        public async Task<long> CountUsersByConditionAsync(System.Linq.Expressions.Expression<Func<Rating, bool>> filter)
        {
            return (await _ratingItems.CountAsync(filter));
        }

        public async Task<List<Rating>> GetRatingAsync()
        {
            return await _ratingItems.AsQueryable().ToListAsync();
        } 

        public async Task AddRatingItemAsync(Rating rating)
        {
            await _ratingItems.InsertOneAsync(rating);
        }

        public async Task<Rating> GetRatingItemAsync(string userName)
        {
            return (await _ratingItems.FindAsync(i => i.UserName == userName)).FirstOrDefault();
        }

        public async Task IncreaseRatingItemAsync(string userName, int increaseAmount)
        {
            var update = Builders<Rating>.Update.Inc(i => i.StarsCount, increaseAmount);
            await _ratingItems.UpdateOneAsync(i => i.UserName == userName, update);
        }
    }
}
