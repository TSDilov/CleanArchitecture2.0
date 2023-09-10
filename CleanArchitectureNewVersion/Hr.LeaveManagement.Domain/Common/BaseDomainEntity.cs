using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Domain.Common
{
    public abstract class BaseDomainEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "System";
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public string LastModifiedBy { get; set; } = "System";
    }
}
