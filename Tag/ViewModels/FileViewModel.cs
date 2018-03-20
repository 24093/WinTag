using System;
using Alkl.WinTag.BusinessObjects;
using Alkl.WinTag.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Alkl.WinTag.ViewModels
{
    internal class FileViewModel : ViewModelBase
    {
        private readonly IPersistanceModel _persistanceModel;

        private readonly ITagsModel _tagsModel;
        
        public string Name { get; }

        public ObservableCollection<TagViewModel> Tags { get; } = new ObservableCollection<TagViewModel>();

        public ObservableCollection<TagViewModel> AvailableTags { get; } = new ObservableCollection<TagViewModel>();

        public bool IsSelected { get; set; }

        public ICommand AddTagCommand { get; }

        public ICommand DeleteTagCommand { get; }

        public FileViewModel(string fileName, IPersistanceModel persistanceModel, ITagsModel tagsModel)
        {
            Name = fileName;
            _tagsModel = tagsModel;
            _persistanceModel = persistanceModel;
            _persistanceModel.FileChanged += PersistanceModelOnFileChanged;
            
            AddTagCommand = new GalaSoft.MvvmLight.Command.RelayCommand<TagViewModel>(ExecuteAddTagCommand);
            DeleteTagCommand = new RelayCommand<TagViewModel>(ExecuteDeleteTagCommand);
        }

        private void PersistanceModelOnFileChanged(object sender, FileChangedEventArgs fileChangedEventArgs)
        {
            if (fileChangedEventArgs.File.Name != Name)
                return;

            Tags.Clear();
            AvailableTags.Clear();

            foreach (var tag in _tagsModel.Tags.Select(t => t.Text))
            {
                if (fileChangedEventArgs.File.Tags.Contains(tag))
                {
                    Tags.Add(new TagViewModel(this, tag));
                }
                else
                {
                    AvailableTags.Add(new TagViewModel(this, tag));
                }
            }
        }

        private void ExecuteAddTagCommand(TagViewModel tag)
        {
            _persistanceModel.AddTag(Name, tag.Name);
        }

        private void ExecuteDeleteTagCommand(TagViewModel tag)
        {
            _persistanceModel.RemoveTag(Name, tag.Name);
        }
    }
}
