
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
	[DefaultProperty("subproduct_type")]
    [Persistent("mrp_subproduct")]
	public partial class mrp_subproduct : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //mrp_subproduct_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_mrp_subproduct_create_uid
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
            //FK FK_mrp_subproduct_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private mrp_bom fbom_id;
            //FK FK_mrp_subproduct_bom_id
            [Custom("Caption", "Bom Id")]
            public mrp_bom bom_id {
                get { return fbom_id; }
                set { SetPropertyValue<mrp_bom>("bom_id", ref fbom_id, value); }
            }
    
            private System.String fsubproduct_type;
            [Size(16)]
            [Custom("Caption", "Subproduct Type")]
            public System.String subproduct_type {
                get { return fsubproduct_type; }
                set { SetPropertyValue("subproduct_type", ref fsubproduct_type, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_mrp_subproduct_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private product_uom fproduct_uom;
            //FK FK_mrp_subproduct_product_uom
            [Custom("Caption", "Product Uom")]
            public product_uom product_uom {
                get { return fproduct_uom; }
                set { SetPropertyValue<product_uom>("product_uom", ref fproduct_uom, value); }
            }
    
            private System.Double fproduct_qty;
            [Custom("Caption", "Product Qty")]
            public System.Double product_qty {
                get { return fproduct_qty; }
                set { SetPropertyValue("product_qty", ref fproduct_qty, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public mrp_subproduct(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

