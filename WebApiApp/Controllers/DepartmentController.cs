using System.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebApiApp.Models;

namespace WebApiApp.Controllers;

    [Route("api/[controller]")]
    [ApiController]

    public class DepartmentController: ControllerBase{

        private readonly string? _connectionString;
        
        public DepartmentController(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("UsersCon");
        }

        [HttpGet]
        public IActionResult Get(){

            try{

                string query = @"
                    select DepartmentId as ""Department"",
                            DepartmentName as ""DepartmentName""
                        from Department
                ";

                DataTable table = new DataTable();

                NpgsqlDataReader myReader;


                using( var conn = new NpgsqlConnection(_connectionString)){
                    conn.Open();

                    using(NpgsqlCommand command = new NpgsqlCommand(query,conn)){
                        
                        myReader = command.ExecuteReader();
                        table.Load(myReader);
                        conn.Close();
                        
                    }
                }

                return Ok(table); 
            }
            catch(Exception ex){
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpPost]
        public IActionResult Post(Department dep)
        {
            try{

                string query = @"
                    insert into Department(DepartmentName)
                    values (@DepartmentName)
                ";

                DataTable table = new DataTable();

                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(_connectionString))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName  ?? (object)DBNull.Value);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();

                    }
                }

                return Ok("Added Successfully");
            }
            catch(Exception ex){
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Department dep)
        {
            try{
                string query = @"
                    update Department
                    set DepartmentName = @DepartmentName
                    where DepartmentId=@DepartmentId 
                ";

                DataTable table = new DataTable();
          
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(_connectionString))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                        myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName ?? (object)DBNull.Value);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();

                    }
                }

                return Ok("Updated Successfully");
            }
            catch(Exception ex){
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try{

                string query = @"
                    delete from Department
                    where DepartmentId=@DepartmentId 
                ";

                DataTable table = new DataTable();
   
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(_connectionString))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@DepartmentId", id);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();

                    }
                }

                return Ok("Deleted Successfully");
            }
            catch(Exception ex){
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }


