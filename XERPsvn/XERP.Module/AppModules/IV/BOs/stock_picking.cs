
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
    [Persistent("stock_picking")]
	public partial class stock_picking : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //stock_picking_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_stock_picking_create_uid
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
            //FK FK_stock_picking_write_uid
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
    
        
            private res_partner_address faddress_id;
            //FK FK_stock_picking_address_id
            [Custom("Caption", "Address Id")]
            public res_partner_address address_id {
                get { return faddress_id; }
                set { SetPropertyValue<res_partner_address>("address_id", ref faddress_id, value); }
            }
    
            private DateTime? fdate_done;
            [Custom("Caption", "Date Done")]
            public DateTime? date_done {
                get { return fdate_done; }
                set { SetPropertyValue("date_done", ref fdate_done, value); }
            }
    
            private DateTime? fmin_date;
            [Custom("Caption", "Min Date")]
            public DateTime? min_date {
                get { return fmin_date; }
                set { SetPropertyValue("min_date", ref fmin_date, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private DateTime? fdate;
            [Custom("Caption", "Date")]
            public DateTime? date {
                get { return fdate; }
                set { SetPropertyValue("date", ref fdate, value); }
            }
    
        
            private stock_location flocation_id;
            //FK FK_stock_picking_location_id
            [Custom("Caption", "Location Id")]
            public stock_location location_id {
                get { return flocation_id; }
                set { SetPropertyValue<stock_location>("location_id", ref flocation_id, value); }
            }
    
        
            private stock_picking fbackorder_id;
            //FK FK_stock_picking_backorder_id
            [Custom("Caption", "Backorder Id")]
            public stock_picking backorder_id {
                get { return fbackorder_id; }
                set { SetPropertyValue<stock_picking>("backorder_id", ref fbackorder_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Boolean fauto_picking;
            [Custom("Caption", "Auto Picking")]
            public System.Boolean auto_picking {
                get { return fauto_picking; }
                set { SetPropertyValue("auto_picking", ref fauto_picking, value); }
            }
    
            private System.String fmove_type;
            [Size(16)]
            [Custom("Caption", "Move Type")]
            public System.String move_type {
                get { return fmove_type; }
                set { SetPropertyValue("move_type", ref fmove_type, value); }
            }
    
            private System.String finvoice_state1;
            [Size(16)]
            [Custom("Caption", "Invoice State1")]
            public System.String invoice_state1 {
                get { return finvoice_state1; }
                set { SetPropertyValue("invoice_state1", ref finvoice_state1, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private stock_location flocation_dest_id;
            //FK FK_stock_picking_location_dest_id
            [Custom("Caption", "Location Dest id")]
            public stock_location location_dest_id {
                get { return flocation_dest_id; }
                set { SetPropertyValue<stock_location>("location_dest_id", ref flocation_dest_id, value); }
            }
    
            private DateTime? fmax_date;
            [Custom("Caption", "Max Date")]
            public DateTime? max_date {
                get { return fmax_date; }
                set { SetPropertyValue("max_date", ref fmax_date, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
        
            private purchase_order fpurchase_id;
            //FK FK_stock_picking_purchase_id
            [Custom("Caption", "Purchase Id")]
            public purchase_order purchase_id {
                get { return fpurchase_id; }
                set { SetPropertyValue<purchase_order>("purchase_id", ref fpurchase_id, value); }
            }
    
        
            private sale_order fsale_id;
            //FK FK_stock_picking_sale_id
            [Custom("Caption", "Sale Id")]
            public sale_order sale_id {
                get { return fsale_id; }
                set { SetPropertyValue<sale_order>("sale_id", ref fsale_id, value); }
            }
    
        
            private delivery_carrier fcarrier_id;
            //FK FK_stock_picking_carrier_id
            [Custom("Caption", "Carrier Id")]
            public delivery_carrier carrier_id {
                get { return fcarrier_id; }
                set { SetPropertyValue<delivery_carrier>("carrier_id", ref fcarrier_id, value); }
            }
    
            private System.Double fvolume;
            [Custom("Caption", "Volume")]
            public System.Double volume {
                get { return fvolume; }
                set { SetPropertyValue("volume", ref fvolume, value); }
            }
    
            private System.Double fweight;
            [Custom("Caption", "Weight")]
            public System.Double weight {
                get { return fweight; }
                set { SetPropertyValue("weight", ref fweight, value); }
            }
    
        
            private sale_journal_sale_journal fsale_journal_id;
            //FK FK_stock_picking_sale_journal_id
            [Custom("Caption", "Sale Journal id")]
            public sale_journal_sale_journal sale_journal_id {
                get { return fsale_journal_id; }
                set { SetPropertyValue<sale_journal_sale_journal>("sale_journal_id", ref fsale_journal_id, value); }
            }
    
        
            private sale_journal_invoice_type finvoice_type_id;
            //FK FK_stock_picking_invoice_type_id
            [Custom("Caption", "Invoice Type id")]
            public sale_journal_invoice_type invoice_type_id {
                get { return finvoice_type_id; }
                set { SetPropertyValue<sale_journal_invoice_type>("invoice_type_id", ref finvoice_type_id, value); }
            }
    
        
            private sale_journal_picking_journal fjournal_id;
            //FK FK_stock_picking_journal_id
            [Custom("Caption", "Journal Id")]
            public sale_journal_picking_journal journal_id {
                get { return fjournal_id; }
                set { SetPropertyValue<sale_journal_picking_journal>("journal_id", ref fjournal_id, value); }
            }
    
        
            private pos_order fpos_order;
            //FK FK_stock_picking_pos_order
            [Custom("Caption", "Pos Order")]
            public pos_order pos_order {
                get { return fpos_order; }
                set { SetPropertyValue<pos_order>("pos_order", ref fpos_order, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public stock_picking(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

