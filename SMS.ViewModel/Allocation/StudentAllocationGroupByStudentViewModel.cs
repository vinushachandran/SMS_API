using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModel.Allocation
{
    public class StudentAllocationGroupByStudentViewModel
    {
        public long? StudentID { get; set; }

        public string StudentName { get; set;}

        public string StudentRegNo { get; set; }

        public bool IsActive { get; set; }

        public List<SubjectAllocationGroupBySubjectViewModel> SubjectAllocations { get; set; }
    }
}
