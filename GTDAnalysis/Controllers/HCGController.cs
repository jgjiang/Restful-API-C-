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


namespace GTDAnalysis.Controllers
{
	public class HCGController : ApiController
	{
        public IHttpActionResult GetAvgHCG(int id)
		{

            string avg = "";
			if (QueryAvg(id) != null)
			{
				return Ok(QueryAvg(id)+"query");
			}

			if(QueryAvg(id) == null){
	            avg = CalAvg(id);
	            double avg_double = double.Parse(avg);
	            InsertResult(avg_double, id);
            }
           
            return Ok(avg+"calculate");

		}

        public void InsertResult (double avg, int id){
            string avg_str = avg.ToString();
            string insertSQL = "INSERT INTO prediction(prediction_a, pid) VALUES ('"+avg_str+"','"+id+"')";
            var cmd = GetMysqlCommand(insertSQL);
            cmd.ExecuteNonQuery();

		}

        public string QueryAvg(int id){
            if (CheckEmptyTable("prediction") == false){
                string prediction_a = "";
                string querySQL = "SELECT prediction_a FROM prediction Where pid = " + id;
				var cmd = GetMysqlCommand(querySQL);
				//var mysqlReader = cmd.ExecuteReader();
                object result = cmd.ExecuteScalar();

                if (result != null){
                    prediction_a = Convert.ToString(result);
                }

                return prediction_a;
            }
           
            return null;

        }

        public string CalAvg(int id){
			
            string query = "SELECT * FROM hcg Where pid = " + id;
			var cmd = GetMysqlCommand(query);
			var reader = cmd.ExecuteReader();

			List<HCG> hcgs = new List<HCG>();
			int sum = 0;
			double avg = 0.0;
   
           
			while (reader.Read())
			{
				int hcg_id = int.Parse(reader["hcg_id"].ToString());
				string mrn = reader["mrn"].ToString();
				int week_num = int.Parse(reader["week_num"].ToString());
				int hcg_value = int.Parse(reader["hcg_value"].ToString());
				int pid = int.Parse(reader["pid"].ToString());

				sum += hcg_value;
				hcgs.Add(new HCG(hcg_id, mrn, week_num, hcg_value, pid));

			}


			if (hcgs.Count == 0)
			{
                return null;
			}
			else
			{
                avg = (double)sum / hcgs.Count;
              
			}

            return avg.ToString();
        }


        public MySqlCommand GetMysqlCommand(string query){
			MySqlConnection conn = new MySqlConnection("host=127.0.0.1;user=root;password=root;database=clinic_cms;");
			conn.Open();
            var cmd = new MySqlCommand(query, conn);
            return cmd;
        }

        public bool CheckEmptyTable(string table)
		{
			MySqlConnection conn = new MySqlConnection("host=127.0.0.1;user=root;password=root;database=clinic_cms;");
			conn.Open();

            string query = "SELECT COUNT(*) from "+table;
			var cmd = new MySqlCommand(query, conn);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
		
            if (result == 0){
                return true; // is empty
            } else {
                return false;//is not empty 
			}

		}

	}
}
