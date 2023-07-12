using Autofac;
using Microsoft.AspNetCore.Mvc;
using University.Infrastructure;
using University.Infrastructure.Exceptions;
using University.Web.Areas.Admin.Models;
using University.Web.Models;
using University.Web.Utilities;

namespace University.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        ILifetimeScope _scope;
        ILogger<StudentController> _logger;

        public StudentController(ILifetimeScope scope, ILogger<StudentController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = _scope.Resolve<StudentListModel>();

            return View(model);
        }
        public async Task<JsonResult> GetStudents()
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<StudentListModel>();

            var data = await model.GetPagedStudentsAsync(dataTablesModel);
            return Json(data);
        }


        public IActionResult Create()
        {
            var model = _scope.Resolve<StudentCreateModel>();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(StudentCreateModel model)
        {
            model.ResolveDependency(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateStudent();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully added a new Student.",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in creating Student.",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }
        public IActionResult Update(Guid id)
        {
            var model = _scope.Resolve<StudentUpdateModel>();
            model.Load(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(StudentUpdateModel model)
        {
            model.ResolveDependency(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    model.UpdateStudent();
                    return RedirectToAction("Index");
                }
                catch (DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");
                }
            }

            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            var model = _scope.Resolve<StudentListModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    model.DeleteStudent(id);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
