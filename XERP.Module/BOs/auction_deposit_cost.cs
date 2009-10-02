
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
    [Persistent("auction_deposit_cost")]
	public partial class auction_deposit_cost : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //auction_deposit_cost_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_auction_deposit_cost_create_uid
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
            //FK FK_auction_deposit_cost_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private auction_deposit fdeposit_id;
            //FK FK_auction_deposit_cost_deposit_id
            [Custom("Caption", "Deposit Id")]
            public auction_deposit deposit_id {
                get { return fdeposit_id; }
                set { SetPropertyValue<auction_deposit>("deposit_id", ref fdeposit_id, value); }
            }
    
        
            private account_account faccount;
            //FK FK_auction_deposit_cost_account
            [Custom("Caption", "Account")]
            public account_account account {
                get { return faccount; }
                set { SetPropertyValue<account_account>("account", ref faccount, value); }
            }
    
            private System.Double famount;
            [Custom("Caption", "Amount")]
            public System.Double amount {
                get { return famount; }
                set { SetPropertyValue("amount", ref famount, value); }
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
		public auction_deposit_cost(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

