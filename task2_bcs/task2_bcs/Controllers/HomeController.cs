using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task2_bcs.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                string filePath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/UploadFiles"), filePath));
            }

            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = Path.GetFullPath(Server.MapPath("~/UploadFiles/unpackzip.bat"));
                process.StartInfo.WorkingDirectory = Path.GetFullPath(Server.MapPath("~/UploadFiles"));
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.Start();
            }
            catch
            {
                return Json("file uploaded successfully");
            }

            return Json("file uploaded successfully");
        }
    }
}