using ProfOrgHelper.Models;
using ProfOrgHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfOrgHelper.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class TablePage : ContentPage
    {
        QueuesViewModel viewModel;

        public TablePage()
        {
            InitializeComponent();
            viewModel = new QueuesViewModel();
            BindingContext = viewModel;
        }

        void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Queue)layout.BindingContext;
            item.Performed = !item.Performed;
            MessagingCenter.Send(this, "EditItem", item);
            //await Navigation.PushAsync(new StudentDetailPage(new StudentDetailViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewQueuePage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Queues.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}