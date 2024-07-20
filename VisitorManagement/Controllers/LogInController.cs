using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using VisitorManagement.Models;

namespace VisitorManagement.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            ViewBag.Message = HttpContext.Session.GetString("user");
            if(ViewBag.Message != "")
            {
                HttpContext.Session.Remove("user");
            }

            return View();
        }

        public IActionResult AddAdmins()
        {

            ViewBag.Message = HttpContext.Session.GetString("user");

            return View();
        }

        public JsonResult getDesignation()
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";

            try
            {
                DataTable dt = DatabaseConn.ExecuteProcedure("GET_DEGIGNATION");

                if (dt.Rows.Count > 0)
                {
                    sb.Append("<select class='form-select'>");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        /*sb.Append("<option value='+dt.Rows[i]["d_name"]+'>" + dt.Rows[i]["d_name"] + "</option>");*/
                        sb.Append("<option value='").Append(dt.Rows[i]["d_id"]).Append("'>").Append(dt.Rows[i]["d_name"]).Append("</option>");

                    }
                    sb.Append("</select>");
                }

                dic["Message"] = sb.ToString();


            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }

            return Json(dic);
        }


        public JsonResult addCAdmins(string name,string email,string phone,string self,string password,string dId)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";


            try
            {
                string[,] param = new string[,]
                {
                    {"@aName",name },
                    {"@aEmail",email },
                    {"@aPhonr",phone },
                    {"@aSelf",self },
                    {"@aPassword",password },
                    {"@aDesig",dId }
                };

                DataTable dt = DatabaseConn.ExecuteProcedure("INSERT_ADMIN", param);
                if (dt.Rows.Count > 0)
                {
                    dic["Message"]= dt.Rows[0]["Msg"].ToString();
                }

            }catch(Exception ex)
            {
                dic["Message"] = ex.Message;
            }
            return Json(dic);
        }

        public JsonResult getVerifyUser(string email,string password)
        {
            Dictionary<string,string> dic = new Dictionary<string,string>();
            dic["Message"] = "";
            try
            {
                string[,] param = new string[,]
                {
                    {"@EMAIL",email },
                    {"@PASSWORD",password }
                };

                DataTable dt = DatabaseConn.ExecuteProcedure("USER_VERIFY", param);

                if(dt.Rows.Count > 0)
                {
                    dic["Message"] = dt.Rows[0]["IS_USER"].ToString();
                }

                HttpContext.Session.SetString("user", dic["Message"]);

            }catch(Exception ex)
            {
                dic["Message"] = ex.Message;
            }
            return Json(dic);
        }

    }
}
