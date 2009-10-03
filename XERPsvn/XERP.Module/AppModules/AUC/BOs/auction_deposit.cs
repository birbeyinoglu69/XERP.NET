
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
	[DefaultProperty("info")]
    [Persistent("auction_deposit")]
	public partial class auction_deposit : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //auction_deposit_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_auction_deposit_create_uid
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
            //FK FK_auction_deposit_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String finfo;
            [Size(64)]
            [Custom("Caption", "Info")]
            public System.String info {
                get { return finfo; }
                set { SetPropertyValue("info", ref finfo, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Boolean ftransfer;
            [Custom("Caption", "Transfer")]
            public System.Boolean transfer {
                get { return ftransfer; }
                set { SetPropertyValue("transfer", ref ftransfer, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_auction_deposit_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private System.Boolean ftotal_neg;
            [Custom("Caption", "Total Neg")]
            public System.Boolean total_neg {
                get { return ftotal_neg; }
                set { SetPropertyValue("total_neg", ref ftotal_neg, value); }
            }
    
            private System.String fmethod;
            [Size(16)]
            [Custom("Caption", "Method")]
            public System.String method {
                get { return fmethod; }
                set { SetPropertyValue("method", ref fmethod, value); }
            }
    
        
            private account_tax ftax_id;
            //FK FK_auction_deposit_tax_id
            [Custom("Caption", "Tax Id")]
            public account_tax tax_id {
                get { return ftax_id; }
                set { SetPropertyValue<account_tax>("tax_id", ref ftax_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public auction_deposit(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

