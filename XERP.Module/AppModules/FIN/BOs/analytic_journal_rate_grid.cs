
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
    [Persistent("analytic_journal_rate_grid")]
	public partial class analytic_journal_rate_grid : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //analytic_journal_rate_grid_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_analytic_journal_rate_grid_create_uid
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
            //FK FK_analytic_journal_rate_grid_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private hr_timesheet_invoice_factor frate_id;
            //FK FK_analytic_journal_rate_grid_rate_id
            [Custom("Caption", "Rate Id")]
            public hr_timesheet_invoice_factor rate_id {
                get { return frate_id; }
                set { SetPropertyValue<hr_timesheet_invoice_factor>("rate_id", ref frate_id, value); }
            }
    
        
            private account_analytic_journal fjournal_id;
            //FK FK_analytic_journal_rate_grid_journal_id
            [Custom("Caption", "Journal Id")]
            public account_analytic_journal journal_id {
                get { return fjournal_id; }
                set { SetPropertyValue<account_analytic_journal>("journal_id", ref fjournal_id, value); }
            }
    
        
            private account_analytic_account faccount_id;
            //FK FK_analytic_journal_rate_grid_account_id
            [Custom("Caption", "Account Id")]
            public account_analytic_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_analytic_account>("account_id", ref faccount_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public analytic_journal_rate_grid(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

