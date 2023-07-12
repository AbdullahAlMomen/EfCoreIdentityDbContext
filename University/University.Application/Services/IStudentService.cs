using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Domain.Entities;

namespace University.Application.Services
{
    public interface IStudentService
    {
        void CreateStudent(string name, string address);
        void DeleteStudent(Guid id);
        Task<Student> GetStudentAsync(Guid id);
        public Task<IList<Student>> GetStudentsAsync();
        Task<(IList<Student> records, int total, int totalDisplay)> GetPagedStudentsAsync(int pageIndex,
            int pageSize, string searchText, string orderBy);
        void UpdateStudent(Guid Id, string name, string address);
    }
}
