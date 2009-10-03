
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
    [DeferredDeletion(false)]
    [Persistent("crossovered_budget_lines")]
	public partial class crossovered_budget_lines : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //crossovered_budget_lines_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_crossovered_budget_lines_create_uid
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
            //FK FK_crossovered_budget_lines_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_analytic_account fanalytic_account_id;
            //FK FK_crossovered_budget_lines_analytic_account_id
            [Custom("Caption", "Analytic Account id")]
            public account_analytic_account analytic_account_id {
                get { return fanalytic_account_id; }
                set { SetPropertyValue<account_analytic_account>("analytic_account_id", ref fanalytic_account_id, value); }
            }
    
        
            private account_budget_post fgeneral_budget_id;
            //FK FK_crossovered_budget_lines_general_budget_id
            [Custom("Caption", "General Budget id")]
            public account_budget_post general_budget_id {
                get { return fgeneral_budget_id; }
                set { SetPropertyValue<account_budget_post>("general_budget_id", ref fgeneral_budget_id, value); }
            }
    
            private System.Decimal fplanned_amount;
            [Custom("Caption", "Planned Amount")]
            public System.Decimal planned_amount {
                get { return fplanned_amount; }
                set { SetPropertyValue("planned_amount", ref fplanned_amount, value); }
            }
    
        
            private crossovered_budget fcrossovered_budget_id;
            //FK FK_crossovered_budget_lines_crossovered_budget_id
            [Custom("Caption", "Crossovered Budget id")]
            public crossovered_budget crossovered_budget_id {
                get { return fcrossovered_budget_id; }
                set { SetPropertyValue<crossovered_budget>("crossovered_budget_id", ref fcrossovered_budget_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public crossovered_budget_lines(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

