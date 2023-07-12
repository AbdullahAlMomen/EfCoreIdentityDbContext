using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Application.Services;
using University.Application.UnitOfWorks;
using University.Domain.Entities;

namespace University.Infrastructure.Services
{
    public class StudentService : IStudentService
    {
        private IApplicationUnitOfWork _applicationUnitOfWork;
        public StudentService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public void CreateStudent(string name, string address)
        {
            if (_applicationUnitOfWork.StudentRepository.IsDuplicateName(name,address, null))
                throw new DuplicateNameException("Student name is duplicate");

            Student Student = new Student()
            {
                Name = name,
                Address = address,
                
            };

            _applicationUnitOfWork.StudentRepository.AddAsync(Student); ;
            _applicationUnitOfWork.Save();

        }

        public void DeleteStudent(Guid id)
        {
            _applicationUnitOfWork.StudentRepository.Remove(id);
            _applicationUnitOfWork.Save();
        }

        public async Task<Student> GetStudentAsync(Guid id)
        {
            return await _applicationUnitOfWork.StudentRepository.GetByIdAsync(id);

        }

        public async Task<IList<Student>> GetStudentsAsync()
        {
            return await _applicationUnitOfWork.StudentRepository.GetAllAsync();
        }

        public async Task<(IList<Student> records, int total, int totalDisplay)> GetPagedStudentsAsync(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            var result = await _applicationUnitOfWork.StudentRepository.GetTableDataAsync(
               x => x.Name.Contains(searchText), orderBy, pageIndex, pageSize);
            return result;
        }

        public void UpdateStudent(Guid Id, string name, string address)
        {
            if (_applicationUnitOfWork.StudentRepository.IsDuplicateName(name,address, Id))
                throw new DuplicateNameException("Student name is duplicate");

            Student _Student = _applicationUnitOfWork.StudentRepository.GetById(Id);
            _Student.Name = name;
            _Student.Address = address;
           

            _applicationUnitOfWork.Save();
        }
    }
}
