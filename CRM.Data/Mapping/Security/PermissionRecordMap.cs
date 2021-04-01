using CRM.Core.Domain.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Mapping.Security
{
    public partial class PermissionRecordMap : IEntityTypeConfiguration<PermissionRecord>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public void Configure(EntityTypeBuilder<PermissionRecord> builder)
        {

            builder.ToTable("PermissionRecord");
            builder.HasKey(pr => pr.Id);
            builder.Property(pr => pr.Name).IsRequired();
            builder.Property(pr => pr.SystemName).IsRequired().HasMaxLength(255);
            builder.Property(pr => pr.Category).IsRequired().HasMaxLength(255);
            
        }
    }

}
