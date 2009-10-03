
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
	[DefaultProperty("communication")]
    [Persistent("payment_line")]
	public partial class payment_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //payment_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_payment_line_create_uid
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
            //FK FK_payment_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private payment_order forder_id;
            //FK FK_payment_line_order_id
            [Custom("Caption", "Order Id")]
            public payment_order order_id {
                get { return forder_id; }
                set { SetPropertyValue<payment_order>("order_id", ref forder_id, value); }
            }
    
            private System.String fcommunication;
            [Size(64)]
            [Custom("Caption", "Communication")]
            public System.String communication {
                get { return fcommunication; }
                set { SetPropertyValue("communication", ref fcommunication, value); }
            }
    
        
            private res_currency fcurrency;
            //FK FK_payment_line_currency
            [Custom("Caption", "Currency")]
            public res_currency currency {
                get { return fcurrency; }
                set { SetPropertyValue<res_currency>("currency", ref fcurrency, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_payment_line_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
        
            private res_currency fcompany_currency;
            //FK FK_payment_line_company_currency
            [Custom("Caption", "Company Currency")]
            public res_currency company_currency {
                get { return fcompany_currency; }
                set { SetPropertyValue<res_currency>("company_currency", ref fcompany_currency, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private account_move_line fmove_line_id;
            //FK FK_payment_line_move_line_id
            [Custom("Caption", "Move Line id")]
            public account_move_line move_line_id {
                get { return fmove_line_id; }
                set { SetPropertyValue<account_move_line>("move_line_id", ref fmove_line_id, value); }
            }
    
        
            private res_partner_bank fbank_id;
            //FK FK_payment_line_bank_id
            [Custom("Caption", "Bank Id")]
            public res_partner_bank bank_id {
                get { return fbank_id; }
                set { SetPropertyValue<res_partner_bank>("bank_id", ref fbank_id, value); }
            }
    
            private System.String fcommunication2;
            [Size(64)]
            [Custom("Caption", "Communication2")]
            public System.String communication2 {
                get { return fcommunication2; }
                set { SetPropertyValue("communication2", ref fcommunication2, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.Decimal famount_currency;
            [Custom("Caption", "Amount Currency")]
            public System.Decimal amount_currency {
                get { return famount_currency; }
                set { SetPropertyValue("amount_currency", ref famount_currency, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public payment_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

