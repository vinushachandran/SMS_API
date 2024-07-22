using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.BL.Subject.Interface;
using SMS.Model.Subject;
using SMS.ViewModel.Subject;

namespace SMS_API.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult GetAllSubjectList([FromQuery] bool? isActive)
        {
            var response = _subjectRepository.GetAllSubjects(isActive);

            var viewModel = new SubjectViewModel
            {
                SubjectList = response.Data
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
        /// Get one subject details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOneSubject/{id}")]
        public IActionResult GetSubject(int id)
        {
            var response = _subjectRepository.GetOneSubject(id);

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
                return StatusCode(500, response.Message);
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
        /// Add a new subject
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddSubject")]
        public IActionResult AddNewSubject([FromQuery] SubjectBO subject)
        {
            var response = _subjectRepository.AddSubject(subject);
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
        /// Update subject details
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateSubject")]
        public IActionResult UpdateSubject([FromQuery] SubjectBO subject)
        {
            var response = _subjectRepository.UpdateSubjectDetails(subject);

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
        /// Get all subjects details based on search criteria
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSearchSubjects")]
        public IActionResult GetSearchSubjects([FromQuery] SubjectSearchViewModel subjectSearchViewModel)
        {
            var response = _subjectRepository.GetSearchSubjects(subjectSearchViewModel);

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
