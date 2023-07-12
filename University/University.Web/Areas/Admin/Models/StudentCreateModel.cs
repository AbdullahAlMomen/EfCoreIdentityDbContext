using Autofac;
using System.ComponentModel.DataAnnotations;
using University.Application.Services;

namespace University.Web.Areas.Admin.Models
{
    public class StudentCreateModel
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

        private IStudentService _StudentService;

        public StudentCreateModel(IStudentService StudentService)
        {
            _StudentService = StudentService;
        }
        public StudentCreateModel()
        {

        }


        internal void ResolveDependency(ILifetimeScope scope)
        {
            _StudentService = scope.Resolve<IStudentService>();
        }

        internal void CreateStudent()
        {
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Address))
            {
                _StudentService.CreateStudent(Name, Address);
            }
        }
    }
}
