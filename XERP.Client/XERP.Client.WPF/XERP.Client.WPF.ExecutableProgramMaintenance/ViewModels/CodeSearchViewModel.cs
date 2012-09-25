﻿using System;
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
    public class CodeSearchViewModel : ViewModelBase<CodeSearchViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private IExecutableProgramServiceAgent _serviceAgent;

        public CodeSearchViewModel()
        { }

        public CodeSearchViewModel(IExecutableProgramServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SearchObject = new ExecutableProgramCode();    
            ResultList = new BindingList<ExecutableProgramCode>();
            SelectedList = new BindingList<ExecutableProgramCode>();
            //make sure of session authentication...
            if (XERP.Client.ClientSessionSingleton.Instance.SessionIsAuthentic)
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

        private ExecutableProgramCode _searchObject;
        public ExecutableProgramCode SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private BindingList<ExecutableProgramCode> _resultList;
        public BindingList<ExecutableProgramCode> ResultList
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
        #endregion Properties

        #region Methods
        private BindingList<ExecutableProgramCode> GetExecutableProgramCodes(string companyID)
        {
            return new BindingList<ExecutableProgramCode>(_serviceAgent.GetExecutableProgramCodes(companyID).ToList());
        }

        private BindingList<ExecutableProgramCode> GetExecutableProgramCodes(ExecutableProgramCode executableProgramQueryObject, string companyID)
        {
            return new BindingList<ExecutableProgramCode>(_serviceAgent.GetExecutableProgramCodes(executableProgramQueryObject, companyID).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetExecutableProgramCodes(SearchObject, ClientSessionSingleton.Instance.CompanyID);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                BindingList<ExecutableProgramCode> selectedList = new BindingList<ExecutableProgramCode>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((ExecutableProgramCode)item);
                }
                MessageBus.Default.Notify(MessageTokens.ExecutableProgramCodeSearchToken.ToString(), this, new NotificationEventArgs<BindingList<ExecutableProgramCode>>("", selectedList));
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