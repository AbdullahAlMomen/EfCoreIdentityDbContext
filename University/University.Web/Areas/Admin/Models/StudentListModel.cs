using University.Application.Services;
using University.Infrastructure;

namespace University.Web.Areas.Admin.Models
{
    public class StudentListModel
    {
        private IStudentService _StudentService;

        public StudentListModel()
        {

        }
        public StudentListModel(IStudentService StudentService)
        {
            _StudentService = StudentService;
        }
        public async Task<object> GetPagedStudentsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _StudentService.GetPagedStudentsAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.SearchText,
                dataTablesUtility.GetSortText(new string[] { "Name", "Address" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Address,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        public void DeleteStudent(Guid id)
        {
            _StudentService.DeleteStudent(id);
        }
    }
}
