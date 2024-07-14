using LibraryERP.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Core.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Loan> builder)
        {
            builder.HasOne(x=>x.Borrower)
                .WithMany(x=>x.Loans)
                .HasForeignKey(x=>x.BorrowerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
