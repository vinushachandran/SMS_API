/// <summary>
/// <author>Vinusha</author>
/// <date>07 October 2024</date> 
/// <Purpose>This file implements the SubjectController class to handle subject-related API operations.</Purpose> 
/// </summary>
using Microsoft.AspNetCore.Mvc;
using SMS.BL.Subject.Interface;
using SMS.Model.Subject;
using SMS.ViewModel.StaticData;
using SMS.ViewModel.Subject;

namespace SMS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        /// <summary>
        /// Get all subject details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllSubjects")]
        public IActionResult GetAllSubjectList([FromQuery] int pageNumber, int numberOfRecoards, bool? isActive)
        {
            var response = _subjectRepository.GetAllSubjects(pageNumber,numberOfRecoards, isActive);
            try
            {
                var viewModel = new SubjectViewModel
                {
                    SubjectList = response.Data,
                    totalPages = response.TotalPages,

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
        /// Get one subject details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOneSubject/{id}")]
        public IActionResult GetSubject(int id)
        {
            var response = _subjectRepository.GetOneSubject(id);

            try
            {
                var viewModel = new SubjectViewModel
                {
                    SubjectDetail = response.Data
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
        /// Delete one subject details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteSubjects/{id}")]
        public IActionResult DeleteSubject(int id)
        {
            var response = _subjectRepository.DeleteSubject(id);

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
        /// Add a new subject
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddSubject")]
        public IActionResult AddNewSubject([FromQuery] SubjectBO subject)
        {
            var response = _subjectRepository.AddSubject(subject);
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
        /// Update subject details
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateSubject")]
        public IActionResult UpdateSubject([FromQuery] SubjectBO subject)
        {
            var response = _subjectRepository.UpdateSubjectDetails(subject);
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
        /// Get all subjects details based on search criteria
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSearchSubjects")]
        public IActionResult GetSearchSubjects([FromQuery] SubjectSearchViewModel subjectSearchViewModel)
        {
            var response = _subjectRepository.GetSearchSubjects(subjectSearchViewModel);
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
        /// Change the active status of a subject
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ToggleSubjectStatus")]
        public IActionResult ToggleStatusOfSubject([FromQuery] long id, bool isEnable)
        {
            var response = _subjectRepository.ToggleEnableSubject(id, isEnable);

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
