using Company.Client.DAL.Entities;
using Company.Client.DAL.Persistence.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Company.Client.DAL.Persistence.Data.Configurations.Departments
{
    internal class DepartmentConfigurations : BaseAuditableEntityConfigurations<int , Department>
    {

        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            base.Configure(builder);

            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.Name).HasColumnType("varchar(10)");
            builder.Property(d => d.Code).HasColumnType("varchar(100)");
            builder.Property(d => d.Description).HasColumnType("varchar(100)");

            //RelationShips :
            // department m - 1 employee [dept has many emps]
            builder.HasMany(d => d.Employees)
                   .WithOne(e => e.Department)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // department 1 - 1 employee [manage]
            builder.HasOne(d => d.Manager)
                   .WithOne(e => e.ManagedDepartment)
                   .HasForeignKey<Department>(d => d.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
