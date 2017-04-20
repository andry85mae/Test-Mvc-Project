using Magneti_Marelli_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                return View("List", Utility.Utility.TestUserFactory());
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
        public ActionResult Edit(int id)
        {
            try
            {
                User u = Utility.Utility.TestUserFactory().FirstOrDefault(x => x.UserId == id);
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
                User u = Utility.Utility.TestUserFactory().FirstOrDefault(x => x.UserId == id);

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
                User u = Utility.Utility.TestUserFactory().FirstOrDefault(x => x.UserId == id);

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
                User u = Utility.Utility.TestUserFactory().FirstOrDefault(x => x.UserId == userId);

                Groups gr = u.UserGroups.FirstOrDefault(g => g.Id == groupId);
                u.UserGroups.Remove(gr);


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

            var users = Utility.Utility.users.Where(u => u.FirstName.ToLower().Contains(searchTerm) || u.LastName.ToLower().Contains(searchTerm)).Select(x => new { id = x.UserId, text = x.FirstName + " " + x.LastName });



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

                User u = Utility.Utility.TestUserFactory().FirstOrDefault(x => x.UserId == userId);
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
            System.Security.Principal.WindowsIdentity wi = System.Security.Principal.WindowsIdentity.GetCurrent();

            User u = new User();

            u.LastLogon = DateTime.Now;
            u.LastPasswordChange = DateTime.Now;
            u.UserModification = string.Empty;
            u.UserId = 1;
            u.FirstName = "Andrea";
            u.LastName = "Maestroni";
            u.LoginName = wi.Name;
            u.DisplayName = wi.Name;
            u.Description = string.Empty;
            u.Mail = string.Empty;
            u.Phone = 0;
            u.City = string.Empty;
            u.Country = string.Empty;
            u.ExpirationDate = DateTime.Now.AddYears(2);
            u.ManagerId = 3;
            u.ManagerLabel = "Sangalli Alberto";
            u.BL = string.Empty;
            u.Site = string.Empty;
            u.IsEnable = true;


            Groups group1 = new Groups() { Id = 1, Name = "Groups1" };
            Groups group2 = new Groups() { Id = 2, Name = "Groups2" };
            Groups group3 = new Groups() { Id = 3, Name = "Groups3" };
            Groups group4 = new Groups() { Id = 4, Name = "Groups4" };
            Groups group5 = new Groups() { Id = 5, Name = "Groups5" };

            u.UserGroups.Add(group1);
            u.UserGroups.Add(group2);
            u.UserGroups.Add(group3);
            u.UserGroups.Add(group4);
            u.UserGroups.Add(group5);



            return PartialView("UserDetails",u);

        }

    }
}