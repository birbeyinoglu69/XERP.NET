using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using XERP.Domain.MenuSecurityDomain.Services;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using System.ComponentModel;
namespace XERP.Domain.MenuSecurityDomain.ClientModels
{
    public class NestedMenuItem : INotifyPropertyChanged
    {
        private IMenuSecurityServiceAgent _serviceAgent;
        private string _companyID;
        public string CompanyID 
        {
            get { return _companyID; }
            set
            {
                _companyID = value;
                OnPropertyChanged("CompanyID");
            }
        }

        private string _menuItemID;
        public string MenuItemID
        {
            get { return _menuItemID; }
            set
            {
                _menuItemID = value;
                OnPropertyChanged("MenuItemID");
            }
        }

        private string _parentMenuID;
        public string ParentMenuID
        {
            get { return _parentMenuID; }
            set
            {
                _parentMenuID = value;
                OnPropertyChanged("ParentMenuID");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private bool? _active;
        public bool? Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged("Active");
            }
        }

        private string _menuItemTypeID;
        public string MenuItemTypeID
        {
            get { return _menuItemTypeID; }
            set
            {
                _menuItemTypeID = value;
                OnPropertyChanged("MenuItemTypeID");
            }
        }

        private string _menuItemCodeID;
        public string MenuItemCodeID
        {
            get { return _menuItemCodeID; }
            set
            {
                _menuItemCodeID = value;
                OnPropertyChanged("MenuItemCodeID");
            }
        }

        private string _imageID;
        public string ImageID
        {
            get { return _imageID; }
            set
            {
                _imageID = value;
                OnPropertyChanged("ImageID");
            }
        }

        private int? _displayOrder;
        public int? DisplayOrder
        {
            get { return _displayOrder; }
            set
            {
                _displayOrder = value;
                OnPropertyChanged("DisplayOrder");
            }
        }

        private bool? _executable;
        public bool? Executable
        {
            get { return _executable; }
            set
            {
                _executable = value;
                OnPropertyChanged("Executable");
            }
        }

        private string _executablePath;
        public string ExecutablePath
        {
            get { return _executablePath; }
            set
            {
                _executablePath = value;
                OnPropertyChanged("ExecutablePath");
            }
        }

        private string _executableProgramID;
        public string ExecutableProgramID
        {
            get { return _executableProgramID; }
            set
            {
                _executableProgramID = value;
                OnPropertyChanged("ExecutableProgramID");
            }
        }

        private bool? _hideMenu;
        public bool? HideMenu
        {
            get { return _hideMenu; }
            set
            {
                _hideMenu = value;
                OnPropertyChanged("HideMenu");
            }
        }

        private bool? _allowAll;
        public bool? AllowAll
        {
            get { return _allowAll; }
            set
            {
                _allowAll = value;
                OnPropertyChanged("AllowAll");
            }
        }

        private BitmapImage _dBImageAsImage;
        public BitmapImage DBImageAsImage
        {
            get { return _dBImageAsImage; }
            set
            {
                _dBImageAsImage = value;
                OnPropertyChanged("DBImageAsImage");
            }
        }

        private Int64 _autoID;
        public Int64 AutoID
        {
            get { return _autoID; }
            set
            {
                _autoID = value;
                OnPropertyChanged("AutoID");
            }
        }

        private byte? _isValid;
        public byte? IsValid
        {
            get { return _isValid; }
            set
            {
                _isValid = value;
                OnPropertyChanged("IsValid");
            }
        }

        private byte? _isExpanded;
        public byte? IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        private bool? _isSelected;
        public bool? IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private string _notValidMessage;
        public string NotValidMessage
        {
            get { return _notValidMessage; }
            set
            {
                _notValidMessage = value;
                OnPropertyChanged("NotValidMessage");
            }
        }

        private ObservableCollection<NestedMenuItem> _children;
        public ObservableCollection<NestedMenuItem> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                OnPropertyChanged("Children");
            }
        }

        public NestedMenuItem() 
        {
            Children = new ObservableCollection<NestedMenuItem>();
        }

        public NestedMenuItem(MenuItem menuItem)
        {
            IsValid = menuItem.IsValid;
            IsExpanded = menuItem.IsExpanded;
            IsSelected = menuItem.IsSelected;
            NotValidMessage = menuItem.NotValidMessage;
            CompanyID = menuItem.CompanyID;
            MenuItemID = menuItem.MenuItemID;
            ParentMenuID = menuItem.ParentMenuID;
            Name = menuItem.Name;
            Description = menuItem.Description;
            Active = menuItem.Active;
            MenuItemTypeID = menuItem.MenuItemTypeID;
            MenuItemCodeID = menuItem.MenuItemCodeID;
            ImageID = menuItem.ImageID;
            DisplayOrder = menuItem.DisplayOrder;
            Executable = menuItem.Executable;
            //ExecutablePath = menuItem.ExecutablePath;
            ExecutableProgramID = menuItem.ExecutableProgramID;
            HideMenu = menuItem.HideMenu;
            AllowAll = menuItem.AllowAll;
            AutoID = menuItem.AutoID;
            DBImageAsImage = GetMenuItemImage(menuItem.ImageID, CompanyID);
            Children = new ObservableCollection<NestedMenuItem>();
        }
        public NestedMenuItem(NestedMenuItem nestedMenuItem)
        {
            IsValid = nestedMenuItem.IsValid;
            IsExpanded = nestedMenuItem.IsExpanded;
            IsSelected = nestedMenuItem.IsSelected;
            NotValidMessage = nestedMenuItem.NotValidMessage;
            CompanyID = nestedMenuItem.CompanyID;
            MenuItemID = nestedMenuItem.MenuItemID;
            ParentMenuID = nestedMenuItem.ParentMenuID;
            Name = nestedMenuItem.Name;
            Description = nestedMenuItem.Description;
            Active = nestedMenuItem.Active;
            MenuItemTypeID = nestedMenuItem.MenuItemTypeID;
            MenuItemCodeID = nestedMenuItem.MenuItemCodeID;
            ImageID = nestedMenuItem.ImageID;
            DisplayOrder = nestedMenuItem.DisplayOrder;
            Executable = nestedMenuItem.Executable;
            ExecutablePath = nestedMenuItem.ExecutablePath;
            ExecutableProgramID = nestedMenuItem.ExecutableProgramID;
            HideMenu = nestedMenuItem.HideMenu;
            AllowAll = nestedMenuItem.AllowAll;
            AutoID = nestedMenuItem.AutoID;
            DBImageAsImage = GetMenuItemImage(nestedMenuItem.ImageID, CompanyID);
            Children = new ObservableCollection<NestedMenuItem>();
        }
        public NestedMenuItem(NestedMenuItem nestedMenuItem, params NestedMenuItem[] children)
        {
            IsValid = nestedMenuItem.IsValid;
            IsExpanded = nestedMenuItem.IsExpanded;
            IsSelected = nestedMenuItem.IsSelected;
            NotValidMessage = nestedMenuItem.NotValidMessage;
            CompanyID = nestedMenuItem.CompanyID;
            MenuItemID = nestedMenuItem.MenuItemID;
            ParentMenuID = nestedMenuItem.ParentMenuID;
            Name = nestedMenuItem.Name;
            Description = nestedMenuItem.Description;
            Active = nestedMenuItem.Active;
            MenuItemTypeID = nestedMenuItem.MenuItemTypeID;
            MenuItemCodeID = nestedMenuItem.MenuItemCodeID;
            ImageID = nestedMenuItem.ImageID;
            DisplayOrder = nestedMenuItem.DisplayOrder;
            Executable = nestedMenuItem.Executable;
            ExecutablePath = nestedMenuItem.ExecutablePath;
            ExecutableProgramID = nestedMenuItem.ExecutableProgramID;
            HideMenu = nestedMenuItem.HideMenu;
            AllowAll = nestedMenuItem.AllowAll;
            AutoID = nestedMenuItem.AutoID;
            DBImageAsImage = GetMenuItemImage(nestedMenuItem.ImageID, CompanyID);
            ObservableCollection<NestedMenuItem> oc = new ObservableCollection<NestedMenuItem>();
            foreach (var child in children)
                oc.Add(child);
            Children = oc;
        }

        private BitmapImage GetMenuItemImage(string imageID, string companyID)
        {
            _serviceAgent = new MenuSecurityServiceAgent();
            return _serviceAgent.GetMenuItemImage(imageID, companyID);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }      
}
