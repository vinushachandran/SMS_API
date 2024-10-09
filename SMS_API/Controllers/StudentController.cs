/// <summary>
/// <author>Vinusha</author>
/// <date>07 October 2024</date> 
/// <Purpose>This file implements the StudentController class to handle student-related API operations.</Purpose> 
/// </summary>
using Microsoft.AspNetCore.Mvc;
using SMS.BL.Student.Interface;
using SMS.Model.Student;
using SMS.ViewModel.Search;
using SMS.ViewModel.StaticData;
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
        public IActionResult GetAllStudentList([FromQuery] int pageNumber, int numberOfRecoards, bool? isActive)
        {
            var response=_studentRepository.GetAllStudents(pageNumber,numberOfRecoards,isActive);
            try
            {
                var viewModel = new StudentViewModel
                {
                    AllStudentsList = response.Data,
                    totalPages = response.TotalPages

                };
                if (response.Success)
                {
                    return Ok(viewModel);
                }
                else
                {
                    return StatusCode(StaticData.STATUSCODE_NOTFOUND, response);
                }
            }
            catch 
            {
                return StatusCode(StaticData.STATUSCODE_INTERNAL_SERVAR_ERROR, response);
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

            try
            {
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
                    return StatusCode(StaticData.STATUSCODE_NOTFOUND, response);
                }
            }
            catch
            {
                return StatusCode(StaticData.STATUSCODE_INTERNAL_SERVAR_ERROR, response);
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
            try
            {
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StaticData.STATUSCODE_NOTFOUND, response);
                }
            }
            catch
            {
                return StatusCode(StaticData.STATUSCODE_INTERNAL_SERVAR_ERROR, response);
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
            try
            {
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StaticData.STATUSCODE_VALIDATION, response);
                }
            }
            catch
            {
                return StatusCode(StaticData.STATUSCODE_INTERNAL_SERVAR_ERROR, response);
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
            try
            {
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StaticData.STATUSCODE_VALIDATION, response);
                }
            }
            catch
            {
                return StatusCode(StaticData.STATUSCODE_INTERNAL_SERVAR_ERROR, response);
            }

            

        }


        /// <summary>
        /// Get all students details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSerachStudents")]
        public IActionResult GetSearchStudents([FromQuery] SearchViewModel studentSearchViewModel)
        {
            var response = _studentRepository.GetSearchStudents(studentSearchViewModel);
            try
            {
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StaticData.STATUSCODE_NOTFOUND, response);
                }
            }
            catch
            {
                return StatusCode(StaticData.STATUSCODE_INTERNAL_SERVAR_ERROR, response);
            }

           
        }

        /// <summary>
        /// toggle enable
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("TogleStudentStatus")]
        public IActionResult ToggleStatusOfStudent([FromQuery] int id, bool isEnable)
        {
            var response = _studentRepository.ToggleEnable(id,isEnable);

            try
            {
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StaticData.STATUSCODE_NOTFOUND, response);
                }
            }
            catch
            {
                return StatusCode(StaticData.STATUSCODE_INTERNAL_SERVAR_ERROR, response);
            }

           
        }

    }
}
