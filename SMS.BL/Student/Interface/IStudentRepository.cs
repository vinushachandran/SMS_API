/// <summary>
///
/// </summary>
/// <author>Vinusha</author>
///
using SMS.Model.Student;
using SMS.ViewModel.RepositoryResponse;
using SMS.ViewModel.Search;
using SMS.ViewModel.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BL.Student.Interface
{
    public interface IStudentRepository
    {
        /// <summary>
        /// Get all student Details
        /// </summary>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<StudentBO>> GetAllStudents(int pageNumber,int numberOfRecoards, bool? isActive = null);


        /// <summary>
        /// Get one student details by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RepositoryResponse<StudentBO> GetOneStudent(int id);

        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RepositoryResponse<bool> DeleteStudent(int id);

        /// <summary>
        /// Chaeck a student is allocated for any subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsStudentAllocated(long? id);


        /// <summary>
        /// Check this reg number address already in use
        /// </summary>
        /// <param name="studentRegNo"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsStudentRegNoInUse(string studentRegNo);

        /// <summary>
        /// Check this name address already in use
        /// </summary>
        /// <param name="studentName"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsStudentNameInUse(string studentName);

        /// <summary>
        /// Check this name address already in use
        /// </summary>
        /// <param name="studentName"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsStudentEmailInUse(string studentEmail);


        /// <summary>
        /// Add a new students
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        RepositoryResponse<bool> AddStudent(StudentBO student);

        /// <summary>
        /// Check student regNo by it's id for edit student
        /// </summary>
        /// <param name="studentRegNo"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        RepositoryResponse<bool> CheckStudentRegNoById(string studentRegNo,long? studentId);

        /// <summary>
        /// Check student regNo by it's id for edit student
        /// </summary>
        /// <param name="studentName"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        RepositoryResponse<bool> CheckStudentNameById(string studentName, long? studentId);

        /// <summary>
        /// Check student regNo by it's id for edit student
        /// </summary>
        /// <param name="studentRegNo"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        RepositoryResponse<bool> CheckStudentEmailById(string studentEmail, long? studentId);

        /// <summary>
        /// udedate student details by id
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        RepositoryResponse <bool> UpdateStudentDetails(StudentBO student);

        /// <summary>
        /// Search student details
        /// </summary>
        /// <param name="studentSearchViewModel"></param>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<StudentBO>> GetSearchStudents(SearchViewModel studentSearchViewModel);

        /// <summary>
        /// Change the active state of a student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        RepositoryResponse<bool> ToggleEnable(long id, bool isEnable);
       
    }
}
