using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.BL.Allocation.Interface;
using SMS.ViewModel.Allocation;

namespace SMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocationController : ControllerBase
    {
        private readonly IAllocationReposiory _subjectAllocationRepository;

        public AllocationController(IAllocationReposiory allocationReposiory)
        {
            _subjectAllocationRepository = allocationReposiory;
        }

        /**************************************Subject Allocation******************************************************************/

        ///// <summary>
        ///// Get all subject allocations grouped by subject
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("GetAllSubjectAllocations")]
        //public IActionResult GetAllSubjectAllocations()
        //{
        //    var response = _subjectAllocationRepository.GetAllSubjectAllocation();

        //    if (response.Success)
        //    {
        //        var viewModel = new List<SubjectAllocationGroupBySubjectViewModel>(response.Data);
        //        return Ok(viewModel);
        //    }
        //    else
        //    {
        //        return StatusCode(500, response.Message);
        //    }
        //}
    }
}
