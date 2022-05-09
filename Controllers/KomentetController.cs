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
    public class KomentetController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public KomentetController (IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select * From dbo.Komentet";
            DataTable table=new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand= new SqlCommand(query, myCon))
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
        public JsonResult Post(Komentet k)
        {
            string query = @"insert into dbo.Komentet values(@NumriPersonal,@Email,@Emri,@Mbiemri,@Komenti)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NumriPersonal", k.NumriPersonal);
                    myCommand.Parameters.AddWithValue("@Email", k.Email);
                    myCommand.Parameters.AddWithValue("@Emri", k.Emri);
                    myCommand.Parameters.AddWithValue("@Mbiemri", k.Mbiemri);
                    myCommand.Parameters.AddWithValue("@Komenti", k.Komenti);
                    myReader=myCommand.ExecuteReader();
                    myReader.Close();
                    myCon.Close();
                }

            }
            return new JsonResult("Komenti U dergua me sukses!");
        }

        [HttpPut]
        public JsonResult Put(Komentet k)
        {
            string query = @"Update  dbo.Komentet Set NumriPersonal=@NumriPersonal, Email=@Email, Emri=@Emri, Mbiemri=@Mbiemri, Komenti=@Komenti where Id=@Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NumriPersonal", k.NumriPersonal);
                    myCommand.Parameters.AddWithValue("@Email", k.Email);
                    myCommand.Parameters.AddWithValue("@Emri", k.Emri);
                    myCommand.Parameters.AddWithValue("@Mbiemri", k.Mbiemri);
                    myCommand.Parameters.AddWithValue("@Komenti", k.Komenti);
                    myCommand.Parameters.AddWithValue("@Id", k.Id);
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myCon.Close();
                }

            }
            return new JsonResult("Komenti U Perditesua me sukses!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"Delete from  dbo.Komentet where Id=@Id";
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
                    myReader.Close();
                    myCon.Close();
                }

            }
            return new JsonResult("Komenti U Fshi me sukses!");
        }
    }

}
