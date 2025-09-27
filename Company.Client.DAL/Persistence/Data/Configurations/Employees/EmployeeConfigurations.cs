using Company.Client.DAL.Common.Enums;
using Company.Client.DAL.Entities;
using Company.Client.DAL.Persistence.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Client.DAL.Persistence.Data.Configurations.Employees
{
    internal class EmployeeConfigurations : BaseAuditableEntityConfigurations<int, Employee>
    {

        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Id)
                   .UseIdentityColumn(1, 1);

            builder.Property(e => e.FirstName)
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.Property(e => e.LastName)
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.Property(e => e.Email)
                   .HasColumnType("varchar(100)");

            builder.Property(e => e.Salary)
                   .HasColumnType("decimal(9,2)");

            // storing enum as a string in database & retreive it as an int
            builder.Property(e => e.Gender)
                   .HasConversion(
                        gender => gender.ToString(), // the value which will be stored in DB
                        gender => Enum.Parse<Gender>(gender) // the retreived value in code
                    );
            builder.Property(e => e.EmployeeType)
                   .HasConversion(
                        e => e.ToString(), // the value which will be stored in DB
                        e => Enum.Parse<EmployeeType>(e) // the retreived value in code
                    );

        }

    }

}
