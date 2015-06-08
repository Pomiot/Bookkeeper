using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Bookkeeper.Model;

namespace Bookkeeper.DataAccess
{
    public class BookRepository
    {
        List<Book> _books;

        public BookRepository()
        {
            if (_books == null)
            {
                loadBooks();
            }

        }

        public List<Book> getBooks()
        {
            return new List<Book>(_books);
        }

        public void saveBooks(List<Book> newBookList)
        {
            string fileName = "BookListAsJSON.json";
            DataContractJsonSerializer bookSerializer = new DataContractJsonSerializer(typeof(List<Book>));
            FileStream fs = File.OpenWrite(fileName);

            bookSerializer.WriteObject(fs, newBookList);

            fs.Close();
            fs.Dispose();
    
        }

        private void loadBooks()
        {
            string fileName = "BookListAsJSON.json";
            DataContractJsonSerializer bookSerializer = new DataContractJsonSerializer(typeof(List<Book>));
            FileStream fs = File.OpenRead(fileName);

            _books = (List<Book>)bookSerializer.ReadObject(fs);

            fs.Close();
            fs.Dispose();
        }
    }
}