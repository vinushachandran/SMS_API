using SMS.BL.Subject.Interface;
using SMS.Data;
using SMS.Model.Subject;
using SMS.Model.Teacher;
using SMS.ViewModel.RepositoryResponse;
using SMS.ViewModel.StaticData;
using SMS.ViewModel.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Subject
{
    public class SubjectRepository:ISubjectRepository
    {
        private readonly SMS_Context _dataContext;  

        public SubjectRepository(SMS_Context datacontext)
        {
            _dataContext = datacontext;
        }

        /// <summary>
        /// Get all subjects list
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<SubjectBO>> GetAllSubjects(bool? isActive = null)
        {
            var response = new RepositoryResponse<IEnumerable<SubjectBO>>();
            try
            {
                var allSubjects = _dataContext.Subject.Select(s => new SubjectBO()
                {
                    SubjectID = s.SubjectID,
                    SubjectCode = s.SubjectCode,
                    Name = s.Name,
                    IsEnable = s.IsEnable,
                });

                if (isActive.HasValue)
                {
                    allSubjects = allSubjects.Where(s => s.IsEnable == isActive.Value);
                }

                response.Data = allSubjects;

                if (response.Data != null)
                {
                    response.Success = true;
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "get all subjects"));
                    return response;
                }

                response.Success = false;
                response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "subjects"));
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "Subjects"));
                return response;
            }
        }


        /// <summary>
        /// Get one subject details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RepositoryResponse<SubjectBO> GetOneSubject(int id)
        {
            var response = new RepositoryResponse<SubjectBO>();

            try
            {
                var oneSubject = _dataContext.Subject.Select(s => new SubjectBO()
                {
                    SubjectID = s.SubjectID,
                    SubjectCode = s.SubjectCode,
                    Name = s.Name,
                    IsEnable = s.IsEnable,
                }).Where(s => s.SubjectID == id).FirstOrDefault();

                response.Data = oneSubject;

                if (response.Data != null)
                {
                    response.Success = true;
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "get this subject"));
                    return response;
                }
                response.Success = false;
                response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "subject"));
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "get one subject's detail"));
                return response;
            }
        }

        /// <summary>
        /// Check one subject is allocated for any teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsSubjectAllocated(long? id)
        {
            var response = new RepositoryResponse<bool>();

            response.Data = _dataContext.Teacher_Subject_Allocation.Any(s => s.SubjectID == id);
            return response;
        }


        /// <summary>
        /// Delete a subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> DeleteSubject(int id)
        {
            var response = new RepositoryResponse<bool>();
            try
            {
                var subject = _dataContext.Subject.SingleOrDefault(s => s.SubjectID == id);
                bool isSubjectUsed = IsSubjectAllocated(id).Data;

                if (subject != null)
                {
                    if (subject.IsEnable)
                    {
                        if (isSubjectUsed)
                        {
                            response.Success = false;
                            response.Message.Add($"This subject {subject.Name} is allocated to a teacher.");
                            return response;
                        }
                        else
                        {
                            _dataContext.Subject.Remove(subject);
                            _dataContext.SaveChanges();
                            response.Success = true;
                            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "delete the subject"));
                            return response;
                        }
                    }
                    else
                    {
                        _dataContext.Subject.Remove(subject);
                        _dataContext.SaveChanges();
                        response.Success = true;
                        response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "delete the subject"));
                        return response;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "subject"));
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "delete the subject's detail"));
                return response;
            }
        }

        /// <summary>
        /// Check if subject code is already available
        /// </summary>
        /// <param name="subjectCode"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsSubjectCodeInUse(string subjectCode)
        {
            var response = new RepositoryResponse<bool>();
            response.Data = _dataContext.Subject.Any(s => s.SubjectCode == subjectCode);
            return response;
        }

        /// <summary>
        /// Check if subject name is already available
        /// </summary>
        /// <param name="subjectName"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> IsSubjectNameInUse(string subjectName)
        {
            var response = new RepositoryResponse<bool>();
            response.Data = _dataContext.Subject.Any(s => s.Name == subjectName);
            return response;
        }

        /// <summary>
        /// Add a new subject
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> AddSubject(SubjectBO subject)
        {
            var response = new RepositoryResponse<bool>();
            bool isSubjectCodeInUse = IsSubjectCodeInUse(subject.SubjectCode).Data;
            bool isSubjectNameInUse = IsSubjectNameInUse(subject.Name).Data;

            try
            {
                if (isSubjectCodeInUse)
                {
                    response.Success = false;
                    response.Message.Add("Subject code already exists!");
                    return response;
                }
                else
                {
                    if (isSubjectNameInUse)
                    {
                        response.Success = false;
                        response.Message.Add("Subject name already exists!");
                        return response;
                    }
                    else
                    {
                        var newSubject = new SubjectBO()
                        {
                            SubjectCode = subject.SubjectCode,
                            Name = subject.Name,
                            IsEnable = subject.IsEnable
                        };
                        _dataContext.Subject.Add(newSubject);
                        _dataContext.SaveChanges();
                        response.Success = true;
                        response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "add a new subject"));
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "add one subject's detail"));
                return response;
            }
        }


        /// <summary>
        /// Check if subject code already exists by excluding a specific subject ID
        /// </summary>
        /// <param name="subjectCode"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> CheckSubjectCodeById(string subjectCode, long? subjectId)
        {
            var response = new RepositoryResponse<bool>();
            response.Data = _dataContext.Subject.Any(s => s.SubjectCode == subjectCode && s.SubjectID != subjectId);
            return response;
        }

        /// <summary>
        /// Check if subject name already exists by excluding a specific subject ID
        /// </summary>
        /// <param name="subjectName"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> CheckSubjectNameById(string subjectName, long? subjectId)
        {
            var response = new RepositoryResponse<bool>();
            response.Data = _dataContext.Subject.Any(s => s.Name == subjectName && s.SubjectID != subjectId);
            return response;
        }


        /// <summary>
        /// Update subject data
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> UpdateSubjectDetails(SubjectBO subject)
        {
            var response = new RepositoryResponse<bool>();

            bool checkSubjectExists = _dataContext.Subject.Any(s => s.SubjectID == subject.SubjectID);
            bool checkSubjectInUse = IsSubjectAllocated(subject.SubjectID).Data;
            bool checkSubjectCodeAvailable = CheckSubjectCodeById(subject.SubjectCode, subject.SubjectID).Data;
            bool checkSubjectNameAvailable = CheckSubjectNameById(subject.Name, subject.SubjectID).Data;

            var editSubject = _dataContext.Subject.SingleOrDefault(s => s.SubjectID == subject.SubjectID);

            try
            {
                if (checkSubjectExists)
                {

                    if (checkSubjectInUse)
                    {
                        response.Success = false;
                        response.Message.Add("Subject allocated for a subject");
                        return response;
                    }

                    if (checkSubjectCodeAvailable)
                    {
                        response.Success = false;
                        response.Message.Add("Subject Code already exists!");
                        return response;
                    }

                    if (checkSubjectNameAvailable)
                    {
                        response.Success = false;
                        response.Message.Add("Subject Name already exists!");
                        return response;
                    }

                    editSubject.SubjectCode = subject.SubjectCode;
                    editSubject.Name = subject.Name;
                    editSubject.IsEnable = subject.IsEnable;
                    _dataContext.SaveChanges();

                    response.Success = true;
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "update subject details"));
                    return response;
                }

                response.Success = false;
                response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "subjects"));
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "update subject details"));
                return response;
            }
        }

        /// <summary>
        /// Search subjects details
        /// </summary>
        /// <param name="subjectSearchViewModel"></param>
        /// <returns></returns>
        public RepositoryResponse<IEnumerable<SubjectBO>> GetSearchSubjects(SubjectSearchViewModel subjectSearchViewModel)
        {
            var response = new RepositoryResponse<IEnumerable<SubjectBO>>();
            var allSubjects = GetAllSubjects();

            try
            {
                if (subjectSearchViewModel.Criteria == "SubjectCode")
                {
                    allSubjects.Data = allSubjects.Data
                        .Where(s => s.SubjectCode.ToUpper().Contains(subjectSearchViewModel.Term.ToUpper()))
                        .ToList();
                }
                else if (subjectSearchViewModel.Criteria == "SubjectName")
                {
                    allSubjects.Data = allSubjects.Data
                        .Where(s => s.Name.ToUpper().Contains(subjectSearchViewModel.Term.ToUpper()))
                        .ToList();
                }
                else
                {
                    allSubjects.Data = allSubjects.Data
                        .Where(s => s.SubjectCode.ToUpper().Contains(subjectSearchViewModel.Term.ToUpper()) ||
                                    s.Name.ToUpper().Contains(subjectSearchViewModel.Term.ToUpper()))
                        .ToList();
                }

                response.Data = allSubjects.Data;

                if (response.Data.Any())
                {
                    response.Success = true;
                    response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "search subjects"));
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, subjectSearchViewModel.Term));
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "search subjects detail"));
                return response;
            }
        }


        /// <summary>
        /// Change the active status of a subject
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public RepositoryResponse<bool> ToggleEnableSubject(long id, bool isEnable)
        {
            var response = new RepositoryResponse<bool>();
            var subject = _dataContext.Subject.SingleOrDefault(s => s.SubjectID == id);
            bool isSubjectUsed = IsSubjectAllocated(id).Data;

            try
            {
                if (subject != null)
                {
                    if (!isSubjectUsed)
                    {
                        if (isEnable)
                        {
                            if (subject.IsEnable == isEnable)
                            {
                                response.Success = false;
                                response.Message.Add("Subject is already enabled");
                                return response;
                            }
                            subject.IsEnable = true;
                            _dataContext.SaveChanges();
                            response.Success = true;
                            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "enabled subject"));
                            return response;
                        }
                        else
                        {
                            if (subject.IsEnable == isEnable)
                            {
                                response.Success = false;
                                response.Message.Add("Subject is already disabled");
                                return response;
                            }
                            subject.IsEnable = false;
                            _dataContext.SaveChanges();
                            response.Success = true;
                            response.Message.Add(string.Format(StaticData.SUCCESS_MESSAGE, "disabled subject"));
                            return response;
                        }
                    }
                    response.Success = false;
                    response.Message.Add($"This subject {subject.SubjectCode} is allocated");
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message.Add(string.Format(StaticData.NO_DATA_FOUND, "subject"));
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message.Add(string.Format(StaticData.SOMETHING_WENT_WRONG, "toggle subject's status"));
                return response;
            }
        }

    }
}
