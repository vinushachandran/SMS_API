/// <summary>
/// <author>Vinusha</author>
/// <date>07 October 2024</date> 
/// <Purpose>This file implements the AllocationRepository class to handle subject and student allocations.</Purpose> 
/// </summary>
using Microsoft.EntityFrameworkCore;
using SMS.BL.Allocation.Interface;
using SMS.Data;
using SMS.Model.Allocation;
using SMS.Model.Subject;
using SMS.ViewModel.Allocation;
using SMS.ViewModel.RepositoryResponse;
using SMS.ViewModel.Search;
using SMS.ViewModel.StaticData;


namespace SMS.BL.Allocation
{
    public class AllocationRepository:IAllocationReposiory
    {
        private readonly SMS_Context _dataContext;

        public AllocationRepository(SMS_Context dataContext)
        {
            _dataContext = dataContext;
        }
        #region Subject Allocation
        /**************************************Subject Allocation******************************************************************/

        /// <summary>
        /// Get all subject allocations
        /// </summary>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<SubjectAllocationGroupBySubjectViewModel>> GetAllSubjectAllocation()
        {
            var response = new RepositoryResponse<IEnumerable<SubjectAllocationGroupBySubjectViewModel>>();
            try
            {
                var query = from tsa in _dataContext.Teacher_Subject_Allocation
                            join s in _dataContext.Subject on tsa.SubjectID equals s.SubjectID
                            join t in _dataContext.Teacher on tsa.TeacherID equals t.TeacherID
                            select new
                            {
                                tsa.SubjectAllocationID,
                                s.SubjectID,
                                s.SubjectCode,
                                s.Name,
                                t.TeacherID,
                                t.TeacherRegNo,
                                TeacherName = t.DisplayName,
                            };

                var sujectAllocationGroup = query.GroupBy(s => new { s.SubjectID, s.SubjectCode, s.Name })
                    .Select(sub => new SubjectAllocationGroupBySubjectViewModel
                    {
                        SubjectID = sub.Key.SubjectID,
                        SubjectCode = sub.Key.SubjectCode,
                        SubjectName = sub.Key.Name,
                        SubjectAllocations = sub.Select(tea => new SubjectAllocationViewModel
                        {
                            SubjectAllocationID = tea.SubjectAllocationID,
                            TeacherName = tea.TeacherName,
                            TeacherRegNo = tea.TeacherRegNo,
                            TeacherID = tea.TeacherID,
                        }).ToList()

                    }).ToList();

                response.Data = sujectAllocationGroup;

                if(response.Data.Any())
                {
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE,"Subject Allocations"));
                    
                }
                else {
                    response.Success= false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "Subject Allocations"));
                }

                return response;
            }



            catch 
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "Subject Allocation"));
                return response;
            }
        }

        /// <summary>
        /// Get one subject allocation detail by it's id
        /// </summary>
        /// <param name="SubjectAllocationID"></param>
        /// <returns></returns>
        public RepositoryResponse<SubjectAllocationBO> GetSubjectAllocationByID(long subjectAllocationID)
        {
            var response= new RepositoryResponse<SubjectAllocationBO>();
            try
            {
                var subjectAllocation=_dataContext.Teacher_Subject_Allocation.Where(s=>s.SubjectAllocationID==subjectAllocationID).FirstOrDefault();

                if(subjectAllocation!=null)
                {
                    response.Data= subjectAllocation;
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "get one subject allocation detail"));
                    
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "in subject allocation"));                    
                   

                }
                return response;

            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "get Subject Allocation by it's id"));
                return response;

            }
        }


        /// <summary>
        /// Add Subject Allocation
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> UpsertSubjectAllocation(SubjectAllocationBO subjectAllocation)
        {
            var response = new RepositoryResponse<bool>();
            try
            {
                var isSubjectAllocationAlreadyExist = IsSubjectAllocationAlreadyExist(subjectAllocation.TeacherID, subjectAllocation.SubjectID);
                

                if (subjectAllocation.SubjectAllocationID==null)
                {
                    if (isSubjectAllocationAlreadyExist.Success)
                    {
                        response.Success = false;
                        response.Message.Add(string.Format("Subject allocation already exist"));
                        
                    }
                    else
                    {
                        var newSubjectAllocation = new SubjectAllocationBO();
                        newSubjectAllocation.SubjectID = subjectAllocation.SubjectID;
                        newSubjectAllocation.TeacherID = subjectAllocation.TeacherID;

                        _dataContext.Teacher_Subject_Allocation.Add(newSubjectAllocation);
                        _dataContext.SaveChanges();

                        response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "add a subject allocation!"));
                        
                    }
                }
                else
                {                    
                    if (isSubjectAllocationAlreadyExist.Success)
                    {
                        response.Success = false;
                        response.Message.Add(string.Format("Subject allocation already exist"));
                        
                    }
                    else
                    {
                        var editSubjectAllocation = _dataContext.Teacher_Subject_Allocation.SingleOrDefault(s => s.SubjectAllocationID == subjectAllocation.SubjectAllocationID);

                        if (editSubjectAllocation != null)
                        {
                            editSubjectAllocation.SubjectID = subjectAllocation.SubjectID;
                            editSubjectAllocation.TeacherID = subjectAllocation.TeacherID;

                            _dataContext.SaveChanges();
                            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "update a subject allocation!"));
                            
                        }
                        else
                        {
                            response.Success = false;
                            response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "update a subject allocation!"));
                        }
                        
                        
                    }
                }
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "Subject Allocation"));
                return response;
            }
        }

        /// <summary>
        /// Check the subject allocation already created
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="subjectID"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsSubjectAllocationAlreadyExist(long teacherID, long subjectID)
        {
            var response = new RepositoryResponse<bool>();
            try
            {
                response.Success=_dataContext.Teacher_Subject_Allocation.Any(s=>s.TeacherID==teacherID && s.SubjectID==subjectID);
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "check subject allocation"));
                return response;
            }
        }

        /// <summary>
        /// Delete Subject Allocation
        /// </summary>
        /// <param name="subjectAllocationID"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> DeleteSubjectAllocation(long subjectAllocationID)
        {
            var response = new RepositoryResponse<bool>();
            try
            {
                var subjectAllocation=_dataContext.Teacher_Subject_Allocation.SingleOrDefault(t=>t.SubjectAllocationID==subjectAllocationID);
                var isSubjectAllocationInUse=IsSubjectAllocationInUsed(subjectAllocationID);

                if(subjectAllocation != null)
                {
                    if (isSubjectAllocationInUse.Success)
                    {
                        response.Success = false;
                        response.Message.Add(string.Format("This subjectallocation allocated for a student"));
                        
                    }
                    else
                    {
                        _dataContext.Teacher_Subject_Allocation.Remove(subjectAllocation);
                        _dataContext.SaveChanges();
                        response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "delete subject allocation"));
                        
                    }

                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "subject allocation"));
                    
                }
                return response;
            }

            
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "delete subject allocation"));
                return response;
            }

        }

        /// <summary>
        /// Check this subject allocation allocated for any student
        /// </summary>
        /// <param name="subjectAllocationID"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsSubjectAllocationInUsed(long subjectAllocationID)
        {
            var response = new RepositoryResponse<bool>();

            try {
                response.Success=_dataContext.Student_Subject_Teacher_Allocation.Any(s=>s.SubjectAllocationID==subjectAllocationID);
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "check subject allocation in used"));
                return response;

            }

        }

        /// <summary>
        /// Search a subject allocation
        /// </summary>
        /// <param name="allocationSearchViewModel"></param>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<SubjectAllocationGroupBySubjectViewModel>> GetSearchSubjectAllocation(SearchViewModel allocationSearchViewModel)
        {
            var response=new RepositoryResponse<IEnumerable<SubjectAllocationGroupBySubjectViewModel>>();
            var allSubjectAllocations = GetAllSubjectAllocation();

            try
            {

                if (allocationSearchViewModel.Criteria == "TeacherName")
                {
                    allSubjectAllocations.Data = allSubjectAllocations.Data.Where(sa => sa.SubjectAllocations.Any(s=>s.TeacherName.ToUpper().Contains(allocationSearchViewModel.Term.ToUpper()))).ToList();
                }
                else if (allocationSearchViewModel.Criteria == "SubjectName")
                {
                    allSubjectAllocations.Data = allSubjectAllocations.Data.Where(s => s.SubjectName.ToUpper().Contains(allocationSearchViewModel.Term.ToUpper())).ToList();
                }
                else
                {
                    allSubjectAllocations.Data = allSubjectAllocations.Data.Where(sa => sa.SubjectAllocations.Any(s => s.TeacherName.ToUpper().Contains(allocationSearchViewModel.Term.ToUpper())) || sa.SubjectName.ToUpper().Contains(allocationSearchViewModel.Term.ToUpper())).ToList();
                }

                response.Data = allSubjectAllocations.Data;

                if (response.Data.Any())
                {
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "searchDetails"));                   
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, allocationSearchViewModel.Term));
                    
                }

                return response;

            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "check subject allocation in used"));
                return response;
            }

        }

        #endregion

        #region Student Allocation
        /**************************************Student Allocation******************************************************************/

        /// <summary>
        /// Get all subject allocations
        /// </summary>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<StudentAllocationGroupByStudentViewModel>> GetAllStudentAllocation()
        {
            var response = new RepositoryResponse<IEnumerable<StudentAllocationGroupByStudentViewModel>>();
            try
            {
                var query = from ssa in _dataContext.Student_Subject_Teacher_Allocation
                            join s in _dataContext.Student on ssa.StudentID equals s.StudentID
                            join tsa in _dataContext.Teacher_Subject_Allocation on ssa.SubjectAllocationID equals tsa.SubjectAllocationID
                            join t in _dataContext.Teacher on tsa.TeacherID equals t.TeacherID
                            join sub in _dataContext.Subject on tsa.SubjectID equals sub.SubjectID
                            select new
                            {
                                ssa.StudentAllocationID,
                                s.StudentID,
                                StudentName = s.DisplayName,
                                s.StudentRegNo,
                                tsa.SubjectAllocationID,
                                sub.SubjectID,
                                sub.SubjectCode,
                                sub.Name,
                                t.TeacherID,
                                t.TeacherRegNo,
                                TeacherName = t.DisplayName,
                                s.IsEnable
                            };
                

                var studentAllocationGroup = query.GroupBy(s => new { s.StudentID, s.StudentRegNo, s.StudentName })
                    .Select(std => new StudentAllocationGroupByStudentViewModel
                    {
                        StudentID = std.Key.StudentID,
                        StudentRegNo = std.Key.StudentRegNo,
                        StudentName = std.Key.StudentName,
                        IsActive=std.Where(s => s.StudentID==std.Key.StudentID).Select(st=>st.IsEnable).FirstOrDefault(),
                        SubjectAllocations = std.GroupBy(sub => new {sub.SubjectID,sub.SubjectCode,sub.Name})
                            .Select(sa=>new SubjectAllocationGroupBySubjectViewModel
                            {
                                SubjectID = sa.Key.SubjectID,
                                SubjectCode = sa.Key.SubjectCode,
                                SubjectName = sa.Key.Name,
                                SubjectAllocations=sa.Select(s => new SubjectAllocationViewModel
                                {
                                   TeacherID = s.TeacherID,
                                   TeacherRegNo = s.TeacherRegNo,
                                   TeacherName = s.TeacherName,
                                   StudentAllocationID=s.StudentAllocationID,
                                   SubjectAllocationID=s.SubjectAllocationID
                                }).ToList()
                            }).ToList()               

                    }).ToList();

                response.Data = studentAllocationGroup;

                if (response.Data.Any())
                {
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "Student Allocations"));

                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "Student Allocations"));
                }

                return response;
            }



            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "Student Allocation"));
                return response;
            }
        }


        /// <summary>
        /// Update Subject Allocation
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> UpsertStudentAllocation(StudentAllocationBO studentAllocation)
        {
            var response = new RepositoryResponse<bool>();
            
            try
            {
                var isStudentAllocationAlreadyExist = IsStudentAllocationAlreadyExist(studentAllocation.SubjectAllocationID, studentAllocation.StudentID);
                

                if(studentAllocation.StudentAllocationID == null)
                {

                    if (isStudentAllocationAlreadyExist.Success)
                    {
                        response.Success = false;
                        response.Message.Add(string.Format("Student allocation already exist"));
                        
                    }
                    else
                    {
                        var newStudentAllocation = new StudentAllocationBO();
                        newStudentAllocation.SubjectAllocationID = studentAllocation.SubjectAllocationID;
                        newStudentAllocation.StudentID = studentAllocation.StudentID;

                        _dataContext.Student_Subject_Teacher_Allocation.Add(newStudentAllocation);
                        _dataContext.SaveChanges();

                        response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "add a student allocation!"));
                        
                    }

                }
                else
                {
                    if (isStudentAllocationAlreadyExist.Success)
                    {
                        response.Success = false;
                        response.Message.Add(string.Format("Subject allocation already exist"));
                        
                    }
                    else
                    {
                        var editStudentAllocation = _dataContext.Student_Subject_Teacher_Allocation.SingleOrDefault(s => s.StudentAllocationID == studentAllocation.StudentAllocationID);

                        if (editStudentAllocation != null)
                        {
                            editStudentAllocation.SubjectAllocationID = studentAllocation.SubjectAllocationID;
                            editStudentAllocation.StudentID = studentAllocation.StudentID;

                            _dataContext.SaveChanges();
                            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "update a student allocation!"));
                            
                        }
                        else
                        {
                            response.Success = false;
                            response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "update a student allocation!"));
                            
                        }                  

                    }

                }
                return response;

            }
            catch 
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "student allocation"));
                return response;
            }

        }

        /// <summary>
        /// Check the student allocation already created
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="subjectID"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsStudentAllocationAlreadyExist(long subjectAllocationID, long studentID)
        {
            var response = new RepositoryResponse<bool>();
            try
            {
                response.Success = _dataContext.Student_Subject_Teacher_Allocation.Any(s => s.SubjectAllocationID == subjectAllocationID && s.StudentID == studentID);
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "check subject allocation"));
                return response;
            }
        }

        /// <summary>
        /// Delete Student Allocation
        /// </summary>
        /// <param name="studentAllocationID"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> DeleteStudentAllocation(long studentAllocationID)
        {
            var response = new RepositoryResponse<bool>();

            try
            {
                var deleteStudentAllocation = _dataContext.Student_Subject_Teacher_Allocation.SingleOrDefault(s => s.StudentAllocationID == studentAllocationID);
                if (deleteStudentAllocation != null)
                {
                    _dataContext.Student_Subject_Teacher_Allocation.Remove(deleteStudentAllocation);
                    _dataContext.SaveChanges();
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "delete student allocation"));
                   
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "subject allocation"));
                    

                }
                return response;


            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "delete student allocation"));
                return response;

            }
        }


        /// <summary>
        /// Search a student allocation
        /// </summary>
        /// <param name="allocationSearchViewModel"></param>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<StudentAllocationGroupByStudentViewModel>> GetSearchStudentAllocation(SearchViewModel allocationSearchViewModel)
        {
            var response = new RepositoryResponse<IEnumerable<StudentAllocationGroupByStudentViewModel>>();
            var allStudentAllocations = GetAllStudentAllocation();

            try
            {
                if (allocationSearchViewModel.Criteria == "StudentName")
                {
                    allStudentAllocations.Data = allStudentAllocations.Data.Where(sa => sa.StudentName.ToUpper().Contains(allocationSearchViewModel.Term.ToUpper())).ToList();
                }
                else if (allocationSearchViewModel.Criteria == "TeacherName")
                {
                    allStudentAllocations.Data=allStudentAllocations.Data.Where(sa=>sa.SubjectAllocations.Any(s=>s.SubjectAllocations.Any(t=>t.TeacherName.ToUpper().Contains(allocationSearchViewModel.Term.ToUpper())))).ToList();
                }
                else if (allocationSearchViewModel.Criteria == "SubjectName")
                {
                    allStudentAllocations.Data = allStudentAllocations.Data.Where(sa => sa.SubjectAllocations.Any(s=>s.SubjectName.ToUpper().Contains(allocationSearchViewModel.Term.ToUpper()))).ToList();
                }
                else
                {
                    allStudentAllocations.Data = allStudentAllocations.Data.Where(sa => sa.StudentName.ToUpper().Contains(allocationSearchViewModel.Term.ToUpper()) || sa.SubjectAllocations.Any(s => s.SubjectAllocations.Any(t => t.TeacherName.ToUpper().Contains(allocationSearchViewModel.Term.ToUpper()))) || sa.SubjectAllocations.Any(s => s.SubjectName.ToUpper().Contains(allocationSearchViewModel.Term.ToUpper()))).ToList();
                }

                response.Data = allStudentAllocations.Data;

                if (response.Data.Any())
                {
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "searchDetails"));
                    
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, allocationSearchViewModel.Term));
                    
                }
                return response;

            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "check student allocation in used"));
                return response;
            }
        }

        /// <summary>
        /// Get allocated subject details
        /// </summary>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<SubjectBO>> GetAllocatedSubjectDetails()
        {
            var response= new RepositoryResponse<IEnumerable<SubjectBO>>();
            try
            {
                var subjectsAllocated = _dataContext.Subject.Where(s => _dataContext.Teacher_Subject_Allocation.Select(tsa => tsa.SubjectID).Contains(s.SubjectID)).ToList();
                response.Data = subjectsAllocated;
                if(response.Data.Any())
                {
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "get all allocated subjects"));
                    
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "allocated subject details"));
                    

                }
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "get allocated subject details"));
                return response;
            }
        }

        /// <summary>
        /// Get teacher by allocated subject
        /// </summary>
        /// <param name="subjectID"></param>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<SubjectAllocationViewModel>> GetAllocatedTeachersForSubject(long subjectID)
        {
            var response=new RepositoryResponse<IEnumerable<SubjectAllocationViewModel>>();

            try
            {
                var allocatedTeachers = _dataContext.Teacher_Subject_Allocation.Where(tsa => tsa.SubjectID == subjectID)
                    .Select(s => new SubjectAllocationViewModel
                    {
                        SubjectAllocationID = s.SubjectAllocationID,
                        TeacherID = s.TeacherID,
                        TeacherName = _dataContext.Teacher.Where(t => t.TeacherID == s.TeacherID).Select(t => t.DisplayName).FirstOrDefault(),
                        TeacherRegNo = _dataContext.Teacher.Where(t => t.TeacherID == s.TeacherID).Select(t => t.TeacherRegNo).FirstOrDefault()
                        
                    }).ToList();

                response.Data = allocatedTeachers;
                if (response.Data.Any())
                {
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "get all allocated subjects"));
                    
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "allocated subject details"));
                   

                }
                return response;



            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "get teacher  allocated for a subject "));
                return response;
            }

        }
        #endregion
    }
}
