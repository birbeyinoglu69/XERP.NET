
using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using DevExpress.Data.Filtering;
using System.Collections.Generic;

namespace XERP
{



    [DefaultClassOptions]
    [DeferredDeletion(true)]
	[DefaultProperty("code")]
    [Persistent("account_account")]
	public partial class account_account : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_account_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
            private System.Int32 fparent_left;
            [Custom("Caption", "Parent Left")]
            public System.Int32 parent_left {
                get { return fparent_left; }
                set { SetPropertyValue("parent_left", ref fparent_left, value); }
            }
    
            private System.Int32 fparent_right;
            [Custom("Caption", "Parent Right")]
            public System.Int32 parent_right {
                get { return fparent_right; }
                set { SetPropertyValue("parent_right", ref fparent_right, value); }
            }
    
        
            private res_users fcreate_uid;
            //FK FK_account_account_create_uid
            [Custom("Caption", "Create Uid")]
            public res_users create_uid {
                get { return fcreate_uid; }
                set { SetPropertyValue<res_users>("create_uid", ref fcreate_uid, value); }
            }
    
            private DateTime? fcreate_date;
            [Custom("Caption", "Create Date")]
            public DateTime? create_date {
                get { return fcreate_date; }
                set { SetPropertyValue("create_date", ref fcreate_date, value); }
            }
    
            private DateTime? fwrite_date;
            [Custom("Caption", "Write Date")]
            public DateTime? write_date {
                get { return fwrite_date; }
                set { SetPropertyValue("write_date", ref fwrite_date, value); }
            }
    
        
            private res_users fwrite_uid;
            //FK FK_account_account_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fcode;
            [Size(64)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
            private System.Boolean freconcile;
            [Custom("Caption", "Reconcile")]
            public System.Boolean reconcile {
                get { return freconcile; }
                set { SetPropertyValue("reconcile", ref freconcile, value); }
            }
    
        
            private res_currency fcurrency_id;
            //FK FK_account_account_currency_id
            [Custom("Caption", "Currency Id")]
            public res_currency currency_id {
                get { return fcurrency_id; }
                set { SetPropertyValue<res_currency>("currency_id", ref fcurrency_id, value); }
            }
    
        
            private account_account_type fuser_type;
            //FK FK_account_account_user_type
            [Custom("Caption", "User Type")]
            public account_account_type user_type {
                get { return fuser_type; }
                set { SetPropertyValue<account_account_type>("user_type", ref fuser_type, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.Boolean fcheck_history;
            [Custom("Caption", "Check History")]
            public System.Boolean check_history {
                get { return fcheck_history; }
                set { SetPropertyValue("check_history", ref fcheck_history, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private res_company fcompany_id;
            //FK FK_account_account_company_id
            [Custom("Caption", "Company Id")]
            public res_company company_id {
                get { return fcompany_id; }
                set { SetPropertyValue<res_company>("company_id", ref fcompany_id, value); }
            }
    
            private System.String fshortcut;
            [Size(12)]
            [Custom("Caption", "Shortcut")]
            public System.String shortcut {
                get { return fshortcut; }
                set { SetPropertyValue("shortcut", ref fshortcut, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
        
            private account_account fparent_id;
            //FK FK_account_account_parent_id
            [Custom("Caption", "Parent Id")]
            public account_account parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<account_account>("parent_id", ref fparent_id, value); }
            }
    
            private System.String fcurrency_mode;
            [Size(16)]
            [Custom("Caption", "Currency Mode")]
            public System.String currency_mode {
                get { return fcurrency_mode; }
                set { SetPropertyValue("currency_mode", ref fcurrency_mode, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.Decimal fopen_bal;
            [Custom("Caption", "Open Bal")]
            public System.Decimal open_bal {
                get { return fopen_bal; }
                set { SetPropertyValue("open_bal", ref fopen_bal, value); }
            }
    
            private System.Int32 flevel;
            [Custom("Caption", "Level")]
            public System.Int32 level {
                get { return flevel; }
                set { SetPropertyValue("level", ref flevel, value); }
            }
    
            private System.String ftype1;
            [Size(16)]
            [Custom("Caption", "Type1")]
            public System.String type1 {
                get { return ftype1; }
                set { SetPropertyValue("type1", ref ftype1, value); }
            }
    
        
            private account_journal fjournal_id;
            //FK FK_account_account_journal_id
            [Custom("Caption", "Journal Id")]
            public account_journal journal_id {
                get { return fjournal_id; }
                set { SetPropertyValue<account_journal>("journal_id", ref fjournal_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_account(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

