
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
	[DefaultProperty("note")]
    [Persistent("report_account_analytic_planning_line")]
	public partial class report_account_analytic_planning_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //report_account_analytic_planning_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_report_account_analytic_planning_line_create_uid
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
            //FK FK_report_account_analytic_planning_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
            private System.Double famount;
            [Custom("Caption", "Amount")]
            public System.Double amount {
                get { return famount; }
                set { SetPropertyValue("amount", ref famount, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_report_account_analytic_planning_line_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
        
            private account_analytic_account faccount_id;
            //FK FK_report_account_analytic_planning_line_account_id
            [Custom("Caption", "Account Id")]
            public account_analytic_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_analytic_account>("account_id", ref faccount_id, value); }
            }
    
        
            private report_account_analytic_planning fplanning_id;
            //FK FK_report_account_analytic_planning_line_planning_id
            [Custom("Caption", "Planning Id")]
            public report_account_analytic_planning planning_id {
                get { return fplanning_id; }
                set { SetPropertyValue<report_account_analytic_planning>("planning_id", ref fplanning_id, value); }
            }
    
        
            private product_uom famount_unit;
            //FK FK_report_account_analytic_planning_line_amount_unit
            [Custom("Caption", "Amount Unit")]
            public product_uom amount_unit {
                get { return famount_unit; }
                set { SetPropertyValue<product_uom>("amount_unit", ref famount_unit, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public report_account_analytic_planning_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

