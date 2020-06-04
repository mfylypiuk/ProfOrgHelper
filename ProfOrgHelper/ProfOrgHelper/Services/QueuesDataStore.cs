using ProfOrgHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProfOrgHelper.Services
{
    public class QueuesDataStore : IDataStore<Queue>
    {
        readonly List<Queue> queues;
        public IDataStore<Student> StudentsDataStore => DependencyService.Get<IDataStore<Student>>();

        public QueuesDataStore()
        {
            var students = StudentsDataStore.GetItemsAsync(true).Result;

            queues = new List<Queue>()
            {
                new Queue { Id = Guid.NewGuid().ToString(), Student = students.FirstOrDefault(), Date = new DateTime(2021, 2, 1), Performed = false },
                new Queue { Id = Guid.NewGuid().ToString(), Student = students.Last(), Date = new DateTime(2020, 4, 1), Performed = true }
            };
        }

        public async Task<bool> AddItemAsync(Queue item)
        {
            queues.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = queues.Where((Queue arg) => arg.Id == id).FirstOrDefault();
            queues.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<Queue> GetItemAsync(string id)
        {
            return await Task.FromResult(queues.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Queue>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(queues);
        }

        public async Task<bool> UpdateItemAsync(Queue item)
        {
            var oldItem = queues.Where((Queue arg) => arg.Id == item.Id).FirstOrDefault();
            queues.Remove(oldItem);
            queues.Add(item);
            return await Task.FromResult(true);
        }
    }
}
