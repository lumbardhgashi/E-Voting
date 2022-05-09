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
    public class KandidatetController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private object myCommand;
        private object table;
        public KandidatetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]

        public JsonResult Get()
        {
            string query = @" 
                             select * from dbo.Kandidatet 
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
        public JsonResult Post(Kandidatet k)
        {

            string query = @"
                             insert into dbo.Kandidatet
                             (EmriKandidatit, MbiemriKandidatit, NumriRendor, NumriPersonal)
                             values (@EmriKandidatit,@MbiemriKandidatit, @NumriRendor, @NumriPersonal)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmriKandidatit", k.EmriKandidatit);
                    myCommand.Parameters.AddWithValue("@MbiemriKandidatit", k.MbiemriKandidatit);
                    myCommand.Parameters.AddWithValue("@NumriRendor", k.NumriRendor);
                    myCommand.Parameters.AddWithValue("@NumriPersonal", k.NumriPersonal);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }

        [HttpPut]
        public JsonResult Put(Kandidatet k)
        {
            string query = @"
                             update dbo.Kandidatet
                             set EmriKandidatit = @EmriKandidatit,
                             MbiemriKandidatit = @MbiemriKandidatit,
                             NumriRendor = @NumriRendor,
                             NumriPersonal = @NumriPersonal
                             where KandidatiId = @KandidatiId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@KandidatiId", k.KandidatiId);
                    myCommand.Parameters.AddWithValue("@EmriKandidatit", k.EmriKandidatit);
                    myCommand.Parameters.AddWithValue("@MbiemriKandidatit", k.MbiemriKandidatit);
                    myCommand.Parameters.AddWithValue("@NumriRendor", k.NumriRendor);
                    myCommand.Parameters.AddWithValue("@NumriPersonal", k.NumriPersonal);
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
                           delete from dbo.Kandidatet
                           where KandidatiId = @KandidatiId
                           ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@KandidatiId", id);

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
