
using Demo.DataAccesse.Models.DepartmentModel;

namespace Demo.DataAccesse.Data.ModelsConfiguratoin
{
    public class DepartmentConfiguration : BaseEntityConfiguration<Department> , IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d=>d.Id).UseIdentityColumn();
            builder.Property(d => d.name).HasColumnType("nvarchar(20)");
            builder.Property(d => d.Code).HasColumnType("nvarchar(20)");
            builder.HasMany(D => D.Employees ) 
                .WithOne (E=> E.Department)
                .HasForeignKey(E=>E.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull); // يعني لو حذفت قسم الموظف هيبقي قسمه null مش هيتحذف 




            base.Configure(builder);  // شغل الكونستراكتور القديم الاول
        }
    }
} 
