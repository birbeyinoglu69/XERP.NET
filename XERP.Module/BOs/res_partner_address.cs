
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
	[DefaultProperty("fax")]
    [Persistent("res_partner_address")]
	public partial class res_partner_address : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_partner_address_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_res_partner_address_create_uid
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
            //FK FK_res_partner_address_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_partner_function ffunction;
            //FK FK_res_partner_address_function
            [Custom("Caption", "Function")]
            public res_partner_function function {
                get { return ffunction; }
                set { SetPropertyValue<res_partner_function>("function", ref ffunction, value); }
            }
    
            private System.String ffax;
            [Size(64)]
            [Custom("Caption", "Fax")]
            public System.String fax {
                get { return ffax; }
                set { SetPropertyValue("fax", ref ffax, value); }
            }
    
            private System.String fstreet2;
            [Size(128)]
            [Custom("Caption", "Street2")]
            public System.String street2 {
                get { return fstreet2; }
                set { SetPropertyValue("street2", ref fstreet2, value); }
            }
    
            private System.String fphone;
            [Size(64)]
            [Custom("Caption", "Phone")]
            public System.String phone {
                get { return fphone; }
                set { SetPropertyValue("phone", ref fphone, value); }
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
    
        
            private res_partner fpartner_id;
            //FK FK_res_partner_address_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private System.String fcity;
            [Size(128)]
            [Custom("Caption", "City")]
            public System.String city {
                get { return fcity; }
                set { SetPropertyValue("city", ref fcity, value); }
            }
    
            private System.String fname;
            [Size(64)]
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
    
            private System.String ftitle;
            [Size(32)]
            [Custom("Caption", "Title")]
            public System.String title {
                get { return ftitle; }
                set { SetPropertyValue("title", ref ftitle, value); }
            }
    
            private System.String fmobile;
            [Size(64)]
            [Custom("Caption", "Mobile")]
            public System.String mobile {
                get { return fmobile; }
                set { SetPropertyValue("mobile", ref fmobile, value); }
            }
    
        
            private res_country fcountry_id;
            //FK FK_res_partner_address_country_id
            [Custom("Caption", "Country Id")]
            public res_country country_id {
                get { return fcountry_id; }
                set { SetPropertyValue<res_country>("country_id", ref fcountry_id, value); }
            }
    
            private System.String fbirthdate;
            [Size(64)]
            [Custom("Caption", "Birthdate")]
            public System.String birthdate {
                get { return fbirthdate; }
                set { SetPropertyValue("birthdate", ref fbirthdate, value); }
            }
    
        
            private res_country_state1 fstate1_id;
            //FK FK_res_partner_address_state1_id
            [Custom("Caption", "State1 Id")]
            public res_country_state1 state1_id {
                get { return fstate1_id; }
                set { SetPropertyValue<res_country_state1>("state1_id", ref fstate1_id, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.String femail;
            [Size(240)]
            [Custom("Caption", "Email")]
            public System.String email {
                get { return femail; }
                set { SetPropertyValue("email", ref femail, value); }
            }
    
            private System.String frelationship;
            [Size(64)]
            [Custom("Caption", "Relationship")]
            public System.String relationship {
                get { return frelationship; }
                set { SetPropertyValue("relationship", ref frelationship, value); }
            }
    
        
            private res_partner frelative_id;
            //FK FK_res_partner_address_relative_id
            [Custom("Caption", "Relative Id")]
            public res_partner relative_id {
                get { return frelative_id; }
                set { SetPropertyValue<res_partner>("relative_id", ref frelative_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_partner_address(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

