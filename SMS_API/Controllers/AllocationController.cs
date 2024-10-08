/// <summary>
/// <author>Vinusha</author>
/// <date>07 October 2024</date> 
/// <Purpose>This file implements the AllocationController class to handle subject and student allocation-related API operations.</Purpose> 
/// </summary>
using Microsoft.AspNetCore.Mvc;
using SMS.BL.Allocation.Interface;
using SMS.Model.Allocation;
using SMS.ViewModel.Allocation;
using SMS.ViewModel.StaticData;
using SMS.ViewModel.Subject;

namespace SMS_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AllocationController : ControllerBase
    {
        private readonly IAllocationReposiory _allocationRepository;

        public AllocationController(IAllocationReposiory allocationReposiory)
        {
            _allocationRepository = allocationReposiory;
        }

        /**************************************Subject Allocation******************************************************************/

        /// <summary>
        /// Get all subject allocations grouped by subject
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllSubjectAllocations")]
        public IActionResult GetAllSubjectAllocations()
        {
            var response = _allocationRepository.GetAllSubjectAllocation();

            try
            {
                if (response.Success)
                {
                    var viewModel = new AllocationViewModel { SubjectAllocations = response.Data };
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
        /// Get one subject allocaton details
        /// </summary>
        /// <param name="subjectAllocationID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOneSubjectAllocation/{subjectAllocationID}")]
        public IActionResult GetOneSubjectAllocation(int subjectAllocationID)
        {
            var response = _allocationRepository.GetSubjectAllocationByID(subjectAllocationID);
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
        /// Add a new subject allocation
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddSubjectAllocation")]
        public IActionResult AddSubjectAllocation([FromQuery] SubjectAllocationBO subjectAllocation)
        {
            var response = _allocationRepository.UpsertSubjectAllocation(subjectAllocation);

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
        /// Update a new subject allocation
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateSubjectAllocation")]
        public IActionResult UpdateSubjectAllocation([FromQuery] SubjectAllocationBO subjectAllocation)
        {
            var response = _allocationRepository.UpsertSubjectAllocation(subjectAllocation);

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

        [HttpDelete]
        [Route("DeleteSubjectAllocation")]
        public IActionResult DeleteSubjectAllocation(long subjectAllocationID)
        {
            var response=_allocationRepository.DeleteSubjectAllocation(subjectAllocationID);
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
        [Route("GetSerachSubjectAllocation")]
        public IActionResult GetSerachSubjectAllocation([FromQuery] AllocationSearchViewModel allocationSearchViewModel)
        {
            var response = _allocationRepository.GetSearchSubjectAllocation(allocationSearchViewModel);
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


        /**************************************Students Allocation******************************************************************/

        /// <summary>
        /// Get all subject allocations grouped by subject
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllStudentAllocations")]
        public IActionResult GetAllStudentAllocations()
        {
            var response = _allocationRepository.GetAllStudentAllocation();
            try
            {
                if (response.Success)
                {
                    var viewModel = new AllocationViewModel { StudentAllocations = response.Data };
                    return Ok(viewModel);
                }
                else
                {
                    return StatusCode(StaticData.STATUSCODE_NOTFOUND, response);
                }

            }
            catch {
                return StatusCode(StaticData.STATUSCODE_INTERNAL_SERVAR_ERROR, response);
            }            
        }
        /// <summary>
        /// Add a new subject allocation
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddStudentAllocation")]
        public IActionResult AddStudentAllocation([FromQuery]StudentAllocationBO studentAllocation)
        {
            var response = _allocationRepository.UpsertStudentAllocation(studentAllocation);

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
        /// Update a new student allocation
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateStudentAllocation")]
        public IActionResult UpdateStudentAllocation([FromQuery] StudentAllocationBO studentAllocation)
        {
            var response = _allocationRepository.UpsertStudentAllocation(studentAllocation);

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
        /// Delete student allocation
        /// </summary>
        /// <param name="studentAllocationID"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteStudentAllocation")]
        public IActionResult DeleteStudentAllocation(long studentAllocationID)
        {
            var response = _allocationRepository.DeleteStudentAllocation(studentAllocationID);
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
        [Route("GetSerachStudentAllocation")]
        public IActionResult GetSerachStudentAllocation([FromQuery] AllocationSearchViewModel allocationSearchViewModel)
        {
            var response = _allocationRepository.GetSearchStudentAllocation(allocationSearchViewModel);
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
        /// Get all subject allocations grouped by subject
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllocatedSubjectDetail")]
        public IActionResult GetAllocatedSubjectDetail()
        {
            var response = _allocationRepository.GetAllocatedSubjectDetails();
            try
            {
                if (response.Success)
                {
                    var viewModel = new SubjectViewModel { SubjectList = response.Data };
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
        /// Get all subject allocations grouped by subject
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllocatedTeachersForASubject")]
        public IActionResult GetAllocatedTeachersForASubject(long subjectID)
        {
            var response = _allocationRepository.GetAllocatedTeachersForSubject(subjectID);
            try
            {
                if (response.Success)
                {
                    var viewModel = new SubjectAllocationGroupBySubjectViewModel { SubjectAllocations = response.Data };
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

    }
}
