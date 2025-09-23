﻿using Company.Client.DAL.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.DAL.Persistence.Data.Configurations.Common
{
    internal class BaseAuditableEntityConfigurations<TKey,TEntity> : BaseEntityConfigurations<TKey , TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : BaseAuditableEntity<TKey>

    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(d => d.CreatedBy).HasColumnType("varchar(50)");
            builder.Property(d => d.LastModifiedBy).HasColumnType("varchar(50)");

            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETUTCDate()");
            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GETUTCDate()");

        }
    }
}
