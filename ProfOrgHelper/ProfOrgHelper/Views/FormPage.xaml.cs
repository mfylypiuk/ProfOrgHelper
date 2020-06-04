using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfOrgHelper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormPage : ContentPage
    {
        public FormPage()
        {
            InitializeComponent();

            BindingContext = this;

            MessagingCenter.Subscribe<EditFormImagePage, string>(this, "EditItem", (obj, link) =>
            {
                FormImage.Source = link;
            });
        }

        private async void ChangeImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EditFormImagePage()));
        }

        private void DeleteImageButton_Clicked(object sender, EventArgs e)
        {
            FormImage.Source = null;
        }
    }
}