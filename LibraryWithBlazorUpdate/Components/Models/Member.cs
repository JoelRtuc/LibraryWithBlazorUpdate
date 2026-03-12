using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWithBlazorUpdate.Components.Models
{
    public class Member : ISearchable
    {
        [Key]
        public string memberId { get; set; }
        public string memberName { get; set; }
        public string email { get; set; }
        public DateTime memberSince { get; set; }
        public List<Loan> loans { get; set; }

        public Member() { }

        public Member(string memberId, string memberName, string email, DateTime memberSince)
        {
            this.memberId = memberId;
            this.memberName = memberName;
            this.email = email;
            this.memberSince = memberSince;
        }

        public void MembersInfo()
        {
            Console.WriteLine($"ID: {memberId}, Name: {memberName}, Email: {email}, Member Since: {memberSince}");
        }

        public bool Matches(string searchItem)
        {
            if (memberId.Contains(searchItem) || memberName.Contains(searchItem))
            {
                return true;
            }
            return false;
        }
    }
}
