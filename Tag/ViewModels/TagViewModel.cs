using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using Alkl.WinTag.BusinessObjects;

namespace Alkl.WinTag.ViewModels
{
    internal class TagViewModel : ViewModelBase
    {
        private readonly FileViewModel _fileViewModel;

        public string Name { get; }

        public int Count { get; }

        public ICommand AddCommand { get; }

        public ICommand DeleteCommand { get; }

        public TagViewModel(FileViewModel file, string tagName)
        {
            _fileViewModel = file;
            Name = tagName;
            Count = 0;

            AddCommand = new RelayCommand(ExecuteAddCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        }

        public TagViewModel(Tag tag)
        {
            Name = tag.Text;
            Count = tag.Count;
        }

        private void ExecuteAddCommand()
        {
            _fileViewModel.AddTagCommand.Execute(this);
        }

        private void ExecuteDeleteCommand()
        {
            _fileViewModel.DeleteTagCommand.Execute(this);
        }
    }
}