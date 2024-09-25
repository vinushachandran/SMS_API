using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.BL.Teacher.Interface;
using SMS.Model.Teacher;
using SMS.ViewModel.Teacher;

namespace SMS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        /// <summary>
        /// Get all teacher details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllTeachers")]
        public IActionResult GetAllTeacherList([FromQuery] bool? isActive)
        {
            var response = _teacherRepository.GetAllTeachers(isActive);

            var viewModel = new TeacherViewModel
            {
                TeachersList = response.Data
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
        /// Get one teacher details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOneTeachers/{id}")]
        public IActionResult GetTeacher(int id)
        {
            var response = _teacherRepository.GetOneTeacher(id);

            var viewModel = new TeacherViewModel
            {
                TeacherDetail = response.Data
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
        /// Delete one teacher details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteTeachers/{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var response = _teacherRepository.DeleteTeacher(id);

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
        /// Add a new teacher
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddTeacher")]
        public IActionResult AddNewTeacher([FromQuery] TeacherBO teacher)
        {
            var response = _teacherRepository.AddTeacher(teacher);
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
        /// Update teacher details
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateTeacher")]
        public IActionResult UpdateTeacher([FromQuery] TeacherBO teacher)
        {
            var response = _teacherRepository.UpdateTeacherDetails(teacher);

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
        /// Get all teachers details based on search criteria
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSearchTeachers")]
        public IActionResult GetSearchTeachers([FromQuery] TeacherSearchViewModel teacherSearchViewModel)
        {
            var response = _teacherRepository.GetSearchTeachers(teacherSearchViewModel);

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
        [Route("ToggleTeacherStatus")]
        public IActionResult ToggleStatusOfTeacher([FromQuery] int id, bool isEnable)
        {
            var response = _teacherRepository.ToggleEnableTeacher(id, isEnable);

            if (response.Success)
            {
                return Ok(response.TotalMessages);
            }
            else
            {
                return StatusCode(500, response.Message);
            }
        }



    }
}
