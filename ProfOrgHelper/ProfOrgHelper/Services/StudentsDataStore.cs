using ProfOrgHelper.Enums;
using ProfOrgHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfOrgHelper.Services
{
    public class StudentsDataStore : IDataStore<Student>
    {
        readonly List<Student> students;

        public StudentsDataStore()
        {
            students = new List<Student>()
            {
                new Student { Id = Guid.NewGuid().ToString(), Name = "Филипюк Максим Володимирович", LearningForm = "Очная", ConsistOfProfKom = false, Nonresident = false, Married = false },
                new Student { Id = Guid.NewGuid().ToString(), Name = "Боднараш Іван Іванович", LearningForm = "Заочная", ConsistOfProfKom = false, Nonresident = true, Married = true, Children = "Боднараш Василь Іванович", FamilyEmergency = "Отсутствуют" },
            };
        }

        public async Task<bool> AddItemAsync(Student item)
        {
            students.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = students.Where((Student arg) => arg.Id == id).FirstOrDefault();
            students.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<Student> GetItemAsync(string id)
        {
            return await Task.FromResult(students.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Student>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(students);
        }

        public async Task<bool> UpdateItemAsync(Student item)
        {
            var oldItem = students.Where((Student arg) => arg.Id == item.Id).FirstOrDefault();
            students.Remove(oldItem);
            students.Add(item);
            return await Task.FromResult(true);
        }
    }
}
