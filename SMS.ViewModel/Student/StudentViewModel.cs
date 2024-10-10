using SMS.Model.Student;

namespace SMS.ViewModel.Student
{
    public class StudentViewModel
    {
        /// <summary>
		/// get all students as list
		/// </summary>
		public IEnumerable<StudentBO> AllStudentsList { get; set; }

        /// <summary>
        /// get one student details
        /// </summary>
        public StudentBO StudentDetail { get; set; }

        /// <summary>
        /// Get student by active state
        /// </summary>
        public bool? IsActive { get; set; }

        public int? totalPages {  get; set; }




    }
}
