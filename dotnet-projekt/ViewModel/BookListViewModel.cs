using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Bookkeeper.DataAccess;
using Bookkeeper.Model;

namespace Bookkeeper.ViewModel
{
    public class BookListViewModel : ViewModelBase
    {
        private readonly BookRepository _bookRepository;

        private ObservableCollection<Book> _currentBooks;
        private ObservableCollection<Book> _allBooks;


        public ObservableCollection<Book> CurrentBooks
        {
            get { return _currentBooks; }
            private set
            {
                _currentBooks = value;
                OnPropertyChanged("CurrentBooks");
            }
        }

        public ObservableCollection<Book> AllBooks
        {
            get { return _allBooks; }
            private set
            {
                _allBooks = value;
                OnPropertyChanged("AllBooks");
            }
        }

        public BookListViewModel(BookRepository bookRepository)
        {
            if (bookRepository == null)
            {
                throw new ArgumentNullException("bookRepository");
            }
            _bookRepository = bookRepository;
            this.AllBooks = new ObservableCollection<Book>(_bookRepository.getBooks());
            //this.CurrentBooks = new ObservableCollection<Book>();
        }

#region Show Only Lent Books

        private RelayCommand _showLentCommand;

        public ICommand ShowLentCommand
        {
            get
            {
                if (_showLentCommand == null)
                {
                    _showLentCommand = new RelayCommand(param => this.ShowLentCommandExecute(),
                        param => this.ShowLentCommandCanExecute);
                }
                return _showLentCommand;
            }
        }

        void ShowLentCommandExecute()
        {
            List<Book> temp = AllBooks.Where(book => book.Lent == true).ToList();

            this.CurrentBooks = new ObservableCollection<Book>(temp);
        }

        private bool ShowLentCommandCanExecute
        {
            get
            {
                if (this.CurrentBooks == null)
                {
                    return false;
                }
                    return true;
            }
        }

#endregion

#region Show All Books

        private RelayCommand _showAllCommand;

        public ICommand ShowAllCommand
        {
            get
            {
                if (_showAllCommand == null)
                {
                    _showAllCommand = new RelayCommand(param => this.ShowAllCommandExecute(),
                        param => this.ShowAllCommandCanExecute);
                }
                return _showAllCommand;
            }
        }

        void ShowAllCommandExecute()
        {
            this.CurrentBooks = this.AllBooks;
        }

        private bool ShowAllCommandCanExecute
        {
            get
            {
                if (this.AllBooks.Count == 0)
                {
                    return false;
                }
                return true;
            }
        }

#endregion

#region Add New Books

        private RelayCommand _addBookCommand;

        public ICommand AddBookCommand
        {
            get
            {
                if (_addBookCommand == null)
                {
                    _addBookCommand = new RelayCommand(param => this.AddBookCommandExecute(),
                        param => this.AddBookCommandCanExecute);
                }
                return _addBookCommand;
            }
        }

        void AddBookCommandExecute()
        {
            this.AllBooks.Add(new Book());
            OnPropertyChanged("AllBooks");
        }

        private bool AddBookCommandCanExecute
        {
            get
            {
                if (this.CurrentBooks == null)
                {
                    return false;
                }
                return true;
            }
        }

#endregion

#region Save Actual Book List

        private RelayCommand _saveBooksCommand;

        public ICommand SaveBooksCommand
        {
            get
            {
                if (_saveBooksCommand == null)
                {
                    _saveBooksCommand = new RelayCommand(param => this.SaveBookCommandExecute(),
                        param => this.SaveBookCommandCanExecute);
                }
                return _saveBooksCommand;
            }
        }

        void SaveBookCommandExecute()
        {
            this._bookRepository.saveBooks(AllBooks.ToList());
        }

        private bool SaveBookCommandCanExecute
        {
            get
            {
                if (this.CurrentBooks == null)
                {
                    return false;
                }
                return true;
            }
        }

#endregion

        protected override void OnDispose()
        {
            this.AllBooks.Clear();
            
        }
    }
}