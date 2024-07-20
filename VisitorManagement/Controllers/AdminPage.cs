using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using VisitorManagement.Models;

namespace VisitorManagement.Controllers
{
    public class AdminPage : Controller
    {
        public IActionResult AdminPages()
        {
            ViewBag.Message = HttpContext.Session.GetString("user");

            return View();
        }

        public JsonResult getMeet()
        {
            Dictionary<string,string> dic = new Dictionary<string,string>();
            dic["Message"] = "";

            StringBuilder sb = new StringBuilder();
            try
            {
                DataTable dt = DatabaseConn.ExecuteProcedure("GET_MEET_UP");
                if (dt.Rows.Count > 0)
                {
                    sb.Append("<table class='table table-striped'> <tr>");
                    sb.Append("<th>Name</th>");
                    sb.Append("<th>Email</th>");
                    sb.Append("<th>Organisatio</th>");
                    sb.Append("<th>Phone</th>");
                    sb.Append("<th>About</th> "); 
                    sb.Append("<th>Accept</th> "); 
                    sb.Append("<th>Reject</th> </tr>"); 

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr><td>" + dt.Rows[i]["m_name"] + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["m_email"] + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["m_org"] + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["m_phone"] + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["m_about"] + "</td>");
                        sb.Append("<td><button class='btn btn-success' onclick='AcceptButton()'>Accept</button> </td>");
                        sb.Append("<td><button class='btn btn-danger'>Reject</button> </td></tr>");
                    }
                    sb.Append("</table>");
                    dic["Message"] = sb.ToString();
                }
                


            }catch(Exception ex)
            {
                dic["Message"] = ex.Message;
            }
            return Json(dic);
        }

    }
}
