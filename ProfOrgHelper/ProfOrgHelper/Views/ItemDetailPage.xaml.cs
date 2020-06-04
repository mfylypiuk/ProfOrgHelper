using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ProfOrgHelper.Models;
using ProfOrgHelper.ViewModels;

namespace ProfOrgHelper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        NoteDetailViewModel viewModel;

        public ItemDetailPage(NoteDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        //async void Save_Clicked(object sender, EventArgs e)
        //{
        //    //MessagingCenter.Send(this, "EditItem", Note);
        //    await Navigation.PopModalAsync();
        //}

        //async void Cancel_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PopModalAsync();
        //}
    }
}