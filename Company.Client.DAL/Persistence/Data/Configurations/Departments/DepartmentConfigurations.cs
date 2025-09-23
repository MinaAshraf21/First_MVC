using Company.Client.DAL.Common.Entities;
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


        }
    }
}
