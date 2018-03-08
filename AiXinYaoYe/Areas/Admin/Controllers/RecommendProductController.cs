using System;
using System.Web.Mvc;

namespace AiXinYaoYe.Areas.Admin.Controllers
{
    public class RecommendProductController:Controller
    {
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            return View();
        }

        public ActionResult Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
