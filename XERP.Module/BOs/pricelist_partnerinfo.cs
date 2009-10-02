
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
    [Persistent("pricelist_partnerinfo")]
	public partial class pricelist_partnerinfo : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //pricelist_partnerinfo_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_pricelist_partnerinfo_create_uid
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
            //FK FK_pricelist_partnerinfo_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Double fmin_quantity;
            [Custom("Caption", "Min Quantity")]
            public System.Double min_quantity {
                get { return fmin_quantity; }
                set { SetPropertyValue("min_quantity", ref fmin_quantity, value); }
            }
    
            private System.Decimal fprice;
            [Custom("Caption", "Price")]
            public System.Decimal price {
                get { return fprice; }
                set { SetPropertyValue("price", ref fprice, value); }
            }
    
        
            private product_supplierinfo fsuppinfo_id;
            //FK FK_pricelist_partnerinfo_suppinfo_id
            [Custom("Caption", "Suppinfo Id")]
            public product_supplierinfo suppinfo_id {
                get { return fsuppinfo_id; }
                set { SetPropertyValue<product_supplierinfo>("suppinfo_id", ref fsuppinfo_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public pricelist_partnerinfo(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

