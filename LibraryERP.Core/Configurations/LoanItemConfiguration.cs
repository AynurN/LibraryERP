using LibraryERP.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Core.Configurations
{
    public class LoanItemConfiguration : IEntityTypeConfiguration<LoanItem>
    {
        public void Configure(EntityTypeBuilder<LoanItem> builder)
        {
            builder.HasOne(x=>x.Loan)
                .WithMany(x=>x.LoanItems)
                .HasForeignKey(x=>x.LoanId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
