
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
	[DefaultProperty("warn_footer")]
    [Persistent("project_project")]
	public partial class project_project : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //project_project_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_project_project_create_uid
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
            //FK FK_project_project_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_partner_address fcontact_id;
            //FK FK_project_project_contact_id
            [Custom("Caption", "Contact Id")]
            public res_partner_address contact_id {
                get { return fcontact_id; }
                set { SetPropertyValue<res_partner_address>("contact_id", ref fcontact_id, value); }
            }
    
        
            private hr_timesheet_group ftimesheet_id;
            //FK FK_project_project_timesheet_id
            [Custom("Caption", "Timesheet Id")]
            public hr_timesheet_group timesheet_id {
                get { return ftimesheet_id; }
                set { SetPropertyValue<hr_timesheet_group>("timesheet_id", ref ftimesheet_id, value); }
            }
    
        
            private res_users fmanager;
            //FK FK_project_project_manager
            [Custom("Caption", "Manager")]
            public res_users manager {
                get { return fmanager; }
                set { SetPropertyValue<res_users>("manager", ref fmanager, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_project_project_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private System.String fwarn_footer;
            [Size(-1)]
            [Custom("Caption", "Warn Footer")]
            public System.String warn_footer {
                get { return fwarn_footer; }
                set { SetPropertyValue("warn_footer", ref fwarn_footer, value); }
            }
    
            private System.Boolean fwarn_manager;
            [Custom("Caption", "Warn Manager")]
            public System.Boolean warn_manager {
                get { return fwarn_manager; }
                set { SetPropertyValue("warn_manager", ref fwarn_manager, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Boolean fwarn_customer;
            [Custom("Caption", "Warn Customer")]
            public System.Boolean warn_customer {
                get { return fwarn_customer; }
                set { SetPropertyValue("warn_customer", ref fwarn_customer, value); }
            }
    
            private System.String fwarn_header;
            [Size(-1)]
            [Custom("Caption", "Warn Header")]
            public System.String warn_header {
                get { return fwarn_header; }
                set { SetPropertyValue("warn_header", ref fwarn_header, value); }
            }
    
            private System.Int32 fpriority;
            [Custom("Caption", "Priority")]
            public System.Int32 priority {
                get { return fpriority; }
                set { SetPropertyValue("priority", ref fpriority, value); }
            }
    
        
            private project_project fparent_id;
            //FK FK_project_project_parent_id
            [Custom("Caption", "Parent Id")]
            public project_project parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<project_project>("parent_id", ref fparent_id, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
        
            private account_analytic_account fcategory_id;
            //FK FK_project_project_category_id
            [Custom("Caption", "Category Id")]
            public account_analytic_account category_id {
                get { return fcategory_id; }
                set { SetPropertyValue<account_analytic_account>("category_id", ref fcategory_id, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public project_project(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

