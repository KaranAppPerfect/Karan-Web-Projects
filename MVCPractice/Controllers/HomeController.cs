using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCPractice.Models;

namespace MVCPractice.Controllers;


public class HomeController : Controller
{
   
    private List<Student> listStudents = new List<Student>();

    public HomeController()
    {
        listStudents = new List<Student>()
        {
            new Student() { StudentId = 101, Name = "James", Branch = Branch.CSE, Gender = Gender.Male, Address = "A1-2018", Email = "James@g.com" },
            new Student() { StudentId = 102, Name = "Priyanka", Branch = Branch.ETC, Gender = Gender.Female, Address = "A1-2019", Email = "Priyanka@g.com" },
            new Student() { StudentId = 103, Name = "David", Branch = Branch.CSE, Gender = Gender.Male, Address = "A1-2020", Email = "David@g.com" }
        };
    }
    
    public ViewResult Index()
    {
        // List<Student> allStudentDetails = _repository.GetAllStudent();
        // return View(allStudentDetails);
        return View(listStudents);
    }

    public ViewResult Details(int Id){
        var studentDetails = listStudents.FirstOrDefault(std => std.StudentId == Id);
        return View(studentDetails);
    }

    [HttpGet]
    public ViewResult Create()
    {
        Student student = new Student
        {
            AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList()
        };

        return View(student);
    }

    [HttpPost]
    public ActionResult Create(Student student)
    {
        student.StudentId = listStudents.Max(x => x.StudentId) + 1;
        listStudents.Add(student);
        return View("Details", student);
    }


}


// // [Route("Home")]

// [Route("[controller]/[action]")]
// public class HomeController : Controller
// {
//     private readonly ILogger<HomeController> _logger;

//     private readonly IStudentRepository _repository = null;

//     public HomeController(ILogger<HomeController> logger, IStudentRepository repository)
//     {
//         _logger = logger;
//         _repository = repository;
//     }
    
//     // [Route("Index")]
//     [Route("/[controller]")]
//     [Route("/")]
//     [Route("")]
//     public ViewResult Index()
//     {
//         // List<Student> allStudentDetails = _repository.GetAllStudent();
//         // return Json(allStudentDetails);
//         return View();
//     }

//     [Route("{id:int?}")]
//     public JsonResult GetStudentDetails(int Id)
//     {
//         Student studentDetails = _repository.GetStudentById(Id);
//         return Json(studentDetails);
//     }
    

//     // [Route("Details")]
//     public ViewResult Details(){

//         ViewData["Title"] = "Student details Page";
//         ViewData["Header"] = "Student Details";

//         Student st = new Student(){
//             StudentId = 1,
//             Name = "Karan",
//             Branch = "IT",
//             Section = "A",
//             Gender = "Male"
//         };

//         ViewData["Student"] = st;


//         return View();
//     }

//     // [Route("Details2")]
//     public ViewResult Details2(){

//         ViewBag.Title = "Student details Page";
//         ViewBag.Header = "Student Details";

//         Student st = new Student(){
//             StudentId = 1,
//             Name = "Karan",
//             Branch = "IT",
//             Section = "A",
//             Gender = "Male"
//         };

//         ViewBag.Student = st;


//         return View();
//     }

//     // [Route("Details3")]
//     public ViewResult Details3(){

//         ViewBag.Title = "Student details Page";
//         ViewBag.Header = "Student Details";

//         Student st = new Student(){
//             StudentId = 1,
//             Name = "Karan",
//             Branch = "IT",
//             Section = "A",
//             Gender = "Male"
//         };

//         return View(st);
//     }



//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

//     // [Route("Error")]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
// }
