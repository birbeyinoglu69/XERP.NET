
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
    [Persistent("analytic_user_funct_grid")]
	public partial class analytic_user_funct_grid : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //analytic_user_funct_grid_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_analytic_user_funct_grid_create_uid
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
            //FK FK_analytic_user_funct_grid_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_analytic_user_funct_grid_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_analytic_user_funct_grid_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private account_analytic_account faccount_id;
            //FK FK_analytic_user_funct_grid_account_id
            [Custom("Caption", "Account Id")]
            public account_analytic_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_analytic_account>("account_id", ref faccount_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public analytic_user_funct_grid(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

