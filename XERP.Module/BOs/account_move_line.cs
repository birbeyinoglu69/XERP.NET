
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
    [Persistent("account_move_line")]
	public partial class account_move_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_move_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_move_line_create_uid
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
            //FK FK_account_move_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_move_reconcile freconcile_id;
            //FK FK_account_move_line_reconcile_id
            [Custom("Caption", "Reconcile Id")]
            public account_move_reconcile reconcile_id {
                get { return freconcile_id; }
                set { SetPropertyValue<account_move_reconcile>("reconcile_id", ref freconcile_id, value); }
            }
    
        
            private account_bank_state1ment fstate1ment_id;
            //FK FK_account_move_line_state1ment_id
            [Custom("Caption", "State1ment Id")]
            public account_bank_state1ment state1ment_id {
                get { return fstate1ment_id; }
                set { SetPropertyValue<account_bank_state1ment>("state1ment_id", ref fstate1ment_id, value); }
            }
    
            private System.Decimal famount_taxed;
            [Custom("Caption", "Amount Taxed")]
            public System.Decimal amount_taxed {
                get { return famount_taxed; }
                set { SetPropertyValue("amount_taxed", ref famount_taxed, value); }
            }
    
        
            private account_account faccount_id;
            //FK FK_account_move_line_account_id
            [Custom("Caption", "Account Id")]
            public account_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_account>("account_id", ref faccount_id, value); }
            }
    
        
            private res_currency fcurrency_id;
            //FK FK_account_move_line_currency_id
            [Custom("Caption", "Currency Id")]
            public res_currency currency_id {
                get { return fcurrency_id; }
                set { SetPropertyValue<res_currency>("currency_id", ref fcurrency_id, value); }
            }
    
        
            private account_period fperiod_id;
            //FK FK_account_move_line_period_id
            [Custom("Caption", "Period Id")]
            public account_period period_id {
                get { return fperiod_id; }
                set { SetPropertyValue<account_period>("period_id", ref fperiod_id, value); }
            }
    
            private System.Double famount_currency;
            [Custom("Caption", "Amount Currency")]
            public System.Double amount_currency {
                get { return famount_currency; }
                set { SetPropertyValue("amount_currency", ref famount_currency, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_account_move_line_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
        
            private account_move_reconcile freconcile_partial_id;
            //FK FK_account_move_line_reconcile_partial_id
            [Custom("Caption", "Reconcile Partial id")]
            public account_move_reconcile reconcile_partial_id {
                get { return freconcile_partial_id; }
                set { SetPropertyValue<account_move_reconcile>("reconcile_partial_id", ref freconcile_partial_id, value); }
            }
    
            private System.Boolean fblocked;
            [Custom("Caption", "Blocked")]
            public System.Boolean blocked {
                get { return fblocked; }
                set { SetPropertyValue("blocked", ref fblocked, value); }
            }
    
        
            private account_analytic_account fanalytic_account_id;
            //FK FK_account_move_line_analytic_account_id
            [Custom("Caption", "Analytic Account id")]
            public account_analytic_account analytic_account_id {
                get { return fanalytic_account_id; }
                set { SetPropertyValue<account_analytic_account>("analytic_account_id", ref fanalytic_account_id, value); }
            }
    
            private System.Decimal ftax_amount;
            [Custom("Caption", "Tax Amount")]
            public System.Decimal tax_amount {
                get { return ftax_amount; }
                set { SetPropertyValue("tax_amount", ref ftax_amount, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private account_tax faccount_tax_id;
            //FK FK_account_move_line_account_tax_id
            [Custom("Caption", "Account Tax id")]
            public account_tax account_tax_id {
                get { return faccount_tax_id; }
                set { SetPropertyValue<account_tax>("account_tax_id", ref faccount_tax_id, value); }
            }
    
            private System.String fcentralisation;
            [Size(6)]
            [Custom("Caption", "Centralisation")]
            public System.String centralisation {
                get { return fcentralisation; }
                set { SetPropertyValue("centralisation", ref fcentralisation, value); }
            }
    
        
            private product_uom fproduct_uom_id;
            //FK FK_account_move_line_product_uom_id
            [Custom("Caption", "Product Uom id")]
            public product_uom product_uom_id {
                get { return fproduct_uom_id; }
                set { SetPropertyValue<product_uom>("product_uom_id", ref fproduct_uom_id, value); }
            }
    
        
            private account_journal fjournal_id;
            //FK FK_account_move_line_journal_id
            [Custom("Caption", "Journal Id")]
            public account_journal journal_id {
                get { return fjournal_id; }
                set { SetPropertyValue<account_journal>("journal_id", ref fjournal_id, value); }
            }
    
        
            private account_tax_code ftax_code_id;
            //FK FK_account_move_line_tax_code_id
            [Custom("Caption", "Tax Code id")]
            public account_tax_code tax_code_id {
                get { return ftax_code_id; }
                set { SetPropertyValue<account_tax_code>("tax_code_id", ref ftax_code_id, value); }
            }
    
            private System.Decimal fcredit;
            [Custom("Caption", "Credit")]
            public System.Decimal credit {
                get { return fcredit; }
                set { SetPropertyValue("credit", ref fcredit, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_account_move_line_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private account_move fmove_id;
            //FK FK_account_move_line_move_id
            [Custom("Caption", "Move Id")]
            public account_move move_id {
                get { return fmove_id; }
                set { SetPropertyValue<account_move>("move_id", ref fmove_id, value); }
            }
    
            private System.Decimal fdebit;
            [Custom("Caption", "Debit")]
            public System.Decimal debit {
                get { return fdebit; }
                set { SetPropertyValue("debit", ref fdebit, value); }
            }
    
            private System.String fref1;
            [Size(32)]
            [Custom("Caption", "Ref")]
            public System.String ref1 {
                get { return fref1; }
                set { SetPropertyValue("ref1", ref fref1, value); }
            }
    
            private System.Decimal fquantity;
            [Custom("Caption", "Quantity")]
            public System.Decimal quantity {
                get { return fquantity; }
                set { SetPropertyValue("quantity", ref fquantity, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_move_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

