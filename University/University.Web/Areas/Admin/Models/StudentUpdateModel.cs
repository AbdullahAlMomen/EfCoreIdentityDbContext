using Autofac;
using System.ComponentModel.DataAnnotations;
using University.Application.Services;
using University.Domain.Entities;

namespace University.Web.Areas.Admin.Models
{
    public class StudentUpdateModel
    {

        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }


        private IStudentService _StudentService;

        public StudentUpdateModel(IStudentService StudentService)
        {
            _StudentService = StudentService;
        }


        public StudentUpdateModel()
        {

        }


        internal void ResolveDependency(ILifetimeScope scope)
        {
            _StudentService = scope.Resolve<IStudentService>();
        }

        internal async Task Load(Guid id)
        {
            Student student = await _StudentService.GetStudentAsync(id);
            Id = student.Id;
            Name = student.Name;
            Address = student.Address;

        }

        internal void UpdateStudent()
        {
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrEmpty(Address))
            {
                _StudentService.UpdateStudent(Id, Name, Address);
            }
        }
    }
}
