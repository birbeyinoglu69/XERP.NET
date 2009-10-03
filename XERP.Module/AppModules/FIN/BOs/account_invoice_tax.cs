
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
    [Persistent("account_invoice_tax")]
	public partial class account_invoice_tax : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_invoice_tax_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_invoice_tax_create_uid
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
            //FK FK_account_invoice_tax_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
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
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
        
            private account_invoice finvoice_id;
            //FK FK_account_invoice_tax_invoice_id
            [Custom("Caption", "Invoice Id")]
            public account_invoice invoice_id {
                get { return finvoice_id; }
                set { SetPropertyValue<account_invoice>("invoice_id", ref finvoice_id, value); }
            }
    
            private System.Boolean fmanual;
            [Custom("Caption", "Manual")]
            public System.Boolean manual {
                get { return fmanual; }
                set { SetPropertyValue("manual", ref fmanual, value); }
            }
    
            private System.Decimal fbase_amount;
            [Custom("Caption", "Base Amount")]
            public System.Decimal base_amount {
                get { return fbase_amount; }
                set { SetPropertyValue("base_amount", ref fbase_amount, value); }
            }
    
        
            private account_tax_code fbase_code_id;
            //FK FK_account_invoice_tax_base_code_id
            [Custom("Caption", "Base Code id")]
            public account_tax_code base_code_id {
                get { return fbase_code_id; }
                set { SetPropertyValue<account_tax_code>("base_code_id", ref fbase_code_id, value); }
            }
    
        
            private account_tax_code ftax_code_id;
            //FK FK_account_invoice_tax_tax_code_id
            [Custom("Caption", "Tax Code id")]
            public account_tax_code tax_code_id {
                get { return ftax_code_id; }
                set { SetPropertyValue<account_tax_code>("tax_code_id", ref ftax_code_id, value); }
            }
    
            private System.Decimal famount;
            [Custom("Caption", "Amount")]
            public System.Decimal amount {
                get { return famount; }
                set { SetPropertyValue("amount", ref famount, value); }
            }
    
            private System.Decimal fbase1;
            [Custom("Caption", "Base")]
            public System.Decimal base1 {
                get { return fbase1; }
                set { SetPropertyValue("base1", ref fbase1, value); }
            }
    
        
            private account_account faccount_id;
            //FK FK_account_invoice_tax_account_id
            [Custom("Caption", "Account Id")]
            public account_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_account>("account_id", ref faccount_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_invoice_tax(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

