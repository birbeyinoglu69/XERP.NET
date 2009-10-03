
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
	[DefaultProperty("close_method")]
    [Persistent("account_account_type")]
	public partial class account_account_type : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_account_type_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_account_type_create_uid
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
            //FK FK_account_account_type_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fclose_method;
            [Size(16)]
            [Custom("Caption", "Close Method")]
            public System.String close_method {
                get { return fclose_method; }
                set { SetPropertyValue("close_method", ref fclose_method, value); }
            }
    
            private System.Boolean fpartner_account;
            [Custom("Caption", "Partner Account")]
            public System.Boolean partner_account {
                get { return fpartner_account; }
                set { SetPropertyValue("partner_account", ref fpartner_account, value); }
            }
    
            private System.String fcode;
            [Size(32)]
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
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
            private System.Int32 fsign;
            [Custom("Caption", "Sign")]
            public System.Int32 sign {
                get { return fsign; }
                set { SetPropertyValue("sign", ref fsign, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_account_type(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

