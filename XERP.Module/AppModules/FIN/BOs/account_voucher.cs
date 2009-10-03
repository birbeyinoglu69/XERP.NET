
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
	[DefaultProperty("state1")]
    [Persistent("account_voucher")]
	public partial class account_voucher : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_voucher_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_voucher_create_uid
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
            //FK FK_account_voucher_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_account faccount_id;
            //FK FK_account_voucher_account_id
            [Custom("Caption", "Account Id")]
            public account_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_account>("account_id", ref faccount_id, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String freference;
            [Size(64)]
            [Custom("Caption", "Reference")]
            public System.String reference {
                get { return freference; }
                set { SetPropertyValue("reference", ref freference, value); }
            }
    
            private System.String fnumber;
            [Size(32)]
            [Custom("Caption", "Number")]
            public System.String number {
                get { return fnumber; }
                set { SetPropertyValue("number", ref fnumber, value); }
            }
    
        
            private res_company fcompany_id;
            //FK FK_account_voucher_company_id
            [Custom("Caption", "Company Id")]
            public res_company company_id {
                get { return fcompany_id; }
                set { SetPropertyValue<res_company>("company_id", ref fcompany_id, value); }
            }
    
        
            private res_currency fcurrency_id;
            //FK FK_account_voucher_currency_id
            [Custom("Caption", "Currency Id")]
            public res_currency currency_id {
                get { return fcurrency_id; }
                set { SetPropertyValue<res_currency>("currency_id", ref fcurrency_id, value); }
            }
    
        
            private account_period fperiod_id;
            //FK FK_account_voucher_period_id
            [Custom("Caption", "Period Id")]
            public account_period period_id {
                get { return fperiod_id; }
                set { SetPropertyValue<account_period>("period_id", ref fperiod_id, value); }
            }
    
            private System.String fnarration;
            [Size(-1)]
            [Custom("Caption", "Narration")]
            public System.String narration {
                get { return fnarration; }
                set { SetPropertyValue("narration", ref fnarration, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_account_voucher_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
        
            private account_move fmove_id;
            //FK FK_account_voucher_move_id
            [Custom("Caption", "Move Id")]
            public account_move move_id {
                get { return fmove_id; }
                set { SetPropertyValue<account_move>("move_id", ref fmove_id, value); }
            }
    
            private System.String fname;
            [Size(256)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String freference_type;
            [Size(16)]
            [Custom("Caption", "Reference Type")]
            public System.String reference_type {
                get { return freference_type; }
                set { SetPropertyValue("reference_type", ref freference_type, value); }
            }
    
        
            private account_journal fjournal_id;
            //FK FK_account_voucher_journal_id
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
    
            private System.String ftype;
            [Size(128)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_voucher(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

