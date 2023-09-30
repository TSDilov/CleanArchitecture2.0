using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Hr.LeaveManagement.MVC.Models.LeaveRequest
{
    public class CreateLeaveRequestVM
    {
        [Display(Name = "Start Date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required]
        public DateTime EndDate { get; set; }

        [BindNever]
        public SelectList LeaveTypes { get; set; }

        [Display(Name = "Leave Type")]
        [Required]
        public int LeaveTypeId { get; set; }

        [Display(Name = "Comments")]
        [MaxLength(300)]
        public string RequestComments { get; set; }
    }
}
