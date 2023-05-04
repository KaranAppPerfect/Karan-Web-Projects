using Microsoft.AspNetCore.Mvc;
using MVCPractice.Models;
using MVCPractice.ViewModels;

namespace FirstCoreMVCWebApplication.Controllers
{

    [Route("[controller]")]
    public class StudentController : Controller
    {

        [Route("")]
        [Route("[action]/{id:int?}")]
        public string Index(string id)
        {
            return $"Return All Students {id}";
        }

        // [Route("[action]")]
        // public ViewResult Details(){
        //     Student student = new Student()
        //     {
        //         StudentId = 101,
        //         Name = "Dillip",
        //         Branch = "CSE",
        //         Section = "A",
        //         Gender = "Male"
        //     };
        //     //Student Address
        //     Address address = new Address()
        //     {
        //         StudentId = 101,
        //         City = "Mumbai",
        //         State = "Maharashtra",
        //         Country = "India",
        //         Pin = "400097"
        //     };
        //     //Creating the View model
        //     StudentDetailsViewModel studentDetailsViewModel = new StudentDetailsViewModel()
        //     {
        //         Student = student,
        //         Address = address,
        //         Title = "Student Details Page",
        //         Header = "Student Details",
        //     };
        //     //Pass the studentDetailsViewModel to the view
        //     return View(studentDetailsViewModel);
        // }


    }
}
