using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstConsoleApp1
{
    class Program
    {        
        private static string name = null;
        private static string phone = null;
        private static Dictionary<string, string> book = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            startPhoneBook();
        }

        public static void startPhoneBook()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("\n\t\"PhoneBook\"\n");

                switch (operationSelection())
                {
                    case "0": uploadContacts(); break;
                    case "1": addContact(); break;
                    case "2": findContactByPhone(); break;
                    case "3": findContactByName(); break;
                    case "4": getAllContact(); break;
                    case "5": removeContact(); break;
                    case "6": clearBook(); break;
                    case "7": break;
                    default: Console.WriteLine("Wrong choice of operation! Try again."); break;
                }

                Console.Write("\nContinue? 1 - Yes, 0 - No: ");
            } while (answer());
        }

        static string operationSelection()
        {
            Console.Write(
                "0. Load my contacts;" +
                "\n1. Add new contact;" +
                "\n2. Search by phone number;" +
                "\n3. Search by name;" +
                "\n4. Show all contact;" +
                "\n5. Remove contact;" +
                "\n6. Clear \"Phonebook\"" +
                "\n7. Exit;" +
                "\n\nSelect operation: ");

            return Console.ReadLine();
        }

        public static bool addContact()
        {
            do
            {
                Console.Write("\nInput name of contact: ");
                name = Console.ReadLine();
                
            } while (isContactExists(name, null, true, false));

            do
            {
                Console.Write("Input phone number of contact (+380): ");
                phone = Console.ReadLine();

            } while (isContactExists(null, phone, true, false));


            if (name == "" || phone == "")
            {
                Console.WriteLine("Empty contact cannot be added!");
                return false;
            }

            int beforeAdded = book.Count;
            book.Add(name, phone);

            if (beforeAdded < book.Count) Console.WriteLine($"Contact: [{name}, {phone}] has been successfully added.");
            else Console.WriteLine("ERROR! Contact has not been added.");

            return true;
        }

        public static bool isContactExists(string name, string phone, bool trueMsg, bool falseMsg)
        {
            int counter = 0;
            foreach (KeyValuePair<string, string> kv in book)
            {
                counter++;
                if (kv.Key == name || kv.Value == phone)
                {
                    if (trueMsg) Console.WriteLine("This contact already exists!");
                    return true;
                }
                if (counter == book.Count)
                {
                    if (falseMsg) Console.WriteLine("No contact found!");
                    return false;
                }
            }
            return false;
        }

        public static int findContactByPhone()
        {
            Console.Write("\nInput phone number: ");
            phone = Console.ReadLine();
            int counter = 0;
            int results = 0;

            foreach (KeyValuePair<string, string> kv in book)
            {
                counter++;
                if (kv.Value.Contains(phone))
                {
                    results++;
                    Console.WriteLine(kv);
                    continue;
                }
                if (counter == book.Count && results > 0) return 1;
                else if (counter == book.Count && results == 0) break;
            }

            Console.WriteLine("Contact not found!");
            return 0;
        }

        public static int findContactByName()
        {
            Console.Write("\nInput name of contact: ");
            name = Console.ReadLine();
            int counter = 0;
            int results = 0;

            foreach (KeyValuePair<string, string> kv in book)
            {
                counter++;
                if (kv.Key.Contains(name))
                {
                    results++;
                    Console.WriteLine(kv);
                    continue;
                }
                if (counter == book.Count && results > 0) return 1;
                else if (counter == book.Count && results == 0) break;
            }

            Console.WriteLine("Contact not found!");
            return 0;
        }

        public static void getAllContact()
        {
            Console.WriteLine();
            if (book.Count == 0)
            {
                Console.WriteLine("List is empty.");
                return;
            }
            foreach (KeyValuePair<string, string> kv in book) Console.WriteLine(kv);
        }

        public static void removeContact()
        {
            Console.Write("Input the exact name, or use the search for more information: ");
            string info = Console.ReadLine();

            bool exists = isContactExists(info, null, false, true);

            if (exists)
            {
                book.Remove(info);
                if (!isContactExists(info, null, false, false)) Console.WriteLine("Contact successfully deleted.");
            }
        }

        public static void clearBook() {
            Console.Write("Сonfirm phonebook deletion operation (1 - Yes, 0 - no): ");
            if (answer()) book.Clear();
            Console.WriteLine("Сontacts successfully deleted!");
        }

        public static Dictionary<string, string> contacts()
        {
            return new Dictionary<string, string>
            {
                {"Dima Life", "63123"},
                {"Dima Kyivstar", "98123"},
                {"Dima Vodafone", "66123"},
                {"Rodion Life", "63456"},
                {"Rodion Kyivstar", "98456"},
                {"Rodion Vodafone", "66456"},
                {"Tanya Life", "63789"},
                {"Tanya Kyivstar", "98789"},
                {"Tanya Vodafone", "66789"},
            };
        }

        public static void uploadContacts()
        {
            Console.WriteLine("Contacts successfully uploaded into the \"PhoneBook\".\nContact view is available in option \'4\'.");
            book = book.Union(contacts()).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static bool answer()
        {

            string choice = null;
            string message = "There is no such option. Try again! ";
            string newLine = "\n";
            do {
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "0": return false;
                    case "1": return true;
                    default:
                    {
                            Console.Write(newLine);
                            moveCursor(0, -1);
                            Console.WriteLine(new string(' ', message.Length + choice.Length));
                            moveCursor(0, -1);
                            Console.Write(message);
                            newLine = null;
                            continue;
                    }
                }                    
            } while (true);
        }

        public static void moveCursor(int left, int top)
        {
            Console.SetCursorPosition(Console.CursorLeft + left, Console.CursorTop + top);
        }

        public static int getLength()
        {
            return book.Count;
        }
    }
}