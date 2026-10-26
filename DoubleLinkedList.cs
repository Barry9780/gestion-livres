using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System;

namespace GestionLivres
{
    public class DoublyLinkedList<T>
    {
        private Node<T> head; // premier élément
        private Node<T> tail; // dernier élément
        private int count;    // nombre d’éléments

        public Node<T> Head => head;
        public Node<T> Tail => tail;
        public int Count => count;
        
        // Ajouter un livre au debut de la liste chainée
        public void AddFirst(T value)
        {
            Node<T> newNode = new Node<T>(value);

            // Si la liste est vide
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }

            count++;
        }

        // Ajouter un livre a la fin de la liste chainée
        public void AddLast(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (tail == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }

            count++;
        }

     
        // SUPPRESSION D’UN NŒUD
        
        public bool RemoveNode(Node<T> node)
        {
            if (node == null) return false;

            // Cas : un seul élément
            if (head == node && tail == node)
            {
                head = tail = null;
            }
            // Supprimer le premier
            else if (node == head)
            {
                head = head.Next;
                head.Prev = null;
            }
            // Supprimer le dernier
            else if (node == tail)
            {
                tail = tail.Prev;
                tail.Next = null;
            }
            // Supprimer au milieu
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }

            count--;
            return true;
        }

        
        // Rechewrche linaire
        public Node<T> Search(Func<T, bool> condition)
        {
            Node<T> current = head;

            while (current != null)
            {
                if (condition(current.Value))
                    return current;

                current = current.Next;
            }

            return null;
        }
        
        // Recours avnt
        public void Traverse(Action<T> action)
        {
            Node<T> current = head;

            while (current != null)
            {
                action(current.Value);
                current = current.Next;
            }
        }

       
        // PARCOURS ARRIÈRE (RÉCURSIF)
       
        public void TraverseReverse(Action<T> action)
        {
            TraverseReverseRec(tail, action);
        }

        // Méthode récursive
        private void TraverseReverseRec(Node<T> node, Action<T> action)
        {
            // Condition d’arrêt
            if (node == null) return;

            action(node.Value);

            // Appel récursif vers le précédent
            TraverseReverseRec(node.Prev, action);
        }

        
        // TRI PAR INSERTION
        
        public void InsertionSort(Comparison<T> compare)
        {
            if (head == null || head.Next == null)
                return;

            Node<T> sorted = null; // nouvelle liste triée
            Node<T> current = head;

            while (current != null)
            {
                Node<T> next = current.Next;

                current.Next = current.Prev = null;

                // Insérer dans la bonne position
                if (sorted == null)
                {
                    sorted = current;
                }
                else if (compare(current.Value, sorted.Value) < 0)
                {
                    current.Next = sorted;
                    sorted.Prev = current;
                    sorted = current;
                }
                else
                {
                    Node<T> temp = sorted;

                    while (temp.Next != null &&
                           compare(current.Value, temp.Next.Value) >= 0)
                    {
                        temp = temp.Next;
                    }

                    current.Next = temp.Next;

                    if (temp.Next != null)
                        temp.Next.Prev = current;

                    temp.Next = current;
                    current.Prev = temp;
                }

                current = next;
            }

            // Mise à jour head et tail
            head = sorted;
            tail = head;

            while (tail.Next != null)
                tail = tail.Next;
        }

        public bool IsEmpty()
        {
            return head == null;
        }
    }
}