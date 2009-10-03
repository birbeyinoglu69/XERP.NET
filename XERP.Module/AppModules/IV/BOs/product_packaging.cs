
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
	[DefaultProperty("name")]
    [Persistent("product_packaging")]
	public partial class product_packaging : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //product_packaging_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_product_packaging_create_uid
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
            //FK FK_product_packaging_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Int32 frows;
            [Custom("Caption", "Rows")]
            public System.Int32 rows {
                get { return frows; }
                set { SetPropertyValue("rows", ref frows, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Double fweight;
            [Custom("Caption", "Weight")]
            public System.Double weight {
                get { return fweight; }
                set { SetPropertyValue("weight", ref fweight, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
            private System.Int32 ful_qty;
            [Custom("Caption", "Ul Qty")]
            public System.Int32 ul_qty {
                get { return ful_qty; }
                set { SetPropertyValue("ul_qty", ref ful_qty, value); }
            }
    
            private System.String fean;
            [Size(14)]
            [Custom("Caption", "Ean")]
            public System.String ean {
                get { return fean; }
                set { SetPropertyValue("ean", ref fean, value); }
            }
    
            private System.Double fqty;
            [Custom("Caption", "Qty")]
            public System.Double qty {
                get { return fqty; }
                set { SetPropertyValue("qty", ref fqty, value); }
            }
    
        
            private product_ul ful;
            //FK FK_product_packaging_ul
            [Custom("Caption", "Ul")]
            public product_ul ul {
                get { return ful; }
                set { SetPropertyValue<product_ul>("ul", ref ful, value); }
            }
    
            private System.Double flength;
            [Custom("Caption", "Length")]
            public System.Double length {
                get { return flength; }
                set { SetPropertyValue("length", ref flength, value); }
            }
    
            private System.String fcode;
            [Size(14)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
            private System.Double fweight_ul;
            [Custom("Caption", "Weight Ul")]
            public System.Double weight_ul {
                get { return fweight_ul; }
                set { SetPropertyValue("weight_ul", ref fweight_ul, value); }
            }
    
            private System.Double fheight;
            [Custom("Caption", "Height")]
            public System.Double height {
                get { return fheight; }
                set { SetPropertyValue("height", ref fheight, value); }
            }
    
            private System.Double fwidth;
            [Custom("Caption", "Width")]
            public System.Double width {
                get { return fwidth; }
                set { SetPropertyValue("width", ref fwidth, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_product_packaging_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public product_packaging(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

