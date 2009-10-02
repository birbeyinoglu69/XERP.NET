
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
	[DefaultProperty("note")]
    [Persistent("stock_move")]
	public partial class stock_move : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //stock_move_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_stock_move_create_uid
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
            //FK FK_stock_move_write_uid
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
    
        
            private res_partner_address faddress_id;
            //FK FK_stock_move_address_id
            [Custom("Caption", "Address Id")]
            public res_partner_address address_id {
                get { return faddress_id; }
                set { SetPropertyValue<res_partner_address>("address_id", ref faddress_id, value); }
            }
    
        
            private product_uom fproduct_uom;
            //FK FK_stock_move_product_uom
            [Custom("Caption", "Product Uom")]
            public product_uom product_uom {
                get { return fproduct_uom; }
                set { SetPropertyValue<product_uom>("product_uom", ref fproduct_uom, value); }
            }
    
            private System.Decimal fprice_unit;
            [Custom("Caption", "Price Unit")]
            public System.Decimal price_unit {
                get { return fprice_unit; }
                set { SetPropertyValue("price_unit", ref fprice_unit, value); }
            }
    
            private DateTime? fdate;
            [Custom("Caption", "Date")]
            public DateTime? date {
                get { return fdate; }
                set { SetPropertyValue("date", ref fdate, value); }
            }
    
        
            private stock_production_lot fprodlot_id;
            //FK FK_stock_move_prodlot_id
            [Custom("Caption", "Prodlot Id")]
            public stock_production_lot prodlot_id {
                get { return fprodlot_id; }
                set { SetPropertyValue<stock_production_lot>("prodlot_id", ref fprodlot_id, value); }
            }
    
        
            private stock_move fmove_dest_id;
            //FK FK_stock_move_move_dest_id
            [Custom("Caption", "Move Dest id")]
            public stock_move move_dest_id {
                get { return fmove_dest_id; }
                set { SetPropertyValue<stock_move>("move_dest_id", ref fmove_dest_id, value); }
            }
    
            private System.Double fproduct_qty;
            [Custom("Caption", "Product Qty")]
            public System.Double product_qty {
                get { return fproduct_qty; }
                set { SetPropertyValue("product_qty", ref fproduct_qty, value); }
            }
    
        
            private product_uom fproduct_uos;
            //FK FK_stock_move_product_uos
            [Custom("Caption", "Product Uos")]
            public product_uom product_uos {
                get { return fproduct_uos; }
                set { SetPropertyValue<product_uom>("product_uos", ref fproduct_uos, value); }
            }
    
        
            private stock_location flocation_id;
            //FK FK_stock_move_location_id
            [Custom("Caption", "Location Id")]
            public stock_location location_id {
                get { return flocation_id; }
                set { SetPropertyValue<stock_location>("location_id", ref flocation_id, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_stock_move_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Boolean fauto_validate;
            [Custom("Caption", "Auto Validate")]
            public System.Boolean auto_validate {
                get { return fauto_validate; }
                set { SetPropertyValue("auto_validate", ref fauto_validate, value); }
            }
    
            private DateTime? fdate_planned;
            [Custom("Caption", "Date Planned")]
            public DateTime? date_planned {
                get { return fdate_planned; }
                set { SetPropertyValue("date_planned", ref fdate_planned, value); }
            }
    
        
            private stock_picking fpicking_id;
            //FK FK_stock_move_picking_id
            [Custom("Caption", "Picking Id")]
            public stock_picking picking_id {
                get { return fpicking_id; }
                set { SetPropertyValue<stock_picking>("picking_id", ref fpicking_id, value); }
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
    
        
            private stock_location flocation_dest_id;
            //FK FK_stock_move_location_dest_id
            [Custom("Caption", "Location Dest id")]
            public stock_location location_dest_id {
                get { return flocation_dest_id; }
                set { SetPropertyValue<stock_location>("location_dest_id", ref flocation_dest_id, value); }
            }
    
        
            private stock_tracking ftracking_id;
            //FK FK_stock_move_tracking_id
            [Custom("Caption", "Tracking Id")]
            public stock_tracking tracking_id {
                get { return ftracking_id; }
                set { SetPropertyValue<stock_tracking>("tracking_id", ref ftracking_id, value); }
            }
    
        
            private product_packaging fproduct_packaging;
            //FK FK_stock_move_product_packaging
            [Custom("Caption", "Product Packaging")]
            public product_packaging product_packaging {
                get { return fproduct_packaging; }
                set { SetPropertyValue<product_packaging>("product_packaging", ref fproduct_packaging, value); }
            }
    
        
            private purchase_order_line fpurchase_line_id;
            //FK FK_stock_move_purchase_line_id
            [Custom("Caption", "Purchase Line id")]
            public purchase_order_line purchase_line_id {
                get { return fpurchase_line_id; }
                set { SetPropertyValue<purchase_order_line>("purchase_line_id", ref fpurchase_line_id, value); }
            }
    
        
            private mrp_production fproduction_id;
            //FK FK_stock_move_production_id
            [Custom("Caption", "Production Id")]
            public mrp_production production_id {
                get { return fproduction_id; }
                set { SetPropertyValue<mrp_production>("production_id", ref fproduction_id, value); }
            }
    
        
            private sale_order_line fsale_line_id;
            //FK FK_stock_move_sale_line_id
            [Custom("Caption", "Sale Line id")]
            public sale_order_line sale_line_id {
                get { return fsale_line_id; }
                set { SetPropertyValue<sale_order_line>("sale_line_id", ref fsale_line_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public stock_move(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

