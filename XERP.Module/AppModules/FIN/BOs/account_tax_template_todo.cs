
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
    [Persistent("account_tax_template_todo")]
	public partial class account_tax_template_todo : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_tax_template_todo_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_tax_template_todo_create_uid
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
            //FK FK_account_tax_template_todo_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_account_template faccount_collected_id;
            //FK FK_account_tax_template_todo_account_collected_id
            [Custom("Caption", "Account Collected id")]
            public account_account_template account_collected_id {
                get { return faccount_collected_id; }
                set { SetPropertyValue<account_account_template>("account_collected_id", ref faccount_collected_id, value); }
            }
    
        
            private account_account_template faccount_paid_id;
            //FK FK_account_tax_template_todo_account_paid_id
            [Custom("Caption", "Account Paid id")]
            public account_account_template account_paid_id {
                get { return faccount_paid_id; }
                set { SetPropertyValue<account_account_template>("account_paid_id", ref faccount_paid_id, value); }
            }
    
        
            private account_tax_template fname;
            //FK FK_account_tax_template_todo_name
            [Custom("Caption", "Name")]
            public account_tax_template name {
                get { return fname; }
                set { SetPropertyValue<account_tax_template>("name", ref fname, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_tax_template_todo(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

