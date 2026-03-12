using LibraryWithBlazorUpdate.Components.Models;
using LibraryWithBlazorUpdate.Data;
using Xunit;

namespace LibraryWithBlazorUpdate.Tests
{
    public class LoanTests
    {
        [Fact]
        public void Constructor_InitializesLoan_WithAllProperties()
        {
            // EMPTY
        }

        [Fact]
        public void Loan_RequiresLibraryItem()
        {
            // EMPTY
        }

        [Fact]
        public void Loan_RequiresMember()
        {
            // EMPTY
        }

        [Fact]
        public void LoanDate_IsSetCorrectly()
        {
            // EMPTY
        }

        [Fact]
        public void DueDate_IsCalculatedAs14DaysAfterLoanDate()
        {
            // EMPTY
        }

        [Fact]
        public void Item_IsSetToUnavailable_WhenLoanCreated()
        {
            // EMPTY
        }

        [Fact]
        public void OverdueFunc_ReturnTrue_WhenDueDatePassed()
        {
            // EMPTY
        }

        [Fact]
        public void OverdueFunc_ReturnFalse_WhenDueDateNotPassed()
        {
            // EMPTY
        }

        [Fact]
        public void Return_MarkLoanAsReturned()
        {
            // EMPTY
        }

        [Fact]
        public void Return_SetItemToAvailable()
        {
            // EMPTY
        }

        [Fact]
        public void Return_RemovesLoanFromManager()
        {
            // EMPTY
        }
    }
}
