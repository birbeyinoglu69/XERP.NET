
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
	[DefaultProperty("code")]
    [Persistent("account_journal")]
	public partial class account_journal : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_journal_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_journal_create_uid
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
            //FK FK_account_journal_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_account fdefault_debit_account_id;
            //FK FK_account_journal_default_debit_account_id
            [Custom("Caption", "Default Debit account id")]
            public account_account default_debit_account_id {
                get { return fdefault_debit_account_id; }
                set { SetPropertyValue<account_account>("default_debit_account_id", ref fdefault_debit_account_id, value); }
            }
    
            private System.String fcode;
            [Size(16)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
        
            private account_journal_view fview_id;
            //FK FK_account_journal_view_id
            [Custom("Caption", "View Id")]
            public account_journal_view view_id {
                get { return fview_id; }
                set { SetPropertyValue<account_journal_view>("view_id", ref fview_id, value); }
            }
    
        
            private res_currency fcurrency;
            //FK FK_account_journal_currency
            [Custom("Caption", "Currency")]
            public res_currency currency {
                get { return fcurrency; }
                set { SetPropertyValue<res_currency>("currency", ref fcurrency, value); }
            }
    
        
            private ir_sequence fsequence_id;
            //FK FK_account_journal_sequence_id
            [Custom("Caption", "Sequence Id")]
            public ir_sequence sequence_id {
                get { return fsequence_id; }
                set { SetPropertyValue<ir_sequence>("sequence_id", ref fsequence_id, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.Boolean fupdate_posted;
            [Custom("Caption", "Update Posted")]
            public System.Boolean update_posted {
                get { return fupdate_posted; }
                set { SetPropertyValue("update_posted", ref fupdate_posted, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_account_journal_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Boolean fcentralisation;
            [Custom("Caption", "Centralisation")]
            public System.Boolean centralisation {
                get { return fcentralisation; }
                set { SetPropertyValue("centralisation", ref fcentralisation, value); }
            }
    
            private System.Boolean fgroup_invoice_lines;
            [Custom("Caption", "Group Invoice lines")]
            public System.Boolean group_invoice_lines {
                get { return fgroup_invoice_lines; }
                set { SetPropertyValue("group_invoice_lines", ref fgroup_invoice_lines, value); }
            }
    
            private System.Boolean frefund_journal;
            [Custom("Caption", "Refund Journal")]
            public System.Boolean refund_journal {
                get { return frefund_journal; }
                set { SetPropertyValue("refund_journal", ref frefund_journal, value); }
            }
    
        
            private ir_sequence finvoice_sequence_id;
            //FK FK_account_journal_invoice_sequence_id
            [Custom("Caption", "Invoice Sequence id")]
            public ir_sequence invoice_sequence_id {
                get { return finvoice_sequence_id; }
                set { SetPropertyValue<ir_sequence>("invoice_sequence_id", ref finvoice_sequence_id, value); }
            }
    
            private System.Boolean fentry_posted;
            [Custom("Caption", "Entry Posted")]
            public System.Boolean entry_posted {
                get { return fentry_posted; }
                set { SetPropertyValue("entry_posted", ref fentry_posted, value); }
            }
    
            private System.String ftype;
            [Size(32)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
        
            private account_account fdefault_credit_account_id;
            //FK FK_account_journal_default_credit_account_id
            [Custom("Caption", "Default Credit account id")]
            public account_account default_credit_account_id {
                get { return fdefault_credit_account_id; }
                set { SetPropertyValue<account_account>("default_credit_account_id", ref fdefault_credit_account_id, value); }
            }
    
        
            private account_analytic_journal fanalytic_journal_id;
            //FK FK_account_journal_analytic_journal_id
            [Custom("Caption", "Analytic Journal id")]
            public account_analytic_journal analytic_journal_id {
                get { return fanalytic_journal_id; }
                set { SetPropertyValue<account_analytic_journal>("analytic_journal_id", ref fanalytic_journal_id, value); }
            }
    
            private System.Boolean fallow_date;
            [Custom("Caption", "Allow Date")]
            public System.Boolean allow_date {
                get { return fallow_date; }
                set { SetPropertyValue("allow_date", ref fallow_date, value); }
            }
    
        
            private account_analytic_plan fplan_id;
            //FK FK_account_journal_plan_id
            [Custom("Caption", "Plan Id")]
            public account_analytic_plan plan_id {
                get { return fplan_id; }
                set { SetPropertyValue<account_analytic_plan>("plan_id", ref fplan_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_journal(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

