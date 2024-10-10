using SMS.Model.Teacher;

namespace SMS.ViewModel.Teacher
{
    public class TeacherViewModel
    {
        /// <summary>
        /// get all teachers as list
        /// </summary>
        public IEnumerable<TeacherBO> TeachersList { get; set; }

        /// <summary>
        /// get one teacher's details
        /// </summary>
        public TeacherBO TeacherDetail { get; set; }

        public int? totalPages { get; set; }
    }
}
