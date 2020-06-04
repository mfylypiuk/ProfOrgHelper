using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProfOrgHelper.Models;
using ProfOrgHelper.Views;
using ProfOrgHelper.ViewModels;

namespace ProfOrgHelper.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class StudentsPage : ContentPage
    {
        StudentsViewModel viewModel;

        public StudentsPage()
        {
            InitializeComponent();
            viewModel = new StudentsViewModel();
            BindingContext = viewModel;
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Student)layout.BindingContext;
            await Navigation.PushAsync(new StudentDetailPage(new StudentDetailViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewStudentPage()));
        }

        void DeleteItem_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var student = (Student)layout.BindingContext;
            MessagingCenter.Send(this, "DeleteItem", student);
        }

        async void EditItem_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var item = (Student)layout.BindingContext;
            await Navigation.PushModalAsync(new NavigationPage(new EditStudentPage(item)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Students.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}