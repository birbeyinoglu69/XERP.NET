
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
	[DefaultProperty("code")]
    [Persistent("mrp_bom")]
	public partial class mrp_bom : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //mrp_bom_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_mrp_bom_create_uid
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
            //FK FK_mrp_bom_write_uid
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
    
            private System.String fcode;
            [Size(16)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
        
            private product_uom fproduct_uom;
            //FK FK_mrp_bom_product_uom
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
    
            private System.String frevision_type;
            [Size(16)]
            [Custom("Caption", "Revision Type")]
            public System.String revision_type {
                get { return frevision_type; }
                set { SetPropertyValue("revision_type", ref frevision_type, value); }
            }
    
            private System.Double fproduct_qty;
            [Custom("Caption", "Product Qty")]
            public System.Double product_qty {
                get { return fproduct_qty; }
                set { SetPropertyValue("product_qty", ref fproduct_qty, value); }
            }
    
        
            private product_uom fproduct_uos;
            //FK FK_mrp_bom_product_uos
            [Custom("Caption", "Product Uos")]
            public product_uom product_uos {
                get { return fproduct_uos; }
                set { SetPropertyValue<product_uom>("product_uos", ref fproduct_uos, value); }
            }
    
            private System.Double fproduct_efficiency;
            [Custom("Caption", "Product Efficiency")]
            public System.Double product_efficiency {
                get { return fproduct_efficiency; }
                set { SetPropertyValue("product_efficiency", ref fproduct_efficiency, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.Double fproduct_rounding;
            [Custom("Caption", "Product Rounding")]
            public System.Double product_rounding {
                get { return fproduct_rounding; }
                set { SetPropertyValue("product_rounding", ref fproduct_rounding, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_mrp_bom_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private mrp_bom fbom_id;
            //FK FK_mrp_bom_bom_id
            [Custom("Caption", "Bom Id")]
            public mrp_bom bom_id {
                get { return fbom_id; }
                set { SetPropertyValue<mrp_bom>("bom_id", ref fbom_id, value); }
            }
    
        
            private mrp_routing frouting_id;
            //FK FK_mrp_bom_routing_id
            [Custom("Caption", "Routing Id")]
            public mrp_routing routing_id {
                get { return frouting_id; }
                set { SetPropertyValue<mrp_routing>("routing_id", ref frouting_id, value); }
            }
    
            private System.String fposition;
            [Size(64)]
            [Custom("Caption", "Position")]
            public System.String position {
                get { return fposition; }
                set { SetPropertyValue("position", ref fposition, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public mrp_bom(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

