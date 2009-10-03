
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
	[DefaultProperty("origin")]
    [Persistent("purchase_order")]
	public partial class purchase_order : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //purchase_order_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_purchase_order_create_uid
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
            //FK FK_purchase_order_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String forigin;
            [Size(64)]
            [Custom("Caption", "Origin")]
            public System.String origin {
                get { return forigin; }
                set { SetPropertyValue("origin", ref forigin, value); }
            }
    
        
            private res_partner_address fpartner_address_id;
            //FK FK_purchase_order_partner_address_id
            [Custom("Caption", "Partner Address id")]
            public res_partner_address partner_address_id {
                get { return fpartner_address_id; }
                set { SetPropertyValue<res_partner_address>("partner_address_id", ref fpartner_address_id, value); }
            }
    
        
            private stock_warehouse fwarehouse_id;
            //FK FK_purchase_order_warehouse_id
            [Custom("Caption", "Warehouse Id")]
            public stock_warehouse warehouse_id {
                get { return fwarehouse_id; }
                set { SetPropertyValue<stock_warehouse>("warehouse_id", ref fwarehouse_id, value); }
            }
    
            private System.String fpartner_ref;
            [Size(64)]
            [Custom("Caption", "Partner Ref")]
            public System.String partner_ref {
                get { return fpartner_ref; }
                set { SetPropertyValue("partner_ref", ref fpartner_ref, value); }
            }
    
        
            private stock_location flocation_id;
            //FK FK_purchase_order_location_id
            [Custom("Caption", "Location Id")]
            public stock_location location_id {
                get { return flocation_id; }
                set { SetPropertyValue<stock_location>("location_id", ref flocation_id, value); }
            }
    
        
            private res_partner_address fdest_address_id;
            //FK FK_purchase_order_dest_address_id
            [Custom("Caption", "Dest Address id")]
            public res_partner_address dest_address_id {
                get { return fdest_address_id; }
                set { SetPropertyValue<res_partner_address>("dest_address_id", ref fdest_address_id, value); }
            }
    
        
            private account_fiscal_position ffiscal_position;
            //FK FK_purchase_order_fiscal_position
            [Custom("Caption", "Fiscal Position")]
            public account_fiscal_position fiscal_position {
                get { return ffiscal_position; }
                set { SetPropertyValue<account_fiscal_position>("fiscal_position", ref ffiscal_position, value); }
            }
    
            private System.Double famount_untaxed;
            [Custom("Caption", "Amount Untaxed")]
            public System.Double amount_untaxed {
                get { return famount_untaxed; }
                set { SetPropertyValue("amount_untaxed", ref famount_untaxed, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_purchase_order_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private System.Boolean fshipped;
            [Custom("Caption", "Shipped")]
            public System.Boolean shipped {
                get { return fshipped; }
                set { SetPropertyValue("shipped", ref fshipped, value); }
            }
    
            private System.Double famount_tax;
            [Custom("Caption", "Amount Tax")]
            public System.Double amount_tax {
                get { return famount_tax; }
                set { SetPropertyValue("amount_tax", ref famount_tax, value); }
            }
    
            private System.String finvoice_method;
            [Size(16)]
            [Custom("Caption", "Invoice Method")]
            public System.String invoice_method {
                get { return finvoice_method; }
                set { SetPropertyValue("invoice_method", ref finvoice_method, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private res_users fvalidator;
            //FK FK_purchase_order_validator
            [Custom("Caption", "Validator")]
            public res_users validator {
                get { return fvalidator; }
                set { SetPropertyValue<res_users>("validator", ref fvalidator, value); }
            }
    
            private DateTime? fminimum_planned_date;
            [Custom("Caption", "Minimum Planned date")]
            public DateTime? minimum_planned_date {
                get { return fminimum_planned_date; }
                set { SetPropertyValue("minimum_planned_date", ref fminimum_planned_date, value); }
            }
    
        
            private account_invoice finvoice_id;
            //FK FK_purchase_order_invoice_id
            [Custom("Caption", "Invoice Id")]
            public account_invoice invoice_id {
                get { return finvoice_id; }
                set { SetPropertyValue<account_invoice>("invoice_id", ref finvoice_id, value); }
            }
    
        
            private product_pricelist fpricelist_id;
            //FK FK_purchase_order_pricelist_id
            [Custom("Caption", "Pricelist Id")]
            public product_pricelist pricelist_id {
                get { return fpricelist_id; }
                set { SetPropertyValue<product_pricelist>("pricelist_id", ref fpricelist_id, value); }
            }
    
            private System.Double famount_total;
            [Custom("Caption", "Amount Total")]
            public System.Double amount_total {
                get { return famount_total; }
                set { SetPropertyValue("amount_total", ref famount_total, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public purchase_order(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

