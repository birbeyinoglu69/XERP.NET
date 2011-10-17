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
using System.Collections.ObjectModel;
using XERP.Web.Models.MenuItemSecurityGroup;
namespace XERP.ClientModels.NestedMenuItem
{
    public class NestedMenuItem
    {
        public string CompanyID { get; set; }
        public string MenuItemID { get; set; }
        public string ParentMenuID { get; set; }
        public Int64 AutoID { get; set; }
        public ObservableCollection<NestedMenuItem> Children { get; set; }

        public NestedMenuItem(string cid, string mid, string pid, Int64 aid)
        {
            CompanyID = cid;
            MenuItemID = mid;
            ParentMenuID = pid;
            AutoID = aid;
            Children = new ObservableCollection<NestedMenuItem>();
        }
        public NestedMenuItem(MenuItem mi)
        {
            CompanyID = mi.CompanyID;
            MenuItemID = mi.MenuItemID;
            ParentMenuID = mi.ParentMenuID;
            AutoID = mi.AutoID;
            Children = new ObservableCollection<NestedMenuItem>();
        }
        public NestedMenuItem(NestedMenuItem nmi)
        {
            CompanyID = nmi.CompanyID;
            MenuItemID = nmi.MenuItemID;
            ParentMenuID = nmi.ParentMenuID;
            AutoID = nmi.AutoID;
            Children = new ObservableCollection<NestedMenuItem>();
        }
        public NestedMenuItem(NestedMenuItem nmi, params NestedMenuItem[] cnmis)
        {
            CompanyID = nmi.CompanyID;
            MenuItemID = nmi.MenuItemID;
            ParentMenuID = nmi.ParentMenuID;
            AutoID = nmi.AutoID;
            ObservableCollection<NestedMenuItem> oc = new ObservableCollection<NestedMenuItem>();
            foreach (var cnmi in cnmis)
                oc.Add(cnmi);
            Children = oc;
        }

    }      
}
