
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
    [Persistent("sale_order_line")]
	public partial class sale_order_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //sale_order_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_sale_order_line_create_uid
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
            //FK FK_sale_order_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Double fproduct_uos_qty;
            [Custom("Caption", "Product Uos qty")]
            public System.Double product_uos_qty {
                get { return fproduct_uos_qty; }
                set { SetPropertyValue("product_uos_qty", ref fproduct_uos_qty, value); }
            }
    
        
            private mrp_procurement fprocurement_id;
            //FK FK_sale_order_line_procurement_id
            [Custom("Caption", "Procurement Id")]
            public mrp_procurement procurement_id {
                get { return fprocurement_id; }
                set { SetPropertyValue<mrp_procurement>("procurement_id", ref fprocurement_id, value); }
            }
    
        
            private product_uom fproduct_uom;
            //FK FK_sale_order_line_product_uom
            [Custom("Caption", "Product Uom")]
            public product_uom product_uom {
                get { return fproduct_uom; }
                set { SetPropertyValue<product_uom>("product_uom", ref fproduct_uom, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
        
            private sale_order forder_id;
            //FK FK_sale_order_line_order_id
            [Custom("Caption", "Order Id")]
            public sale_order order_id {
                get { return forder_id; }
                set { SetPropertyValue<sale_order>("order_id", ref forder_id, value); }
            }
    
            private System.Decimal fprice_unit;
            [Custom("Caption", "Price Unit")]
            public System.Decimal price_unit {
                get { return fprice_unit; }
                set { SetPropertyValue("price_unit", ref fprice_unit, value); }
            }
    
            private System.Decimal fproduct_uom_qty;
            [Custom("Caption", "Product Uom qty")]
            public System.Decimal product_uom_qty {
                get { return fproduct_uom_qty; }
                set { SetPropertyValue("product_uom_qty", ref fproduct_uom_qty, value); }
            }
    
            private System.Decimal fdiscount;
            [Custom("Caption", "Discount")]
            public System.Decimal discount {
                get { return fdiscount; }
                set { SetPropertyValue("discount", ref fdiscount, value); }
            }
    
        
            private product_uom fproduct_uos;
            //FK FK_sale_order_line_product_uos
            [Custom("Caption", "Product Uos")]
            public product_uom product_uos {
                get { return fproduct_uos; }
                set { SetPropertyValue<product_uom>("product_uos", ref fproduct_uos, value); }
            }
    
            private System.Boolean finvoiced;
            [Custom("Caption", "Invoiced")]
            public System.Boolean invoiced {
                get { return finvoiced; }
                set { SetPropertyValue("invoiced", ref finvoiced, value); }
            }
    
            private System.Double fdelay;
            [Custom("Caption", "Delay")]
            public System.Double delay {
                get { return fdelay; }
                set { SetPropertyValue("delay", ref fdelay, value); }
            }
    
            private System.String fname;
            [Size(256)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_sale_order_line_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
            private System.Double fth_weight;
            [Custom("Caption", "Th Weight")]
            public System.Double th_weight {
                get { return fth_weight; }
                set { SetPropertyValue("th_weight", ref fth_weight, value); }
            }
    
        
            private product_packaging fproduct_packaging;
            //FK FK_sale_order_line_product_packaging
            [Custom("Caption", "Product Packaging")]
            public product_packaging product_packaging {
                get { return fproduct_packaging; }
                set { SetPropertyValue<product_packaging>("product_packaging", ref fproduct_packaging, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
        
            private res_partner_address faddress_allotment_id;
            //FK FK_sale_order_line_address_allotment_id
            [Custom("Caption", "Address Allotment id")]
            public res_partner_address address_allotment_id {
                get { return faddress_allotment_id; }
                set { SetPropertyValue<res_partner_address>("address_allotment_id", ref faddress_allotment_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public sale_order_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

