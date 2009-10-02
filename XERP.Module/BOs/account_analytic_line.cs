
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
    [Persistent("account_analytic_line")]
	public partial class account_analytic_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_analytic_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_analytic_line_create_uid
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
            //FK FK_account_analytic_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fcode;
            [Size(8)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_account_analytic_line_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fname;
            [Size(256)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private account_account fgeneral_account_id;
            //FK FK_account_analytic_line_general_account_id
            [Custom("Caption", "General Account id")]
            public account_account general_account_id {
                get { return fgeneral_account_id; }
                set { SetPropertyValue<account_account>("general_account_id", ref fgeneral_account_id, value); }
            }
    
        
            private product_uom fproduct_uom_id;
            //FK FK_account_analytic_line_product_uom_id
            [Custom("Caption", "Product Uom id")]
            public product_uom product_uom_id {
                get { return fproduct_uom_id; }
                set { SetPropertyValue<product_uom>("product_uom_id", ref fproduct_uom_id, value); }
            }
    
        
            private account_analytic_journal fjournal_id;
            //FK FK_account_analytic_line_journal_id
            [Custom("Caption", "Journal Id")]
            public account_analytic_journal journal_id {
                get { return fjournal_id; }
                set { SetPropertyValue<account_analytic_journal>("journal_id", ref fjournal_id, value); }
            }
    
            private System.Double famount;
            [Custom("Caption", "Amount")]
            public System.Double amount {
                get { return famount; }
                set { SetPropertyValue("amount", ref famount, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_account_analytic_line_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
            private System.Double funit_amount;
            [Custom("Caption", "Unit Amount")]
            public System.Double unit_amount {
                get { return funit_amount; }
                set { SetPropertyValue("unit_amount", ref funit_amount, value); }
            }
    
            private System.String fref1;
            [Size(32)]
            [Custom("Caption", "Ref")]
            public System.String ref1 {
                get { return fref1; }
                set { SetPropertyValue("ref1", ref fref1, value); }
            }
    
        
            private account_move_line fmove_id;
            //FK FK_account_analytic_line_move_id
            [Custom("Caption", "Move Id")]
            public account_move_line move_id {
                get { return fmove_id; }
                set { SetPropertyValue<account_move_line>("move_id", ref fmove_id, value); }
            }
    
        
            private account_analytic_account faccount_id;
            //FK FK_account_analytic_line_account_id
            [Custom("Caption", "Account Id")]
            public account_analytic_account account_id {
                get { return faccount_id; }
                set { SetPropertyValue<account_analytic_account>("account_id", ref faccount_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_analytic_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

