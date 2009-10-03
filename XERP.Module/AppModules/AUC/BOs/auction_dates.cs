
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
    [Persistent("auction_dates")]
	public partial class auction_dates : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //auction_dates_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_auction_dates_create_uid
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
            //FK FK_auction_dates_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_journal fjournal_seller_id;
            //FK FK_auction_dates_journal_seller_id
            [Custom("Caption", "Journal Seller id")]
            public account_journal journal_seller_id {
                get { return fjournal_seller_id; }
                set { SetPropertyValue<account_journal>("journal_seller_id", ref fjournal_seller_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private account_account facc_income;
            //FK FK_auction_dates_acc_income
            [Custom("Caption", "Acc Income")]
            public account_account acc_income {
                get { return facc_income; }
                set { SetPropertyValue<account_account>("acc_income", ref facc_income, value); }
            }
    
        
            private account_journal fjournal_id;
            //FK FK_auction_dates_journal_id
            [Custom("Caption", "Journal Id")]
            public account_journal journal_id {
                get { return fjournal_id; }
                set { SetPropertyValue<account_journal>("journal_id", ref fjournal_id, value); }
            }
    
            private System.Double fadj_total;
            [Custom("Caption", "Adj Total")]
            public System.Double adj_total {
                get { return fadj_total; }
                set { SetPropertyValue("adj_total", ref fadj_total, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private account_analytic_account faccount_analytic_id;
            //FK FK_auction_dates_account_analytic_id
            [Custom("Caption", "Account Analytic id")]
            public account_analytic_account account_analytic_id {
                get { return faccount_analytic_id; }
                set { SetPropertyValue<account_analytic_account>("account_analytic_id", ref faccount_analytic_id, value); }
            }
    
        
            private account_account facc_expense;
            //FK FK_auction_dates_acc_expense
            [Custom("Caption", "Acc Expense")]
            public account_account acc_expense {
                get { return facc_expense; }
                set { SetPropertyValue<account_account>("acc_expense", ref facc_expense, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public auction_dates(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

