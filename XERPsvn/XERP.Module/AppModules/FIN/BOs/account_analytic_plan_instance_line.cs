
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
    [Persistent("account_analytic_plan_instance_line")]
	public partial class account_analytic_plan_instance_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_analytic_plan_instance_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_analytic_plan_instance_line_create_uid
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
            //FK FK_account_analytic_plan_instance_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_analytic_account fanalytic_account_id;
            //FK FK_account_analytic_plan_instance_line_analytic_account_id
            [Custom("Caption", "Analytic Account id")]
            public account_analytic_account analytic_account_id {
                get { return fanalytic_account_id; }
                set { SetPropertyValue<account_analytic_account>("analytic_account_id", ref fanalytic_account_id, value); }
            }
    
            private System.Double frate;
            [Custom("Caption", "Rate")]
            public System.Double rate {
                get { return frate; }
                set { SetPropertyValue("rate", ref frate, value); }
            }
    
        
            private account_analytic_plan_instance fplan_id;
            //FK FK_account_analytic_plan_instance_line_plan_id
            [Custom("Caption", "Plan Id")]
            public account_analytic_plan_instance plan_id {
                get { return fplan_id; }
                set { SetPropertyValue<account_analytic_plan_instance>("plan_id", ref fplan_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_analytic_plan_instance_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

