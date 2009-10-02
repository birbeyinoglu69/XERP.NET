
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
    [Persistent("res_users")]
	public partial class res_users : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_users_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.String flogin;
            [Size(64)]
            [Custom("Caption", "Login")]
            public System.String login {
                get { return flogin; }
                set { SetPropertyValue("login", ref flogin, value); }
            }
    
            private System.String fpassword;
            [Size(64)]
            [Custom("Caption", "Password")]
            public System.String password {
                get { return fpassword; }
                set { SetPropertyValue("password", ref fpassword, value); }
            }
    
            private System.String fcontext_tz;
            [Size(64)]
            [Custom("Caption", "Context Tz")]
            public System.String context_tz {
                get { return fcontext_tz; }
                set { SetPropertyValue("context_tz", ref fcontext_tz, value); }
            }
    
            private System.String fsignature;
            [Size(-1)]
            [Custom("Caption", "Signature")]
            public System.String signature {
                get { return fsignature; }
                set { SetPropertyValue("signature", ref fsignature, value); }
            }
    
            private System.String fcontext_lang;
            [Size(64)]
            [Custom("Caption", "Context Lang")]
            public System.String context_lang {
                get { return fcontext_lang; }
                set { SetPropertyValue("context_lang", ref fcontext_lang, value); }
            }
    
            private System.Int32 faction_id;
            [Custom("Caption", "Action Id")]
            public System.Int32 action_id {
                get { return faction_id; }
                set { SetPropertyValue("action_id", ref faction_id, value); }
            }
    
        
            private res_users fcreate_uid;
            //FK FK_res_users_create_uid
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
            //FK FK_res_users_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Int32 fmenu_id;
            [Custom("Caption", "Menu Id")]
            public System.Int32 menu_id {
                get { return fmenu_id; }
                set { SetPropertyValue("menu_id", ref fmenu_id, value); }
            }
    
        
            private res_partner_address faddress_id;
            //FK FK_res_users_address_id
            [Custom("Caption", "Address Id")]
            public res_partner_address address_id {
                get { return faddress_id; }
                set { SetPropertyValue<res_partner_address>("address_id", ref faddress_id, value); }
            }
    
        
            private res_company fcompany_id;
            //FK FK_res_users_company_id
            [Custom("Caption", "Company Id")]
            public res_company company_id {
                get { return fcompany_id; }
                set { SetPropertyValue<res_company>("company_id", ref fcompany_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_users(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

