
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
    [Persistent("stock_location_path")]
	public partial class stock_location_path : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //stock_location_path_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_stock_location_path_create_uid
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
            //FK FK_stock_location_path_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private stock_location flocation_from_id;
            //FK FK_stock_location_path_location_from_id
            [Custom("Caption", "Location From id")]
            public stock_location location_from_id {
                get { return flocation_from_id; }
                set { SetPropertyValue<stock_location>("location_from_id", ref flocation_from_id, value); }
            }
    
            private System.Int32 fdelay;
            [Custom("Caption", "Delay")]
            public System.Int32 delay {
                get { return fdelay; }
                set { SetPropertyValue("delay", ref fdelay, value); }
            }
    
        
            private stock_location flocation_dest_id;
            //FK FK_stock_location_path_location_dest_id
            [Custom("Caption", "Location Dest id")]
            public stock_location location_dest_id {
                get { return flocation_dest_id; }
                set { SetPropertyValue<stock_location>("location_dest_id", ref flocation_dest_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fauto;
            [Size(16)]
            [Custom("Caption", "Auto")]
            public System.String auto {
                get { return fauto; }
                set { SetPropertyValue("auto", ref fauto, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_stock_location_path_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public stock_location_path(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

