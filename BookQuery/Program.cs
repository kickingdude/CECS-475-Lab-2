using System;
using System.Linq;

namespace BookQuery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BooksEntities1 dbcontext =
             new BooksEntities1();
            // get authors and ISBNs of each book they co-authored
            // get authors and titles of each book they co-authored
            var TitlesandAuthors =
               from book in dbcontext.Titles
               from author in book.Authors
               orderby book.Title1
               select new
               {
                   author.FirstName,
                   author.LastName,
                   book.Title1
               };

            Console.WriteLine("\r\n\r\nAuthors and titles:");

            // display authors and titles in tabular format
            foreach (var element in TitlesandAuthors)
            {
                Console.Write(
                   String.Format("\r\n\t{0,-10} {1,-10} {2}",
                      element.Title1, element.FirstName, element.LastName));
            } // end foreach

            var AuthorsandTitles =
               from author in dbcontext.Authors
               from book in author.Titles
               orderby book.Title1, author.FirstName, author.LastName
               select new
               {
                   author.FirstName,
                   author.LastName,
                   book.Title1
               };

            Console.WriteLine("\r\n\r\nAuthors and titles with authors sorted for each :");

            // display authors and ISBNs in tabular format
            foreach (var element in AuthorsandTitles)
            {
                Console.Write(
                   String.Format("\r\n\t{0,-10} {1,-10} {2}",
                      element.Title1, element.FirstName, element.LastName));
            } // end foreach



            // get authors and titles of each book 
            // they co-authored; group by author
            var titlesByAuthor =
               from titles in dbcontext.Titles
               orderby titles.Title1
               select new
               {


                   Titles = titles.Title1,
                   Name = titles.Authors

               };

            Console.Write("\r\n\r\nTitles grouped by author:");

            // display titles written by each author, grouped by author
            foreach (var Titles in titlesByAuthor)
            {
                // display author's name
                Console.Write("\r\n\t" + Titles.Titles + ":");

                // display titles written by that author
                foreach (var author in Titles.Name)
                {
                    Console.Write("\r\n\t\t" + author.FirstName + " " + author.LastName + "");
                } // end inner foreach
            } // end outer foreach
            Console.WriteLine("");

        }
    }
}
