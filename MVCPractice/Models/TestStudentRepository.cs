using System.Collections.Generic;
using System.Linq;

namespace MVCPractice.Models
{
    // public class TestStudentRepository : IStudentRepository
    // {
    //     public List<Student> DataSource()
    //     {
    //       return  new List<Student>()
    //         {
    //             new Student() { StudentId = 101, Name = "James", Branch = "CSE", Section = "A", Gender = "Male" },
    //             new Student() { StudentId = 102, Name = "Smith", Branch = "ETC", Section = "B", Gender = "Male" },
    //             new Student() { StudentId = 103, Name = "David", Branch = "CSE", Section = "A", Gender = "Male" },
    //             new Student() { StudentId = 104, Name = "Sara", Branch = "CSE", Section = "A", Gender = "Female" },
    //             new Student() { StudentId = 105, Name = "Pam", Branch = "ETC", Section = "B", Gender = "Female" }
    //         };
    //     }

    //     public Student GetStudentById(int StudentId)
    //     {

    //         Student? s = DataSource().FirstOrDefault(e => e.StudentId == StudentId);

    //         return s;
    //     }

    //     public List<Student> GetAllStudent()
    //     {
    //         return DataSource();
    //     }
    // }
}