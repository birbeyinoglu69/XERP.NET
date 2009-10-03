
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
    [Persistent("sale_shop")]
	public partial class sale_shop : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //sale_shop_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_sale_shop_create_uid
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
            //FK FK_sale_shop_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private product_pricelist fpricelist_id;
            //FK FK_sale_shop_pricelist_id
            [Custom("Caption", "Pricelist Id")]
            public product_pricelist pricelist_id {
                get { return fpricelist_id; }
                set { SetPropertyValue<product_pricelist>("pricelist_id", ref fpricelist_id, value); }
            }
    
        
            private account_analytic_account fproject_id;
            //FK FK_sale_shop_project_id
            [Custom("Caption", "Project Id")]
            public account_analytic_account project_id {
                get { return fproject_id; }
                set { SetPropertyValue<account_analytic_account>("project_id", ref fproject_id, value); }
            }
    
        
            private account_payment_term fpayment_default_id;
            //FK FK_sale_shop_payment_default_id
            [Custom("Caption", "Payment Default id")]
            public account_payment_term payment_default_id {
                get { return fpayment_default_id; }
                set { SetPropertyValue<account_payment_term>("payment_default_id", ref fpayment_default_id, value); }
            }
    
        
            private stock_warehouse fwarehouse_id;
            //FK FK_sale_shop_warehouse_id
            [Custom("Caption", "Warehouse Id")]
            public stock_warehouse warehouse_id {
                get { return fwarehouse_id; }
                set { SetPropertyValue<stock_warehouse>("warehouse_id", ref fwarehouse_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public sale_shop(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

