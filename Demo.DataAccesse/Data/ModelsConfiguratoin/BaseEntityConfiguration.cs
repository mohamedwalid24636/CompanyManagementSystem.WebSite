using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Models.Shared;

namespace Demo.DataAccesse.Data.ModelsConfiguratoin
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GetDate()");
            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GetDate()");
        }
    }
}
