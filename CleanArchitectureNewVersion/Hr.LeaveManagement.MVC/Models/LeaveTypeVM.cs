using System.ComponentModel.DataAnnotations;

namespace Hr.LeaveManagement.MVC.Models
{
    public class LeaveTypeVM : CreateLeaveTypeVM
    {
        public int Id { get; set; }
    }

    public class CreateLeaveTypeVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Default Number days")]
        public string DefaultDays { get; set; }
    }
}
