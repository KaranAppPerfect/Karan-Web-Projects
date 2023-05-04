using System.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using StudentCrudApp.Models;


namespace StudentCrudApp.Controllers{

    public class StudentController: Controller{

        private readonly string? _connectionString;

        public StudentController(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("StudentAppCon");
        }


        [HttpGet]
        public IActionResult GetAll(){

            try{
                
                string query = @" Select * from student";

                using( var conn = new NpgsqlConnection(_connectionString)){
                    
                    conn.Open();
                    List<Student> students = new List<Student>();

                    using(NpgsqlCommand command = new NpgsqlCommand(query,conn)){

                        NpgsqlDataReader myReader = command.ExecuteReader();

                        while (myReader.Read())
                        {
                            Student st = new Student(){
                                StudentId = myReader.GetInt32(0),
                                Name = myReader.GetString(1),
                                Branch= (Branch)myReader.GetInt32(2),
                                Gender= (Gender)myReader.GetInt32(3),
                                Email= myReader.GetString(4),
                                Address = myReader.GetString(5)
                            };

                            students.Add(st);
                        }
                        myReader.Close();
                        conn.Close();
                    }
                    return View(students);
                }

            }
            catch(Exception ex){
                System.Console.WriteLine("Error occured");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }

        }

        [HttpGet]
        public IActionResult Get(int id){

            try{
                
                string query = "Select * from Student where StudentId = @id";

                using( var conn = new NpgsqlConnection(_connectionString)){
                    
                    conn.Open();

                    Student st = new Student();
                    
                    using(NpgsqlCommand command = new NpgsqlCommand(query,conn)){
                        
                        command.Parameters.AddWithValue("id", id);
                        NpgsqlDataReader myReader = command.ExecuteReader();

                        while(myReader.Read()){

                            st = new Student(){
                                StudentId = myReader.GetInt32(0),
                                Name = myReader.GetString(1),
                                Branch= (Branch)myReader.GetInt32(2),
                                Gender= (Gender)myReader.GetInt32(3),
                                Email= myReader.GetString(4),
                                Address = myReader.GetString(5)
                            };
                        }

                        myReader.Close();
                        conn.Close();
                    }

                    return View(st);
                }

            }
            catch(Exception ex){
                System.Console.WriteLine("Error occured");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }

        }

        [HttpGet]

        public ViewResult Create(){
            Student student = new Student
            {
                AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList()
            };

            return View(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            try{
                
                string query = "Insert into Student Values (@id,@name, @branch, @gender, @email, @address)";

                using( var conn = new NpgsqlConnection(_connectionString)){
                    
                    conn.Open();

                    Random random = new Random();

                    student.StudentId = random.Next();
                    
                    using(NpgsqlCommand command = new NpgsqlCommand(query,conn)){
                        
                        command.Parameters.AddWithValue("id", student.StudentId);
                        command.Parameters.AddWithValue("name",student.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("branch",(int)student.Branch);
                        command.Parameters.AddWithValue("gender",(int) student.Gender);
                        command.Parameters.AddWithValue("email",student.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("address",student.Address ?? (object)DBNull.Value);
                        

                        int nrows = command.ExecuteNonQuery();

                        conn.Close();
                    }

                    return RedirectToAction("GetAll");
                }

            }
            catch(Exception ex){
                System.Console.WriteLine("Error occured");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
            
        }

        public IActionResult Edit(int id){

            try{
                
                string query = "Select * from Student where StudentId = @id";

                using( var conn = new NpgsqlConnection(_connectionString)){
                    
                    conn.Open();

                    Student st = new Student();
                    
                    using(NpgsqlCommand command = new NpgsqlCommand(query,conn)){
                        
                        command.Parameters.AddWithValue("id", id);
                        NpgsqlDataReader myReader = command.ExecuteReader();

                        while(myReader.Read()){

                            st = new Student(){
                                StudentId = myReader.GetInt32(0),
                                Name = myReader.GetString(1),
                                Branch= (Branch)myReader.GetInt32(2),
                                Gender= (Gender)myReader.GetInt32(3),
                                Email= myReader.GetString(4),
                                Address = myReader.GetString(5)
                            };
                        }

                        st.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

                        myReader.Close();
                        conn.Close();
                    }

                    return View(st);
                }

            }
            catch(Exception ex){
                System.Console.WriteLine("Error occured");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
        }

        [HttpPost]
        public IActionResult Update(int id, Student student){
            try{
                
                string query = "Update Student Set Name = @name, Branch = @branch, Gender = @gender, " +
                    "Email = @email, Address = @address Where StudentId = @id";

                using( var conn = new NpgsqlConnection(_connectionString)){
                    
                    conn.Open();

                    using(NpgsqlCommand command = new NpgsqlCommand(query,conn)){
                        
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("name",student.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("branch",(int)student.Branch);
                        command.Parameters.AddWithValue("gender",(int) student.Gender);
                        command.Parameters.AddWithValue("email",student.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("address",student.Address ?? (object)DBNull.Value);
                        

                        int nrows = command.ExecuteNonQuery();

                        conn.Close();
                    }

                    return RedirectToAction("Get",new {id = id} );
                }

            }
            catch(Exception ex){
                System.Console.WriteLine("Error occured");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
        }

        public IActionResult Delete(int id){
            try{
                
                string query = "Delete from Student where StudentId = @id";

                using( var conn = new NpgsqlConnection(_connectionString)){
                    
                    conn.Open();
                    
                    using(NpgsqlCommand command = new NpgsqlCommand(query,conn)){
                        
                        command.Parameters.AddWithValue("id", id);
                        int nrows = command.ExecuteNonQuery();
                        System.Console.WriteLine(nrows+" rows deleted");

                        conn.Close();
                    }

                    return RedirectToAction("GetAll");
                }

            }
            catch(Exception ex){
                System.Console.WriteLine("Error occured");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
        }


    }


}