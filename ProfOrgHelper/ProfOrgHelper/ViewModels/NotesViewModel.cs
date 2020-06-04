using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ProfOrgHelper.Models;
using ProfOrgHelper.Views;
using ProfOrgHelper.Services;
using System.Linq;

namespace ProfOrgHelper.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
        public IDataStore<Note> DataStore => DependencyService.Get<IDataStore<Note>>();
        public ObservableCollection<Note> Notes { get; set; }
        public Command LoadItemsCommand { get; set; }

        public NotesViewModel()
        {
            Notes = new ObservableCollection<Note>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Note>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item;
                Notes.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<ItemsPage, Note>(this, "DeleteItem", async (obj, note) =>
            {
                Notes.Remove(note);
                await DataStore.DeleteItemAsync(note.Id);
                SortNotes();
            });

            MessagingCenter.Subscribe<EditNotePage, Note>(this, "EditItem", async (obj, note) =>
            {
                await DataStore.UpdateItemAsync(note);
                await ExecuteLoadItemsCommand();
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Notes.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Notes.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void SortNotes()
        {
            var tempNotes = Notes.OrderBy(s => s.Date).ToList();

            Notes.Clear();

            foreach (var queue in tempNotes)
            {
                Notes.Add(queue);
            }
        }
    }
}