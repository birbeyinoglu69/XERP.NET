using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XERP.MenuSecurityDomain.MenuSecurityDataService;
using System.Data.Services.Client;
using System.Reflection;
using System.Collections.ObjectModel;
using XERP.MenuSecurityDomain.Services;
using XERP.Client;
using System.Linq.Dynamic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;


namespace XERP.MenuSecurityDomain.Services
{
    public class MenuSecurityServiceAgent : XERP.MenuSecurityDomain.Services.IMenuSecurityServiceAgent
    {
        private ServiceUtility _serviceUtility = new ServiceUtility();
        private Uri _rootUri;
        private MenuSecurityEntities _context;

        public MenuSecurityServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = _serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new MenuSecurityEntities(_rootUri); 
        }

        public IEnumerable<MenuItem> GetMenuItemsAvailableToUser(string systemUserID)
        {
            //WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<MenuItem>("GetMenuItemsAllowedByUser").AddQueryOption("systemUserID", "'Base'");
            return query;
        }

        public IEnumerable<MenuItem> GetMenuItemByID(string menuItemID, string companyID)
        {
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var queryRelult = (from q in _context.MenuItems
                               where q.MenuItemID == menuItemID &&
                               q.CompanyID == companyID
                               select q).ToList();
            return queryRelult;
        }

        public IEnumerable<MenuItem> GetMenuItemByAutoID(Int64 autoID)
        {
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var queryRelult = (from q in _context.MenuItems
                               where q.AutoID == autoID
                               select q).ToList();
            return queryRelult;
        }

        public IEnumerable<MenuItem> GetMenuItemChildren(string menuItemID, string companyID)
        {
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var queryRelult = (from q in _context.MenuItems
                               where q.ParentMenuID == menuItemID &&
                               q.CompanyID == companyID
                               select q);
            return queryRelult;
        }

        public BitmapImage  GetMenuItemImage(string imageID, string companyID)
        {
            
            
            DBStoredImage dbStoredImage = _context.DBStoredImages.First();
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            dbStoredImage = (from q in _context.DBStoredImages
                             where q.ImageID == imageID &&
                               q.CompanyID == companyID
                             select q).SingleOrDefault();
            
            MemoryStream stream = new MemoryStream();
            stream.Write(dbStoredImage.StoredImage, 0, dbStoredImage.StoredImage.Length);
            //Image image = Image.FromStream(stream);

            System.Windows.Media.Imaging.BitmapImage wpfImg = new System.Windows.Media.Imaging.BitmapImage();
            wpfImg.BeginInit();             
            wpfImg.StreamSource = stream;             
            wpfImg.EndInit();

            return wpfImg;
        }
    }
}
            
//MemoryStream ms = new MemoryStream();             
//ms.Write(imageData, 0, imageData.Length);             
//System.Windows.Media.Imaging.BitmapImage wpfImg = new System.Windows.Media.Imaging.BitmapImage();             
//wpfImg.BeginInit();             
//wpfImg.StreamSource = ms;             
//wpfImg.EndInit(); 