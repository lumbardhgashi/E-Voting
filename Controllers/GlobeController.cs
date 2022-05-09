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
    public class GlobeController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public GlobeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.Globe";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using(SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using(SqlCommand myCommand = new SqlCommand(query,myConn))
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
        public JsonResult Post(Globe globe)
        {
            string query = @"insert into dbo.Globe values(@Shteti,@Presidenti)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using(SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@Shteti", globe.Shteti);
                    myCommand.Parameters.AddWithValue("@Presidenti", globe.Presidenti);
                    myReader=myCommand.ExecuteReader();
                    myReader.Close();
                    myConn.Close();

                }

            }
            return new JsonResult("State has been inserted successfully !");
        }

        [HttpPut]
        public JsonResult Put(Globe globe)
        {
            string query = @"Update dbo.Globe Set Shteti=@Shteti, Presidenti=@Presidenti where Id=@Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using( SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query,myConn))
                {
                    myCommand.Parameters.AddWithValue("@Shteti", globe.Shteti);
                    myCommand.Parameters.AddWithValue("@Presidenti", globe.Presidenti);
                    myCommand.Parameters.AddWithValue("@Id", globe.Id);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myConn.Close();
                }

            }
            return new JsonResult("State has been updated successfully!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"Delete from dbo.Globe where Id=@Id";
            DataTable table= new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using(SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myConn))
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
