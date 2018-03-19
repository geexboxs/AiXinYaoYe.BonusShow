using System.Linq;
using AiXinYaoYeV2.Database;
using Microsoft.AspNetCore.Mvc;

namespace AiXinYaoYeV2.Controllers
{
    public class RecommendProductController : Controller
    {
        private MyDbContext db;

        public RecommendProductController(MyDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
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
            var recommandProduct = db.RecommandProducts.FirstOrDefault(a => a.Id == id);
            return View(recommandProduct);
        }
    }
}
