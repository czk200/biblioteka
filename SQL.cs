using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using static Library.BooksView;
using static Library.Clients;

namespace Library
{
    class SQL
    {
        private static string connStr = "server=sql11.freesqldatabase.com;port=3306;Initial Catalog=sql11496434;username=sql11496434;password=UVZpJgJVw4";

        public static bool Register(string login, string password) {

            MySqlConnection connection = new MySqlConnection(connStr);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand($"INSERT INTO uzytkownicy (nazwa, haslo) VALUES ('{login}', '{password}')", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public static bool CheckLogin(string login, string password) {

            bool res = false;
            MySqlConnection connection = new MySqlConnection(connStr);

            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand($"SELECT EXISTS (SELECT * FROM uzytkownicy WHERE nazwa = '{login}' AND haslo = '{password}')", connection);

                if ((Int64)command.ExecuteScalar() == 1)
                {
                    res = true;
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return res;
        }

        public static List<Book> GetBooks() {

            List<Book> books = new List<Book>();
            MySqlConnection connection = new MySqlConnection(connStr);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM ksiazki", connection);
                MySqlDataReader reader =  command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book()
                    {
                        name = reader.GetString("tytul"),
                        author = reader.GetString("autor"),
                        available = reader.GetBoolean("dostepna")
                    };
                    books.Add(book);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            return books;
        }

        public static Book GetBook(int id)
        {
            Book book = null;
            MySqlConnection connection = new MySqlConnection(connStr);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand($"SELECT * FROM ksiazki WHERE ID_ksiazki = {id}", connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    book = new Book()
                    {
                        name = reader.GetString("tytul"),
                        author = reader.GetString("autor"),
                        available = reader.GetBoolean("dostepna")
                    };
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            return book;
        }

        public static bool AddBook(string name, string author, bool available) {

            MySqlConnection connection = new MySqlConnection(connStr);
            try
            {
                connection.Open();
                int x = available ? 1 : 0;
                MySqlCommand command = new MySqlCommand($"INSERT INTO ksiazki (tytul, autor, dostepna) VALUES ('{name}','{author}','{x}')", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public static bool RemoveBook(string name, string author) {

            MySqlConnection connection = new MySqlConnection(connStr);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand($"DELETE FROM ksiazki WHERE tytul='{name}' AND autor = '{author}'", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;

        }


        public static List<Client> GetClients()
        {

            List<Client> clients = new List<Client>();
            MySqlConnection connection = new MySqlConnection(connStr);
            try
            {
                connection.Open();
                Book book;
                MySqlCommand command = new MySqlCommand("SELECT * FROM klienci", connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Client client = new Client()
                    {
                        name = reader.GetString("imie"),
                        surname = reader.GetString("nazwisko"),
                        available = reader.GetBoolean("aktywne"),
                        idKsiazki = reader.GetInt32("ID_ksiazki")
                    };
                    clients.Add(client);
                }
                reader.Close();

                foreach (Client client in clients)
                {
                    if (client.idKsiazki != -1)
                    {
                        command = new MySqlCommand($"SELECT * FROM ksiazki WHERE ID_ksiazki = {client.idKsiazki}", connection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            book = new Book()
                            {
                                name = reader.GetString("tytul"),
                                author = reader.GetString("autor"),
                                available = reader.GetBoolean("dostepna")
                            };
                            client.bookName = book.name;
                            client.bookAuthor = book.author;
                        }
                        reader.Close();
                    }
                }

                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            return clients;
        }

        public static bool AddClient(string name, string author, bool available)
        {

            MySqlConnection connection = new MySqlConnection(connStr);
            try
            {
                connection.Open();
                int x = available ? 1 : 0;
                MySqlCommand command = new MySqlCommand($"INSERT INTO klienci (imie, nazwisko, aktywne) VALUES ('{name}','{author}','{x}')", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public static void LinkClientToBook(string name, string surname, string bookName, string bookAuthor) {

            MySqlConnection connection = new MySqlConnection(connStr);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand($"UPDATE klienci SET ID_ksiazki = (SELECT COALESCE((SELECT ID_ksiazki FROM ksiazki WHERE tytul = '{bookName}' AND autor = '{bookAuthor}'), -1)) WHERE imie = '{name}' AND nazwisko = '{surname}'", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void UnLinkClientToBook(string name, string surname) {
            MySqlConnection connection = new MySqlConnection(connStr);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand($"UPDATE klienci SET ID_ksiazki = -1 WHERE imie = '{name}' AND nazwisko = '{surname}'", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static bool RemoveClient(string name, string surname)
        {
            MySqlConnection connection = new MySqlConnection(connStr);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand($"DELETE FROM klienci WHERE imie = '{name}' AND nazwisko = '{surname}'", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;

        }
    }
}
