using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AiXinYaoYe.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        [HttpPost]
        public ActionResult Upload()
        {
            var file = HttpContext.Request.Files[0];
            var path = Server.MapPath("~/Content/UploadImage/");
            var res = "";
            var url = "";
            try
            {
                if (!file.Equals(null))
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Guid.NewGuid() + "." +
                                   file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    file.SaveAs(path + fileName);
                    url = "/Content/UploadImage/" + fileName;
                    res = "success";
                }
                else
                {
                    res = "图片不存在";
                }
            }
            catch (Exception e)
            {
                res = e.Message;
                Console.WriteLine(e);
                throw;
            }
            return Json(new { res, url });
        }
    }
}