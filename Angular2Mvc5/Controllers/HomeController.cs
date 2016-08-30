using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.DAL;
using Models.ViewModel;
using WrapperExtensions;

namespace Angular2Mvc5.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository _repository;

        public HomeController()
        {
            this._repository = new Repository(new ModelStateWrapper(this.ModelState));
        }


        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetTodoList()
        {
            var tl = this._repository.Entity<List>().OrderBy(o => o.Id).ToList();

            return Json(tl, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddList(List list)
        {
            _repository.Insert(list);
            _repository.Save();

            return Json(new JSONReturnVM<List>(list, this.ModelState));
        }

        [HttpPost]
        public JsonResult UpdateList(List list)
        {
            _repository.Update(list);
            _repository.Save();

            return Json(new JSONReturnVM<List>(list, this.ModelState));
        }

        [HttpPost]
        public JsonResult DeleteList(List list)
        {
            _repository.Delete<List>(list.Id);
            _repository.Save();

            return Json(new JSONReturnVM<List>(list, this.ModelState));
        }

        [HttpPost]
        public JsonResult AddTask(Task task)
        {
            _repository.Insert(task);
            _repository.Save();

            return Json(new JSONReturnVM<Task>(task, this.ModelState));
        }

        [HttpPost]
        public JsonResult UpdateTask(Task task)
        {
            _repository.Update(task);
            _repository.Save();

            return Json(new JSONReturnVM<Task>(task, this.ModelState));
        }

        [HttpPost]
        public JsonResult DeleteTask(Task task)
        {
            this._repository.Delete<Task>(task.Id);
            this._repository.Save();

            return Json(new JSONReturnVM<Task>(task, this.ModelState));
        }
    }
}