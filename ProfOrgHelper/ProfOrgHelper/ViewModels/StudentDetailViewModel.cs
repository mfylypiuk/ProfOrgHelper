using ProfOrgHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfOrgHelper.ViewModels
{
    public class StudentDetailViewModel : BaseViewModel
    {
        public Student Student { get; set; }

        public StudentDetailViewModel(Student student)
        {
            Title = student?.Name;
            Student = student;
        }
    }
}
