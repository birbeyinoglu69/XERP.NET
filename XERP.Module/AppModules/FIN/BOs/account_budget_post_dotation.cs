
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
	[DefaultProperty("name")]
    [Persistent("account_budget_post_dotation")]
	public partial class account_budget_post_dotation : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_budget_post_dotation_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_budget_post_dotation_create_uid
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
            //FK FK_account_budget_post_dotation_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_budget_post fpost_id;
            //FK FK_account_budget_post_dotation_post_id
            [Custom("Caption", "Post Id")]
            public account_budget_post post_id {
                get { return fpost_id; }
                set { SetPropertyValue<account_budget_post>("post_id", ref fpost_id, value); }
            }
    
            private System.Decimal famount;
            [Custom("Caption", "Amount")]
            public System.Decimal amount {
                get { return famount; }
                set { SetPropertyValue("amount", ref famount, value); }
            }
    
        
            private account_period fperiod_id;
            //FK FK_account_budget_post_dotation_period_id
            [Custom("Caption", "Period Id")]
            public account_period period_id {
                get { return fperiod_id; }
                set { SetPropertyValue<account_period>("period_id", ref fperiod_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Double ftot_planned;
            [Custom("Caption", "Tot Planned")]
            public System.Double tot_planned {
                get { return ftot_planned; }
                set { SetPropertyValue("tot_planned", ref ftot_planned, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_budget_post_dotation(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

