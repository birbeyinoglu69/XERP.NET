
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
    [Persistent("pos_order_line")]
	public partial class pos_order_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //pos_order_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_pos_order_line_create_uid
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
            //FK FK_pos_order_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fname;
            [Size(512)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private pos_order forder_id;
            //FK FK_pos_order_line_order_id
            [Custom("Caption", "Order Id")]
            public pos_order order_id {
                get { return forder_id; }
                set { SetPropertyValue<pos_order>("order_id", ref forder_id, value); }
            }
    
            private System.Double fprice_unit;
            [Custom("Caption", "Price Unit")]
            public System.Double price_unit {
                get { return fprice_unit; }
                set { SetPropertyValue("price_unit", ref fprice_unit, value); }
            }
    
            private System.Double fqty;
            [Custom("Caption", "Qty")]
            public System.Double qty {
                get { return fqty; }
                set { SetPropertyValue("qty", ref fqty, value); }
            }
    
            private System.Decimal fdiscount;
            [Custom("Caption", "Discount")]
            public System.Decimal discount {
                get { return fdiscount; }
                set { SetPropertyValue("discount", ref fdiscount, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_pos_order_line_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public pos_order_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

