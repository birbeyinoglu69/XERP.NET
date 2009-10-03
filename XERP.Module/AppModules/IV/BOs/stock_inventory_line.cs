
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
    [Persistent("stock_inventory_line")]
	public partial class stock_inventory_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //stock_inventory_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_stock_inventory_line_create_uid
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
            //FK FK_stock_inventory_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private stock_inventory finventory_id;
            //FK FK_stock_inventory_line_inventory_id
            [Custom("Caption", "Inventory Id")]
            public stock_inventory inventory_id {
                get { return finventory_id; }
                set { SetPropertyValue<stock_inventory>("inventory_id", ref finventory_id, value); }
            }
    
        
            private stock_location flocation_id;
            //FK FK_stock_inventory_line_location_id
            [Custom("Caption", "Location Id")]
            public stock_location location_id {
                get { return flocation_id; }
                set { SetPropertyValue<stock_location>("location_id", ref flocation_id, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_stock_inventory_line_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private product_uom fproduct_uom;
            //FK FK_stock_inventory_line_product_uom
            [Custom("Caption", "Product Uom")]
            public product_uom product_uom {
                get { return fproduct_uom; }
                set { SetPropertyValue<product_uom>("product_uom", ref fproduct_uom, value); }
            }
    
            private System.Double fproduct_qty;
            [Custom("Caption", "Product Qty")]
            public System.Double product_qty {
                get { return fproduct_qty; }
                set { SetPropertyValue("product_qty", ref fproduct_qty, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public stock_inventory_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

