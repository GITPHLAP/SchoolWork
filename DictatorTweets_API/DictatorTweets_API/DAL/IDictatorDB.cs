using DictatorTweets_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DictatorTweets_API.DAL
{
    public interface IDictatorDB
    {
        private static string connectionString;

        Task<Dictator> CreateDictator(Dictator dictator);

        void UpdateDictator(Dictator dictator);

        void DeleteDictator(Dictator dictator);

        Task<List<Dictator>> GetDictators();
    }
}
