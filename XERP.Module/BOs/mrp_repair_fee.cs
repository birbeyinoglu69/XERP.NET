
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
    [Persistent("mrp_repair_fee")]
	public partial class mrp_repair_fee : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //mrp_repair_fee_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_mrp_repair_fee_create_uid
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
            //FK FK_mrp_repair_fee_write_uid
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
    
        
            private product_uom fproduct_uom;
            //FK FK_mrp_repair_fee_product_uom
            [Custom("Caption", "Product Uom")]
            public product_uom product_uom {
                get { return fproduct_uom; }
                set { SetPropertyValue<product_uom>("product_uom", ref fproduct_uom, value); }
            }
    
        
            private mrp_repair frepair_id;
            //FK FK_mrp_repair_fee_repair_id
            [Custom("Caption", "Repair Id")]
            public mrp_repair repair_id {
                get { return frepair_id; }
                set { SetPropertyValue<mrp_repair>("repair_id", ref frepair_id, value); }
            }
    
            private System.Double fprice_unit;
            [Custom("Caption", "Price Unit")]
            public System.Double price_unit {
                get { return fprice_unit; }
                set { SetPropertyValue("price_unit", ref fprice_unit, value); }
            }
    
            private System.Decimal fproduct_uom_qty;
            [Custom("Caption", "Product Uom qty")]
            public System.Decimal product_uom_qty {
                get { return fproduct_uom_qty; }
                set { SetPropertyValue("product_uom_qty", ref fproduct_uom_qty, value); }
            }
    
            private System.Boolean fto_invoice;
            [Custom("Caption", "To Invoice")]
            public System.Boolean to_invoice {
                get { return fto_invoice; }
                set { SetPropertyValue("to_invoice", ref fto_invoice, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_mrp_repair_fee_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private account_invoice_line finvoice_line_id;
            //FK FK_mrp_repair_fee_invoice_line_id
            [Custom("Caption", "Invoice Line id")]
            public account_invoice_line invoice_line_id {
                get { return finvoice_line_id; }
                set { SetPropertyValue<account_invoice_line>("invoice_line_id", ref finvoice_line_id, value); }
            }
    
            private System.Boolean finvoiced;
            [Custom("Caption", "Invoiced")]
            public System.Boolean invoiced {
                get { return finvoiced; }
                set { SetPropertyValue("invoiced", ref finvoiced, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public mrp_repair_fee(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

