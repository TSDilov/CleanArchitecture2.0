using Hr.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hr.LeaveManagement.Persistence.Configurations.Entities
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Vacation",
                    CreatedBy = "Administrator",
                    DateCreated = DateTime.Now,
                    LastModifiedBy = "Administrator",
                    LastModifiedDate = DateTime.Now,
                },
                new LeaveType
                {
                    Id= 2,
                    DefaultDays = 5,   
                    Name = "Sick",
                    CreatedBy = "Administrator",
                    DateCreated = DateTime.Now,
                    LastModifiedBy = "Administrator",
                    LastModifiedDate = DateTime.Now,
                }
            );
        }
    }
}
