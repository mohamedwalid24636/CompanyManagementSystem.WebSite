using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Models.Shared.Enums;

namespace Demo.DataAccesse.Data.ModelsConfiguratoin
{
    public class EmployeeConfiguration : BaseEntityConfiguration<Employee> , IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("nvarchar(50)");
            builder.Property(E => E.Address).HasColumnType("nvarchar(150)");
            builder.Property(E => E.Salary).HasColumnType("Decimal(10,2)");
            builder.Property(E => E.Gender).HasConversion((EmpGender) => EmpGender.ToString(), (_gender) => (Gender)Enum.Parse(typeof(Gender), _gender));  // save it as string and retrieve it as enum
            builder.Property(E => E.EmployeeType).HasConversion(EmpType => EmpType.ToString(), _Type => (EmployeeType)Enum.Parse(typeof(EmployeeType), _Type));

            base.Configure(builder);  // Call the base class configuration method to apply common configurations
        }
    }
}
