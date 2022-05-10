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
    public class NumriTelefonitController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public NumriTelefonitController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"
                              select * from dbo.NumriTelefonit;
                                ";

            DataTable table = new DataTable();
            string dboDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(dboDataSource))
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

        public JsonResult Post(NumriTelefonit nrT)
        {
            string query = @" insert into dbo.NumriTelefonit values(@NrTelefonit)         
                                  ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NrTelefonit", nrT.NrTelefonit);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("inserted successfully!");
        }


        [HttpPut]

        public JsonResult Put(NumriTelefonit nrT)
        {
            string query = @" update dbo.NumriTelefonit set NrTelefonit = @NrTelefonit where NrTelId = @NrTelId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {

                    myCommand.Parameters.AddWithValue("@NrTelId", nrT.NrTelId);
                    myCommand.Parameters.AddWithValue("@NrTelefonit", nrT.NrTelefonit);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated successfully!");
        }

        [HttpDelete("{Id}")]

        public JsonResult Delete(int id)
        {
            string query = @" delete dbo.NumriTelefonit where NrTelId = @NrTelId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NrTelId", id);

                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Delete Successfully!");
        }

    }
}
