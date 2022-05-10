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
    public class PartiaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PartiaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"
                                        Select * from dbo.Partia
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

        public JsonResult Post(Partia pa)
        {
            string query = @"
                              insert into dbo.Partia values (@EmriPartis, @KryetariPartis, @NumriAntareve, @NumriRendorIpartis)
                                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {


                    myCommand.Parameters.AddWithValue("@EmriPartis", pa.EmriPartis);
                    myCommand.Parameters.AddWithValue("@KryetariPartis", pa.KryetariPartis);
                    myCommand.Parameters.AddWithValue("@NumriAntareve", pa.NumriAntareve);
                    myCommand.Parameters.AddWithValue("@NumriRendorIpartis", pa.NumriRendorIpartis);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Insert Successfully");
        }


        [HttpPut]

        public JsonResult Put(Partia pa)
        {
            string query = @"
                              update dbo.Partia 
                                set EmriPartis = @EmriPartis,
                                    KryetariPartis = @KryetariPartis,    
                                    NumriAntareve = @NumriAntareve,
                                    NumriRendorIpartis = @NumriRendorIpartis
                                        where PartiaId = @PartiaId
                                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {


                    myCommand.Parameters.AddWithValue("@EmriPartis", pa.EmriPartis);
                    myCommand.Parameters.AddWithValue("@KryetariPartis", pa.KryetariPartis);
                    myCommand.Parameters.AddWithValue("@NumriAntareve", pa.NumriAntareve);
                    myCommand.Parameters.AddWithValue("@NumriRendorIpartis", pa.NumriRendorIpartis);
                    myCommand.Parameters.AddWithValue("@PartiaId", pa.PartiaId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Update Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"Delete from dbo.Partia where PartiaId=@Id";
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
            return new JsonResult("Delete Successfully !");


        }
    }
}

