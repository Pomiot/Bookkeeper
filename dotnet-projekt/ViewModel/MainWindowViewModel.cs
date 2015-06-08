using System.Collections.ObjectModel;
using Bookkeeper.DataAccess;
using Bookkeeper.ViewModel;

namespace Bookkeeper.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly BookRepository _bookRepository;

        private ObservableCollection<ViewModelBase> _viewModels;

        public MainWindowViewModel()
        {
            _bookRepository = new BookRepository();
            BookListViewModel viewModel = new BookListViewModel(_bookRepository);
            this.ViewModels.Add(viewModel);
        }

        public ObservableCollection<ViewModelBase> ViewModels
        {
            get
            {
                if (_viewModels == null)
                {
                    _viewModels = new ObservableCollection<ViewModelBase>();

                }
                return _viewModels;
            }
        }
    }
}