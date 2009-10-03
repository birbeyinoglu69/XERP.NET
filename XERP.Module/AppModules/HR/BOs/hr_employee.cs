
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
	[DefaultProperty("otherid")]
    [Persistent("hr_employee")]
	public partial class hr_employee : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //hr_employee_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_hr_employee_create_uid
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
            //FK FK_hr_employee_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fotherid;
            [Size(32)]
            [Custom("Caption", "Otherid")]
            public System.String otherid {
                get { return fotherid; }
                set { SetPropertyValue("otherid", ref fotherid, value); }
            }
    
        
            private res_partner_address faddress_id;
            //FK FK_hr_employee_address_id
            [Custom("Caption", "Address Id")]
            public res_partner_address address_id {
                get { return faddress_id; }
                set { SetPropertyValue<res_partner_address>("address_id", ref faddress_id, value); }
            }
    
            private System.String fmarital;
            [Size(32)]
            [Custom("Caption", "Marital")]
            public System.String marital {
                get { return fmarital; }
                set { SetPropertyValue("marital", ref fmarital, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.String fsinid;
            [Size(32)]
            [Custom("Caption", "Sinid")]
            public System.String sinid {
                get { return fsinid; }
                set { SetPropertyValue("sinid", ref fsinid, value); }
            }
    
            private System.String fwork_email;
            [Size(128)]
            [Custom("Caption", "Work Email")]
            public System.String work_email {
                get { return fwork_email; }
                set { SetPropertyValue("work_email", ref fwork_email, value); }
            }
    
        
            private res_partner_address faddress_home_id;
            //FK FK_hr_employee_address_home_id
            [Custom("Caption", "Address Home id")]
            public res_partner_address address_home_id {
                get { return faddress_home_id; }
                set { SetPropertyValue<res_partner_address>("address_home_id", ref faddress_home_id, value); }
            }
    
            private System.String fwork_location;
            [Size(32)]
            [Custom("Caption", "Work Location")]
            public System.String work_location {
                get { return fwork_location; }
                set { SetPropertyValue("work_location", ref fwork_location, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_hr_employee_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fwork_phone;
            [Size(32)]
            [Custom("Caption", "Work Phone")]
            public System.String work_phone {
                get { return fwork_phone; }
                set { SetPropertyValue("work_phone", ref fwork_phone, value); }
            }
    
            private System.String fgender;
            [Size(16)]
            [Custom("Caption", "Gender")]
            public System.String gender {
                get { return fgender; }
                set { SetPropertyValue("gender", ref fgender, value); }
            }
    
            private System.String fssnid;
            [Size(32)]
            [Custom("Caption", "Ssnid")]
            public System.String ssnid {
                get { return fssnid; }
                set { SetPropertyValue("ssnid", ref fssnid, value); }
            }
    
        
            private res_country fcountry_id;
            //FK FK_hr_employee_country_id
            [Custom("Caption", "Country Id")]
            public res_country country_id {
                get { return fcountry_id; }
                set { SetPropertyValue<res_country>("country_id", ref fcountry_id, value); }
            }
    
        
            private res_company fcompany_id;
            //FK FK_hr_employee_company_id
            [Custom("Caption", "Company Id")]
            public res_company company_id {
                get { return fcompany_id; }
                set { SetPropertyValue<res_company>("company_id", ref fcompany_id, value); }
            }
    
        
            private hr_employee fparent_id;
            //FK FK_hr_employee_parent_id
            [Custom("Caption", "Parent Id")]
            public hr_employee parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<hr_employee>("parent_id", ref fparent_id, value); }
            }
    
        
            private hr_employee_category fcategory_id;
            //FK FK_hr_employee_category_id
            [Custom("Caption", "Category Id")]
            public hr_employee_category category_id {
                get { return fcategory_id; }
                set { SetPropertyValue<hr_employee_category>("category_id", ref fcategory_id, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private System.String faudiens_num;
            [Size(30)]
            [Custom("Caption", "Audiens Num")]
            public System.String audiens_num {
                get { return faudiens_num; }
                set { SetPropertyValue("audiens_num", ref faudiens_num, value); }
            }
    
            private System.Boolean fmanager;
            [Custom("Caption", "Manager")]
            public System.Boolean manager {
                get { return fmanager; }
                set { SetPropertyValue("manager", ref fmanager, value); }
            }
    
            private System.Int32 fchildren;
            [Custom("Caption", "Children")]
            public System.Int32 children {
                get { return fchildren; }
                set { SetPropertyValue("children", ref fchildren, value); }
            }
    
        
            private hr_employee_marital_status fmarital_status;
            //FK FK_hr_employee_marital_status
            [Custom("Caption", "Marital Status")]
            public hr_employee_marital_status marital_status {
                get { return fmarital_status; }
                set { SetPropertyValue<hr_employee_marital_status>("marital_status", ref fmarital_status, value); }
            }
    
            private System.String fplace_of_birth;
            [Size(30)]
            [Custom("Caption", "Place Of birth")]
            public System.String place_of_birth {
                get { return fplace_of_birth; }
                set { SetPropertyValue("place_of_birth", ref fplace_of_birth, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_hr_employee_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
        
            private account_analytic_journal fjournal_id;
            //FK FK_hr_employee_journal_id
            [Custom("Caption", "Journal Id")]
            public account_analytic_journal journal_id {
                get { return fjournal_id; }
                set { SetPropertyValue<account_analytic_journal>("journal_id", ref fjournal_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public hr_employee(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

