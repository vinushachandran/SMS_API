using SMS.Model.Subject;

namespace SMS.ViewModel.Subject
{
    public class SubjectViewModel
    {
        /// <summary>
        /// get all subjects as list
        /// </summary>
        public IEnumerable<SubjectBO> SubjectList { get; set; }

        /// <summary>
        /// get one subject's details
        /// </summary>
        public SubjectBO SubjectDetail { get; set; }

        public int? totalPages { get; set; }
    }
}
