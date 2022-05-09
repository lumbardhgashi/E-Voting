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
    public class FletVotimiJoZyrtarController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private object myCommand;
        private object table;

        public FletVotimiJoZyrtarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @" 
                             select * from dbo.FletVotimiJoZyrtar 
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
        public JsonResult Post(FletVotimiJoZyrtar fvjozyrtar)
        {
            string query = @"
                             insert into dbo.FletVotimiJoZyrtar
                             (KohaStartuese, KohaPerfunduese)
                             values (@KohaStartuese, @KohaPerfunduese)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@KohaStartuese", fvjozyrtar.KohaStartuese);
                    myCommand.Parameters.AddWithValue("@KohaPerfunduese", fvjozyrtar.KohaPerfunduese);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }

        [HttpPut]
        public JsonResult Put(FletVotimiJoZyrtar fvjozyrtar)
        {
            string query = @"
                             update dbo.FletVotimiJoZyrtar
                             set KohaStartuese= @KohaStartuese,
                                 KohaPerfunduese = @KohaPerfunduese
                             where Id = @Id
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", fvjozyrtar.Id);
                    myCommand.Parameters.AddWithValue("@KohaStartuese", fvjozyrtar.KohaStartuese);
                    myCommand.Parameters.AddWithValue("@KohaPerfunduese", fvjozyrtar.KohaPerfunduese);
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
                           delete from dbo.FletVotimiJoZyrtar
                           where Id = @Id
                           ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);

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
