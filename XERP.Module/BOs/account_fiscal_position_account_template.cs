
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
    [Persistent("account_fiscal_position_account_template")]
	public partial class account_fiscal_position_account_template : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_fiscal_position_account_template_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_fiscal_position_account_template_create_uid
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
            //FK FK_account_fiscal_position_account_template_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_fiscal_position_template fposition_id;
            //FK FK_account_fiscal_position_account_template_position_id
            [Custom("Caption", "Position Id")]
            public account_fiscal_position_template position_id {
                get { return fposition_id; }
                set { SetPropertyValue<account_fiscal_position_template>("position_id", ref fposition_id, value); }
            }
    
        
            private account_account_template faccount_dest_id;
            //FK FK_account_fiscal_position_account_template_account_dest_id
            [Custom("Caption", "Account Dest id")]
            public account_account_template account_dest_id {
                get { return faccount_dest_id; }
                set { SetPropertyValue<account_account_template>("account_dest_id", ref faccount_dest_id, value); }
            }
    
        
            private account_account_template faccount_src_id;
            //FK FK_account_fiscal_position_account_template_account_src_id
            [Custom("Caption", "Account Src id")]
            public account_account_template account_src_id {
                get { return faccount_src_id; }
                set { SetPropertyValue<account_account_template>("account_src_id", ref faccount_src_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_fiscal_position_account_template(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

