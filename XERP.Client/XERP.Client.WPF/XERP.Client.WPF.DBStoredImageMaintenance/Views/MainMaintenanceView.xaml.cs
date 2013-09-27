using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using XERP.Client.WPF.DBStoredImageMaintenance.ViewModels;
using SimpleMvvmToolkit;
using System.Collections.Generic;
using XERP.Client.Models;
using XERP.Client.WPF;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
namespace XERP.Client.WPF.DBStoredImageMaintenance.Views
{
    /// <summary>
    /// Interaction logic for DBStoredImageMaintenanceView.xaml
    /// </summary>
    public partial class MainMaintenanceView : UserControl
    {
        ViewModelLocator _vml = new ViewModelLocator();
        MainMaintenanceViewModel _viewModel;
        private LogIn.MainWindow _logInWindow;
        public MainMaintenanceView()
        {
            try
            {
                this.Resources.MergedDictionaries.Add(XERP.Client.WPF.DBStoredImageMaintenance.Resources.SharedDictionaryManager.MenuImagesSharedDictionary);
                this.Resources.MergedDictionaries.Add(XERP.Client.WPF.DBStoredImageMaintenance.Resources.SharedDictionaryManager.BaseControlsSharedDictionary);
                _viewModel = _vml.MainMaintenanceViewModel;
                DataContext = _viewModel;
                _viewModel.ErrorNotice += OnErrorNotice;
                _viewModel.MessageNotice += OnMessageNotice;
                _viewModel.SearchNotice += OnSearchNotice;
                _viewModel.SaveRequiredNotice += OnSaveRequiredNotice;
                _viewModel.NewRecordNeededNotice += OnNewRecordNeededNotice;
                _viewModel.AuthenticatedNotice += OnAuthenticatedNotice;
                //_viewModel.NewRecordCreatedNotice += OnNewRecordCreatedNotice;

                InitializeComponent();
                
                if (!XERP.Client.ClientSessionSingleton.Instance.SessionIsAuthentic)
                {
                    DisplayLogIn();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OnErrorNotice(object sender, NotificationEventArgs<Exception> e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        }

        private void OnMessageNotice(object sender, NotificationEventArgs e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            WiggleToGhostField();
            if (_viewModel.AllowCommit == true)
            {
                _viewModel.SaveCommand();
            }
        }

        private void WiggleToGhostField()
        {//textboxex by default set bindings on lost focus
            //so we have a ghost control on each tab and will wiggle focus to it and back to commit data on saves...
            UIElement elem = Keyboard.FocusedElement as UIElement;
            Keyboard.Focus(ghost);
            //Keyboard.Focus(ghost2);
            Keyboard.Focus(elem);
        }

        private void OnSearchNotice(Object sender, NotificationEventArgs e)
        {
            MainSearchWindow searchWindow = new MainSearchWindow();
            searchWindow.Show(); 
        }

        private void OnAuthenticatedNotice(Object sender, NotificationEventArgs e)
        {
            _logInWindow.Close();
        }

        private void DisplayLogIn()
        {
            _logInWindow = new LogIn.MainWindow();
            _logInWindow.ShowDialog(); 
        }

        private void OnSaveRequiredNotice(Object sender, NotificationEventArgs<bool, MessageBoxResult> e)
        {
            string messageBoxText = e.Message;
            string caption = "XERP Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    e.Completed(MessageBoxResult.Yes);
                    break;
                case MessageBoxResult.No:
                    e.Completed(MessageBoxResult.No);
                    break;
                case MessageBoxResult.Cancel:
                    e.Completed(MessageBoxResult.Cancel);
                    break;
            }
        }

        private void OnNewRecordNeededNotice(Object sender, NotificationEventArgs<bool, MessageBoxResult> e)
        {
            string messageBoxText = e.Message;
            string caption = "XERP Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    e.Completed(MessageBoxResult.Yes);
                    break;
                case MessageBoxResult.No:
                    e.Completed(MessageBoxResult.No);
                    break;
                case MessageBoxResult.Cancel:
                    e.Completed(MessageBoxResult.Cancel);
                    break;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//set the focus as the control is loaded...
            txtKey.Focus();
        }
        //Hot keys Control S--Save Control N--New, Delete Key--Delete
        private void UserControl_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N || e.Key == Key.S)
            {
                if (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    switch (e.Key)
                    {
                        case Key.N:
                            if (_viewModel.AllowNew)
                            {
                                _viewModel.NewDBStoredImageCommand("");
                                return;
                            }
                            //MessageBox.Show("New DBStoredImage Is Not Enabled...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case Key.S:
                            if (mnuSave.IsEnabled)
                            {
                                WiggleToGhostField();
                                _viewModel.SaveCommand();
                                return;
                            }
                            //MessageBox.Show("Save Is Not Enabled...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void ReplaceGetIcon_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select An Icon";
            dlg.DefaultExt = ".png";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + 
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + 
                "Portable Network Graphic (*.png)|*.png" +
                "ICON (*.ico;*.icon)|*.ico;*.icon|"; 
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                BitmapImage img = new BitmapImage(new Uri(dlg.FileName));
                //don't allow for huge picture imports...
                if (img.PixelWidth > 200 || img.PixelHeight > 200)
                {
                    MessageBox.Show("Image is too large.  Reduce the image size to 200 by 200 pixels or find another image that is.");
                    return;
                }

                //set the bit stream...
                int height = img.PixelHeight;
                int width = img.PixelWidth;
                int stride = width * ((img.Format.BitsPerPixel + 7) / 8);
                byte[] bits = new byte[height * stride];
                img.CopyPixels(bits, stride, 0);
                //set the viewmodel objects accordingly...
                _viewModel.IconImage = img;
                _viewModel.SelectedDBStoredImage.StoredImage = bits;
            }
        }
    }  
}
