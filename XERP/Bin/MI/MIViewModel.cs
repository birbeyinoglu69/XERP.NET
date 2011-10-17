using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using XERP.Web.Services.MenuItemSecurityGroup;
using XERP.Web.Models.MenuItemSecurityGroup;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using System.Linq;
using XERP.ClientModels.NestedMenuItem;
//private MenuItemSecurityGroupDomainContext _menuItemSecurityGroupContext = new MenuItemSecurityGroupDomainContext();
namespace XERP.ViewModel.MIViewModel
{   
    public class MIViewModel : ViewModelBase
    {
        //context to communicate with Domain Service...
        private MenuItemSecurityGroupDomainContext _context;
        //used to return the NestedMenu
        private ObservableCollection<NestedMenuItem> nestedMenuList;
        //used to collect the flatlist of MenuItems
        private List<NestedMenuItem> flatList = null;
        private NestedMenuItem MasterMI = null;
        public MIViewModel()
        {
            //NestedMenuItem nmi = new NestedMenuItem("B", "", 1);
            //nmi.Children.Add(new NestedMenuItem("B_1", "B", 2));
            nestedMenuList = new ObservableCollection<NestedMenuItem>();
            //nestedMenuList.Add(nmi);
            _context = new MenuItemSecurityGroupDomainContext();
            
            var mis = _context.Load(_context.GetFlatMenuItemsQuery());
            mis.Completed += (sender, e) =>
            {
                if (mis.HasError)
                {
                    MessageBox.Show(mis.Error.Message);
                    mis.MarkErrorAsHandled();
                }
                else
                {
                    flatList = new List<NestedMenuItem>();
                    foreach (MenuItem mi in mis.Entities.ToList())
                    {
                        flatList.Add(new NestedMenuItem(mi));
                        //nestedMenuList.Add(new NestedMenuItem(mi));
                    }
                    
                    //get root Item
                    MasterMI = flatList.Single<NestedMenuItem>(x => x.ParentMenuID == "");
            //        //feed root Item and it will recurse and add children and grandchildren and so on...
                    getChidlren(MasterMI);
            //        //nestedMenuList = new ObservableCollection<NestedMenuItem>();
                    nestedMenuList.Add(MasterMI);       
                }
            };
        }
        private void getChidlren(NestedMenuItem pnmi)
        {
            foreach (NestedMenuItem cnmi in flatList.Where(nmi => nmi.ParentMenuID == pnmi.MenuItemID).ToList())
            {
                pnmi.Children.Add(cnmi);
                getChidlren(cnmi);
            }
        }
        
        public ObservableCollection<NestedMenuItem> NestedMenuList  
        {  
            get { return nestedMenuList; }  
        }  
    }
    
}

