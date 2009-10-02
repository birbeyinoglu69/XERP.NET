
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
    [Persistent("account_model_line")]
	public partial class account_model_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_model_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_model_line_create_uid
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
            //FK FK_account_model_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_model fmodel_id;
            //FK FK_account_model_line_model_id
            [Custom("Caption", "Model Id")]
            public account_model model_id {
                get { return fmodel_id; }
                set { SetPropertyValue<account_model>("model_id", ref fmodel_id, value); }
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
    
            private System.String fref1;
            [Size(16)]
            [Custom("Caption", "Ref")]
            public System.String ref1 {
                get { return fref1; }
                set { SetPropertyValue("ref1", ref fref1, value); }
            }
    
        
            private res_currency fcurrency_id;
            //FK FK_account_model_line_currency_id
            [Custom("Caption", "Currency Id")]
            public res_currency currency_id {
                get { return fcurrency_id; }
                set { SetPropertyValue<res_currency>("currency_id", ref fcurrency_id, value); }
            }
    
            private System.Decimal fcredit;
            [Custom("Caption", "Credit")]
            public System.Decimal credit {
                get { return fcredit; }
                set { SetPropertyValue("credit", ref fcredit, value); }
            }
    
            private System.String fdate_maturity;
            [Size(16)]
            [Custom("Caption", "Date Maturity")]
            public System.String date_maturity {
                get { return fdate_maturity; }
                set { SetPropertyValue("date_maturity", ref fdate_maturity, value); }
            }
    
            private System.Decimal fdebit;
            [Custom("Caption", "Debit")]
            public System.Decimal debit {
                get { return fdebit; }
                set { SetPropertyValue("debit", ref fdebit, value); }
            }
    
            private System.String fdate;
            [Size(16)]
            [Custom("Caption", "Date")]
            public System.String date {
                get { return fdate; }
                set { SetPropertyValue("date", ref fdate, value); }
            }
    
            private System.Double famount_currency;
            [Custom("Caption", "Amount Currency")]
            public System.Double amount_currency {
                get { return famount_currency; }
                set { SetPropertyValue("amount_currency", ref famount_currency, value); }
            }
    
            private System.Decimal fquantity;
            [Custom("Caption", "Quantity")]
            public System.Decimal quantity {
                get { return fquantity; }
                set { SetPropertyValue("quantity", ref fquantity, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_account_model_line_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
        
            private account_account faccount_id;
            //FK FK_account_model_line_account_id
            [Custom("Caption", "Account Id")]
            public account_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_account>("account_id", ref faccount_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_model_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

