
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
    [Persistent("sale_order")]
	public partial class sale_order : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //sale_order_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_sale_order_create_uid
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
            //FK FK_sale_order_write_uid
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
    
            private System.String fincoterm;
            [Size(3)]
            [Custom("Caption", "Incoterm")]
            public System.String incoterm {
                get { return fincoterm; }
                set { SetPropertyValue("incoterm", ref fincoterm, value); }
            }
    
            private System.String fpicking_policy;
            [Size(16)]
            [Custom("Caption", "Picking Policy")]
            public System.String picking_policy {
                get { return fpicking_policy; }
                set { SetPropertyValue("picking_policy", ref fpicking_policy, value); }
            }
    
            private System.String forder_policy;
            [Size(16)]
            [Custom("Caption", "Order Policy")]
            public System.String order_policy {
                get { return forder_policy; }
                set { SetPropertyValue("order_policy", ref forder_policy, value); }
            }
    
        
            private res_partner_address fpartner_order_id;
            //FK FK_sale_order_partner_order_id
            [Custom("Caption", "Partner Order id")]
            public res_partner_address partner_order_id {
                get { return fpartner_order_id; }
                set { SetPropertyValue<res_partner_address>("partner_order_id", ref fpartner_order_id, value); }
            }
    
        
            private sale_shop fshop_id;
            //FK FK_sale_order_shop_id
            [Custom("Caption", "Shop Id")]
            public sale_shop shop_id {
                get { return fshop_id; }
                set { SetPropertyValue<sale_shop>("shop_id", ref fshop_id, value); }
            }
    
            private System.String fclient_order_ref;
            [Size(64)]
            [Custom("Caption", "Client Order ref")]
            public System.String client_order_ref {
                get { return fclient_order_ref; }
                set { SetPropertyValue("client_order_ref", ref fclient_order_ref, value); }
            }
    
        
            private res_partner_address fpartner_invoice_id;
            //FK FK_sale_order_partner_invoice_id
            [Custom("Caption", "Partner Invoice id")]
            public res_partner_address partner_invoice_id {
                get { return fpartner_invoice_id; }
                set { SetPropertyValue<res_partner_address>("partner_invoice_id", ref fpartner_invoice_id, value); }
            }
    
            private System.Double famount_untaxed;
            [Custom("Caption", "Amount Untaxed")]
            public System.Double amount_untaxed {
                get { return famount_untaxed; }
                set { SetPropertyValue("amount_untaxed", ref famount_untaxed, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_sale_order_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
        
            private account_fiscal_position ffiscal_position;
            //FK FK_sale_order_fiscal_position
            [Custom("Caption", "Fiscal Position")]
            public account_fiscal_position fiscal_position {
                get { return ffiscal_position; }
                set { SetPropertyValue<account_fiscal_position>("fiscal_position", ref ffiscal_position, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_sale_order_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private res_partner_address fpartner_shipping_id;
            //FK FK_sale_order_partner_shipping_id
            [Custom("Caption", "Partner Shipping id")]
            public res_partner_address partner_shipping_id {
                get { return fpartner_shipping_id; }
                set { SetPropertyValue<res_partner_address>("partner_shipping_id", ref fpartner_shipping_id, value); }
            }
    
        
            private account_payment_term fpayment_term;
            //FK FK_sale_order_payment_term
            [Custom("Caption", "Payment Term")]
            public account_payment_term payment_term {
                get { return fpayment_term; }
                set { SetPropertyValue<account_payment_term>("payment_term", ref fpayment_term, value); }
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
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String finvoice_quantity;
            [Size(16)]
            [Custom("Caption", "Invoice Quantity")]
            public System.String invoice_quantity {
                get { return finvoice_quantity; }
                set { SetPropertyValue("invoice_quantity", ref finvoice_quantity, value); }
            }
    
        
            private product_pricelist fpricelist_id;
            //FK FK_sale_order_pricelist_id
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
    
        
            private account_analytic_account fproject_id;
            //FK FK_sale_order_project_id
            [Custom("Caption", "Project Id")]
            public account_analytic_account project_id {
                get { return fproject_id; }
                set { SetPropertyValue<account_analytic_account>("project_id", ref fproject_id, value); }
            }
    
        
            private delivery_carrier fcarrier_id;
            //FK FK_sale_order_carrier_id
            [Custom("Caption", "Carrier Id")]
            public delivery_carrier carrier_id {
                get { return fcarrier_id; }
                set { SetPropertyValue<delivery_carrier>("carrier_id", ref fcarrier_id, value); }
            }
    
        
            private sale_journal_sale_journal fjournal_id;
            //FK FK_sale_order_journal_id
            [Custom("Caption", "Journal Id")]
            public sale_journal_sale_journal journal_id {
                get { return fjournal_id; }
                set { SetPropertyValue<sale_journal_sale_journal>("journal_id", ref fjournal_id, value); }
            }
    
        
            private sale_journal_invoice_type finvoice_type_id;
            //FK FK_sale_order_invoice_type_id
            [Custom("Caption", "Invoice Type id")]
            public sale_journal_invoice_type invoice_type_id {
                get { return finvoice_type_id; }
                set { SetPropertyValue<sale_journal_invoice_type>("invoice_type_id", ref finvoice_type_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public sale_order(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

