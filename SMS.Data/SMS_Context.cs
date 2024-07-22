using Microsoft.EntityFrameworkCore;
using SMS.Model.Allocation;
using SMS.Model.Student;
using SMS.Model.Subject;
using SMS.Model.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class SMS_Context:DbContext
    {
        public SMS_Context(DbContextOptions<SMS_Context> options) : base(options) { }

        public DbSet<StudentBO> Student { get; set; }

        public DbSet<TeacherBO> Teacher { get; set; }

        public DbSet<SubjectBO> Subject { get; set; }

        public DbSet<StudentAllocationBO> Student_Subject_Teacher_Allocation { get; set; }

        public DbSet<SubjectAllocationBO> Teacher_Subject_Allocation { get; set; }

        

    }
}
