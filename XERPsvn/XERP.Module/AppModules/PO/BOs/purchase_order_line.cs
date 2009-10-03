
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
	[DefaultProperty("notes")]
    [Persistent("purchase_order_line")]
	public partial class purchase_order_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //purchase_order_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_purchase_order_line_create_uid
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
            //FK FK_purchase_order_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_purchase_order_line_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private product_uom fproduct_uom;
            //FK FK_purchase_order_line_product_uom
            [Custom("Caption", "Product Uom")]
            public product_uom product_uom {
                get { return fproduct_uom; }
                set { SetPropertyValue<product_uom>("product_uom", ref fproduct_uom, value); }
            }
    
            private DateTime? fdate_planned;
            [Custom("Caption", "Date Planned")]
            public DateTime? date_planned {
                get { return fdate_planned; }
                set { SetPropertyValue("date_planned", ref fdate_planned, value); }
            }
    
        
            private purchase_order forder_id;
            //FK FK_purchase_order_line_order_id
            [Custom("Caption", "Order Id")]
            public purchase_order order_id {
                get { return forder_id; }
                set { SetPropertyValue<purchase_order>("order_id", ref forder_id, value); }
            }
    
            private System.Decimal fprice_unit;
            [Custom("Caption", "Price Unit")]
            public System.Decimal price_unit {
                get { return fprice_unit; }
                set { SetPropertyValue("price_unit", ref fprice_unit, value); }
            }
    
        
            private stock_move fmove_dest_id;
            //FK FK_purchase_order_line_move_dest_id
            [Custom("Caption", "Move Dest id")]
            public stock_move move_dest_id {
                get { return fmove_dest_id; }
                set { SetPropertyValue<stock_move>("move_dest_id", ref fmove_dest_id, value); }
            }
    
            private System.Decimal fproduct_qty;
            [Custom("Caption", "Product Qty")]
            public System.Decimal product_qty {
                get { return fproduct_qty; }
                set { SetPropertyValue("product_qty", ref fproduct_qty, value); }
            }
    
        
            private account_analytic_account faccount_analytic_id;
            //FK FK_purchase_order_line_account_analytic_id
            [Custom("Caption", "Account Analytic id")]
            public account_analytic_account account_analytic_id {
                get { return faccount_analytic_id; }
                set { SetPropertyValue<account_analytic_account>("account_analytic_id", ref faccount_analytic_id, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private System.String fname;
            [Size(256)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public purchase_order_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

