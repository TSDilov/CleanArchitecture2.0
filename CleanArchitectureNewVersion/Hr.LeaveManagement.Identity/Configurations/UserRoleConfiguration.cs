using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hr.LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "73196186-acc1-44b6-9361-ae85feafc3ec",
                    UserId = "c6706ec3-ff2e-4e98-a7a9-49ec9010e2be"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "14bd648e-49b1-4c6b-b06d-d5f056d4aa72",
                    UserId = "7fef8651-b17c-4669-9d7c-c94ed2e4b7ae"
                }
                );
        }
    }
}
