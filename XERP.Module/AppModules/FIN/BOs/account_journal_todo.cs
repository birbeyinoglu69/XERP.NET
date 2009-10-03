
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
    [Persistent("account_journal_todo")]
	public partial class account_journal_todo : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_journal_todo_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_journal_todo_create_uid
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
            //FK FK_account_journal_todo_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_account fdefault_debit_account_id;
            //FK FK_account_journal_todo_default_debit_account_id
            [Custom("Caption", "Default Debit account id")]
            public account_account default_debit_account_id {
                get { return fdefault_debit_account_id; }
                set { SetPropertyValue<account_account>("default_debit_account_id", ref fdefault_debit_account_id, value); }
            }
    
        
            private account_account fdefault_credit_account_id;
            //FK FK_account_journal_todo_default_credit_account_id
            [Custom("Caption", "Default Credit account id")]
            public account_account default_credit_account_id {
                get { return fdefault_credit_account_id; }
                set { SetPropertyValue<account_account>("default_credit_account_id", ref fdefault_credit_account_id, value); }
            }
    
        
            private account_journal fname;
            //FK FK_account_journal_todo_name
            [Custom("Caption", "Name")]
            public account_journal name {
                get { return fname; }
                set { SetPropertyValue<account_journal>("name", ref fname, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_journal_todo(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

