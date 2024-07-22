/// <summary>
///
/// </summary>
/// <author>Vinusha</author>
///
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.BL.Student.Interface;
using SMS.Model.Student;
using SMS.ViewModel.Student;

namespace SMS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Get all students details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllStudents")]
        public IActionResult GetAllStudentList([FromQuery]bool? isActive)
        {
            var response=_studentRepository.GetAllStudents(isActive);

            var viewModel = new StudentViewModel
            {
                AllStudentsList = response.Data
            };
            if (response.Success)
            {
                return Ok(viewModel);
            }
            else
            {
                return StatusCode(500,response.Message);
            }
        }

        /// <summary>
        /// Get ine student details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOneStudent/{id}")]
        public IActionResult GetStudent(int id)
        {
            var response = _studentRepository.GetOneStudent(id);

            var viewModel = new StudentViewModel
            {
                StudentDetail = response.Data
            };
            if (response.Success)
            {
                return Ok(viewModel);
            }
            else
            {
                return StatusCode(500, response.Message);
            }

        }

        /// <summary>
        /// Delete one student details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var response=_studentRepository.DeleteStudent(id);

            if (response.Success)
            {
                return Ok(response.TotalMessages);
            }
            else
            {
                return StatusCode(500, response.Message);
            }

        }

        /// <summary>
        /// Add a new student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddStudent")]
        public IActionResult AddNewStudent([FromQuery]StudentBO student)
        {
            var response=_studentRepository.AddStudent(student);
            if (response.Success)
            {
                return Ok(response.TotalMessages);
            }
            else
            {
                return StatusCode(500, response.Message);
            }

        }

        /// <summary>
        /// Update student details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateStudent")]
        public IActionResult UpdateStudent([FromQuery]StudentBO student)
        {
            var response=_studentRepository.UpdateStudentDetails(student);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response.Message);
            }

        }


        /// <summary>
        /// Get all students details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSerachStudents")]
        public IActionResult GetSearchStudents([FromQuery] StudentSearchViewModel studentSearchViewModel)
        {
            var response = _studentRepository.GetSearchStudents(studentSearchViewModel);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response.Message);
            }
        }

        [HttpPut]
        [Route("TogleStudentStatus")]
        public IActionResult ToggleStatusOfStudent([FromQuery] int id, bool isEnable)
        {
            var response = _studentRepository.ToggleEnable(id,isEnable);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response.Message);
            }
        }

    }
}
