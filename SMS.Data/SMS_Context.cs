using Microsoft.EntityFrameworkCore;
using SMS.Model.Student;
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

    }
}
