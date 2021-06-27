using LabMVC15_04_2021.Models;
using LabMVC15_04_2021.Models.Data;
using LabMVC15_04_2021.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LabMVC15_04_2021.Controllers
   
{
    public class HomeController : Controller
    {

        StudentDAO studentDAO;
       // StudentDAO sd = new StudentDAO();
      


        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

        }


    public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Insert([FromBody] Student student)
        {
            Exception e;
            //llamada viene de la vista, este es el intermediario
            // ESTE METODO ES EL QUE LLAMA AL MODELO (BD)
            //regla de negocio: simulando que ya existe e@e.com
            //if (student.Email.Equals("e@e.com"))

                    
                    //llamada al modelo para insertar el estudiante(ahora pasandole al parametro al constructor)
                    studentDAO = new StudentDAO(_configuration);

                if (studentDAO.VerifyEmail(student.Email))
                {
                    return Error();
                }
                else
                {
                    int resultToReturn = studentDAO.Insert(student);
                    // aca guardamos un 1 o un 0 dependiendo de si se inserto  el estudiante o no

                    return Ok(resultToReturn);// return Ok(resultToReturn);//retornamos el 1 o el 0 a la vista
                }

                  




       

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
