using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace XERP.Client.WPF.CompanyMaintenance
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class MainSearchWindow : Window
    {
        public MainSearchWindow()
        {
            try
            {

                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
