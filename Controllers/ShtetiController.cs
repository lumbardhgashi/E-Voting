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
    public class ShtetiController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ShtetiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"
                                        Select * from dbo.Shteti
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

        public JsonResult Post(Shteti Sh)
        {
            string query = @"
                              insert into dbo.Shteti values (@ShtetiEmri, @Kryeministri, @NrPopullsis, @NrQyteteve, @OrganizataPerZgjedhje)
                                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {


                    myCommand.Parameters.AddWithValue("@ShtetiEmri", Sh.ShtetiEmri);
                    myCommand.Parameters.AddWithValue("@Kryeministri", Sh.Kryeministri);
                    myCommand.Parameters.AddWithValue("@NrPopullsis", Sh.NrPopullsis);
                    myCommand.Parameters.AddWithValue("@NrQyteteve", Sh.NrQyteteve);
                    myCommand.Parameters.AddWithValue("@OrganizataPerZgjedhje", Sh.OrganizataPerZgjedhje);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Insert Successfully");
        }


        [HttpPut]

        public JsonResult Put(Shteti Sh)
        {
            string query = @"
                              update dbo.Shteti 
                                set ShtetiEmri = @ShtetiEmri,
                                    Kryeministri = @Kryeministri,    
                                    NrPopullsis = @NrPopullsis,
                                    NrQyteteve = @NrQyteteve,
                                    OrganizataPerZgjedhje = @OrganizataPerZgjedhje
                                        where ShtetiId = @ShtetiId
                                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {


                    myCommand.Parameters.AddWithValue("@ShtetiEmri", Sh.ShtetiEmri);
                    myCommand.Parameters.AddWithValue("@Kryeministri", Sh.Kryeministri);
                    myCommand.Parameters.AddWithValue("@NrPopullsis", Sh.NrPopullsis);
                    myCommand.Parameters.AddWithValue("@NrQyteteve", Sh.NrQyteteve);
                    myCommand.Parameters.AddWithValue("@OrganizataPerZgjedhje", Sh.OrganizataPerZgjedhje);
                    myCommand.Parameters.AddWithValue("@ShtetiId", Sh.ShtetiId);
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
            string query = @"Delete from dbo.Shteti where ShtetiId=@Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-VotingAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader=myCommand.ExecuteReader();
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Delete Successfully !");

           
        }
    }
}