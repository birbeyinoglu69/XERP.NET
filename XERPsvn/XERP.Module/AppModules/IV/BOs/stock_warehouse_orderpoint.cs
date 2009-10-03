
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
	[DefaultProperty("logic")]
    [Persistent("stock_warehouse_orderpoint")]
	public partial class stock_warehouse_orderpoint : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //stock_warehouse_orderpoint_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_stock_warehouse_orderpoint_create_uid
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
            //FK FK_stock_warehouse_orderpoint_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Double fproduct_max_qty;
            [Custom("Caption", "Product Max qty")]
            public System.Double product_max_qty {
                get { return fproduct_max_qty; }
                set { SetPropertyValue("product_max_qty", ref fproduct_max_qty, value); }
            }
    
            private System.Double fproduct_min_qty;
            [Custom("Caption", "Product Min qty")]
            public System.Double product_min_qty {
                get { return fproduct_min_qty; }
                set { SetPropertyValue("product_min_qty", ref fproduct_min_qty, value); }
            }
    
            private System.Int32 fqty_multiple;
            [Custom("Caption", "Qty Multiple")]
            public System.Int32 qty_multiple {
                get { return fqty_multiple; }
                set { SetPropertyValue("qty_multiple", ref fqty_multiple, value); }
            }
    
        
            private mrp_procurement fprocurement_id;
            //FK FK_stock_warehouse_orderpoint_procurement_id
            [Custom("Caption", "Procurement Id")]
            public mrp_procurement procurement_id {
                get { return fprocurement_id; }
                set { SetPropertyValue<mrp_procurement>("procurement_id", ref fprocurement_id, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_stock_warehouse_orderpoint_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private product_uom fproduct_uom;
            //FK FK_stock_warehouse_orderpoint_product_uom
            [Custom("Caption", "Product Uom")]
            public product_uom product_uom {
                get { return fproduct_uom; }
                set { SetPropertyValue<product_uom>("product_uom", ref fproduct_uom, value); }
            }
    
        
            private stock_warehouse fwarehouse_id;
            //FK FK_stock_warehouse_orderpoint_warehouse_id
            [Custom("Caption", "Warehouse Id")]
            public stock_warehouse warehouse_id {
                get { return fwarehouse_id; }
                set { SetPropertyValue<stock_warehouse>("warehouse_id", ref fwarehouse_id, value); }
            }
    
            private System.String flogic;
            [Size(16)]
            [Custom("Caption", "Logic")]
            public System.String logic {
                get { return flogic; }
                set { SetPropertyValue("logic", ref flogic, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
        
            private stock_location flocation_id;
            //FK FK_stock_warehouse_orderpoint_location_id
            [Custom("Caption", "Location Id")]
            public stock_location location_id {
                get { return flocation_id; }
                set { SetPropertyValue<stock_location>("location_id", ref flocation_id, value); }
            }
    
            private System.String fname;
            [Size(32)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public stock_warehouse_orderpoint(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

