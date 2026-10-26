using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.IO;

namespace GestionLivres
{
    public class BookManager
    {
        private DoublyLinkedList<Book> books = new DoublyLinkedList<Book>();

        
        // AJOUTER UN LIVRE
       
        public void AddBook(Book book)
        {
            // Vérifier doublon
            var exist = books.Search(b => b.Title.ToLower() == book.Title.ToLower());

            if (exist != null)
                throw new Exception("Livre déjà existant");

            books.AddLast(book);
            Save("Livres.csv");
        }

        // AFFICHAGE AVANT
        public void DisplayForward()
        {
            if (books.IsEmpty())
            {
                Console.WriteLine("Liste vide");
                return;
            }

            // En-tête du tableau
            Console.WriteLine();
            Console.WriteLine("{0,-25} {1,-20} {2,-8} {3,-10}", "Titre", "Auteur", "Année", "Statut");
            Console.WriteLine(new string('-', 70));

            // Affichage des livres
            books.Traverse(b =>
            {
                string statut = b.IsRead ? "lu" : "à lire";
                Console.WriteLine("{0,-25} {1,-20} {2,-8} {3,-10}",
                    b.Title, b.Author, b.Year, statut);
            });
        }

        // AFFICHAGE ARRIÈRE
        public void DisplayBackward()
        {
            if (books.IsEmpty())
            {
                Console.WriteLine("Liste vide");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("{0,-25} {1,-20} {2,-8} {3,-10}", "Titre", "Auteur", "Année", "Statut");
            Console.WriteLine(new string('-', 70));

            books.TraverseReverse(b =>
            {
                string statut = b.IsRead ? "lu" : "à lire";

                Console.WriteLine("{0,-25} {1,-20} {2,-8} {3,-10}",
                    b.Title,
                    b.Author,
                    b.Year,
                    statut);
            });
        }

        // RECHERCHE un livre
        public Book Search(string title)
        {
            var node = books.Search(b => b.Title.ToLower() == title.ToLower());
            return node?.Value;
        }

        // SUPPRESSION d'un livre
        public bool Delete(string title)
        {
            var node = books.Search(b => b.Title.ToLower() == title.ToLower());

            if (node == null)
                return false;

            return books.RemoveNode(node);
        }

        // MARQUER un livre COMME LU
        public void MarkAsRead(string title)
        {
            var node = books.Search(b => b.Title.ToLower() == title.ToLower());

            if (node != null)
                node.Value.IsRead = true;
        }

        // Trier un livre
        public void SortByTitle()
        {
            books.InsertionSort((a, b) =>
                string.Compare(a.Title, b.Title, StringComparison.OrdinalIgnoreCase));
        }

        public void SortByAuthor()
        {
            books.InsertionSort((a, b) =>
                string.Compare(a.Author, b.Author, StringComparison.OrdinalIgnoreCase));
        }

        // CHARGER le fichier CSV
        
        public void Load(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Fichier non trouvé");
                return;
            }

            foreach (var line in File.ReadAllLines(path))
            {
                var parts = line.Split(',');

                if (parts.Length != 4) continue;

                string title = parts[0];
                string author = parts[1];
                int year = int.Parse(parts[2]);
                bool isRead = parts[3].ToLower() == "lu";

                books.AddLast(new Book(title, author, year, isRead));
            }
        }
        
        // SDauvegarder le fichier csv
       
        public void Save(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                books.Traverse(b => sw.WriteLine(b.ToCSV()));
            }
        }
    }
}