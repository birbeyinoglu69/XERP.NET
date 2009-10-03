
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
    [Persistent("mrp_production")]
	public partial class mrp_production : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //mrp_production_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_mrp_production_create_uid
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
            //FK FK_mrp_production_write_uid
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
    
            private System.Double fproduct_uos_qty;
            [Custom("Caption", "Product Uos qty")]
            public System.Double product_uos_qty {
                get { return fproduct_uos_qty; }
                set { SetPropertyValue("product_uos_qty", ref fproduct_uos_qty, value); }
            }
    
        
            private product_uom fproduct_uom;
            //FK FK_mrp_production_product_uom
            [Custom("Caption", "Product Uom")]
            public product_uom product_uom {
                get { return fproduct_uom; }
                set { SetPropertyValue<product_uom>("product_uom", ref fproduct_uom, value); }
            }
    
        
            private stock_move fmove_prod_id;
            //FK FK_mrp_production_move_prod_id
            [Custom("Caption", "Move Prod id")]
            public stock_move move_prod_id {
                get { return fmove_prod_id; }
                set { SetPropertyValue<stock_move>("move_prod_id", ref fmove_prod_id, value); }
            }
    
        
            private mrp_bom fbom_id;
            //FK FK_mrp_production_bom_id
            [Custom("Caption", "Bom Id")]
            public mrp_bom bom_id {
                get { return fbom_id; }
                set { SetPropertyValue<mrp_bom>("bom_id", ref fbom_id, value); }
            }
    
            private DateTime? fdate_finnished;
            [Custom("Caption", "Date Finnished")]
            public DateTime? date_finnished {
                get { return fdate_finnished; }
                set { SetPropertyValue("date_finnished", ref fdate_finnished, value); }
            }
    
            private System.Double fproduct_qty;
            [Custom("Caption", "Product Qty")]
            public System.Double product_qty {
                get { return fproduct_qty; }
                set { SetPropertyValue("product_qty", ref fproduct_qty, value); }
            }
    
        
            private product_uom fproduct_uos;
            //FK FK_mrp_production_product_uos
            [Custom("Caption", "Product Uos")]
            public product_uom product_uos {
                get { return fproduct_uos; }
                set { SetPropertyValue<product_uom>("product_uos", ref fproduct_uos, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_mrp_production_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private stock_location flocation_src_id;
            //FK FK_mrp_production_location_src_id
            [Custom("Caption", "Location Src id")]
            public stock_location location_src_id {
                get { return flocation_src_id; }
                set { SetPropertyValue<stock_location>("location_src_id", ref flocation_src_id, value); }
            }
    
            private DateTime? fdate_planned;
            [Custom("Caption", "Date Planned")]
            public DateTime? date_planned {
                get { return fdate_planned; }
                set { SetPropertyValue("date_planned", ref fdate_planned, value); }
            }
    
            private DateTime? fdate_start;
            [Custom("Caption", "Date Start")]
            public DateTime? date_start {
                get { return fdate_start; }
                set { SetPropertyValue("date_start", ref fdate_start, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fpriority;
            [Size(16)]
            [Custom("Caption", "Priority")]
            public System.String priority {
                get { return fpriority; }
                set { SetPropertyValue("priority", ref fpriority, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private mrp_routing frouting_id;
            //FK FK_mrp_production_routing_id
            [Custom("Caption", "Routing Id")]
            public mrp_routing routing_id {
                get { return frouting_id; }
                set { SetPropertyValue<mrp_routing>("routing_id", ref frouting_id, value); }
            }
    
        
            private stock_location flocation_dest_id;
            //FK FK_mrp_production_location_dest_id
            [Custom("Caption", "Location Dest id")]
            public stock_location location_dest_id {
                get { return flocation_dest_id; }
                set { SetPropertyValue<stock_location>("location_dest_id", ref flocation_dest_id, value); }
            }
    
        
            private stock_picking fpicking_id;
            //FK FK_mrp_production_picking_id
            [Custom("Caption", "Picking Id")]
            public stock_picking picking_id {
                get { return fpicking_id; }
                set { SetPropertyValue<stock_picking>("picking_id", ref fpicking_id, value); }
            }
    
            private System.Boolean fallow_reorder;
            [Custom("Caption", "Allow Reorder")]
            public System.Boolean allow_reorder {
                get { return fallow_reorder; }
                set { SetPropertyValue("allow_reorder", ref fallow_reorder, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public mrp_production(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

