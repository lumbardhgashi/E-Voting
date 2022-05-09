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
    public class QytetetController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private object myCommand;
        private object table;
        public QytetetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]

        public JsonResult Get()
        {
            string query = @" 
                             select * from dbo.Qytetet
                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post(Qytetet q)
        {

            string query = @"
                             insert into dbo.Qytetet
                             (EmriQytetit,PopullsiaQytetit,KryetariQytetit)
                             values (@EmriQytetit,@PopullsiaQytetit, @KryetariQytetit)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmriQytetit", q.EmriQytetit);
                    myCommand.Parameters.AddWithValue("@PopullsiaQytetit", q.PopullsiaQytetit);
                    myCommand.Parameters.AddWithValue("@KryetariQytetit", q.KryetariQytetit);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }

        [HttpPut]
        public JsonResult Put(Qytetet q)
        {
            string query = @"
                             update dbo.Qytetet
                             set EmriQytetit = @EmriQytetit,
                             PopullsiaQytetit = @PopullsiaQytetit,
                             KryetariQytetit = @KryetariQytetit
                             where QytetiId = @QytetiId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@QytetiId", q.QytetiId);
                    myCommand.Parameters.AddWithValue("@EmriQytetit", q.EmriQytetit);
                    myCommand.Parameters.AddWithValue("@PopullsiaQytetit", q.PopullsiaQytetit);
                    myCommand.Parameters.AddWithValue("@KryetariQytetit", q.KryetariQytetit);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");

        }
        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Qytetet
                           where QytetiId = @QytetiId
                           ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@QytetiId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Successfully");

        }
    }
}
