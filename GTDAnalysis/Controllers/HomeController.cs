using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Http;
using System.Net;
using GTDAnalysis.Models;
using MySql.Data.MySqlClient;
using System.Data;


namespace GTDAnalysis.Controllers
{
    public class HomeController : ApiController
    {
        public void GetAllHCG(int id){
            string query = "SELECT * FROM hcg Where pid = "+id;
            MySqlConnection conn = new MySqlConnection("host=localhost;user=root;password=root;database=clinic_web;");
            conn.Open();

            var cmd = new MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read()){
                Console.Write(reader.GetEnumerator()); 
            }

             

        }

    }
}
