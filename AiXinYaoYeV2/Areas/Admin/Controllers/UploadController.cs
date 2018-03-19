using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AiXinYaoYeV2.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UploadController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public UploadController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        [HttpPost]
        public ActionResult Upload()
        {
            var file = HttpContext.Request.Form.Files[0];
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "Content\\UploadImage\\");
            var res = "";
            var url = "";
            try
            {
                if (!file.Equals(null))
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Guid.NewGuid() + "." +
                                   file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    using (FileStream fs = System.IO.File.OpenWrite(path + fileName))
                    {
                        file.CopyTo(fs);
                    }
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