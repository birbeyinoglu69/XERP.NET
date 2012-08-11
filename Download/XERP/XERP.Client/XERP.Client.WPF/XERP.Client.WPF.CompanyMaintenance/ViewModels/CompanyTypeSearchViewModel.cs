using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Services.Client;
using System.ComponentModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;
//XERP namespace
using XERP.Domain.CompanyDomain.CompanyDataService;
using XERP.Domain.CompanyDomain.Services;


namespace XERP.Client.WPF.CompanyMaintenance.ViewModels
{
    public class CompanySearchViewModel : ViewModelBase<CompanySearchViewModel>
    {
        #region Initialization and Cleanup
        //Search Programs will authenticate to the cordinating maintenance form
        private const string _executableProgramName = "CompanyMaintenance";
        private ICompanyServiceAgent _serviceAgent;

        public CompanySearchViewModel()
        { }

        public CompanySearchViewModel(ICompanyServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            CompanyTypeList = GetCompanyTypes();
            CompanyCodeList = GetCompanyCodes();

            SearchObject = new Company();    
            ResultList = new ObservableCollection<Company>();
            SelectedList = new ObservableCollection<Company>();
            //make sure of session authentication...
            if (XERP.Client.ClientSessionSingleton.Instance.SessionIsAuthentic)
            {
                //make sure user has rights to UI...
                DoFormsAuthentication();
            }
            else
            {//User is not authenticated...
                RegisterToReceiveMessages<bool>("StartUpLogIn", OnStartUpLogIn);
                UserControlIsEnabledProperty = false;
                //we will do forms authentication once the log in returns a valid System User...
            }
        }
        #endregion Initialization and Cleanup


        private void DoFormsAuthentication()
        {
            //on log in session information is collected about the system user...
            //we need to make the system user is allowed access to this UI...
            if (ClientSessionSingleton.Instance.ExecutableProgramIDList.Contains(_executableProgramName))
            {
                UserControlIsEnabledProperty = true;
            }
            else
            {
                UserControlIsEnabledProperty = false;
            }
        }

        private void OnStartUpLogIn(object sender, NotificationEventArgs<bool> e)
        {//if true is returned login was successful...
            if (e.Data)
            {
                UserControlIsEnabledProperty = true;
                DoFormsAuthentication();
                NotifyAuthenticated();
            }
            else
            {
                UserControlIsEnabledProperty = false;
            }
            UnregisterToReceiveMessages<bool>("StartUpLogIn", OnStartUpLogIn);
        }


        #region Notifications
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs> CloseNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        #endregion Notifications

        private void NotifyAuthenticated()
        {
            Notify(AuthenticatedNotice, new NotificationEventArgs());
        }


        #region Properties
        private bool? _userControlIsEnabledProperty;
        public bool? UserControlIsEnabledProperty
        {
            get { return _userControlIsEnabledProperty; }
            set
            {
                _userControlIsEnabledProperty = value;
                NotifyPropertyChanged(m => m.UserControlIsEnabledProperty);
            }
        }

        private Company _searchObject;
        public Company SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private ObservableCollection<Company> _resultList;
        public ObservableCollection<Company> ResultList
        {
            get { return _resultList; }
            set
            {
                _resultList = value;
                NotifyPropertyChanged(m => m.ResultList);
            }
        }

        private System.Collections.IList _selectedList;
        public System.Collections.IList SelectedList
        {
            get { return _selectedList; }
            set
            {
                _selectedList = value;
                NotifyPropertyChanged(m => m.SelectedList);
            }
        }

        private ObservableCollection<CompanyType> _companyTypeList;
        public ObservableCollection<CompanyType> CompanyTypeList
        {
            get { return _companyTypeList; }
            set
            {
                _companyTypeList = value;
                NotifyPropertyChanged(m => m.CompanyTypeList);
            }
        }

        private ObservableCollection<CompanyCode> _companyCodeList;
        public ObservableCollection<CompanyCode> CompanyCodeList
        {
            get { return _companyCodeList; }
            set
            {
                _companyCodeList = value;
                NotifyPropertyChanged(m => m.CompanyCodeList);
            }
        }
        #endregion Properties

        #region Methods
        private ObservableCollection<CompanyType> GetCompanyTypes()
        {
            return new ObservableCollection<CompanyType>(_serviceAgent.GetCompanyTypesReadOnly().ToList());
        }

        private ObservableCollection<CompanyCode> GetCompanyCodes()
        {
            return new ObservableCollection<CompanyCode>(_serviceAgent.GetCompanyCodesReadOnly().ToList());
        }

        private ObservableCollection<Company> GetCompanies()
        {
            return new ObservableCollection<Company>(_serviceAgent.GetCompanies().ToList());
        }

        private ObservableCollection<Company> GetCompanies(Company companyQueryObject)
        {
            return new ObservableCollection<Company>(_serviceAgent.GetCompanies(companyQueryObject).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetCompanies(SearchObject);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                ObservableCollection<Company> selectedList = new ObservableCollection<Company>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((Company)item);
                }
                MessageBus.Default.Notify("CompanySearch", this, new NotificationEventArgs<ObservableCollection<Company>>("", selectedList)); 
            }
            NotifyClose("");
        }
        #endregion Commands

        #region Helpers
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }

        private void NotifyClose(string message)
        {
            Notify(CloseNotice, new NotificationEventArgs(message));
        }
        #endregion Helpers
    }
}