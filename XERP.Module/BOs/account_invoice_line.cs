
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
	[DefaultProperty("origin")]
    [Persistent("account_invoice_line")]
	public partial class account_invoice_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_invoice_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_invoice_line_create_uid
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
            //FK FK_account_invoice_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String forigin;
            [Size(256)]
            [Custom("Caption", "Origin")]
            public System.String origin {
                get { return forigin; }
                set { SetPropertyValue("origin", ref forigin, value); }
            }
    
        
            private product_uom fuos_id;
            //FK FK_account_invoice_line_uos_id
            [Custom("Caption", "Uos Id")]
            public product_uom uos_id {
                get { return fuos_id; }
                set { SetPropertyValue<product_uom>("uos_id", ref fuos_id, value); }
            }
    
            private System.String fname;
            [Size(256)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private account_invoice finvoice_id;
            //FK FK_account_invoice_line_invoice_id
            [Custom("Caption", "Invoice Id")]
            public account_invoice invoice_id {
                get { return finvoice_id; }
                set { SetPropertyValue<account_invoice>("invoice_id", ref finvoice_id, value); }
            }
    
            private System.Decimal fprice_unit;
            [Custom("Caption", "Price Unit")]
            public System.Decimal price_unit {
                get { return fprice_unit; }
                set { SetPropertyValue("price_unit", ref fprice_unit, value); }
            }
    
            private System.Double fprice_subtotal;
            [Custom("Caption", "Price Subtotal")]
            public System.Double price_subtotal {
                get { return fprice_subtotal; }
                set { SetPropertyValue("price_subtotal", ref fprice_subtotal, value); }
            }
    
        
            private account_account faccount_id;
            //FK FK_account_invoice_line_account_id
            [Custom("Caption", "Account Id")]
            public account_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_account>("account_id", ref faccount_id, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
            private System.Decimal fdiscount;
            [Custom("Caption", "Discount")]
            public System.Decimal discount {
                get { return fdiscount; }
                set { SetPropertyValue("discount", ref fdiscount, value); }
            }
    
        
            private account_analytic_account faccount_analytic_id;
            //FK FK_account_invoice_line_account_analytic_id
            [Custom("Caption", "Account Analytic id")]
            public account_analytic_account account_analytic_id {
                get { return faccount_analytic_id; }
                set { SetPropertyValue<account_analytic_account>("account_analytic_id", ref faccount_analytic_id, value); }
            }
    
            private System.Double fquantity;
            [Custom("Caption", "Quantity")]
            public System.Double quantity {
                get { return fquantity; }
                set { SetPropertyValue("quantity", ref fquantity, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_account_invoice_line_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
            private System.Double fprice_subtotal_incl;
            [Custom("Caption", "Price Subtotal incl")]
            public System.Double price_subtotal_incl {
                get { return fprice_subtotal_incl; }
                set { SetPropertyValue("price_subtotal_incl", ref fprice_subtotal_incl, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_invoice_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

