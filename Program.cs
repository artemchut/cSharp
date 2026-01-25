using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Channels;

public class Program
{
    public static void Main()
    {
        Library library = new Library();

        Book book1 = new Book("spider man", "marvel", 2010, true);
        Book book2 = new Book("a level computer science", "adam joice", 1900, false);
        library.AddBook(book1);
        library.AddBook(book2);


        while (true)
        {
            Console.WriteLine("[1] - Add a book");
            Console.WriteLine("[2] - Borrow a book");
            Console.WriteLine("[3] - Return a book");
            Console.WriteLine("[4] - Display all books");
            Console.WriteLine("[5] - Exit");
            Console.Write("Enter the option: ");
            string? option = Console.ReadLine();

            if (option == "1")
            { 
                Console.Write("Enter book's title: ");
                string? newTitle = Console.ReadLine();
                Console.Write("Enter book's author: ");
                string? newAuthor = Console.ReadLine();
                Console.Write("Enter year the book was published: ");
                int newYearPublished = Convert.ToInt16(Console.ReadLine());

                library.AddNewBook(newTitle, newAuthor, newYearPublished);
            }
            else if (option == "2")
            { 
                Console.Write("Enter book's title: ");
                string? newTitle = Console.ReadLine();
                foreach (Book book in library.allBooks)
                {
                    if (book.GetTitle() == newTitle)
                    {
                        book.Borrow();
                    }
                }  
            }
            else if (option == "3")
            { 
                Console.Write("Enter book's title: ");
                string? newTitle = Console.ReadLine();
                foreach (Book book in library.allBooks)
                {
                    if (book.GetTitle() == newTitle)
                    {
                        book.ReturnBook();
                    }
                }  
            }
            else if (option == "4")
            { 
                library.DisplayAllBooks();
            }
            else
            {
                break;
            }
        }
        
        // library.RemoveBook(book1.GetTitle());
        // library.DisplayAllBooks();

    }


    public class Library
    {
        public List<Book> allBooks = new List<Book>{};
        public void AddBook(Book book)
        {
            allBooks.Add(book);
        }

        public void DisplayAllBooks()
        {
            foreach (Book i in allBooks)
            {
                Console.WriteLine(i.GetTitle());
                Console.WriteLine(i.GetAuthor());
                Console.WriteLine(i.GetYearPublished());
                Console.WriteLine(i.GetIsBorrowed());
                Console.WriteLine();
            }
        }
        public void FindBook(string title)
        {
            bool found = false;
            foreach (Book i in allBooks)
            {
                if (i.GetTitle() == title)
                {
                    Console.WriteLine("The book was found: ");
                    Console.WriteLine(i.GetTitle());
                    Console.WriteLine(i.GetAuthor());
                    Console.WriteLine(i.GetYearPublished());
                    Console.WriteLine(i.GetIsBorrowed());
                    Console.WriteLine();
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine($"Book {title} was not found");
            }
        }
        public void RemoveBook(string title)
        {
            foreach (Book i in allBooks)
            {
                if (i.GetTitle() == title)
                {
                    allBooks.Remove(i);
                    break;
                }
            }
        }
        public void AddNewBook(string title, string author, int yearPublished)
        {
            Book newBook = new Book(title, author, yearPublished, false);
            AddBook(newBook);
        }
    }


    public class Book
    {
        private string title;
        private string author;
        private int yearPublished;
        private bool isBorrowed = false;

        public Book(string aTitle, string anAuthor, int aYearPublished, bool ifIsBorrowed)
        {
            title = aTitle;
            author = anAuthor;
            yearPublished = aYearPublished;
            isBorrowed = ifIsBorrowed;
        }

        // ALL GETS
        public string GetTitle()
        {
            return title;
        }
        public string GetAuthor()
        {
            return author;
        }
        public int GetYearPublished()
        {
            return yearPublished;
        }
        public bool GetIsBorrowed()
        {
            return isBorrowed;
        }


        // ALL OTHER FUNCTIONS
        public string Borrow()
        {
            if (isBorrowed)
            {
                return $"This book is already borrowed";
            }
            isBorrowed = true;
            return $"Book '{title}' has been borrowed";
        }
        public string ReturnBook()
        {
            isBorrowed = false;
            return $"Bookook '{title}' has been returned";
        }
    }
}