
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
	[DefaultProperty("date_prefered")]
    [Persistent("payment_order")]
	public partial class payment_order : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //payment_order_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_payment_order_create_uid
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
            //FK FK_payment_order_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fdate_prefered;
            [Size(16)]
            [Custom("Caption", "Date Prefered")]
            public System.String date_prefered {
                get { return fdate_prefered; }
                set { SetPropertyValue("date_prefered", ref fdate_prefered, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_payment_order_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String freference;
            [Size(128)]
            [Custom("Caption", "Reference")]
            public System.String reference {
                get { return freference; }
                set { SetPropertyValue("reference", ref freference, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private payment_mode fmode;
            //FK FK_payment_order_mode
            [Custom("Caption", "Mode")]
            public payment_mode mode {
                get { return fmode; }
                set { SetPropertyValue<payment_mode>("mode", ref fmode, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public payment_order(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

