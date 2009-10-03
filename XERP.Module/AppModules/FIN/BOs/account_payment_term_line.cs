
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
    [Persistent("account_payment_term_line")]
	public partial class account_payment_term_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_payment_term_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_payment_term_line_create_uid
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
            //FK FK_account_payment_term_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_payment_term fpayment_id;
            //FK FK_account_payment_term_line_payment_id
            [Custom("Caption", "Payment Id")]
            public account_payment_term payment_id {
                get { return fpayment_id; }
                set { SetPropertyValue<account_payment_term>("payment_id", ref fpayment_id, value); }
            }
    
            private System.String fname;
            [Size(32)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Double fvalue_amount;
            [Custom("Caption", "Value Amount")]
            public System.Double value_amount {
                get { return fvalue_amount; }
                set { SetPropertyValue("value_amount", ref fvalue_amount, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
            private System.Int32 fdays2;
            [Custom("Caption", "Days2")]
            public System.Int32 days2 {
                get { return fdays2; }
                set { SetPropertyValue("days2", ref fdays2, value); }
            }
    
            private System.Int32 fdays;
            [Custom("Caption", "Days")]
            public System.Int32 days {
                get { return fdays; }
                set { SetPropertyValue("days", ref fdays, value); }
            }
    
            private System.String fvalue;
            [Size(16)]
            [Custom("Caption", "Value")]
            public System.String value {
                get { return fvalue; }
                set { SetPropertyValue("value", ref fvalue, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_payment_term_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

