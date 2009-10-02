
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
	[DefaultProperty("product_code")]
    [Persistent("product_supplierinfo")]
	public partial class product_supplierinfo : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //product_supplierinfo_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_product_supplierinfo_create_uid
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
            //FK FK_product_supplierinfo_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private product_template fproduct_id;
            //FK FK_product_supplierinfo_product_id
            [Custom("Caption", "Product Id")]
            public product_template product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_template>("product_id", ref fproduct_id, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
            private System.Double fqty;
            [Custom("Caption", "Qty")]
            public System.Double qty {
                get { return fqty; }
                set { SetPropertyValue("qty", ref fqty, value); }
            }
    
            private System.Int32 fdelay;
            [Custom("Caption", "Delay")]
            public System.Int32 delay {
                get { return fdelay; }
                set { SetPropertyValue("delay", ref fdelay, value); }
            }
    
            private System.String fproduct_code;
            [Size(64)]
            [Custom("Caption", "Product Code")]
            public System.String product_code {
                get { return fproduct_code; }
                set { SetPropertyValue("product_code", ref fproduct_code, value); }
            }
    
            private System.String fproduct_name;
            [Size(128)]
            [Custom("Caption", "Product Name")]
            public System.String product_name {
                get { return fproduct_name; }
                set { SetPropertyValue("product_name", ref fproduct_name, value); }
            }
    
        
            private res_partner fname;
            //FK FK_product_supplierinfo_name
            [Custom("Caption", "Name")]
            public res_partner name {
                get { return fname; }
                set { SetPropertyValue<res_partner>("name", ref fname, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public product_supplierinfo(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

