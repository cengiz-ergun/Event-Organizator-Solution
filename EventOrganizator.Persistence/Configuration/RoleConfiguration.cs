using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventOrganizator.Domain.Entities.Identity;
using EventOrganizator.Application.Constants;

namespace EventOrganizator.Persistence.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name =  Roles.Administrator,
                    NormalizedName = Roles.Administrator.Normalize()
                },
                new IdentityRole
                {
                    Name = Roles.Member,
                    NormalizedName = Roles.Member.Normalize()
                }
            );
        }
    }
}
