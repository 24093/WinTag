using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GongSolutions.Wpf.DragDrop;
using Tag.Models;

namespace Tag.ViewModels
{
    internal class FileListViewModel : ViewModelBase, IDropTarget
    {
        private readonly IPersistanceModel _persistanceModel;

        public FileListViewModel(IPersistanceModel persistanceModel)
        {
            _persistanceModel = persistanceModel;
        }

        public void DragOver(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject) dropInfo.Data).GetFileDropList().Cast<string>();

            if (dragFileList.Any(filename => !File.Exists(filename)))
            {
                dropInfo.Effects = DragDropEffects.None;
            }
            else
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();

            _persistanceModel.AddFiles(dragFileList);
        }
    }
}
