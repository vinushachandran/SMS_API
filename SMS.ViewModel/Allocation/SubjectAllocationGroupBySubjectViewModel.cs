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
        public long? SubjectID { get; set; }
        public string SubjectName { get; set; }

        public string SubjectCode { get; set; }

        public IEnumerable<SubjectAllocationViewModel> SubjectAllocations { get; set; }
    }
}
