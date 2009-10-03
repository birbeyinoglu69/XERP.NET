
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
    [Persistent("delivery_grid")]
	public partial class delivery_grid : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //delivery_grid_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_delivery_grid_create_uid
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
            //FK FK_delivery_grid_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
        
            private delivery_carrier fcarrier_id;
            //FK FK_delivery_grid_carrier_id
            [Custom("Caption", "Carrier Id")]
            public delivery_carrier carrier_id {
                get { return fcarrier_id; }
                set { SetPropertyValue<delivery_carrier>("carrier_id", ref fcarrier_id, value); }
            }
    
            private System.String fzip_from;
            [Size(12)]
            [Custom("Caption", "Zip From")]
            public System.String zip_from {
                get { return fzip_from; }
                set { SetPropertyValue("zip_from", ref fzip_from, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.String fzip_to;
            [Size(12)]
            [Custom("Caption", "Zip To")]
            public System.String zip_to {
                get { return fzip_to; }
                set { SetPropertyValue("zip_to", ref fzip_to, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public delivery_grid(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

