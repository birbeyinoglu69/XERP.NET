
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
    [Persistent("account_bank_state1ment_line")]
	public partial class account_bank_state1ment_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_bank_state1ment_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_bank_state1ment_line_create_uid
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
            //FK FK_account_bank_state1ment_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_bank_state1ment_reconcile freconcile_id;
            //FK FK_account_bank_state1ment_line_reconcile_id
            [Custom("Caption", "Reconcile Id")]
            public account_bank_state1ment_reconcile reconcile_id {
                get { return freconcile_id; }
                set { SetPropertyValue<account_bank_state1ment_reconcile>("reconcile_id", ref freconcile_id, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_account_bank_state1ment_line_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
        
            private account_account faccount_id;
            //FK FK_account_bank_state1ment_line_account_id
            [Custom("Caption", "Account Id")]
            public account_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_account>("account_id", ref faccount_id, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
            private System.Double famount;
            [Custom("Caption", "Amount")]
            public System.Double amount {
                get { return famount; }
                set { SetPropertyValue("amount", ref famount, value); }
            }
    
            private System.String fref1;
            [Size(32)]
            [Custom("Caption", "Ref")]
            public System.String ref1 {
                get { return fref1; }
                set { SetPropertyValue("ref1", ref fref1, value); }
            }
    
        
            private account_bank_state1ment fstate1ment_id;
            //FK FK_account_bank_state1ment_line_state1ment_id
            [Custom("Caption", "State1ment Id")]
            public account_bank_state1ment state1ment_id {
                get { return fstate1ment_id; }
                set { SetPropertyValue<account_bank_state1ment>("state1ment_id", ref fstate1ment_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_bank_state1ment_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

