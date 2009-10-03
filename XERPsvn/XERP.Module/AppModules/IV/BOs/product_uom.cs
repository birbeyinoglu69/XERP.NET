
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
	[DefaultProperty("name")]
    [Persistent("product_uom")]
	public partial class product_uom : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //product_uom_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_product_uom_create_uid
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
            //FK FK_product_uom_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
        
            private product_uom_categ fcategory_id;
            //FK FK_product_uom_category_id
            [Custom("Caption", "Category Id")]
            public product_uom_categ category_id {
                get { return fcategory_id; }
                set { SetPropertyValue<product_uom_categ>("category_id", ref fcategory_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Decimal frounding;
            [Custom("Caption", "Rounding")]
            public System.Decimal rounding {
                get { return frounding; }
                set { SetPropertyValue("rounding", ref frounding, value); }
            }
    
            private System.Decimal ffactor;
            [Custom("Caption", "Factor")]
            public System.Decimal factor {
                get { return ffactor; }
                set { SetPropertyValue("factor", ref ffactor, value); }
            }
    
            private System.Decimal ffactor_inv_data;
            [Custom("Caption", "Factor Inv data")]
            public System.Decimal factor_inv_data {
                get { return ffactor_inv_data; }
                set { SetPropertyValue("factor_inv_data", ref ffactor_inv_data, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public product_uom(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

