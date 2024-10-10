using SMS.Model.Teacher;
using SMS.ViewModel.RepositoryResponse;
using SMS.ViewModel.Search;

namespace SMS.BL.Teacher.Interface
{
    public interface ITeacherRepository
    {
        /// <summary>
        /// Get all students details
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<TeacherBO>> GetAllTeachers(int pageNumber, int numberOfRecoards, bool? isActive = null);

        /// <summary>
        /// Get one student details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RepositoryResponse<TeacherBO> GetOneTeacher(long id);

        /// <summary>
        /// Check teacher allocated for any subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RepositoryResponse<bool> ISTeacherAllocated(long? id);

        /// <summary>
        /// Delete teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RepositoryResponse<bool> DeleteTeacher(long id);

        /// <summary>
        /// Check teacher reg number already exsist
        /// </summary>
        /// <param name="teacherRegNo"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsTeacherRegNoInUse(string teacherRegNo);

        /// <summary>
        /// Check teacher Name already exsist
        /// </summary>
        /// <param name="teacherName"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsTeacherNameInUse(string teacherName);

        /// <summary>
        /// Check teacher email already exsist
        /// </summary>
        /// <param name="teacherEmail"></param>
        /// <returns></returns>
        RepositoryResponse<bool> IsTeacherEmailInUse(string teacherEmail);

        /// <summary>
        /// Add a new teacher
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        RepositoryResponse<bool> AddTeacher(TeacherBO teacher);

        /// <summary>
        /// Check teacher Name already exsist by id
        /// </summary>
        /// <param name="teacherRegNo"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        RepositoryResponse<bool> CheckTeacherRegNoById(string teacherRegNo, long? teacherId);

        /// <summary>
        /// Check teacher Name already exsist by id
        /// </summary>
        /// <param name="teacherName"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        RepositoryResponse<bool> CheckTeacherNameById(string teacherName, long? teacherId);

        /// <summary>
        /// Check teacher email already exsist by id
        /// </summary>
        /// <param name="teacherEmail"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        RepositoryResponse<bool> CheckTeacherEmailById(string teacherEmail, long? teacherId);
        /// <summary>
        /// Update one teacher deatals
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        RepositoryResponse<bool> UpdateTeacherDetails(TeacherBO teacher);

        /// <summary>
        /// Seach teacher
        /// </summary>
        /// <param name="teacherSearchViewModel"></param>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<TeacherBO>> GetSearchTeachers(SearchViewModel teacherSearchViewModel);

        /// <summary>
        /// Chage tha active state of teacher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        RepositoryResponse<bool> ToggleEnableTeacher(long id, bool isEnable);

    }
}
