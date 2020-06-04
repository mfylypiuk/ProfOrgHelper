using ProfOrgHelper.Models;
using ProfOrgHelper.Services;
using ProfOrgHelper.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProfOrgHelper.ViewModels
{
    public class QueuesViewModel : BaseViewModel
    {
        public IDataStore<Queue> DataStore => DependencyService.Get<IDataStore<Queue>>();
        public ObservableCollection<Queue> Queues { get; set; }
        public Command LoadItemsCommand { get; set; }

        public QueuesViewModel()
        {
            Queues = new ObservableCollection<Queue>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewQueuePage, Queue>(this, "AddItem", async (obj, queue) =>
            {
                queue.Id = Guid.NewGuid().ToString();
                Queues.Add(queue);
                await DataStore.AddItemAsync(queue);
                SortQueues();
            });

            MessagingCenter.Subscribe<TablePage, Queue>(this, "EditItem", async (obj, queue) =>
            {
                await DataStore.UpdateItemAsync(queue);
                await ExecuteLoadItemsCommand();
                SortQueues();
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Queues.Clear();

                var items = await DataStore.GetItemsAsync(true);

                foreach (Queue item in items)
                {
                    Queues.Add(item);
                }

                SortQueues();
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

        private void SortQueues()
        {
            var tempQueues = Queues.OrderBy(s => s.Date).ToList();
            tempQueues = tempQueues.OrderBy(s => s.Performed).ToList();

            Queues.Clear();

            foreach (var queue in tempQueues)
            {
                Queues.Add(queue);
            }
        }
    }
}
