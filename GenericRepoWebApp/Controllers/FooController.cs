using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenericRepoWebApp.Data.DataAccess.Infrastructure;
using GenericRepoWebApp.Data.DataAccess.SqlServer;

namespace GenericRepoWebApp.Controllers {

    public class FooController : Controller {

        private readonly IFooRepository _fooRepo;

        public FooController(IFooRepository fooRepo) {
            _fooRepo = fooRepo;
        }

        public ViewResult Index() {

            var model = _fooRepo.GetAll();

            return View(model);
        }

        public ActionResult Details(int id) {

            var model = _fooRepo.GetSingle(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        public ActionResult Edit(int id) {

            var model = _fooRepo.GetSingle(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [ActionName("Edit"), HttpPost]
        public ActionResult Edit_post(Foo foo) {

            if (ModelState.IsValid) {

                try {

                    _fooRepo.Edit(foo);
                    _fooRepo.Save();

                    return RedirectToAction("details", new { id = foo.FooId });

                } catch (Exception ex) {

                    ModelState.AddModelError(string.Empty, "Something went wrong. Message: " + ex.Message);
                }
            }

            //If we come here, something went wrong. Return it back.
            return View(foo);
        }

        public ActionResult Create() {

            return View();
        }

        [ActionName("Create"), HttpPost]
        public ActionResult Create_post(Foo foo) {

            if (ModelState.IsValid) {

                try {

                    _fooRepo.Add(foo);
                    _fooRepo.Save();

                    return RedirectToAction("details", new { id = foo.FooId });

                } catch (Exception ex) {

                    ModelState.AddModelError(string.Empty, "Something went wrong. Message: " + ex.Message);
                }
            }

            //If we come here, something went wrong. Return it back.
            return View(foo);

        }

        public ActionResult Delete(int id) {

            var model = _fooRepo.GetSingle(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [ActionName("Delete"), HttpPost]
        public ActionResult Delete_post(int id) {

            var model = _fooRepo.GetSingle(id);

            if (model == null)
                return HttpNotFound();

            _fooRepo.Delete(model);
            _fooRepo.Save();

            return RedirectToAction("Index");
        }
    }
}