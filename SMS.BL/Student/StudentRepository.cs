/// <summary>
/// <author>Vinusha</author>
/// <date>07 October 2024</date> 
/// <Purpose>This file implements the StudentRepository class to handle student-related operations.</Purpose> 
/// </summary>
using SMS.BL.Student.Interface;
using SMS.Data;
using SMS.Model.Student;
using SMS.ViewModel.RepositoryResponse;
using SMS.ViewModel.Search;
using SMS.ViewModel.StaticData;
using SMS.ViewModel.Student;

namespace SMS.BL.Student
{
    public class StudentRepository:IStudentRepository
    {
        private readonly SMS_Context _dataContext;

        public StudentRepository(SMS_Context dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Get all students details
        /// </summary>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<StudentBO>> GetAllStudents(int pageNumber, int numberOfRecoards, bool? isActive=null)
        {
            var response=new RepositoryResponse<IEnumerable<StudentBO>>();
            try
            {
                var allStudents = _dataContext.Student.Select(s => new StudentBO()
                {
                    StudentID=s.StudentID,
                    StudentRegNo=s.StudentRegNo,
                    FirstName=s.FirstName,
                    MiddleName=s.MiddleName,
                    LastName=s.LastName,
                    DisplayName=s.DisplayName,
                    Email=s.Email,
                    DOB=s.DOB,
                    Gender=s.Gender,
                    ContactNo=s.ContactNo,
                    Address=s.Address,
                    IsEnable=s.IsEnable,
                });

                if(isActive.HasValue)
                {
                    allStudents=allStudents.Where(s=>s.IsEnable==isActive.Value);

                }
                var totalStudentsCount=allStudents.Count();
                response.TotalPages= (int)Math.Ceiling((double)totalStudentsCount/numberOfRecoards);
                response.Data = allStudents.Skip((pageNumber-1)*numberOfRecoards).Take(numberOfRecoards).ToList();

                

                if(response.Data.Any())
                {
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "get all students"));
                    return response;
                }
                response.Success = false;
                response.Message.Add(string.Format(StaticData.NO_DATA_FOUND,"students"));
                return response;

            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "Students"));
                return response ;
            }          

        }


        /// <summary>
        /// Get one student details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RepositoryResponse<StudentBO> GetOneStudent(int id)
        {
            var response = new RepositoryResponse<StudentBO>();

            try
            {
                var oneStudent= _dataContext.Student.Select(s => new StudentBO()
                {
                    StudentID = s.StudentID,
                    StudentRegNo = s.StudentRegNo,
                    FirstName = s.FirstName,
                    MiddleName = s.MiddleName,
                    LastName = s.LastName,
                    DisplayName = s.DisplayName,
                    Email = s.Email,
                    DOB = s.DOB,
                    Gender = s.Gender,
                    ContactNo = s.ContactNo,
                    Address = s.Address,
                    IsEnable = s.IsEnable,
                }).Where(s=>s.StudentID == id).FirstOrDefault();

                response.Data = oneStudent;

                if (response.Data != null)
                {                    
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "get this student"));
                    return response;
                }
                response.Success = false;
                response.Message.Add(string.Format(StaticData.NO_DATA_FOUND,"student"));
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "get one students detail"));
                return response;
            }
           
        }


        /// <summary>
        /// Check a student is allocated for any subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsStudentAllocated(long? id)
        {
            var response = new RepositoryResponse<bool>();

            response.Success = _dataContext.Student_Subject_Teacher_Allocation.Any(s => s.StudentID == id);
            return response;


        }
        /// <summary>
        /// Delete a student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public RepositoryResponse<bool> DeleteStudent(int id)
        {
            var response = new RepositoryResponse<bool>();
            try
            {
                var student = _dataContext.Student.SingleOrDefault(s => s.StudentID == id);
                bool isStudentUsed=IsStudentAllocated(id).Success;

                if (student != null)
                {
                    if(student.IsEnable)
                    {
                        if (isStudentUsed)
                        {
                            response.Success = false;
                            response.Message.Add(string.Format("This student "+student.DisplayName+" is allocated for a subject"));
                            return response ;
                        }
                        else
                        {                            
                            _dataContext.Student.Remove(student);
                            _dataContext.SaveChanges();                         
                            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "delete the student"));
                            return response;

                        }
                    }
                    else
                    {
                        _dataContext.Student.Remove(student);
                        _dataContext.SaveChanges();                        
                        response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "delete the student"));
                        return response;
                    }
                    
                    
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND,"students"));
                    return response;

                }

            }
            catch 
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "delete the students detail"));
                return response;
            }

        }

        /// <summary>
        /// Check reg number already available
        /// </summary>
        /// <param name="studentRegNo"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsStudentRegNoInUse(string studentRegNo)
        {
            var response = new RepositoryResponse<bool>();
            response.Success=_dataContext.Student.Any(s=>s.StudentRegNo == studentRegNo);
            return response;

        }
        /// <summary>
        /// Check student name already available
        /// </summary>
        /// <param name="studentName"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsStudentNameInUse(string studentName)
        {
            var response = new RepositoryResponse<bool>();
            response.Success= _dataContext.Student.Any(s=>s.DisplayName == studentName);  
            return response;

        }

        /// <summary>
        /// Check student email already available
        /// </summary>
        /// <param name="studentEmail"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsStudentEmailInUse(string studentEmail)
        {
            var response = new RepositoryResponse<bool>();
            response.Success = _dataContext.Student.Any(s=> s.Email == studentEmail);
            return response;

        }


        /// <summary>
        /// Add a new student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> AddStudent(StudentBO student)
        {
            var response = new RepositoryResponse<bool>();
            bool isStudentRegNoInUse = IsStudentRegNoInUse(student.StudentRegNo).Success;
            bool isStudentNameInUse = IsStudentNameInUse(student.DisplayName).Success;
            bool isStudentEmailInUse = IsStudentEmailInUse(student.Email). Success;

            try
            {
                if (isStudentRegNoInUse)
                {
                    response.Success = false;
                    response.Message.Add(string.Format("Student RegNo already exsist!"));
                    return response;
                }
                else
                {
                    if (isStudentNameInUse)
                    {
                        response.Success = false;
                        response.Message.Add(string.Format("Student Display name already exsist!"));
                        return response;
                    }
                    else
                    {
                        if (isStudentEmailInUse)
                        {
                            response.Success = false;
                            response.Message.Add(string.Format("Student Email already exsist!"));
                            return response;
                        }
                        else
                        {
                            var newStudent=new StudentBO();
                            newStudent.StudentRegNo= student.StudentRegNo;
                            newStudent.FirstName= student.FirstName;
                            newStudent.MiddleName= student.MiddleName;
                            newStudent.LastName= student.LastName;
                            newStudent.DisplayName=student.DisplayName;
                            newStudent.Address= student.Address;
                            newStudent.DOB= student.DOB;
                            newStudent.Email = student.Email;
                            newStudent.Gender= student.Gender;
                            newStudent.ContactNo= student.ContactNo;
                            newStudent.IsEnable= student.IsEnable;
                            _dataContext.Student.Add(newStudent);
                            _dataContext.SaveChanges();                            
                            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "add a new student"));
                            return response;
                        }
                    }
                }            
               
            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "add one students detail"));
                return response ;
            }
        }

        /// <summary>
        /// Check student reg no already exsist
        /// </summary>
        /// <param name="studentRegNo"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> CheckStudentRegNoById(string studentRegNo, long? studentId)
        {
            var response = new RepositoryResponse<bool>();
            response.Success = _dataContext.Student.Any(s => s.StudentRegNo == studentRegNo && s.StudentID!=studentId);
            return response;
        }

        /// <summary>
        /// Check student name already exsist
        /// </summary>
        /// <param name="studentName"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> CheckStudentNameById(string studentName, long? studentId)
        {
            var response = new RepositoryResponse<bool>();
            response.Success = _dataContext.Student.Any(s => s.DisplayName == studentName && s.StudentID != studentId);
            return response;

        }

        /// <summary>
        /// Check student email already exist
        /// </summary>
        /// <param name="studentEmail"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> CheckStudentEmailById(string studentEmail, long? studentId)
        {
            var response = new RepositoryResponse<bool>();
            response.Success = _dataContext.Student.Any(s => s.Email == studentEmail && s.StudentID != studentId);
            return response;
        }

        /// <summary>
        /// Update student data
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>

        public RepositoryResponse<bool> UpdateStudentDetails(StudentBO student)
        {
            var response = new RepositoryResponse<bool>();

            bool checkStudentInUse = IsStudentAllocated(student.StudentID).Success;

            bool checkRegNoAvailable = CheckStudentRegNoById(student.StudentRegNo, student.StudentID).Success;
            bool checkNameAvailable = CheckStudentNameById(student.DisplayName, student.StudentID).Success;
            bool checkEmailAvailable = CheckStudentEmailById(student.Email, student.StudentID).Success;

            var editStudent=_dataContext.Student.SingleOrDefault(s=>s.StudentID == student.StudentID);

            try
            {
                if (editStudent!=null)
                {
                    if (checkStudentInUse)
                    {
                        response.Success = false;
                        response.Message.Add(string.Format("Student allocated for a subject"));
                        return response;    
                    }

                    if (checkRegNoAvailable)
                    {
                        response.Success = false;
                        response.Message.Add(string.Format("Student RegNo already exsist!"));
                        return response;

                    }
                    if (checkNameAvailable)
                    {
                        response.Success = false;
                        response.Message.Add(string.Format("Student Display name already exsist!"));
                        return response;

                    }
                    if (checkEmailAvailable)
                    {
                        response.Success = false;
                        response.Message.Add(string.Format("Student Display name already exsist!"));
                        return response;
                    }

                    editStudent.StudentRegNo= student.StudentRegNo;
                    editStudent.FirstName = student.FirstName;
                    editStudent.MiddleName = student.MiddleName;
                    editStudent.LastName = student.LastName;
                    editStudent.DisplayName = student.DisplayName;
                    editStudent.Email = student.Email;
                    editStudent.Gender = student.Gender;
                    editStudent.DOB = student.DOB;
                    editStudent.Address = student.Address;
                    editStudent.ContactNo = student.ContactNo;
                    editStudent.IsEnable = student.IsEnable;
                    _dataContext.SaveChanges();                                        
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE,"update student details"));
                    return response;


                }
                response.Success = false;
                response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "students"));
                return response;


            }
            catch 
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "update one students detail"));
                return response;
            }

        }


        /// <summary>
        /// Search students details
        /// </summary>
        /// <param name="studentSearchViewModel"></param>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<StudentBO>> GetSearchStudents(SearchViewModel studentSearchViewModel)
        {
            var response = new RepositoryResponse<IEnumerable<StudentBO>>();
            var allStudents = GetAllStudents(1,5);

            try
            {
                if (studentSearchViewModel.Criteria == "StudentReg")
                {
                    allStudents.Data = allStudents.Data.Where(s => s.StudentRegNo.ToUpper().Contains( studentSearchViewModel.Term.ToUpper())).ToList();
                }
                else if (studentSearchViewModel.Criteria == "DisplayName")
                {
                    allStudents.Data = allStudents.Data.Where(s => s.DisplayName.ToUpper().Contains(studentSearchViewModel.Term.ToUpper())).ToList();
                }
                else
                {
                    allStudents.Data = allStudents.Data.Where(s => s.StudentRegNo.ToUpper().Contains(studentSearchViewModel.Term.ToUpper()) || s.DisplayName.ToUpper().Contains(studentSearchViewModel.Term.ToUpper())).ToList();
                }
                response.Data = allStudents.Data;
                response.TotalPages = 1;

                if (response.Data.Any())
                {                 
                 
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "searchDetails"));
                    return response;
                }
                else
                {
                    response.Success=false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, studentSearchViewModel.Term));
                    return response;
                }

                
                
            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "search students detail"));
                return response;
            }
        }

        /// <summary>
        /// Chnge the active status of student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> ToggleEnable(long id, bool isEnable)
        {
            var response = new RepositoryResponse<bool>();
            var student=_dataContext.Student.SingleOrDefault(s=>s.StudentID== id);

            try
            {
                if (student!=null)
                {
                    if (isEnable)
                    {
                        if (student.IsEnable == isEnable)
                        {
                            response.Success = false;
                            response.Message.Add(string.Format("Student data already enabled"));
                            return response;
                        }
                        student.IsEnable = true;
                        _dataContext.SaveChanges();
                        response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "enabled"));
                        return response;
                    }
                    else
                    {
                        if (student.IsEnable == isEnable)
                        {
                            response.Success = false;
                            response.Message.Add(string.Format("Student data already disabled"));
                            return response;
                        }
                        student.IsEnable=false;
                        _dataContext.SaveChanges();
                        response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "disabled"));
                        return response;
                    }
                }
                response.Success = false;
                response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "this student"));
                return response;


            }
            catch
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "toggle students status"));
                return response;
            }
        }



    }
}
