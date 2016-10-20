using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//绑定命名空间
using Nancy.ModelBinding;
namespace WebApplication1.Modules.User
{
    /// </summary>
    public class NewUser
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
    public class Home : NancyModule
    {
        public static  List<Student> lstStudent = new List<Student>();
        public Home()
        { 
            Get["/"] = p1 =>
            {
              
                return View["/Index", lstStudent];
            };

            Post["/json/get"] = p123 =>
            {

                var form = Request.Form["selRollBack"];
                string str = form;


                return Response.AsJson("d");
            };
            Post["/register"] = bing1 =>
            {
                NewUser registerAttempt = this.Bind<NewUser>(); //model binding!

                object obj = registerAttempt;


               return View["/Index", lstStudent];
            };

            Get["/{ID}"] = p =>
            {
                var ID = p.ID;
                var student = lstStudent.Find(s => s.ID == ID);
                if (student == null) 
                {
                    return View["/Index", lstStudent];
                }
                lstStudent.Remove(student);
                return View["/Index", lstStudent];
            };

            Get["/Home/modify/{ID}"] = get =>
            {
                string strID = get.ID;
                var ID=0;
                if(!int.TryParse(strID, out ID))
                {
                    return View["/Index", lstStudent];
                }

                var student = lstStudent.Find(s=>s.ID==ID);
                if (student == null)
                {
                    return View["/Index", lstStudent];
                }
                return View["/Home", student];
            };

            Post["/Home/Add"] = add =>
                {
                    var rd = new Random();      
                    var form = Request.Form;
                    var Name = form["Name"];
                    var student = new Student();
                    student.ID = rd.Next(10000);
                    student.Name = Name;
                    lstStudent.Add(student);
                    return View["/Index", lstStudent];
                };


            Post["/Home/Search"] = search =>
            {
                var form = Request.Form;
                var Name = form["Search"];
                if (string.IsNullOrEmpty(Name))
                {
                    return View["/Index", lstStudent];
                }
                var lst1Student1 = lstStudent.Where(s => s.Name == Name).ToList();
                if (lst1Student1== null||lst1Student1.Count==0)
                {
                    return View["/Index", new List<Student>()];
                }

                return View["/Index", lst1Student1];
            };

            Post["/Home/modify"] = modify =>
            { 
                var form = Request.Form;
                var ID = form["hidden"];
                var Name = form["Name"];
                var student = lstStudent.Find(s => s.ID == ID);
                if (student== null)
                {
                    return View["/Index", lstStudent];
                }

                student.Name = Name;
                return View["/Index", lstStudent];
            };

           

        }
    }
}