
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
    [Persistent("account_subscription")]
	public partial class account_subscription : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_subscription_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_subscription_create_uid
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
            //FK FK_account_subscription_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_model fmodel_id;
            //FK FK_account_subscription_model_id
            [Custom("Caption", "Model Id")]
            public account_model model_id {
                get { return fmodel_id; }
                set { SetPropertyValue<account_model>("model_id", ref fmodel_id, value); }
            }
    
            private System.Int32 fperiod_nbr;
            [Custom("Caption", "Period Nbr")]
            public System.Int32 period_nbr {
                get { return fperiod_nbr; }
                set { SetPropertyValue("period_nbr", ref fperiod_nbr, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Int32 fperiod_total;
            [Custom("Caption", "Period Total")]
            public System.Int32 period_total {
                get { return fperiod_total; }
                set { SetPropertyValue("period_total", ref fperiod_total, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String fperiod_type;
            [Size(16)]
            [Custom("Caption", "Period Type")]
            public System.String period_type {
                get { return fperiod_type; }
                set { SetPropertyValue("period_type", ref fperiod_type, value); }
            }
    
            private System.String fref1;
            [Size(16)]
            [Custom("Caption", "Ref")]
            public System.String ref1 {
                get { return fref1; }
                set { SetPropertyValue("ref1", ref fref1, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_subscription(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

