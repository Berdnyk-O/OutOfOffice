using Microsoft.AspNetCore.Mvc;

namespace OutOfOffice.Controllers
{
    public class ListsController : Controller
    {
        public ActionResult Employees()
        {
            return View();
        }

        /*// GET: ListsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
