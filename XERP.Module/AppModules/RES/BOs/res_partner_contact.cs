
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
	[DefaultProperty("website")]
    [Persistent("res_partner_contact")]
	public partial class res_partner_contact : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_partner_contact_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_res_partner_contact_create_uid
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
            //FK FK_res_partner_contact_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fwebsite;
            [Size(120)]
            [Custom("Caption", "Website")]
            public System.String website {
                get { return fwebsite; }
                set { SetPropertyValue("website", ref fwebsite, value); }
            }
    
            private System.String ffirst_name;
            [Size(30)]
            [Custom("Caption", "First Name")]
            public System.String first_name {
                get { return ffirst_name; }
                set { SetPropertyValue("first_name", ref ffirst_name, value); }
            }
    
            private System.String fname;
            [Size(30)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String ftitle;
            [Size(16)]
            [Custom("Caption", "Title")]
            public System.String title {
                get { return ftitle; }
                set { SetPropertyValue("title", ref ftitle, value); }
            }
    
            private System.String fmobile;
            [Size(30)]
            [Custom("Caption", "Mobile")]
            public System.String mobile {
                get { return fmobile; }
                set { SetPropertyValue("mobile", ref fmobile, value); }
            }
    
        
            private res_country fcountry_id;
            //FK FK_res_partner_contact_country_id
            [Custom("Caption", "Country Id")]
            public res_country country_id {
                get { return fcountry_id; }
                set { SetPropertyValue<res_country>("country_id", ref fcountry_id, value); }
            }
    
        
            private res_lang flang_id;
            //FK FK_res_partner_contact_lang_id
            [Custom("Caption", "Lang Id")]
            public res_lang lang_id {
                get { return flang_id; }
                set { SetPropertyValue<res_lang>("lang_id", ref flang_id, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.String femail;
            [Size(240)]
            [Custom("Caption", "Email")]
            public System.String email {
                get { return femail; }
                set { SetPropertyValue("email", ref femail, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_partner_contact(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

