﻿using System;
using System.Linq;
using System.ComponentModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
//XERP namespace
using XERP.Domain.SecurityGroupDomain.SecurityGroupDataService;
using XERP.Domain.SecurityGroupDomain.Services;

namespace XERP.Client.WPF.SecurityGroupMaintenance.ViewModels
{
    public class TypeSearchViewModel : ViewModelBase<TypeSearchViewModel>
    {
        #region Initialization and Cleanup
        //GlobalProperties Class allows us to share properties amonst multiple classes...
        private GlobalProperties _globalProperties = new GlobalProperties();
        private ISecurityGroupServiceAgent _serviceAgent;

        public TypeSearchViewModel()
        { }

        public TypeSearchViewModel(ISecurityGroupServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SearchObject = new SecurityGroupType();    
            ResultList = new BindingList<SecurityGroupType>();
            SelectedList = new BindingList<SecurityGroupType>();
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

        private SecurityGroupType _searchObject;
        public SecurityGroupType SearchObject
        {
            get { return _searchObject; }
            set
            {
                _searchObject = value;
                NotifyPropertyChanged(m => m.SearchObject);
            }
        }

        private BindingList<SecurityGroupType> _resultList;
        public BindingList<SecurityGroupType> ResultList
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
        private BindingList<SecurityGroupType> GetSecurityGroupTypes()
        {
            return new BindingList<SecurityGroupType>(_serviceAgent.GetSecurityGroupTypes().ToList());
        }

        private BindingList<SecurityGroupType> GetSecurityGroupTypes(SecurityGroupType securityGroupQueryObject)
        {
            return new BindingList<SecurityGroupType>(_serviceAgent.GetSecurityGroupTypes(securityGroupQueryObject).ToList());
        }
        #endregion Methods

        #region Commands
        public void SearchCommand()
        {
            ResultList = GetSecurityGroupTypes(SearchObject);
        }

        public void CommitSearchCommand()
        {
            if (SelectedList != null)
            {
                BindingList<SecurityGroupType> selectedList = new BindingList<SecurityGroupType>();
                foreach (var item in SelectedList)
                {
                    selectedList.Add((SecurityGroupType)item);
                }
                MessageBus.Default.Notify(MessageTokens.SecurityGroupTypeSearchToken.ToString(), this, new NotificationEventArgs<BindingList<SecurityGroupType>>("", selectedList));
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