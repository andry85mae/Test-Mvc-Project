using Magneti_Marelli_Test.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Magneti_Marelli_Test.Controllers
{
    public class GroupController : Controller
    {
       [HttpPost]
        [HandleError]
        public ActionResult GetGroups(string searchTerm)
        {

            try
            {
                List<Groups> groups = new List<Groups>();
                ViewBag.Groups = groups;


                return PartialView("_ModalView", groups);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HandleError]
        public ActionResult GetModal(string Id)
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

        [HttpPost]
        [HandleError]
        public string Query(string searchTerm,int userId)
        {
            try
            {
                List<Groups> groups = new List<Groups>();

                int UserId = userId;

                Groups group1 = new Groups() { Id = 1, Name = "Groups1" };
                Groups group2 = new Groups() { Id = 2, Name = "Groups2" };
                Groups group3 = new Groups() { Id = 3, Name = "Groups3" };

                groups.Add(group1);
                groups.Add(group2);
                groups.Add(group3);


                var filtergroups=groups.Where(g => g.Name.Contains(searchTerm));

                StringBuilder sb = new StringBuilder();
                sb.Append("<table id='grid' class='table table-condensed'>");


                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th> Id </th>");
                sb.Append("<th> Name </th>");
                sb.Append("<th>  </th>");
                sb.Append("<th>  </th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tbody>");

                foreach (Groups g in filtergroups) {
                    sb.Append("<tr>");

                    sb.Append("<td>" + g.Id + "</td>");
                    sb.Append("<td>" + g.Name + "</td>");
                    sb.Append("<td> <button class='btn btn-primary btn-xs' onclick='Add("+ UserId + ","+ g.Id + ")'>Add</button></td>");
                    sb.Append("<td> <button class='btn btn-primary btn-xs' onclick='Remove(" + UserId + "," + g.Id + ")'>Remove</button></td>");

                    sb.Append("</tr>");
                }

                sb.Append("</tbody>");
                sb.Append("</table>");

                return  sb.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }


}

//Extra classes to format the results the way the select2 dropdown wants them

