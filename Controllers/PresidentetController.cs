using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using E_Voting.Models;

namespace E_Voting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresidentetController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public PresidentetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.Presidentet";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Presidentet p)
        {
            string query = @"insert into dbo.Presidentet values(@Emri,@Mbiemri,@NumriPersonal,@NumriRendorPresidencial)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@Emri", p.Emri);
                    myCommand.Parameters.AddWithValue("@Mbiemri", p.Mbiemri);
                    myCommand.Parameters.AddWithValue("@NumriPersonal", p.NumriPersonal);
                    myCommand.Parameters.AddWithValue("@NumriRendorPresidencial", p.NumriRendorPresidencial);
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myConn.Close();

                }

            }
            return new JsonResult("President has been inserted successfully !");
        }

        [HttpPut]
        public JsonResult Put(Presidentet p)
        {
            string query = @"Update dbo.Presidentet Set Emri=@Emri, Mbiemri=@Mbiemri, NumriPersonal=@NumriPersonal, NumriRendorPresidencial=@NumriRendorPresidencial where PresidentiId=@Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@Emri", p.Emri);
                    myCommand.Parameters.AddWithValue("@Mbiemri", p.Mbiemri);
                    myCommand.Parameters.AddWithValue("@NumriPersonal", p.NumriPersonal);
                    myCommand.Parameters.AddWithValue("@NumriRendorPresidencial", p.NumriRendorPresidencial);
                    myCommand.Parameters.AddWithValue("@Id", p.PresidentiId);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myConn.Close();
                }

            }
            return new JsonResult("President has been updated successfully!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"Delete from dbo.Presidentet where PresidentiId=@Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myConn.Close();

                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}

