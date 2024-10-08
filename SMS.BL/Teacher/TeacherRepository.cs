/// <summary>
/// <author>Vinusha</author>
/// <date>07 October 2024</date> 
/// <Purpose>This file implements the TeacherRepository class to handle teacher-related operations.</Purpose> 
/// </summary>
using SMS.BL.Teacher.Interface;
using SMS.Data;
using SMS.Model.Teacher;
using SMS.ViewModel.RepositoryResponse;
using SMS.ViewModel.Search;
using SMS.ViewModel.StaticData;
using SMS.ViewModel.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Teacher
{
    public class TeacherRepository:ITeacherRepository
    {
        private readonly SMS_Context _dataContext;

        public TeacherRepository(SMS_Context dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Get all students list
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<TeacherBO>> GetAllTeachers(int pageNumber, int numberOfRecoards, bool? isActive = null)
        {
            var response = new RepositoryResponse<IEnumerable<TeacherBO>>();
            try
            {
                var allTeachers = _dataContext.Teacher.Select(t => new TeacherBO()
                {
                    TeacherID = t.TeacherID,
                    TeacherRegNo = t.TeacherRegNo,
                    FirstName = t.FirstName,
                    MiddleName = t.MiddleName,
                    LastName = t.LastName,
                    DisplayName = t.DisplayName,
                    Email = t.Email,
                    DOB = t.DOB,
                    Gender = t.Gender,
                    ContactNo = t.ContactNo,
                    Address = t.Address,
                    IsEnable = t.IsEnable,
                });

                if (isActive.HasValue)
                {
                    allTeachers = allTeachers.Where(t => t.IsEnable == isActive.Value);
                }

                var totalTeacherCount = allTeachers.Count();
                response.TotalPages = (int)Math.Ceiling((double)totalTeacherCount / numberOfRecoards);
                response.Data = allTeachers.Skip((pageNumber - 1) * numberOfRecoards).Take(numberOfRecoards).ToList();

                if (response.Data.Any())
                {                    
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "get all teachers"));
                    return response;
                }

                response.Success = false;
                response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "teachers"));
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "Teachers"));
                return response;
            }
        }

        /// <summary>
        /// Get one teacher details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RepositoryResponse<TeacherBO> GetOneTeacher(int id)
        {
            var response = new RepositoryResponse<TeacherBO>();

            try
            {
                var oneTeacher = _dataContext.Teacher.Select(t => new TeacherBO()
                {
                    TeacherID = t.TeacherID,
                    TeacherRegNo = t.TeacherRegNo,
                    FirstName = t.FirstName,
                    MiddleName = t.MiddleName,
                    LastName = t.LastName,
                    DisplayName = t.DisplayName,
                    Email = t.Email,
                    DOB = t.DOB,
                    Gender = t.Gender,
                    ContactNo = t.ContactNo,
                    Address = t.Address,
                    IsEnable = t.IsEnable,
                }).Where(t => t.TeacherID == id).FirstOrDefault();

                response.Data = oneTeacher;

                if (response.Data != null)
                {                    
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "get this teacher"));
                    return response;
                }
                response.Success = false;
                response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "teacher"));
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "get one teacher's detail"));
                return response;
            }
        }


        public RepositoryResponse<bool> ISTeacherAllocated(long? id)
        {
            var response = new RepositoryResponse<bool>();

            response.Success = _dataContext.Teacher_Subject_Allocation.Any(s => s.TeacherID == id);
            return response;
        }

        /// <summary>
        /// Delete a teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> DeleteTeacher(int id)
        {
            var response = new RepositoryResponse<bool>();
            try
            {
                var teacher = _dataContext.Teacher.SingleOrDefault(t => t.TeacherID == id);
                bool isTeacherUsed = ISTeacherAllocated(id).Success;

                if (teacher != null)
                {
                    if (teacher.IsEnable)
                    {
                        if (isTeacherUsed)
                        {
                            response.Success = false;
                            response.Message.Add(string.Format("This teacher " + teacher.DisplayName + " is allocated for a subject"));
                            
                        }
                        else
                        {
                            _dataContext.Teacher.Remove(teacher);
                            _dataContext.SaveChanges();                           
                            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "delete the teacher"));
                            
                        }
                    }
                    else
                    {
                        _dataContext.Teacher.Remove(teacher);
                        _dataContext.SaveChanges();                        
                        response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "delete the teacher"));
                        
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "teachers"));
                    
                }
                return response;
            }
            catch 
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "delete the teacher's detail"));
                return response;
            }
        }

        /// <summary>
        /// Check reg number already available
        /// </summary>
        /// <param name="teacherRegNo"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsTeacherRegNoInUse(string teacherRegNo)
        {
            var response = new RepositoryResponse<bool>();
            response.Success = _dataContext.Teacher.Any(t => t.TeacherRegNo == teacherRegNo);
            return response;
        }

        /// <summary>
        /// Check teacher name already available
        /// </summary>
        /// <param name="teacherName"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsTeacherNameInUse(string teacherName)
        {
            var response = new RepositoryResponse<bool>();
            response.Success = _dataContext.Teacher.Any(t => t.DisplayName == teacherName);
            return response;
        }

        /// <summary>
        /// Check teacher email already available
        /// </summary>
        /// <param name="teacherEmail"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsTeacherEmailInUse(string teacherEmail)
        {
            var response = new RepositoryResponse<bool>();
            response.Success = _dataContext.Teacher.Any(t => t.Email == teacherEmail);
            return response;
        }

        /// <summary>
        /// Add a new teacher
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> AddTeacher(TeacherBO teacher)
        {
            var response = new RepositoryResponse<bool>();
            bool isTeacherRegNoInUse = IsTeacherRegNoInUse(teacher.TeacherRegNo).Success;
            bool isTeacherNameInUse = IsTeacherNameInUse(teacher.DisplayName).Success;
            bool isTeacherEmailInUse = IsTeacherEmailInUse(teacher.Email).Success;

            try
            {
                if (isTeacherRegNoInUse)
                {
                    response.Success = false;
                    response.Message.Add("Teacher RegNo already exists!");
                    return response;
                }
                else
                {
                    if (isTeacherNameInUse)
                    {
                        response.Success = false;
                        response.Message.Add("Teacher Display name already exists!");
                        return response;
                    }
                    else
                    {
                        if (isTeacherEmailInUse)
                        {
                            response.Success = false;
                            response.Message.Add("Teacher Email already exists!");
                            return response;
                        }
                        else
                        {
                            var newTeacher = new TeacherBO()
                            {
                                TeacherRegNo = teacher.TeacherRegNo,
                                FirstName = teacher.FirstName,
                                MiddleName = teacher.MiddleName,
                                LastName = teacher.LastName,
                                DisplayName = teacher.DisplayName,
                                Address = teacher.Address,
                                DOB = teacher.DOB,
                                Email = teacher.Email,
                                Gender = teacher.Gender,
                                ContactNo = teacher.ContactNo,
                                IsEnable = teacher.IsEnable
                            };
                            _dataContext.Teacher.Add(newTeacher);
                            _dataContext.SaveChanges();                            
                            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "add a new teacher"));
                            return response;
                        }
                    }
                }
            }
            catch 
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "add one teacher's detail"));
                return response;
            }
        }

        /// <summary>
        /// Check teacher reg no already exists
        /// </summary>
        /// <param name="teacherRegNo"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> CheckTeacherRegNoById(string teacherRegNo, long? teacherId)
        {
            var response = new RepositoryResponse<bool>();
            response.Success = _dataContext.Teacher.Any(t => t.TeacherRegNo == teacherRegNo && t.TeacherID != teacherId);
            return response;
        }

        /// <summary>
        /// Check teacher name already exists
        /// </summary>
        /// <param name="teacherName"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> CheckTeacherNameById(string teacherName, long? teacherId)
        {
            var response = new RepositoryResponse<bool>();
            response.Success = _dataContext.Teacher.Any(t => t.DisplayName == teacherName && t.TeacherID != teacherId);
            return response;
        }

        /// <summary>
        /// Check teacher email already exists
        /// </summary>
        /// <param name="teacherEmail"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> CheckTeacherEmailById(string teacherEmail, long? teacherId)
        {
            var response = new RepositoryResponse<bool>();
            response.Success = _dataContext.Teacher.Any(t => t.Email == teacherEmail && t.TeacherID != teacherId);
            return response;
        }

        /// <summary>
        /// Update teacher data
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> UpdateTeacherDetails(TeacherBO teacher)
        {
            var response = new RepositoryResponse<bool>();
                        
            bool checkTeacherInUse = ISTeacherAllocated(teacher.TeacherID).Success;
            bool checkRegNoAvailable = CheckTeacherRegNoById(teacher.TeacherRegNo, teacher.TeacherID).Success;
            bool checkNameAvailable = CheckTeacherNameById(teacher.DisplayName, teacher.TeacherID).Success;
            bool checkEmailAvailable = CheckTeacherEmailById(teacher.Email, teacher.TeacherID).Success;

            var editTeacher = _dataContext.Teacher.SingleOrDefault(t => t.TeacherID == teacher.TeacherID);

            try
            {
                if (editTeacher!=null)
                {
                    if (checkTeacherInUse)
                    {
                        response.Success = false;
                        response.Message.Add("Teacher allocated for a subject");
                        return response;
                    }

                    if (checkRegNoAvailable)
                    {
                        response.Success = false;
                        response.Message.Add("Teacher RegNo already exists!");
                        return response;
                    }

                    if (checkNameAvailable)
                    {
                        response.Success = false;
                        response.Message.Add("Teacher Display name already exists!");
                        return response;
                    }

                    if (checkEmailAvailable)
                    {
                        response.Success = false;
                        response.Message.Add("Teacher email already exists!");
                        return response;
                    }

                    editTeacher.TeacherRegNo = teacher.TeacherRegNo;
                    editTeacher.FirstName = teacher.FirstName;
                    editTeacher.MiddleName = teacher.MiddleName;
                    editTeacher.LastName = teacher.LastName;
                    editTeacher.DisplayName = teacher.DisplayName;
                    editTeacher.Email = teacher.Email;
                    editTeacher.Gender = teacher.Gender;
                    editTeacher.DOB = teacher.DOB;
                    editTeacher.Address = teacher.Address;
                    editTeacher.ContactNo = teacher.ContactNo;
                    editTeacher.IsEnable = teacher.IsEnable;
                    _dataContext.SaveChanges();
                                        
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "update teacher details"));
                    return response;
                }

                response.Success = false;
                response.Message.Add(string.Format( StaticData.NO_DATA_FOUND, "teachers"));
                return response;
            }
            catch 
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "update teacher details"));
                return response;
            }
        }

        /// <summary>
        /// Search teachers details
        /// </summary>
        /// <param name="teacherSearchViewModel"></param>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<TeacherBO>> GetSearchTeachers(SearchViewModel teacherSearchViewModel)
        {
            var response = new RepositoryResponse<IEnumerable<TeacherBO>>();
            var allTeachers = GetAllTeachers(1,5); 

            try
            {
                if (teacherSearchViewModel.Criteria == "TeacherReg")
                {
                    allTeachers.Data = allTeachers.Data
                        .Where(t => t.TeacherRegNo.ToUpper().Contains(teacherSearchViewModel.Term.ToUpper()))
                        .ToList();
                }
                else if (teacherSearchViewModel.Criteria == "DisplayName")
                {
                    allTeachers.Data = allTeachers.Data
                        .Where(t => t.DisplayName.ToUpper().Contains(teacherSearchViewModel.Term.ToUpper()))
                        .ToList();
                }
                else
                {
                    allTeachers.Data = allTeachers.Data
                        .Where(t => t.TeacherRegNo.ToUpper().Contains(teacherSearchViewModel.Term.ToUpper()) ||
                                    t.DisplayName.ToUpper().Contains(teacherSearchViewModel.Term.ToUpper()))
                        .ToList();
                }
                response.Data = allTeachers.Data;

                if (response.Data.Any())
                {                    
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "search teachers"));
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, teacherSearchViewModel.Term));
                    return response;
                }
            }
            catch 
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "search teachers detail"));
                return response;
            }
        }

        /// <summary>
        /// Change the active status of a teacher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> ToggleEnableTeacher(long id, bool isEnable)
        {
            var response = new RepositoryResponse<bool>();
            var teacher = _dataContext.Teacher.SingleOrDefault(t => t.TeacherID == id);
            bool isTeacherUsed = ISTeacherAllocated(id).Success;

            try
            {
                if (teacher != null)
                {
                    if (!isTeacherUsed)
                    {
                        if (isEnable)
                        {
                            if (teacher.IsEnable == isEnable)
                            {
                                response.Success = false;
                                response.Message.Add("Teacher data already enabled");
                                return response;
                            }
                            teacher.IsEnable = true;
                            _dataContext.SaveChanges();                            
                            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "enabled"));
                            return response;
                        }
                        else
                        {
                            if (teacher.IsEnable == isEnable)
                            {
                                response.Success = false;
                                response.Message.Add("Teacher data already disabled");
                                return response;
                            }
                            teacher.IsEnable = false;
                            _dataContext.SaveChanges();                            
                            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "disabled"));
                            return response;
                        }
                    }
                    response.Success = false;
                    response.Message.Add(string.Format("This teacher" + teacher.DisplayName + " allocated for a subject"));
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "this teacher"));
                    return response;
                }


                
            }
            catch 
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "toggle teacher's status"));
                return response;
            }
        }

    }
}
