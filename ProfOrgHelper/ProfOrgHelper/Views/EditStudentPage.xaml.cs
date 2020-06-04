using ProfOrgHelper.Models;
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
    public partial class EditStudentPage : ContentPage
    {
        public Student Student { get; set; }

        public EditStudentPage(Student student)
        {
            InitializeComponent();

            Student = student;
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "EditItem", Student);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}