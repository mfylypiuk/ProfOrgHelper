using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfOrgHelper.Models;

namespace ProfOrgHelper.Services
{
    public class NotesDataStore : IDataStore<Note>
    {
        readonly List<Note> items;

        public NotesDataStore()
        {
            items = new List<Note>()
            {
                new Note { Id = Guid.NewGuid().ToString(), Text = "Боднарашу допомогу не надавати", Date = new DateTime(2000, 3, 6) },
                new Note { Id = Guid.NewGuid().ToString(), Text = "Филипюку видати допомогу два рази", Date = DateTime.Now }
            };
        }

        public async Task<bool> AddItemAsync(Note item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Note item)
        {
            var oldItem = items.Where((Note arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Note arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Note> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Note>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}