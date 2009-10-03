
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
    [Persistent("account_analytic_default")]
	public partial class account_analytic_default : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_analytic_default_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_analytic_default_create_uid
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
            //FK FK_account_analytic_default_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_account_analytic_default_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_account_analytic_default_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
        
            private res_company fcompany_id;
            //FK FK_account_analytic_default_company_id
            [Custom("Caption", "Company Id")]
            public res_company company_id {
                get { return fcompany_id; }
                set { SetPropertyValue<res_company>("company_id", ref fcompany_id, value); }
            }
    
        
            private account_analytic_account fanalytic_id;
            //FK FK_account_analytic_default_analytic_id
            [Custom("Caption", "Analytic Id")]
            public account_analytic_account analytic_id {
                get { return fanalytic_id; }
                set { SetPropertyValue<account_analytic_account>("analytic_id", ref fanalytic_id, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_account_analytic_default_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
        
            private account_analytic_plan_instance fanalytics_id;
            //FK FK_account_analytic_default_analytics_id
            [Custom("Caption", "Analytics Id")]
            public account_analytic_plan_instance analytics_id {
                get { return fanalytics_id; }
                set { SetPropertyValue<account_analytic_plan_instance>("analytics_id", ref fanalytics_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_analytic_default(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

