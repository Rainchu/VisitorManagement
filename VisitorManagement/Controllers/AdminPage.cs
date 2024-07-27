using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;
using VisitorManagement.Models;

namespace VisitorManagement.Controllers
{
    public class AdminPage : Controller
    {

        public IActionResult MyRequest()
        {
            ViewBag.Message = HttpContext.Session.GetString("user");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Name = HttpContext.Session.GetString("name");
            return View();
        }

        public IActionResult AddAdmins()
        {

            ViewBag.Message = HttpContext.Session.GetString("user");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Name = HttpContext.Session.GetString("name");

            return View();
        }

        public IActionResult AdminPages()
        {
            ViewBag.Message = HttpContext.Session.GetString("user");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Email = HttpContext.Session.GetString("email");

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
                    


                    sb.Append("<div class='row offset-md-1 bg-primary-subtle'>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string cardClass = dt.Rows[i]["status"].ToString() == "1" ? " card-disabled" : "";



                        sb.Append("<div class='card col-md-5 m-1 shadow-lg "+cardClass+"'> <div class='card-body'>");
                        sb.Append("<div class='row'>");
                        sb.Append("<div class='col-md-4'>");
                        sb.Append("<p>Name</p>");
                        sb.Append("<p>Email</p>");
                        sb.Append("<p>Organisation</p>");
                        sb.Append("<p>Phone</p>");
                        sb.Append("<p>Meeter</p>");
                        sb.Append("<p>Date</p>");
                        sb.Append("<p>Time</p>");
                        sb.Append("<p>About</p>");
                        
                        sb.Append("</div>");

                        sb.Append("<div class='col-md-8'>");
                        sb.Append("<p>"+dt.Rows[i]["m_name"] + "</p>");
                        sb.Append("<p>" + dt.Rows[i]["m_email"] + "</p>");
                        sb.Append("<p>" + dt.Rows[i]["m_org"] + "</p>");
                        sb.Append("<p>" + dt.Rows[i]["m_phone"] + "</p>");
                        sb.Append("<p>" + dt.Rows[i]["a_name"] + "</p>");
                        sb.Append("<p>" + dt.Rows[i]["meetDate"] + "</p>");
                        sb.Append("<p>" + dt.Rows[i]["meetTime"] + "</p>");
                        sb.Append("<textarea class='p-1' disabled cols='30' rows='2'> " + dt.Rows[i]["m_about"] + "</textarea>");
                      
                        sb.Append("</div></div>");
                      

                               sb.Append("<div class='row offset-1 mt-2'>");
                        sb.Append("<div class='col'>");
                                 sb.Append("<button data-bs-toggle='modal' data-bs-target='#model_dialog' class='btn btn-success' onclick=\"AcceptButton('" + dt.Rows[i]["a_email"] + "', '" + dt.Rows[i]["m_email"] + "','" + dt.Rows[i]["m_id"] +"')\">Accept</button></div> ");
                        sb.Append("<div class='col'>");
                        sb.Append("<button class='btn btn-danger' onclick=\"RejectRequest('" + dt.Rows[i]["m_email"] + "','" + dt.Rows[i]["m_id"] +"')\">Reject</button></div></div>");




                        sb.Append("</div></div>");



                                        
                    }
                    sb.Append("</div>");
                    dic["Message"] = sb.ToString();
                }
                


            }catch(Exception ex)
            {
                dic["Message"] = ex.Message;
            }
            return Json(dic);
        }

        public JsonResult sendEmail(string fromEmail,string toEmail,string subject,string body,string m_id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            var status = "2";


            try
            {
                string[,] param = new string[,]
                {
                    {"@mId",m_id},
                    {"@mStatus",status }
                };
                DataTable dt = DatabaseConn.ExecuteProcedure("UPDATE_MEET", param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dic["Message"] = dt.Rows[0]["Msg"].ToString();

                }


                string fromEmail1 = fromEmail;
                string password = "szftgybhmaxrmzva"; // Ensure this is an app-specific password

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromEmail);
                message.Subject = subject;
                message.To.Add(new MailAddress(toEmail));
                message.Body = "<html><body>"+body+"</body></html>";
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Use port 587 for TLS/STARTTLS
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true
                };

                smtp.Send(message);
                   }
            catch (SmtpException ex)
            {
                Console.WriteLine("SMTP Exception: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
                Console.WriteLine("General Exception: " + ex.Message);
            }
            return Json(dic);
        }

        public JsonResult rejectEmail(string fromEmail, string toEmail, string subject, string body,string m_id)
        {
            string status = "1";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";

            try
            {


                string[,] param = new string[,]
                {
                    {"@mId",m_id },
                    {"@mStatus",status }
                };

                DataTable dt = DatabaseConn.ExecuteProcedure("UPDATE_MEET", param);

                if (dt.Rows.Count > 0)
                {
                    dic["Message"] = dt.Rows[0]["Msg"].ToString();
                }



                string fromEmail1 = fromEmail;
                string password = "szftgybhmaxrmzva"; // Ensure this is an app-specific password

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromEmail);
                message.Subject = subject;
                message.To.Add(new MailAddress(toEmail));
                message.Body = "<html><body>" + body + "</body></html>";
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Use port 587 for TLS/STARTTLS
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true
                };

                smtp.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("SMTP Exception: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
                Console.WriteLine("General Exception: " + ex.Message);
            }
            return Json(dic);
        }


        public JsonResult getMeetByEmail()
        {
            string email = HttpContext.Session.GetString("email");
            StringBuilder sb = new StringBuilder();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic["Message"] = "";
            try
            {
                string[,] param = new string[,]
                {
                    {"@aEmail",email }
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
