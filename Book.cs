using System;
using System.Collections.Generic;
using System.Text;

namespace GestionLivres
{
    // Les attributs d;un livre
    public class Book
    {
        public string Title;
        public string Author;
        public int Year;
        public bool IsRead;


        // Le constructeur
        public Book(string title, string author, int year, bool isRead)
        {
            Title = title;
            Author = author;
            Year = year;
            IsRead = isRead;
        }

        // Affichage d'un livre
        public override string ToString()
        {
            string status = IsRead ? "lu" : "à lire";
            return $"{Title} | {Author} | {Year} | {status}";
        }

        // Convertir en ligne CSV
        public string ToCSV()
        {
            string status = IsRead ? "lu" : "a lire";
            return $"{Title},{Author},{Year},{status}";
        }
    }
}
