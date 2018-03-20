using Alkl.WinTag.Models;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace Alkl.WinTag.ViewModels
{
    internal class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IPersistanceModel, JsonFileModel>();
            SimpleIoc.Default.Register<ITagsModel, TagManager>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<TagCloudViewModel>();
            SimpleIoc.Default.Register<FileListViewModel>();

            Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public TagCloudViewModel TagCloudViewModel => ServiceLocator.Current.GetInstance<TagCloudViewModel>();

        public FileListViewModel FileListViewModel => ServiceLocator.Current.GetInstance<FileListViewModel>();

        private void NotifyUserMethod(NotificationMessage message)
        {
            MessageBox.Show(message.Notification);
        }
    }
}
