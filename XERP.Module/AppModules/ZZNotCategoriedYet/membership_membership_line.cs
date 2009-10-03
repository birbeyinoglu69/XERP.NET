
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
    [Persistent("membership_membership_line")]
	public partial class membership_membership_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //membership_membership_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_membership_membership_line_create_uid
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
            //FK FK_membership_membership_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_invoice_line faccount_invoice_line;
            //FK FK_membership_membership_line_account_invoice_line
            [Custom("Caption", "Account Invoice line")]
            public account_invoice_line account_invoice_line {
                get { return faccount_invoice_line; }
                set { SetPropertyValue<account_invoice_line>("account_invoice_line", ref faccount_invoice_line, value); }
            }
    
        
            private res_partner fpartner;
            //FK FK_membership_membership_line_partner
            [Custom("Caption", "Partner")]
            public res_partner partner {
                get { return fpartner; }
                set { SetPropertyValue<res_partner>("partner", ref fpartner, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public membership_membership_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

