using SMS.Model.Subject;
using SMS.Model.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
