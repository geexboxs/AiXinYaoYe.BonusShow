using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AiXinYaoYe.Database;

namespace AiXinYaoYe.Controllers
{
    public class BonusProductController:Controller
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List() {
            var db = new MyDbContext();
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
            var db = new MyDbContext();
            var bonusProduct = db.BonusProducts.FirstOrDefault(a => a.Id == id);
            return View(bonusProduct);
        }
    }
}
