/// <summary>
/// <author>Vinusha</author>
/// <date>07 October 2024</date> 
/// <Purpose>This file implements the TeacherController class to handle teacher-related API operations.</Purpose> 
/// </summary>
using Microsoft.AspNetCore.Mvc;
using SMS.BL.Teacher.Interface;
using SMS.Model.Teacher;
using SMS.ViewModel.Search;
using SMS.ViewModel.StaticData;
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
        public IActionResult GetAllTeacherList([FromQuery] int pageNumber, int numberOfRecoards, bool? isActive)
        {
            var response = _teacherRepository.GetAllTeachers(pageNumber,numberOfRecoards, isActive);
            try
            {
                var viewModel = new TeacherViewModel
                {
                    TeachersList = response.Data,
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
        /// Get one teacher details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOneTeachers/{id}")]
        public IActionResult GetTeacher(int id)
        {
            var response = _teacherRepository.GetOneTeacher(id);

            try
            {
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
                    return StatusCode(StaticData.STATUSCODE_NOTFOUND, response);
                }
            }
            catch
            {
                return StatusCode(StaticData.STATUSCODE_INTERNAL_SERVAR_ERROR, response);
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
        /// Add a new teacher
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddTeacher")]
        public IActionResult AddNewTeacher([FromQuery] TeacherBO teacher)
        {
            var response = _teacherRepository.AddTeacher(teacher);
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
        /// Update teacher details
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateTeacher")]
        public IActionResult UpdateTeacher([FromQuery] TeacherBO teacher)
        {
            var response = _teacherRepository.UpdateTeacherDetails(teacher);
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
        /// Get all teachers details based on search criteria
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSearchTeachers")]
        public IActionResult GetSearchTeachers([FromQuery] SearchViewModel teacherSearchViewModel)
        {
            var response = _teacherRepository.GetSearchTeachers(teacherSearchViewModel);
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

        [HttpPut]
        [Route("ToggleTeacherStatus")]
        public IActionResult ToggleStatusOfTeacher([FromQuery] int id, bool isEnable)
        {
            var response = _teacherRepository.ToggleEnableTeacher(id, isEnable);

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
