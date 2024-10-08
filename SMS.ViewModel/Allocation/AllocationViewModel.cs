using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModel.Allocation
{
    public class AllocationViewModel
    {
        public IEnumerable<SubjectAllocationGroupBySubjectViewModel> SubjectAllocations { get; set; }

        public IEnumerable<StudentAllocationGroupByStudentViewModel> StudentAllocations { get; set; }
    }
}
