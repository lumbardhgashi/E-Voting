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
    public class FletVotimiZyrtarController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public FletVotimiZyrtarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.FletVotimiZyrtar";
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
        public JsonResult Post(FletVotimiZyrtar f)
        {
            string query = @"insert into dbo.FletVotimiZyrtar values(@TipiZgjedhjeve,@KohaStartuese,@KohaPerfunduese)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@TipiZgjedhjeve", f.TipiZgjedhjeve);
                    myCommand.Parameters.AddWithValue("@KohaStartuese",f.KohaStartuese);
                    myCommand.Parameters.AddWithValue("@KohaPerfunduese", f.KohaPerfunduese);
                    
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myConn.Close();

                }

            }
            return new JsonResult("FletVotimiZyrtar has been inserted successfully !");
        }

        [HttpPut]
        public JsonResult Put(FletVotimiZyrtar f)
        {
            string query = @"Update dbo.FletVotimiZyrtar Set TipiZgjedhjeve=@TipiZgjedhjeve,KohaStartuese=@KohaStartuese, KohaPerfunduese=@KohaPerfunduese where Id=@Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@TipiZgjedhjeve", f.TipiZgjedhjeve);
                    myCommand.Parameters.AddWithValue("@KohaStartuese", f.KohaStartuese);
                    myCommand.Parameters.AddWithValue("@KohaPerfunduese", f.KohaPerfunduese);
                    
                    myCommand.Parameters.AddWithValue("@Id",f.Id );
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myConn.Close();
                }

            }
            return new JsonResult("FletVotimiZyrtar has been updated successfully!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"Delete from dbo.FletVotimiZyrtar where Id=@Id";
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


