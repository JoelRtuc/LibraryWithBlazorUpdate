using LibraryWithBlazorUpdate.Components;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWithBlazorUpdate.Components.Models
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // foreign keys
        public int LibraryItemId { get; set; }
        public string MemberId { get; set; }

        // navigation
        [ForeignKey(nameof(LibraryItemId))]
        public virtual LibraryItem item { get; set; }
        [ForeignKey(nameof(MemberId))]
        public virtual Member member { get; set; }

        public bool isOverdue { get; set; }
        public bool isReturned { get; set; }
        public DateTime loanDate { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime? returnDate { get; set; }

        public Loan() { }

        public Loan(LibraryItem item, Member member, DateTime loanDate, ClassManager manager)
        {
            this.item = item;
            this.member = member;
            if (member != null)
            {
                member.loans.Add(this);
                MemberId = member.memberId;
            }
            if (item != null && item.isAvailable)
            {
                LibraryItemId = item.Id;
                item.loans.Add(this);
                item.isAvailable = false;
            }
            else if(!item.isAvailable)
            {
                throw new InvalidOperationException("Item is not available for loan.");
            }

            this.loanDate = loanDate;
            dueDate = returnDueDate();
            manager?.allLoans.Add(this);
        }

        DateTime returnDueDate()
        {
            return loanDate.AddDays(14);
        }

        public bool OverdueFunc()
        {
            return dueDate <= DateTime.Now;
        }

        public void Return(Book book, ClassManager manager)
        {
            isReturned = true;
            returnDate = DateTime.Now;
            manager?.allLoans.Remove(this);
            if (book != null) book.isAvailable = true;
        }
    }
}
