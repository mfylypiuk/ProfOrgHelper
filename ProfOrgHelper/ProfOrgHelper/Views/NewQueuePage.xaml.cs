using ProfOrgHelper.Models;
using ProfOrgHelper.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfOrgHelper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewQueuePage : ContentPage
    {
        public IDataStore<Student> StudentsDataStore => DependencyService.Get<IDataStore<Student>>();
        public Queue Queue { get; set; }
        public ObservableCollection<string> Students { get; set; }

        private List<Student> students;

        public NewQueuePage()
        {
            InitializeComponent();

            Queue = new Queue();
            students = new List<Student>();
            Students = new ObservableCollection<string>();

            foreach (var student in StudentsDataStore.GetItemsAsync(true).Result)
            {
                students.Add(student);
                Students.Add(student.Name);
            }

            BindingContext = this;
        }

        private void StudentPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Queue.Student = students.FirstOrDefault(s => s.Name == ((Picker)sender).SelectedItem.ToString());
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Queue);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}