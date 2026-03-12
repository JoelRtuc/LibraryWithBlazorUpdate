using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWithBlazorUpdate.Components.Models;


namespace LibraryWithBlazorUpdate.Components
{

    public class ClassManager
    {
        public List<Loan> allLoans = new List<Loan>();

        public List<LibraryItem> SortListByYear(List<LibraryItem> itemsToSort)
        {
            List<LibraryItem> items = itemsToSort.OrderByDescending(p => p.publishedYear).ToList();

            return items;
        }

        public void SearchClass(LibraryItem[] itemsToSearch, string searchTerm)
        {
            foreach (LibraryItem item in itemsToSearch)
            {
                if (item.title.Contains(searchTerm) || item.author.Contains(searchTerm))
                {
                    Console.WriteLine($"{item.title} {item.author} {item.publishedYear}");
                }
            }
        }

        public Member ProlificLoaner(Member[] members)
        {
            int max = members.Max(m => m.loans.Count);
            foreach (Member member in members)
            {
                if(member.loans.Count >= max)
                {
                    return member;
                }
            }
            return null;
        }

        public int WhatBooksLoaned(LibraryItem[] items)
        {
            int isAvailable = items.Length;
            foreach (LibraryItem item in items)
            {
                if (item.isAvailable)
                {
                    isAvailable--;
                }
            }

            return isAvailable;
        }

        public int AllBooks(LibraryItem[] items)
        {
            return items.Length;
        }

    }

}
