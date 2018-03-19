using System.Linq;
using AiXinYaoYeV2.Database;
using Microsoft.AspNetCore.Mvc;

namespace AiXinYaoYeV2.Controllers
{
    public class BonusProductController:Controller
    {
        private MyDbContext db;

        public BonusProductController(MyDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List() {
            var bonusProducts = db.BonusProducts.ToList();
            return View(bonusProducts);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            var bonusProduct = db.BonusProducts.FirstOrDefault(a => a.Id == id);
            return View(bonusProduct);
        }
    }
}
