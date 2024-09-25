using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModel.Allocation
{
    public class SubjectAllocationGroupBySubjectViewModel
    {
        [DisplayName("Teacher Name")]
        public string SubjectName { get; set; }
        [DisplayName("Teacher Reg No")]
        public string SubjectCode { get; set; }

        public IEnumerable<SubjectAllocationViewModel> SubjectAllocations { get; set; }
    }
}
