using System;
using Alkl.WinTag.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Alkl.WinTag.ViewModels
{
    internal class FileListViewModel : ViewModelBase, IDropTarget
    {
        private readonly IPersistanceModel _persistanceModel;

        private readonly ITagsModel _tagsModel;
        
        public ObservableCollection<FileViewModel> Files { get; } = new ObservableCollection<FileViewModel>();

        public FileListViewModel(IPersistanceModel persistanceModel, ITagsModel tagsModel)
        {
            _tagsModel = tagsModel;
            _persistanceModel = persistanceModel;
            _persistanceModel.FilesChanged += PersistanceModelOnFilesChanged;

#if DEBUG
            _persistanceModel.Load(Environment.CurrentDirectory);
#endif
        }

        private void PersistanceModelOnFilesChanged(object sender, FilesChangedEventArgs filesChangedEventArgs)
        {
            Files.Clear();

            foreach (var file in filesChangedEventArgs.Files)
            {
                Files.Add(new FileViewModel(file.Name, _persistanceModel, _tagsModel));
            }
        }
        
        public void DragOver(IDropInfo dropInfo)
        {
            var draggedPath = ((DataObject) dropInfo.Data).GetFileDropList().Cast<string>().FirstOrDefault();
            var valid = Directory.Exists(draggedPath);
            dropInfo.Effects = valid ? DragDropEffects.Copy : DragDropEffects.None;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var draggedPath = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>().FirstOrDefault();
            _persistanceModel.Load(draggedPath);
        }
    }
}
