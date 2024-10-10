using SMS.Model.Allocation;
using SMS.Model.Subject;
using SMS.ViewModel.Allocation;
using SMS.ViewModel.RepositoryResponse;
using SMS.ViewModel.Search;

namespace SMS.BL.Allocation.Interface
{
    public interface IAllocationReposiory
    {
        #region Subject Allocation
        /**************************************Subject Allocation******************************************************************/
        /// <summary>
        /// Get all subject allocations
        /// </summary>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<SubjectAllocationGroupBySubjectViewModel>> GetAllSubjectAllocation();

        /// <summary>
        /// Get one subject allocation detail by it's id
        /// </summary>
        /// <param name="SubjectAllocationID"></param>
        /// <returns></returns>
        RepositoryResponse<SubjectAllocationBO> GetSubjectAllocationByID(long subjectAllocationID);



        /// <summary>
        /// Insert or Update Subject Allocation
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>
        RepositoryResponse<bool> UpsertSubjectAllocation(SubjectAllocationBO subjectAllocation);

        /// <summary>
        /// Check the subject allocation already created
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="subjectID"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsSubjectAllocationAlreadyExist(long teacherID, long subjectID);

        /// <summary>
        /// Delete Subject Allocation
        /// </summary>
        /// <param name="subjectAllocationID"></param>
        /// <returns></returns>
        RepositoryResponse<bool> DeleteSubjectAllocation(long subjectAllocationID);

        /// <summary>
        /// Check this subject allocation allocated for any student
        /// </summary>
        /// <param name="subjectAllocationID"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsSubjectAllocationInUsed(long subjectAllocationID);

        /// <summary>
        /// Search a subject allocation
        /// </summary>
        /// <param name="allocationSearchViewModel"></param>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<SubjectAllocationGroupBySubjectViewModel>> GetSearchSubjectAllocation(SearchViewModel allocationSearchViewModel);


        #endregion



        #region Student Allocation
        /**************************************Student Allocation******************************************************************/
        /// <summary>
        /// Get all subject allocations
        /// </summary>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<StudentAllocationGroupByStudentViewModel>> GetAllStudentAllocation();

        /// <summary>
        /// Get allocated subject details
        /// </summary>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<SubjectBO>> GetAllocatedSubjectDetails();

        /// <summary>
        /// Get teacher by allocated subject
        /// </summary>
        /// <param name="subjectID"></param>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<SubjectAllocationViewModel>> GetAllocatedTeachersForSubject(long subjectID);

        /// <summary>
        /// Inser or update Subject Allocation
        /// </summary>
        /// <param name="subjectAllocation"></param>
        /// <returns></returns>
        RepositoryResponse<bool> UpsertStudentAllocation(StudentAllocationBO studentAllocation);

        /// <summary>
        /// Check the subject allocation already created
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="subjectID"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsStudentAllocationAlreadyExist(long subjectAllocationID, long studentID);

        /// <summary>
        /// Delete Student Allocation
        /// </summary>
        /// <param name="studentAllocationID"></param>
        /// <returns></returns>
        RepositoryResponse<bool> DeleteStudentAllocation(long studentAllocationID);

        /// <summary>
        /// Search a student allocation
        /// </summary>
        /// <param name="allocationSearchViewModel"></param>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<StudentAllocationGroupByStudentViewModel>> GetSearchStudentAllocation(SearchViewModel allocationSearchViewModel);
        #endregion
    }
}
