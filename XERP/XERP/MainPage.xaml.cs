using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using XERP.Web;
using System.ServiceModel.DomainServices.Client;
using XERP.Web.Services.Company;
using XERP.Web.Models.Company;
using XERP.Web.Models.Plant;
using XERP.Web.Services.Plant;
using XERP.Web.Models.SystemUser;
using XERP.Web.Services.SystemUser;
namespace XERP
{
    public partial class MainPage : UserControl
    {
        private CompanyDomainContext _companyContext = new CompanyDomainContext();
        private PlantDomainContext _plantContext = new PlantDomainContext();
        private SystemUserDomainContext _systemUserContext = new SystemUserDomainContext();
        public MainPage()
        {
            InitializeComponent();
            LoadOperation<XERP.Web.Models.Company.Company> loadOp = this._companyContext.Load(this._companyContext.GetCompaniesQuery());
            CompanyGrid.ItemsSource = loadOp.Entities;
            LoadOperation<XERP.Web.Models.Plant.Company> loadOp2 = this._plantContext.Load(this._plantContext.GetCompaniesQuery());
            PlantGrid.ItemsSource = loadOp2.Entities;
            LoadOperation<XERP.Web.Models.SystemUser.Company> loadOp3 = this._systemUserContext.Load(this._systemUserContext.GetCompaniesQuery());
            SystemUserGrid.ItemsSource = loadOp3.Entities;
        }

    }
}
