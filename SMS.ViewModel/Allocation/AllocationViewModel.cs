namespace SMS.ViewModel.Allocation
{
    public class AllocationViewModel
    {
        public IEnumerable<SubjectAllocationGroupBySubjectViewModel> SubjectAllocations { get; set; }

        public IEnumerable<StudentAllocationGroupByStudentViewModel> StudentAllocations { get; set; }
    }
}
