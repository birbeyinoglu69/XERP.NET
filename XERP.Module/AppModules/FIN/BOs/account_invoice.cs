
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
	[DefaultProperty("origin")]
    [Persistent("account_invoice")]
	public partial class account_invoice : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_invoice_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_invoice_create_uid
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
            //FK FK_account_invoice_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String forigin;
            [Size(64)]
            [Custom("Caption", "Origin")]
            public System.String origin {
                get { return forigin; }
                set { SetPropertyValue("origin", ref forigin, value); }
            }
    
            private System.String fcomment;
            [Size(-1)]
            [Custom("Caption", "Comment")]
            public System.String comment {
                get { return fcomment; }
                set { SetPropertyValue("comment", ref fcomment, value); }
            }
    
            private System.Decimal fcheck_total;
            [Custom("Caption", "Check Total")]
            public System.Decimal check_total {
                get { return fcheck_total; }
                set { SetPropertyValue("check_total", ref fcheck_total, value); }
            }
    
            private System.String freference;
            [Size(64)]
            [Custom("Caption", "Reference")]
            public System.String reference {
                get { return freference; }
                set { SetPropertyValue("reference", ref freference, value); }
            }
    
        
            private account_payment_term fpayment_term;
            //FK FK_account_invoice_payment_term
            [Custom("Caption", "Payment Term")]
            public account_payment_term payment_term {
                get { return fpayment_term; }
                set { SetPropertyValue<account_payment_term>("payment_term", ref fpayment_term, value); }
            }
    
            private System.String fnumber;
            [Size(32)]
            [Custom("Caption", "Number")]
            public System.String number {
                get { return fnumber; }
                set { SetPropertyValue("number", ref fnumber, value); }
            }
    
        
            private account_journal fjournal_id;
            //FK FK_account_invoice_journal_id
            [Custom("Caption", "Journal Id")]
            public account_journal journal_id {
                get { return fjournal_id; }
                set { SetPropertyValue<account_journal>("journal_id", ref fjournal_id, value); }
            }
    
        
            private res_currency fcurrency_id;
            //FK FK_account_invoice_currency_id
            [Custom("Caption", "Currency Id")]
            public res_currency currency_id {
                get { return fcurrency_id; }
                set { SetPropertyValue<res_currency>("currency_id", ref fcurrency_id, value); }
            }
    
        
            private res_partner_address faddress_invoice_id;
            //FK FK_account_invoice_address_invoice_id
            [Custom("Caption", "Address Invoice id")]
            public res_partner_address address_invoice_id {
                get { return faddress_invoice_id; }
                set { SetPropertyValue<res_partner_address>("address_invoice_id", ref faddress_invoice_id, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_account_invoice_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
        
            private account_account faccount_id;
            //FK FK_account_invoice_account_id
            [Custom("Caption", "Account Id")]
            public account_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_account>("account_id", ref faccount_id, value); }
            }
    
        
            private account_fiscal_position ffiscal_position;
            //FK FK_account_invoice_fiscal_position
            [Custom("Caption", "Fiscal Position")]
            public account_fiscal_position fiscal_position {
                get { return ffiscal_position; }
                set { SetPropertyValue<account_fiscal_position>("fiscal_position", ref ffiscal_position, value); }
            }
    
            private System.Double famount_untaxed;
            [Custom("Caption", "Amount Untaxed")]
            public System.Double amount_untaxed {
                get { return famount_untaxed; }
                set { SetPropertyValue("amount_untaxed", ref famount_untaxed, value); }
            }
    
            private System.String freference_type;
            [Size(16)]
            [Custom("Caption", "Reference Type")]
            public System.String reference_type {
                get { return freference_type; }
                set { SetPropertyValue("reference_type", ref freference_type, value); }
            }
    
        
            private res_company fcompany_id;
            //FK FK_account_invoice_company_id
            [Custom("Caption", "Company Id")]
            public res_company company_id {
                get { return fcompany_id; }
                set { SetPropertyValue<res_company>("company_id", ref fcompany_id, value); }
            }
    
            private System.Double famount_tax;
            [Custom("Caption", "Amount Tax")]
            public System.Double amount_tax {
                get { return famount_tax; }
                set { SetPropertyValue("amount_tax", ref famount_tax, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private res_partner_bank fpartner_bank;
            //FK FK_account_invoice_partner_bank
            [Custom("Caption", "Partner Bank")]
            public res_partner_bank partner_bank {
                get { return fpartner_bank; }
                set { SetPropertyValue<res_partner_bank>("partner_bank", ref fpartner_bank, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.Boolean freconciled;
            [Custom("Caption", "Reconciled")]
            public System.Boolean reconciled {
                get { return freconciled; }
                set { SetPropertyValue("reconciled", ref freconciled, value); }
            }
    
            private System.Double fresidual;
            [Custom("Caption", "Residual")]
            public System.Double residual {
                get { return fresidual; }
                set { SetPropertyValue("residual", ref fresidual, value); }
            }
    
            private System.String fmove_name;
            [Size(64)]
            [Custom("Caption", "Move Name")]
            public System.String move_name {
                get { return fmove_name; }
                set { SetPropertyValue("move_name", ref fmove_name, value); }
            }
    
        
            private account_period fperiod_id;
            //FK FK_account_invoice_period_id
            [Custom("Caption", "Period Id")]
            public account_period period_id {
                get { return fperiod_id; }
                set { SetPropertyValue<account_period>("period_id", ref fperiod_id, value); }
            }
    
        
            private account_move fmove_id;
            //FK FK_account_invoice_move_id
            [Custom("Caption", "Move Id")]
            public account_move move_id {
                get { return fmove_id; }
                set { SetPropertyValue<account_move>("move_id", ref fmove_id, value); }
            }
    
            private System.Double famount_total;
            [Custom("Caption", "Amount Total")]
            public System.Double amount_total {
                get { return famount_total; }
                set { SetPropertyValue("amount_total", ref famount_total, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private res_partner_address faddress_contact_id;
            //FK FK_account_invoice_address_contact_id
            [Custom("Caption", "Address Contact id")]
            public res_partner_address address_contact_id {
                get { return faddress_contact_id; }
                set { SetPropertyValue<res_partner_address>("address_contact_id", ref faddress_contact_id, value); }
            }
    
            private System.String fprice_type;
            [Size(16)]
            [Custom("Caption", "Price Type")]
            public System.String price_type {
                get { return fprice_type; }
                set { SetPropertyValue("price_type", ref fprice_type, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_invoice(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

