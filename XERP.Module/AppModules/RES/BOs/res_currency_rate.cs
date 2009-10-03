
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
    [Persistent("res_currency_rate")]
	public partial class res_currency_rate : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_currency_rate_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_res_currency_rate_create_uid
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
            //FK FK_res_currency_rate_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_currency fcurrency_id;
            //FK FK_res_currency_rate_currency_id
            [Custom("Caption", "Currency Id")]
            public res_currency currency_id {
                get { return fcurrency_id; }
                set { SetPropertyValue<res_currency>("currency_id", ref fcurrency_id, value); }
            }
    
            private System.Decimal frate;
            [Custom("Caption", "Rate")]
            public System.Decimal rate {
                get { return frate; }
                set { SetPropertyValue("rate", ref frate, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_currency_rate(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

