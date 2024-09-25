using Microsoft.EntityFrameworkCore;
using SMS.BL.Allocation.Interface;
using SMS.Data;
using SMS.Model.Allocation;
using SMS.ViewModel.Allocation;
using SMS.ViewModel.RepositoryResponse;
using SMS.ViewModel.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Allocation
{
    public class AllocationRepository:IAllocationReposiory
    {
        private readonly SMS_Context _dataContext;

        public AllocationRepository(SMS_Context dataContext)
        {
            _dataContext = dataContext;
        }

        /**************************************Subject Allocation******************************************************************/

        ///// <summary>
        ///// Get all subject allocations
        ///// </summary>
        ///// <returns></returns>
        //public RepositoryResponse<IEnumerable<SubjectAllocationGroupBySubjectViewModel>> GetAllSubjectAllocation()
        //{
        //    var response=new RepositoryResponse<IEnumerable<SubjectAllocationGroupBySubjectViewModel>>();
        //    try
        //    {
        //        var allSubjectAllocations = _dataContext.Teacher_Subject_Allocation.Include("Subject").Include("Teacher").ToList();

        //        if (allSubjectAllocations.Any())
        //        {
        //            var result = allSubjectAllocations.Select(item => new
        //            {
        //                SubjectAllocationID = item.SubjectAllocationID,
        //                SubjectCode = item.Subject.SubjectCode,
        //                SubjectName = item.Subject.Name,
        //                TeacherRegNo = item.Teacher.TeacherRegNo,
        //                TeacherName = item.Teacher.DisplayName

        //            }).GroupBy(s => new { s.SubjectName, s.SubjectCode }).ToList();

        //            var data = result.Select(s => new SubjectAllocationGroupBySubjectViewModel
        //            {
        //                TeacherName = s.Key.SubjectName,
        //                TeacherRegNo = s.Key.SubjectCode,
        //                SubjectAllocations = s.Select(x => new SubjectAllocationViewModel
        //                {
        //                    SubjectAllocationID = x.SubjectAllocationID,
        //                    TeacherRegNo = x.TeacherRegNo,
        //                    TeacherName = x.TeacherName,

        //                }).ToList()
        //            });

        //            response.Data = data;
        //            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "Subject allocations"));
        //            return response;
        //        }

        //        else
        //        {
        //            response.Success=false;
        //            response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "in subject allocations"));
        //            return response;
        //        }

        //    }



        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "Subject Allocation"));
        //        return response;
        //    }
        //}
    }
}
