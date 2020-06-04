using System;
using ProfOrgHelper.Models;

namespace ProfOrgHelper.ViewModels
{
    public class NoteDetailViewModel : BaseViewModel
    {
        public Note Note { get; set; }

        public NoteDetailViewModel(Note item)
        {
            Title = item?.Text;
            Note = item;
        }
    }
}
