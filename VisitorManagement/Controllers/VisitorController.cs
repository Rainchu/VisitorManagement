using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Text;
using VisitorManagement.Models;

namespace VisitorManagement.Controllers
{
    public class VisitorController : Controller
    {
        public IActionResult VisitorHome()
        {
            return View();
        }

        public JsonResult getPerson()
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            try
            {
                DataTable dt = DatabaseConn.ExecuteProcedure("GET_PERSON_DETAIL");

                if (dt.Rows.Count > 0)
                {
                    sb.Append("<select class='form-select'>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<option value='").Append(dt.Rows[i]["a_id"]).Append("'>").Append(dt.Rows[i]["a_name"]).Append("</option>");

                    }
                    sb.Append("</select>");
                    dic["Message"] = sb.ToString();

                }

            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }
            return Json(dic);
        }
    }
}
