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
    public class PopullsiaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PopullsiaController (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @" select * from dbo.Popullsia";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query ,myCon))
                {
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Popullsia pop)
        {
            string query = @"insert into dbo.Popullsia values(@Emri,@Mbiemri,@NumriPersonal,@Email)";
            DataTable table= new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Emri", pop.Emri);
                    myCommand.Parameters.AddWithValue("@Mbiemri", pop.Mbiemri);
                    myCommand.Parameters.AddWithValue("@NumriPersonal", pop.NumriPersonal);
                    myCommand.Parameters.AddWithValue("@Email", pop.Email);
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Successfully Inserted into Popullsia Table on E-Voting DataBase !");
        }

        [HttpPut]
        public JsonResult Put(Popullsia pop)
        {
            string query = @"Update dbo.Popullsia
                            Set Emri =@Emri,
                            Mbiemri=@Mbiemri,
                            NumriPersonal=@NumriPersonal,
                            Email = @Email  Where Id=@Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Emri", pop.Emri);
                    myCommand.Parameters.AddWithValue("@Mbiemri", pop.Mbiemri);
                    myCommand.Parameters.AddWithValue("@NumriPersonal", pop.NumriPersonal);
                    myCommand.Parameters.AddWithValue("@Email", pop.Email);
                    myCommand.Parameters.AddWithValue("@Id", pop.Id);
                    myReader= myCommand.ExecuteReader();
                    table.Load(myReader) ;
                    myReader.Close();
                    myCon.Close(); 
                }
            }
            return new JsonResult("User Has been Updated Successfully !");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete( int id)
        {
            string query = @"Delete From dbo.Popullsia where Id=@Id";
            DataTable table =new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader= myCommand.ExecuteReader();
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(" User Has been Deleted Succesfully !");
        }
    }
}
