using SMS.Model.Subject;
using SMS.ViewModel.RepositoryResponse;
using SMS.ViewModel.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Subject.Interface
{
    public interface ISubjectRepository
    {
        /// <summary>
        /// Get all subject list
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<SubjectBO>> GetAllSubjects(bool? isActive = null);

        /// <summary>
        /// Get one subject details by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RepositoryResponse<SubjectBO> GetOneSubject(int id);

        /// <summary>
        /// Check one subject is allocated for any teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsSubjectAllocated(long? id);

        /// <summary>
        /// Delete a subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RepositoryResponse<bool> DeleteSubject(int id);

        /// <summary>
        /// Check if subject code is already available
        /// </summary>
        /// <param name="subjectCode"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsSubjectCodeInUse(string subjectCode);

        /// <summary>
        /// Check if subject name is already available
        /// </summary>
        /// <param name="subjectName"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsSubjectNameInUse(string subjectName);

        /// <summary>
        /// Add a new subject
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        RepositoryResponse<bool> AddSubject(SubjectBO subject);

        /// <summary>
        /// Check if subject code already exists by excluding a specific subject ID
        /// </summary>
        /// <param name="subjectCode"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        RepositoryResponse<bool> CheckSubjectCodeById(string subjectCode, long? subjectId);

        /// <summary>
        /// Check if subject name already exists by excluding a specific subject ID
        /// </summary>
        /// <param name="subjectName"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        RepositoryResponse<bool> CheckSubjectNameById(string subjectName, long? subjectId);

        /// <summary>
        /// Update subject data
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        RepositoryResponse<bool> UpdateSubjectDetails(SubjectBO subject);


        /// <summary>
        /// Search subjects details
        /// </summary>
        /// <param name="subjectSearchViewModel"></param>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<SubjectBO>> GetSearchSubjects(SubjectSearchViewModel subjectSearchViewModel);

        /// <summary>
        /// Change the active status of a subject
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        RepositoryResponse<bool> ToggleEnableSubject(long id, bool isEnable);
    }
}
