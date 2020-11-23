using Common_Objects;
using Common_Objects.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace CPR_Module.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            var loginUser = new Login_User();
            return View(loginUser);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(Login_User user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var username = user.Username;
                var password = user.Password;

                var userModel = new UserModel();
                var loggedInAgent = userModel.DoLogin(username, password);

                if (loggedInAgent == null)
                {
                    ModelState.AddModelError("", "The Username or Password is incorrect! Please try again!");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    Session.Remove("CurrentUser");
                    Session.Remove("MenuLayout");
                    Session.Add("CurrentUser", loggedInAgent);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View(user);
        }

        public ActionResult LogOff_CPR()
        {
            //Session.Remove("CurrentUser");
            //Session.Remove("MenuLayout");
            Session["CurrentUser"] = null;
            Session.Remove("MenuLayout");
            Session["MenuLayout"] = null;
            FormsAuthentication.SignOut();

            //return RedirectToAction("Index", "Home");
            return Redirect("http://training.dsd.gov.za/SDIIS_Staging/User/LogOff");
            //return Redirect("http://localhost:52033/Home/Index");
        }

        public ActionResult ForgotPassword()
        {
            var forgetPassword = new User_Forgot_Password();
            return View(forgetPassword);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ForgotPassword(User_Forgot_Password userToReset)
        {
            if (ModelState.IsValid)
            {
                var username = userToReset.Username;

                var userModel = new UserModel();
                var resetUser = userModel.GetSpecificUser(username);

                if (resetUser == null)
                {
                    ModelState.AddModelError("", "The Username specified cannot be found! Please try again!");
                }
                else
                {
                    var tempPassword = Membership.GeneratePassword(8, 2);
                    var userFullName = resetUser.First_Name + " " + resetUser.Last_Name;

                    userModel.ChangeUserPassword(resetUser.User_Id, tempPassword);

                    var message = "Dear " + userFullName;
                    message += "<br /><br />";
                    message += "A password reset process was requested for your username.<br /><br />";
                    message += "Your new temporary password is: " + tempPassword + "<br /></br />";
                    message += "Please click <a href='" + Url.Action("ResetPassword", "User", null, Request.Url.Scheme, null) + "'>here</a> to reset your password.";

                    var mailSent = Mailer.SendMail(userFullName, resetUser.Email_Address, "Email Reset Request", message);

                    if (mailSent)
                        ViewBag.Message = string.Format("Reset Instructions was sent to '{0}'. Please review the email and follow the instructions to reset your password", resetUser.Email_Address);
                    else
                        ViewBag.Message = "Reset Instructions email could not be sent due to a technical difficulty. Please try again later!";
                }
            }

            return View(userToReset);
        }

        public ActionResult ResetPassword()
        {
            var resetPassword = new User_Reset_Password();
            return View(resetPassword);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResetPassword(User_Reset_Password resetPassword)
        {
            var userModel = new UserModel();
            var user = string.IsNullOrEmpty(resetPassword.Username) ? new User() : userModel.GetSpecificUser(resetPassword.Username);

            if (!string.IsNullOrEmpty(resetPassword.Username))
            {
                if (user == null)
                {
                    ModelState.AddModelError("Username", "The Username supplied does not exist!");
                }
            }

            if (resetPassword.NewPassword != resetPassword.ConfirmNewPassword)
                ModelState.AddModelError("ConfirmNewPassword", "The New Password and Confirm Password fields does not match!");

            if (resetPassword.OldPassword != user.Password)
                ModelState.AddModelError("OldPassword", "The Old (Current) Password is not valid!");

            if (ModelState.IsValid)
            {
                if ((user != null) && (user.User_Id != 0))
                {
                    var updatedUser = userModel.ChangeUserPassword(user.User_Id, resetPassword.NewPassword);

                    if (updatedUser == null)
                    {
                        ModelState.AddModelError("", "The Username specified cannot be found! Please try again!");
                        return View(resetPassword);
                    }

                    return RedirectToAction("Login");
                }
            }

            return View(resetPassword);
        }
    }
}