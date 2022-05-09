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
    public class GjykataController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private object myCommand;
        private object table;

        public GjykataController(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @" 
                             select * from dbo.Gjykata 
                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
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
        public JsonResult Post(Gjykata gj)
        {
            string query = @"
                             insert into dbo.Gjykata
                             values (@GjykataQyteti)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand= new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@GjykataQyteti", gj.GjykataQyteti);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }

        [HttpPut]
        public JsonResult Put(Gjykata gj)
        {
            string query = @"
                             update dbo.Gjykata
                             set GjykataQyteti= @GjykataQyteti
                             where GjykataId = @GjykataId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@GjykataId", gj.GjykataId);
                    myCommand.Parameters.AddWithValue("@GjykataQyteti", gj.GjykataQyteti);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");

        }

        [HttpDelete("{id}")]

        public JsonResult Delete (int id)
        {
            string query = @"
                           delete from dbo.Gjykata
                           where GjykataId = @GjykataId
                           ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@GjykataId", id);

                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Successfully");

        }


    }


    
}
