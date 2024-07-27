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

            ViewBag.Message = HttpContext.Session.GetString("user");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Name = HttpContext.Session.GetString("name");

            return View();
        }

        public IActionResult Requests()
        {
            ViewBag.Message = HttpContext.Session.GetString("user");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Name = HttpContext.Session.GetString("name");
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
                    sb.Append("<select class='form-select' id='personSelect'>");
                    sb.Append("<option value=''>Select a person</option>");
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
    
    
       public JsonResult saveMeeting(string name,string org,string email,string phone,string date,string time,string m_id,string about)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";


            try
            {
                string[,] param = new string[,]
                {
                    {"@mName",name },
                    {"@mEmail",email },
                    {"@mOrg",org },
                    {"@mPhone",phone },
                    {"@mDate",date },
                    {"@mTime",time },
                    {"@mAdmin",m_id },
                    {"@mAbout", about}
                };

                DataTable dt = DatabaseConn.ExecuteProcedure("INSERT_MEET", param);
                if(dt.Rows.Count > 0)
                {
                    dic["Message"] = dt.Rows[0]["Msg"].ToString();
                }



            }catch(Exception ex)
            {
                dic["Message"] = ex.Message;
            }
            return Json(dic);
        }


        public JsonResult getMeetByEmailVisitor()
        {
            string email = HttpContext.Session.GetString("email");
            StringBuilder sb = new StringBuilder();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic["Message"] = "";
            try
            {
                string[,] param = new string[,]
                {
                    {"@mEmail",email }
                };

                DataTable dt = DatabaseConn.ExecuteProcedure("GET_MEET_BY_EMAIL", param);

                if (dt.Rows.Count > 0)
                {
                    sb.Append("<table class='table table-striped'> <tr>");
                    sb.Append("<th>Name</th>");
                    sb.Append("<th>Email</th>");
                    sb.Append("<th>Organisation</th>");
                    sb.Append("<th>Phone</th>");
                    sb.Append("<th>About</th> ");
                    sb.Append("<th>Meeter</th>");
                    sb.Append("<th>Accept</th> ");
                    sb.Append("<th>Reject</th> </tr>");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr><td>" + dt.Rows[i]["m_name"] + "</td>");
                        sb.Append("<td class='text-wrap'>" + dt.Rows[i]["m_email"] + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["m_org"] + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["m_phone"] + "</td>");
                        sb.Append("<td class='text-wrap'>" + dt.Rows[i]["m_about"] + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["a_name"] + "</td>");
                        sb.Append("<td><button class='btn btn-success' onclick=\"AcceptButton('" + dt.Rows[i]["a_email"] + "', '" + dt.Rows[i]["m_email"] + "')\">Accept</button> </td>");

                        /* sb.Append("<td><button class='btn btn-success' onclick='AcceptButton(" + dt.Rows[i]["a_email"] + ")'>Accept</button> </td>");
                         sb.Append("<td><button class='btn btn-danger' onclick='RejectRequest(" + dt.Rows[i]["m_email"] +")'>Reject</button> </td></tr>");
                        */
                        sb.Append("<td><button class='btn btn-danger' onclick=\"RejectRequest('" + dt.Rows[i]["m_email"] + "')\">Reject</button> </td></tr>");

                    }
                    sb.Append("</table>");
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
