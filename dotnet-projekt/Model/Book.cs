
using System;
using System.Runtime.Serialization;

namespace Bookkeeper.Model
{
    [DataContract]
    public class Book
    {
        public static Book CreateBook(string title, string author)
        {
            return new Book {Author = author, Title = title};
        }

        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public bool Lent { get; set; }
        [DataMember]
        public string LentTo { get; set; }
    }
}
