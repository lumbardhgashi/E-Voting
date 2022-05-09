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
    public class OrganizataPerZgjedhjetController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OrganizataPerZgjedhjetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @" select * from dbo.OrganizatatPerZgjedhje";
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
        public JsonResult Post(OrganizatatPerZgjedhje o)
        {
            string query = @"insert into dbo.OrganizatatPerZgjedhje values(@Emri,@KryetariOrganizates,@NumriPersonalIKryetarit)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Emri", o.Emri);
                    myCommand.Parameters.AddWithValue("@KryetariOrganizates", o.KryetariOrganizates);
                    myCommand.Parameters.AddWithValue("@NumriPersonalIKryetarit", o.NumriPersonalKryetarit);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Successfully Inserted into OrganizatatPerZgjedhje Table on E-Voting DataBase !");
        }

        [HttpPut]
        public JsonResult Put(OrganizatatPerZgjedhje oz)
        {
            string query = @" Update dbo.OrganizatatPerZgjedhje 
                               Set Emri = @Emri,
                                   KryetariOrganizates = @KryetariOrganizates,
                                   NumriPersonalKryetarit = @NumriPersonalKryetarit
                                   where Id = @Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Emri", oz.Emri);
                    myCommand.Parameters.AddWithValue("@KryetariOrganizates", oz.KryetariOrganizates);
                    myCommand.Parameters.AddWithValue("@NumriPersonalKryetarit", oz.NumriPersonalKryetarit);
                    myCommand.Parameters.AddWithValue("@Id", oz.Id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("OrganizatatPerZgjedhje Has been Updated Successfully on OrganizatatPerZgjedhje On E-Voting DataBase!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"Delete From dbo.OrganizatatPerZgjedhje where Id=@Id";
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
            return new JsonResult(" OrganizatatPerZgjedhje Has been Deleted Succesfully From OrganizatatPerZgjedhje Table On E-Voting DataBase!");
        }
    }
}


