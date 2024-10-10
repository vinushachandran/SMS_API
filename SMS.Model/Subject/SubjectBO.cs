using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SMS.Model.Subject
{
    public class SubjectBO
    {
        [Key]
        public long SubjectID { get; set; }

        [Required(ErrorMessage = "Subject Code is required")]
        [DisplayName("Subject Code")]
        public string SubjectCode { get; set; }
        [Required(ErrorMessage = "Subject Nmae is required")]
        [DisplayName("Subject Name")]
        public string Name { get; set; }

        [DisplayName("Active Status")]
        public bool IsEnable { get; set; }
    }
}
