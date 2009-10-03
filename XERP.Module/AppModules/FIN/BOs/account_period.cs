
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
	[DefaultProperty("code")]
    [Persistent("account_period")]
	public partial class account_period : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_period_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_period_create_uid
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
            //FK FK_account_period_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private account_fiscalyear ffiscalyear_id;
            //FK FK_account_period_fiscalyear_id
            [Custom("Caption", "Fiscalyear Id")]
            public account_fiscalyear fiscalyear_id {
                get { return ffiscalyear_id; }
                set { SetPropertyValue<account_fiscalyear>("fiscalyear_id", ref ffiscalyear_id, value); }
            }
    
            private System.String fcode;
            [Size(12)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.Boolean fspecial;
            [Custom("Caption", "Special")]
            public System.Boolean special {
                get { return fspecial; }
                set { SetPropertyValue("special", ref fspecial, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_period(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

