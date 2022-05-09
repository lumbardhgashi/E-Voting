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
    public class ListaEZezeEKadidateveController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ListaEZezeEKadidateveController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @" select * from dbo.ListaEZezeEKandidateve";
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
        public JsonResult Post(ListaEZezePerKandidat lp)
        {
            string query = @"insert into dbo.ListaEZezeEKandidateve values(@EmriKandidatit,@NumriPersonal)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmriKandidatit",lp.EmriKandidatit);
                    myCommand.Parameters.AddWithValue("@NumriPersonal", lp.NumriPersonal);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Successfully Inserted into ListaEZezeEKandidateve Table on E-Voting DataBase !");
        }

        [HttpPut]
        public JsonResult Put(ListaEZezePerKandidat lp)
        {
            string query = @" Update dbo.ListaEZezeEKandidateve 
                               Set EmriKandidatit = @EmriKandidatit,
                                   NumriPersonal = @NumriPersonal
                                   where KandidatiId = @KandidatiId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmriKandidatit", lp.EmriKandidatit);                    
                    myCommand.Parameters.AddWithValue("@NumriPersonal", lp.NumriPersonal);
                    myCommand.Parameters.AddWithValue("@KandidatiId", lp.KandidatiId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Kandidati Has been Updated Successfully on ListaEZezePerKandidat On E-Voting DataBase!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"Delete From dbo.ListaEZezeEKandidateve where KandidatiId=@Id";
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
            return new JsonResult(" Kandidati Has been Deleted Succesfully From ListaEZezeEKadidateve Table On E-Voting DataBase!");
        }
    }
}


