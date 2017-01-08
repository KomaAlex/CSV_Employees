using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CSV_Employees.Models;


namespace CSV_Employees.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase FileUpload)
        {
            //Error handling
            if (!ModelState.IsValid)
            {
                return View();    
            }
            else
            {

                //check if content is empty or not
                if (FileUpload != null && FileUpload.ContentLength > 0)
                {  //validating the file extension - allow only ".csv" file extension
                    string AllowedFileExtensions = ".csv";
                    if (AllowedFileExtensions.Contains(
                        FileUpload.FileName.Substring(FileUpload.FileName.LastIndexOf('.'))))
                    //if (FileUpload.FileName.EndsWith(".csv"))
                    {
                        //file path work
                        string fileName = Path.GetFileName(FileUpload.FileName); // assigning file name to the variable
                        string path = Path.Combine(Server.MapPath("~/App_Data/uploads"), Path.GetFileName(FileUpload.FileName));

                        //try and upload
                        try
                        {
                            FileUpload.SaveAs(path); // trying to save file on server using predefined path
                            ProcessCSV(path); // processing file at the server path 
                            string[] lines = System.IO.File.ReadAllLines(path);
                            int cnt = lines.Count();
                            ModelState.Clear();
                            ViewBag.Message = "File uploaded successfully";
                            ViewBag.Count = string.Format("({0}) records has been loaded to the table.", cnt - 1);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = ex.Message;
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("File",
                        "Please input file of type: " + string.Join(", ", AllowedFileExtensions));
                        //ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }

 
            }
            return View(FileUpload);
        }

        private static void ProcessCSV(string fileName)
        {
            //Set up variables for reading input file
            string line = string.Empty;
            string[] strArray;
            //regular expression found at internet that splits on comma, but not in a sentence
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            //Set the filename in to the stream
            StreamReader sr = new StreamReader(fileName);

            //reading lines to the stream
            line = sr.ReadLine();
            //spliting lines to array
            strArray = r.Split(line);
            
            //call to the DB Context
            var context = new DBEntities();
            //read each line in the .csv file until it's empty
            while ((line = sr.ReadLine()) != null)
            {
                //Inserting data to the tables
                var array = r.Split(line);
                var data = new Employees
                {
                    Payroll_number = array[0],
                    Forenames = array[1],
                    Surname = array[2],
                    Telephone = array[4],
                    Mobile = array[5],
                    Address = array[6],
                    Address_2 = array[7],
                    Postcode = array[8],
                    EMail_Home = array[9]
                    
                };
                //Parsing data, because without parsing it will return false -> It will lead to NULL value record in Database
                DateTime dateBOD = DateTime.UtcNow;
                if (DateTime.TryParseExact(array[3], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateBOD)) data.Date_of_Birth = dateBOD;
                DateTime dateStart = DateTime.UtcNow;
                if (DateTime.TryParseExact(array[10], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateStart))
                    data.Start_Date = dateStart;
                
                context.Employees.Add(data);
            }
            //Save data
            context.SaveChanges();
            //Tidy Streamreader up
            sr.Dispose();
            


        }


    }
}