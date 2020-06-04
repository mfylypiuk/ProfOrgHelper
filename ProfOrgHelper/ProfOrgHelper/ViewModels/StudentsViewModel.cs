using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ProfOrgHelper.Models;
using ProfOrgHelper.Views;
using ProfOrgHelper.Services;
using System.Linq;
using System.Collections.Generic;

namespace ProfOrgHelper.ViewModels
{
    public class StudentsViewModel : BaseViewModel
    {
        public IDataStore<Student> DataStore => DependencyService.Get<IDataStore<Student>>();
        public ObservableCollection<Student> Students { get; set; }
        public Command LoadItemsCommand { get; set; }

        public StudentsViewModel()
        {
            Students = new ObservableCollection<Student>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewStudentPage, Student>(this, "AddItem", async (obj, student) =>
            {
                Students.Add(student);
                await DataStore.AddItemAsync(student);
                SortStudents();
            });

            MessagingCenter.Subscribe<StudentsPage, Student>(this, "DeleteItem", async (obj, student) =>
            {
                Students.Remove(student);
                await DataStore.DeleteItemAsync(student.Id);
                SortStudents();
            });

            MessagingCenter.Subscribe<EditStudentPage, Student>(this, "EditItem", async (obj, student) =>
            {
                await DataStore.UpdateItemAsync(student);
                await ExecuteLoadItemsCommand();
                SortStudents();
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Students.Clear();

                var items = await DataStore.GetItemsAsync(true);

                foreach (Student item in items)
                {
                    Students.Add(item);
                }

                SortStudents();
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

        private void SortStudents()
        {
            var tempStudents = Students.OrderBy(s => s.Name).ToList();

            Students.Clear();

            foreach (var student in tempStudents)
            {
                Students.Add(student);
            }
        }
    }
}