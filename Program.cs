using System;

namespace GestionLivres
{
    class Program
    {
        static void Main()
        {
            BookManager manager = new BookManager();
            string file = "livres.csv";

            manager.Load(file);

            while (true)
            {
                Console.WriteLine("\n1. Ajouter un livre");
                Console.WriteLine("2. Afficher");
                Console.WriteLine("3. Afficher inverse");
                Console.WriteLine("4. Rechercher un livre");
                Console.WriteLine("5. Supprimer un livre");
                Console.WriteLine("6. Trier titre");
                Console.WriteLine("7. Quitter");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Console.Write("Titre: ");
                        string t = Console.ReadLine();

                        Console.Write("Auteur: ");
                        string a = Console.ReadLine();

                        Console.Write("Année: ");
                        int y = int.Parse(Console.ReadLine());

                        manager.AddBook(new Book(t, a, y, false));
                        break;

                    case "2":
                        manager.DisplayForward();
                        break;

                    case "3":
                        manager.DisplayBackward();
                        break;

                    case "4":
                        Console.Write("Titre: ");
                        var b = manager.Search(Console.ReadLine());
                        Console.WriteLine(b == null ? "Introuvable" : b.ToString());
                        break;

                    case "5":
                        Console.Write("Titre: ");
                        Console.WriteLine(manager.Delete(Console.ReadLine()) ? "Supprimé" : "Introuvable");
                        break;

                    case "6":
                        manager.SortByTitle();
                        Console.WriteLine("Tri effectué");
                        break;

                    case "7":
                        manager.Save(file);
                        return;
                }
            }
        }
    }
}