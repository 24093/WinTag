using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Alkl.WinTag.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Alkl.WinTag.ViewModels
{
    internal class TagCloudViewModel : ViewModelBase
    {
        private readonly ITagsModel _tagsModel;

        public ICommand AddCommand { get; }

        public ObservableCollection<TagViewModel> Tags { get; } = new ObservableCollection<TagViewModel>();

        public TagCloudViewModel(ITagsModel tagsModel)
        {
            _tagsModel = tagsModel;

            foreach (var tag in _tagsModel.Tags)
            {
                Tags.Add(new TagViewModel(tag));
            }

            AddCommand = new RelayCommand<string>(ExecuteAddCommand);
        }

        private void ExecuteAddCommand(string tagName)
        {
            _tagsModel.AddTag(tagName);
        }
    }
}
