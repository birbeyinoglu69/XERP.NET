
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
	[DefaultProperty("internal_notes")]
    [Persistent("mrp_repair")]
	public partial class mrp_repair : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //mrp_repair_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_mrp_repair_create_uid
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
            //FK FK_mrp_repair_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_partner_address faddress_id;
            //FK FK_mrp_repair_address_id
            [Custom("Caption", "Address Id")]
            public res_partner_address address_id {
                get { return faddress_id; }
                set { SetPropertyValue<res_partner_address>("address_id", ref faddress_id, value); }
            }
    
            private System.String finternal_notes;
            [Size(-1)]
            [Custom("Caption", "Internal Notes")]
            public System.String internal_notes {
                get { return finternal_notes; }
                set { SetPropertyValue("internal_notes", ref finternal_notes, value); }
            }
    
        
            private stock_production_lot fprodlot_id;
            //FK FK_mrp_repair_prodlot_id
            [Custom("Caption", "Prodlot Id")]
            public stock_production_lot prodlot_id {
                get { return fprodlot_id; }
                set { SetPropertyValue<stock_production_lot>("prodlot_id", ref fprodlot_id, value); }
            }
    
        
            private res_partner_address fpartner_invoice_id;
            //FK FK_mrp_repair_partner_invoice_id
            [Custom("Caption", "Partner Invoice id")]
            public res_partner_address partner_invoice_id {
                get { return fpartner_invoice_id; }
                set { SetPropertyValue<res_partner_address>("partner_invoice_id", ref fpartner_invoice_id, value); }
            }
    
            private System.String fquotation_notes;
            [Size(-1)]
            [Custom("Caption", "Quotation Notes")]
            public System.String quotation_notes {
                get { return fquotation_notes; }
                set { SetPropertyValue("quotation_notes", ref fquotation_notes, value); }
            }
    
        
            private stock_location flocation_id;
            //FK FK_mrp_repair_location_id
            [Custom("Caption", "Location Id")]
            public stock_location location_id {
                get { return flocation_id; }
                set { SetPropertyValue<stock_location>("location_id", ref flocation_id, value); }
            }
    
        
            private stock_move fmove_id;
            //FK FK_mrp_repair_move_id
            [Custom("Caption", "Move Id")]
            public stock_move move_id {
                get { return fmove_id; }
                set { SetPropertyValue<stock_move>("move_id", ref fmove_id, value); }
            }
    
            private System.Boolean finvoiced;
            [Custom("Caption", "Invoiced")]
            public System.Boolean invoiced {
                get { return finvoiced; }
                set { SetPropertyValue("invoiced", ref finvoiced, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_mrp_repair_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_mrp_repair_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private System.Boolean fdeliver_bool;
            [Custom("Caption", "Deliver Bool")]
            public System.Boolean deliver_bool {
                get { return fdeliver_bool; }
                set { SetPropertyValue("deliver_bool", ref fdeliver_bool, value); }
            }
    
            private System.String fname;
            [Size(24)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
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
    
        
            private stock_location flocation_dest_id;
            //FK FK_mrp_repair_location_dest_id
            [Custom("Caption", "Location Dest id")]
            public stock_location location_dest_id {
                get { return flocation_dest_id; }
                set { SetPropertyValue<stock_location>("location_dest_id", ref flocation_dest_id, value); }
            }
    
        
            private account_invoice finvoice_id;
            //FK FK_mrp_repair_invoice_id
            [Custom("Caption", "Invoice Id")]
            public account_invoice invoice_id {
                get { return finvoice_id; }
                set { SetPropertyValue<account_invoice>("invoice_id", ref finvoice_id, value); }
            }
    
        
            private product_pricelist fpricelist_id;
            //FK FK_mrp_repair_pricelist_id
            [Custom("Caption", "Pricelist Id")]
            public product_pricelist pricelist_id {
                get { return fpricelist_id; }
                set { SetPropertyValue<product_pricelist>("pricelist_id", ref fpricelist_id, value); }
            }
    
            private System.Boolean frepaired;
            [Custom("Caption", "Repaired")]
            public System.Boolean repaired {
                get { return frepaired; }
                set { SetPropertyValue("repaired", ref frepaired, value); }
            }
    
        
            private stock_picking fpicking_id;
            //FK FK_mrp_repair_picking_id
            [Custom("Caption", "Picking Id")]
            public stock_picking picking_id {
                get { return fpicking_id; }
                set { SetPropertyValue<stock_picking>("picking_id", ref fpicking_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public mrp_repair(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

