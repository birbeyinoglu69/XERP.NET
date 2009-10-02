
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
	[DefaultProperty("city")]
    [Persistent("res_bank")]
	public partial class res_bank : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_bank_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_res_bank_create_uid
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
            //FK FK_res_bank_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fcity;
            [Size(128)]
            [Custom("Caption", "City")]
            public System.String city {
                get { return fcity; }
                set { SetPropertyValue("city", ref fcity, value); }
            }
    
            private System.String ffax;
            [Size(64)]
            [Custom("Caption", "Fax")]
            public System.String fax {
                get { return ffax; }
                set { SetPropertyValue("fax", ref ffax, value); }
            }
    
            private System.String fcode;
            [Size(64)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fzip;
            [Size(24)]
            [Custom("Caption", "Zip")]
            public System.String zip {
                get { return fzip; }
                set { SetPropertyValue("zip", ref fzip, value); }
            }
    
        
            private res_country fcountry;
            //FK FK_res_bank_country
            [Custom("Caption", "Country")]
            public res_country country {
                get { return fcountry; }
                set { SetPropertyValue<res_country>("country", ref fcountry, value); }
            }
    
            private System.String fstreet2;
            [Size(128)]
            [Custom("Caption", "Street2")]
            public System.String street2 {
                get { return fstreet2; }
                set { SetPropertyValue("street2", ref fstreet2, value); }
            }
    
            private System.String fbic;
            [Size(11)]
            [Custom("Caption", "Bic")]
            public System.String bic {
                get { return fbic; }
                set { SetPropertyValue("bic", ref fbic, value); }
            }
    
            private System.String fphone;
            [Size(64)]
            [Custom("Caption", "Phone")]
            public System.String phone {
                get { return fphone; }
                set { SetPropertyValue("phone", ref fphone, value); }
            }
    
        
            private res_country_state1 fstate1;
            //FK FK_res_bank_state1
            [Custom("Caption", "State1")]
            public res_country_state1 state1 {
                get { return fstate1; }
                set { SetPropertyValue<res_country_state1>("state1", ref fstate1, value); }
            }
    
            private System.String fstreet;
            [Size(128)]
            [Custom("Caption", "Street")]
            public System.String street {
                get { return fstreet; }
                set { SetPropertyValue("street", ref fstreet, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.String femail;
            [Size(64)]
            [Custom("Caption", "Email")]
            public System.String email {
                get { return femail; }
                set { SetPropertyValue("email", ref femail, value); }
            }
    
            private System.String fclearing;
            [Size(64)]
            [Custom("Caption", "Clearing")]
            public System.String clearing {
                get { return fclearing; }
                set { SetPropertyValue("clearing", ref fclearing, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_bank(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

