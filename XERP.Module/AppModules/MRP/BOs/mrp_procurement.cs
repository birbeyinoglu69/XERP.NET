
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
    [Persistent("mrp_procurement")]
	public partial class mrp_procurement : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //mrp_procurement_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_mrp_procurement_create_uid
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
            //FK FK_mrp_procurement_write_uid
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
            //FK FK_mrp_procurement_product_uom
            [Custom("Caption", "Product Uom")]
            public product_uom product_uom {
                get { return fproduct_uom; }
                set { SetPropertyValue<product_uom>("product_uom", ref fproduct_uom, value); }
            }
    
        
            private product_uom fproduct_uos;
            //FK FK_mrp_procurement_product_uos
            [Custom("Caption", "Product Uos")]
            public product_uom product_uos {
                get { return fproduct_uos; }
                set { SetPropertyValue<product_uom>("product_uos", ref fproduct_uos, value); }
            }
    
            private System.Double fproduct_qty;
            [Custom("Caption", "Product Qty")]
            public System.Double product_qty {
                get { return fproduct_qty; }
                set { SetPropertyValue("product_qty", ref fproduct_qty, value); }
            }
    
            private System.String fprocure_method;
            [Size(16)]
            [Custom("Caption", "Procure Method")]
            public System.String procure_method {
                get { return fprocure_method; }
                set { SetPropertyValue("procure_method", ref fprocure_method, value); }
            }
    
            private System.String fmessage;
            [Size(64)]
            [Custom("Caption", "Message")]
            public System.String message {
                get { return fmessage; }
                set { SetPropertyValue("message", ref fmessage, value); }
            }
    
        
            private stock_location flocation_id;
            //FK FK_mrp_procurement_location_id
            [Custom("Caption", "Location Id")]
            public stock_location location_id {
                get { return flocation_id; }
                set { SetPropertyValue<stock_location>("location_id", ref flocation_id, value); }
            }
    
        
            private stock_move fmove_id;
            //FK FK_mrp_procurement_move_id
            [Custom("Caption", "Move Id")]
            public stock_move move_id {
                get { return fmove_id; }
                set { SetPropertyValue<stock_move>("move_id", ref fmove_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private purchase_order fpurchase_id;
            //FK FK_mrp_procurement_purchase_id
            [Custom("Caption", "Purchase Id")]
            public purchase_order purchase_id {
                get { return fpurchase_id; }
                set { SetPropertyValue<purchase_order>("purchase_id", ref fpurchase_id, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_mrp_procurement_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
            private DateTime? fdate_planned;
            [Custom("Caption", "Date Planned")]
            public DateTime? date_planned {
                get { return fdate_planned; }
                set { SetPropertyValue("date_planned", ref fdate_planned, value); }
            }
    
            private System.Boolean fclose_move;
            [Custom("Caption", "Close Move")]
            public System.Boolean close_move {
                get { return fclose_move; }
                set { SetPropertyValue("close_move", ref fclose_move, value); }
            }
    
            private DateTime? fdate_close;
            [Custom("Caption", "Date Close")]
            public DateTime? date_close {
                get { return fdate_close; }
                set { SetPropertyValue("date_close", ref fdate_close, value); }
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
    
        
            private mrp_bom fbom_id;
            //FK FK_mrp_procurement_bom_id
            [Custom("Caption", "Bom Id")]
            public mrp_bom bom_id {
                get { return fbom_id; }
                set { SetPropertyValue<mrp_bom>("bom_id", ref fbom_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public mrp_procurement(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

