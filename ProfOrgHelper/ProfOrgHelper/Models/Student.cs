using ProfOrgHelper.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfOrgHelper.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LearningForm { get; set; }
        public bool ConsistOfProfKom { get; set; }
        public bool Nonresident { get; set; }
        public bool Married { get; set; }
        public string Children { get; set; }
        public string FamilyEmergency { get; set; }
    }
}
