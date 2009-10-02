
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
    [Persistent("hr_expense_line")]
	public partial class hr_expense_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //hr_expense_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_hr_expense_line_create_uid
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
            //FK FK_hr_expense_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private product_uom fuom_id;
            //FK FK_hr_expense_line_uom_id
            [Custom("Caption", "Uom Id")]
            public product_uom uom_id {
                get { return fuom_id; }
                set { SetPropertyValue<product_uom>("uom_id", ref fuom_id, value); }
            }
    
            private System.String fname;
            [Size(128)]
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
    
        
            private account_analytic_account fanalytic_account;
            //FK FK_hr_expense_line_analytic_account
            [Custom("Caption", "Analytic Account")]
            public account_analytic_account analytic_account {
                get { return fanalytic_account; }
                set { SetPropertyValue<account_analytic_account>("analytic_account", ref fanalytic_account, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_hr_expense_line_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private hr_expense_expense fexpense_id;
            //FK FK_hr_expense_line_expense_id
            [Custom("Caption", "Expense Id")]
            public hr_expense_expense expense_id {
                get { return fexpense_id; }
                set { SetPropertyValue<hr_expense_expense>("expense_id", ref fexpense_id, value); }
            }
    
            private System.Double funit_amount;
            [Custom("Caption", "Unit Amount")]
            public System.Double unit_amount {
                get { return funit_amount; }
                set { SetPropertyValue("unit_amount", ref funit_amount, value); }
            }
    
            private System.Double funit_quantity;
            [Custom("Caption", "Unit Quantity")]
            public System.Double unit_quantity {
                get { return funit_quantity; }
                set { SetPropertyValue("unit_quantity", ref funit_quantity, value); }
            }
    
            private System.String fref1;
            [Size(32)]
            [Custom("Caption", "Ref")]
            public System.String ref1 {
                get { return fref1; }
                set { SetPropertyValue("ref1", ref fref1, value); }
            }
    
            private System.String fdescription;
            [Size(-1)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public hr_expense_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

