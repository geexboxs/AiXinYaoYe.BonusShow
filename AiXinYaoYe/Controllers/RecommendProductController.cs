using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AiXinYaoYe.Database;

namespace AiXinYaoYe.Controllers
{
    public class RecommendProductController : Controller
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var db = new MyDbContext();
            var recommandProducts = db.RecommandProducts.ToList();
            return View(recommandProducts);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            var db = new MyDbContext();
            var recommandProduct = db.RecommandProducts.FirstOrDefault(a => a.Id == id);
            return View(recommandProduct);
        }
    }
}
