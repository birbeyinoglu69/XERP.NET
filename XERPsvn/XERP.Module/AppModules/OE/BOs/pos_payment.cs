
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
	[DefaultProperty("payment_name")]
    [Persistent("pos_payment")]
	public partial class pos_payment : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //pos_payment_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_pos_payment_create_uid
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
            //FK FK_pos_payment_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_payment_term fpayment_id;
            //FK FK_pos_payment_payment_id
            [Custom("Caption", "Payment Id")]
            public account_payment_term payment_id {
                get { return fpayment_id; }
                set { SetPropertyValue<account_payment_term>("payment_id", ref fpayment_id, value); }
            }
    
            private System.String fpayment_name;
            [Size(32)]
            [Custom("Caption", "Payment Name")]
            public System.String payment_name {
                get { return fpayment_name; }
                set { SetPropertyValue("payment_name", ref fpayment_name, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private pos_order forder_id;
            //FK FK_pos_payment_order_id
            [Custom("Caption", "Order Id")]
            public pos_order order_id {
                get { return forder_id; }
                set { SetPropertyValue<pos_order>("order_id", ref forder_id, value); }
            }
    
        
            private account_journal fjournal_id;
            //FK FK_pos_payment_journal_id
            [Custom("Caption", "Journal Id")]
            public account_journal journal_id {
                get { return fjournal_id; }
                set { SetPropertyValue<account_journal>("journal_id", ref fjournal_id, value); }
            }
    
            private System.Double famount;
            [Custom("Caption", "Amount")]
            public System.Double amount {
                get { return famount; }
                set { SetPropertyValue("amount", ref famount, value); }
            }
    
            private System.String fpayment_nb;
            [Size(32)]
            [Custom("Caption", "Payment Nb")]
            public System.String payment_nb {
                get { return fpayment_nb; }
                set { SetPropertyValue("payment_nb", ref fpayment_nb, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public pos_payment(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

