﻿using EventOrganizator.Domain.Entities.Identity;
using EventOrganizator.Persistence.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Contexts
{
    public class EventOrganizatorDbContext: IdentityDbContext<AppUser, AppRole, string>
    {
        public EventOrganizatorDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // System.InvalidOperationException: 'Unable to track an entity of type 'AppUser' because its primary key property 'Id' is null.'
            // Code at the below is a solution for that exception
            var keysProperties = modelBuilder.Model.GetEntityTypes().Select(x => x.FindPrimaryKey()).SelectMany(x => x.Properties);
            foreach (var property in keysProperties)
            {
                property.ValueGenerated = ValueGenerated.OnAdd;
            }

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}