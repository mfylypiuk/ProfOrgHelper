using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProfOrgHelper.Services;
using ProfOrgHelper.Views;

namespace ProfOrgHelper
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<NotesDataStore>();
            DependencyService.Register<StudentsDataStore>();
            DependencyService.Register<QueuesDataStore>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
