using System;
using System.Collections.Generic;
using System.Text;

namespace ProfOrgHelper.Models
{
    public class Queue
    {
        public string Id { get; set; }
        public Student Student { get; set; }
        public DateTime Date { get; set; }
        public bool Performed { get; set; }
    }
}
