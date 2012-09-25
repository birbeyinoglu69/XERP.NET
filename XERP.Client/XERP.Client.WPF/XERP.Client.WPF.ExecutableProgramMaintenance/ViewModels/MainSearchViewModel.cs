using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
//XERP namespace
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;


namespace XERP.Client.WPF.ExecutableProgramMaintenance.ViewModels
{
    public class MainSearchViewModel : ViewModelBase<MainSearchViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private IExecutableProgramServiceAgent _serviceAgent;

        public MainSearchViewModel()
        { }

        public MainSearchViewModel(IExecutableProgramServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            ExecutableProgramTypeList = GetExecutableProgramTypes();
            ExecutableProgramCodeList = GetExecutableProgramCodes();

            SearchObject = new ExecutableProgram();    
            ResultList = new BindingList<ExecutableProgram>();
            SelectedList = new BindingList<ExecutableProgram>();
            //make sure of session authentication...
            if (ClientSessionSingleton.Instance.SessionIsAuthentic)
            {
                //make sure user has rights to UI...
                DoFormsAuthentication();
            }
            else
            {//User is not authenticated...
                RegisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
                FormIsEnabled = false;
                //we will do forms authentication once the log in returns a valid System User...
            }
        }
        #endregion Initialization and Cleanup

        #region Authentication
        private void DoFormsAuthentication()
        {
            //on log in session information is collected about the system user...
            //we need to make the system user is allowed access to this UI...
            if (ClientSessionSingleton.Instance.ExecutableProgramIDList.Contains(_globalProperties.ExecutableProgramName))
            {
                FormIsEnabled = true;
            }
            else
            {
                FormIsEnabled = false;
            }
        }

        private void OnStartUpLogIn(object sender, NotificationEventArgs<bool> e)
        {//if true is returned login was successful...
            if (e.Data)
            {
                FormIsEnabled = true;
                DoFormsAuthentication();
                NotifyAuthenticated();
            }
            else
            {
                FormIsEnabled = false;
            }
            UnregisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
        }
        #endregion Authentication

        #region Notifications
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs> MessageNotice;
        public event EventHandler<NotificationEventArgs> CloseNotice;
        public event EventHandler<NotificationEventArgs> AuthenticatedNotice;
        #endregion Notifications

        private void NotifyAuthenticated()
        {
            Notify(AuthenticatedNotice, new NotificationEventArgs());
        }


        #region Properties
        

        private bool? _formIsEnabled;
        public bool? FormIsEnabled
        {
            get { return _formIsEnabled; }
            set
            {
                _formIsEnabled = value;
                NotifyPropertyChanged(m => m.FormIsEnabled);
            }
        }

        private ExecutableProgram _searchObject;
        public ExecutableProgram SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private BindingList<ExecutableProgram> _resultList;
        public BindingList<ExecutableProgram> ResultList
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

        private ObservableCollection<ExecutableProgramType> _executableProgramTypeList;
        public ObservableCollection<ExecutableProgramType> ExecutableProgramTypeList
        {
            get { return _executableProgramTypeList; }
            set
            {
                _executableProgramTypeList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramTypeList);
            }
        }

        private ObservableCollection<ExecutableProgramCode> _executableProgramCodeList;
        public ObservableCollection<ExecutableProgramCode> ExecutableProgramCodeList
        {
            get { return _executableProgramCodeList; }
            set
            {
                _executableProgramCodeList = value;
                NotifyPropertyChanged(m => m.ExecutableProgramCodeList);
            }
        }
        #endregion Properties

        #region Methods
        private ObservableCollection<ExecutableProgramType> GetExecutableProgramTypes()
        {
            return new ObservableCollection<ExecutableProgramType>(_serviceAgent.GetExecutableProgramTypesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private ObservableCollection<ExecutableProgramCode> GetExecutableProgramCodes()
        {
            return new ObservableCollection<ExecutableProgramCode>(_serviceAgent.GetExecutableProgramCodesReadOnly(ClientSessionSingleton.Instance.CompanyID).ToList());
        }

        private BindingList<ExecutableProgram> GetExecutablePrograms(string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<ExecutableProgram>(_serviceAgent.GetExecutablePrograms(companyID).ToList());
        }

        private BindingList<ExecutableProgram> GetExecutablePrograms(ExecutableProgram executableProgramQueryObject, string companyID)
        {//note this get is to the singleton repository...
            return new BindingList<ExecutableProgram>(_serviceAgent.GetExecutablePrograms(executableProgramQueryObject, companyID).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetExecutablePrograms(SearchObject, ClientSessionSingleton.Instance.CompanyID);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                BindingList<ExecutableProgram> selectedList = new BindingList<ExecutableProgram>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((ExecutableProgram)item);
                }
                MessageBus.Default.Notify(MessageTokens.ExecutableProgramSearchToken.ToString(), this, new NotificationEventArgs<BindingList<ExecutableProgram>>("", selectedList)); 
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