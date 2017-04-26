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
                List<Group> groups = new List<Group>();
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
        public string Query(string searchTerm,string userLoginName)
        {
            try
            {
                List<Group> groups = new List<Group>();
                

                Group group1 = new Group() { DistinguishedName = "1", Name = "Groups1" };
                Group group2 = new Group() { DistinguishedName = "2", Name = "Groups2" };
                Group group3 = new Group() { DistinguishedName = "3", Name = "Groups3" };

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
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tbody>");

                foreach (Group g in filtergroups) {
                    sb.Append("<tr>");

                    sb.Append("<td>" + g.DistinguishedName + "</td>");
                    sb.Append("<td>" + g.Name + "</td>");
                    string var1 = "Add(\"" + userLoginName.Replace(@"\", @"\\") + "\",\"" + g.DistinguishedName + "\")";
                    sb.Append("<td> <button class='btn btn-primary btn-xs'  onclick='" + var1 + "'>Add</button></td>");

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

