
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
    [Persistent("account_chart_template")]
	public partial class account_chart_template : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_chart_template_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_chart_template_create_uid
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
            //FK FK_account_chart_template_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_account_template fproperty_account_expense_categ;
            //FK FK_account_chart_template_property_account_expense_categ
            [Custom("Caption", "Property Account expense categ")]
            public account_account_template property_account_expense_categ {
                get { return fproperty_account_expense_categ; }
                set { SetPropertyValue<account_account_template>("property_account_expense_categ", ref fproperty_account_expense_categ, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private account_account_template fproperty_account_expense;
            //FK FK_account_chart_template_property_account_expense
            [Custom("Caption", "Property Account expense")]
            public account_account_template property_account_expense {
                get { return fproperty_account_expense; }
                set { SetPropertyValue<account_account_template>("property_account_expense", ref fproperty_account_expense, value); }
            }
    
        
            private account_account_template fproperty_account_receivable;
            //FK FK_account_chart_template_property_account_receivable
            [Custom("Caption", "Property Account receivable")]
            public account_account_template property_account_receivable {
                get { return fproperty_account_receivable; }
                set { SetPropertyValue<account_account_template>("property_account_receivable", ref fproperty_account_receivable, value); }
            }
    
        
            private account_account_template fproperty_account_payable;
            //FK FK_account_chart_template_property_account_payable
            [Custom("Caption", "Property Account payable")]
            public account_account_template property_account_payable {
                get { return fproperty_account_payable; }
                set { SetPropertyValue<account_account_template>("property_account_payable", ref fproperty_account_payable, value); }
            }
    
        
            private account_tax_code_template ftax_code_root_id;
            //FK FK_account_chart_template_tax_code_root_id
            [Custom("Caption", "Tax Code root id")]
            public account_tax_code_template tax_code_root_id {
                get { return ftax_code_root_id; }
                set { SetPropertyValue<account_tax_code_template>("tax_code_root_id", ref ftax_code_root_id, value); }
            }
    
        
            private account_account_template fproperty_account_income_categ;
            //FK FK_account_chart_template_property_account_income_categ
            [Custom("Caption", "Property Account income categ")]
            public account_account_template property_account_income_categ {
                get { return fproperty_account_income_categ; }
                set { SetPropertyValue<account_account_template>("property_account_income_categ", ref fproperty_account_income_categ, value); }
            }
    
        
            private account_account_template fproperty_account_income;
            //FK FK_account_chart_template_property_account_income
            [Custom("Caption", "Property Account income")]
            public account_account_template property_account_income {
                get { return fproperty_account_income; }
                set { SetPropertyValue<account_account_template>("property_account_income", ref fproperty_account_income, value); }
            }
    
        
            private account_account_template fbank_account_view_id;
            //FK FK_account_chart_template_bank_account_view_id
            [Custom("Caption", "Bank Account view id")]
            public account_account_template bank_account_view_id {
                get { return fbank_account_view_id; }
                set { SetPropertyValue<account_account_template>("bank_account_view_id", ref fbank_account_view_id, value); }
            }
    
        
            private account_account_template faccount_root_id;
            //FK FK_account_chart_template_account_root_id
            [Custom("Caption", "Account Root id")]
            public account_account_template account_root_id {
                get { return faccount_root_id; }
                set { SetPropertyValue<account_account_template>("account_root_id", ref faccount_root_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_chart_template(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

