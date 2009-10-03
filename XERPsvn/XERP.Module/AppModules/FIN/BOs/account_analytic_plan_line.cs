
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
	[DefaultProperty("name")]
    [Persistent("account_analytic_plan_line")]
	public partial class account_analytic_plan_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_analytic_plan_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_analytic_plan_line_create_uid
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
            //FK FK_account_analytic_plan_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Double fmin_required;
            [Custom("Caption", "Min Required")]
            public System.Double min_required {
                get { return fmin_required; }
                set { SetPropertyValue("min_required", ref fmin_required, value); }
            }
    
        
            private account_analytic_account froot_analytic_id;
            //FK FK_account_analytic_plan_line_root_analytic_id
            [Custom("Caption", "Root Analytic id")]
            public account_analytic_account root_analytic_id {
                get { return froot_analytic_id; }
                set { SetPropertyValue<account_analytic_account>("root_analytic_id", ref froot_analytic_id, value); }
            }
    
        
            private account_analytic_plan fplan_id;
            //FK FK_account_analytic_plan_line_plan_id
            [Custom("Caption", "Plan Id")]
            public account_analytic_plan plan_id {
                get { return fplan_id; }
                set { SetPropertyValue<account_analytic_plan>("plan_id", ref fplan_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Double fmax_required;
            [Custom("Caption", "Max Required")]
            public System.Double max_required {
                get { return fmax_required; }
                set { SetPropertyValue("max_required", ref fmax_required, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_analytic_plan_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

