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

        [HandleError]
        public JsonResult GetManagerList(string searchTerm)

        {
            if (string.IsNullOrEmpty(searchTerm))
                return Json(null, JsonRequestBehavior.AllowGet);

            var users = Utility.Utility.users.Where(u => u.FirstName.ToLower().Contains(searchTerm) || u.LastName.ToLower().Contains(searchTerm)).Select(x => new { id = x.UserId, text = x.FirstName + " " + x.LastName });



            return Json(users, JsonRequestBehavior.AllowGet);

        }

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

    }
}