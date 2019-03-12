using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlternetSiparisYazilimi.Controllers
{
    public class XXController : Controller
    {
        // GET: XX
        public ActionResult Index()
        {
            return View();
        }

        // GET: XX/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: XX/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: XX/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]IFormCollection collection)
        {
            try
            {
                string isim = collection["isim"];
                //byte[] resim = collection["pic"];
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: XX/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: XX/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: XX/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: XX/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}