using Magneti_Marelli_Test.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Magneti_Marelli_Test.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// used in List view for search user
        /// </summary>
        /// <param name="qry"> query for filter user</param>
        /// <returns>List of user</returns>
        [HttpPost]
        [HandleError]
        public ActionResult Query(string qry)
        {
            try
            {
                List<User> users = Utility.DirectoryEntryUtility.GetUsersByQuery(this.HttpContext, qry);
                return View("List", users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Used for navigate in List view with empty result
        /// </summary>
        /// <returns></returns>
        [HandleError]
        public ActionResult Index()
        {
            try
            {

                HttpCookie ck = Utility.Utility.CreateCurrentUserCookie();
                HttpContext.Response.Cookies.Add(ck);

                List<User> users = new List<User>();
                return View("List", users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Used for navigate in Edit view with user model
        /// </summary>
        /// <param name="id">id of user to load</param>
        /// <returns></returns>
        [HandleError]
        public ActionResult Edit(string loginName)
        {
            try
            {
                User u = Utility.DirectoryEntryUtility.GetUserByLoginName(loginName);
                return View("Details", u);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// used for delete user in details view 
        /// </summary>
        /// <param name="id">id of the user to delete</param>
        /// <returns>view with list</returns>
        [HandleError]
        public ActionResult Delete(int id)
        {
            try
            {
                //User u = Utility.Utility.TestUserFactory().FirstOrDefault(x => x.UserId == id);

                User u = new Models.User();
                return View("List", new List<User>());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// used for save user in details view for deleting user
        /// </summary>
        /// <param name="id">id of the user to delete</param>
        /// <returns>view with list</returns>
        [HandleError]
        public ActionResult Save(int id)
        {
            try
            {
                //User u = Utility.Utility.TestUserFactory().FirstOrDefault(x => x.UserId == id);

                User u = new Models.User();
                return View("Details", u);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// open group modal ad pass userId in details view
        /// </summary>
        /// <param name="Id">user id to add or remove groups</param>
        /// <returns>modal</returns>
        [HandleError]
        public ActionResult ModalAction(int Id)
        {
            try
            {
                ViewBag.Id = Id;
                return PartialView("_ModalView");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// reomve groups for user
        /// </summary>
        /// <param name="groupId">id of group to remove</param>
        /// <param name="userId">user id where remove the group</param>
        /// <returns>remove groups</returns>
        [HandleError]
        public ActionResult RemoveGroups(int groupId, int userId)
        {
            try
            {
                User u = new Models.User();

                return View("Details", u);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// create a new empty user to modify for create a user, in list view
        /// </summary>
        /// <returns>a new user in detail view</returns>
        [HandleError]
        public ActionResult AddUser()
        {
            try
            {
                User user_new = new User();

                return View("NewUser", user_new);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// create a user
        /// </summary>
        /// <param name="newUser">user to add</param>
        /// <returns>list view with empty</returns>
        [HttpPost]
        [HandleError]
        public ActionResult CreateUser(User newUser)
        {
            try
            {

                Utility.Utility.users.Add(newUser);

                return View("List", new List<User>());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// used in select2 to select,retrive,search user
        /// </summary>
        /// <param name="searchTerm">query for filtering</param>
        /// <returns></returns>
        [HandleError]
        public JsonResult GetManagerList(string searchTerm)

        {
            if (string.IsNullOrEmpty(searchTerm))
                return Json(null, JsonRequestBehavior.AllowGet);

            var users = Utility.Utility.users;



            return Json(users, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// reset password in detail view
        /// </summary>
        /// <param name="userId">userid to reset</param>
        /// <returns></returns>
        [HandleError]
        public ActionResult ResetPassword(int userId)
        {

            try
            {

                User u = new Models.User();
                return View("Details", u);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// get partial view for rendering user account dettails
        /// </summary>
        /// <returns>partial view with current login user details</returns>
        public PartialViewResult GetCurrentUserDetails()
        {
            
            SiteUser user = Utility.Utility.GetCurrentUser(this.HttpContext);

            return PartialView("UserDetails", user);

        }


        [HandleError]
        public ActionResult GetModal(string Id)
        {

            try
            {
                ViewBag.Id = Id;

                return PartialView("_ModalUserView");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [HandleError]
        public string QueryManager(string searchTerm)
        {
            try
            {
                

                var users = Utility.Utility.users;

                StringBuilder sb = new StringBuilder();
                sb.Append("<table id='gridUser' class='table table-condensed'>");


                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th> LoginName </th>");
                sb.Append("<th> DisplayName </th>");
                sb.Append("<th>  </th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tbody>");

                foreach (User g in users)
                {

                    string displayName = g.DisplayName != null ? g.DisplayName.Replace(@"\", @"\\") :g.LoginName.Replace(@"\", @"\\"); 
                    string loginName = g.LoginName.Replace(@"\", @"\\");
                    sb.Append("<tr>");

                    sb.Append("<td>" + g.LoginName + "</td>");
                    sb.Append("<td>" + g.DisplayName + "</td>");
                    string var1 = "AddOrModifyManager(\"" + loginName + "\",\"" + displayName + "\")";
                    sb.Append(@"<td> <button class='btn btn-primary btn-xs' onclick='"+var1+"' >Add/Modify</button></td>");

                    sb.Append("</tr>");
                }

                sb.Append("</tbody>");
                sb.Append("</table>");

                return sb.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}