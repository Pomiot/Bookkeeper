using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Bookkeeper.Model;

namespace Bookkeeper.DataAccess
{
    public class BookRepository
    {
        private List<Book> _books;

        private const string FileName = "BookListAsJSON.json";
        
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
            var bookSerializer = new DataContractJsonSerializer(typeof(List<Book>));

            using (var fs = File.OpenWrite(FileName))
                bookSerializer.WriteObject(fs, newBookList);
        }

        private void loadBooks()
        {
            var bookSerializer = new DataContractJsonSerializer(typeof(List<Book>));

            if (!File.Exists(FileName))
            {
                using (var filewriter = new StreamWriter(FileName))
                    filewriter.Write("[]");
            }
            using (var fs = File.OpenRead(FileName))
                _books = (List<Book>) bookSerializer.ReadObject(fs);
        }
    }
}