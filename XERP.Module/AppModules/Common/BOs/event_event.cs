
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
	[DefaultProperty("state1")]
    [Persistent("event_event")]
	public partial class event_event : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //event_event_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_event_event_create_uid
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
            //FK FK_event_event_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Boolean fmail_auto_confirm;
            [Custom("Caption", "Mail Auto confirm")]
            public System.Boolean mail_auto_confirm {
                get { return fmail_auto_confirm; }
                set { SetPropertyValue("mail_auto_confirm", ref fmail_auto_confirm, value); }
            }
    
            private DateTime? fdate_begin;
            [Custom("Caption", "Date Begin")]
            public DateTime? date_begin {
                get { return fdate_begin; }
                set { SetPropertyValue("date_begin", ref fdate_begin, value); }
            }
    
        
            private product_product fproduct_id;
            //FK FK_event_event_product_id
            [Custom("Caption", "Product Id")]
            public product_product product_id {
                get { return fproduct_id; }
                set { SetPropertyValue<product_product>("product_id", ref fproduct_id, value); }
            }
    
            private DateTime? fdate_end;
            [Custom("Caption", "Date End")]
            public DateTime? date_end {
                get { return fdate_end; }
                set { SetPropertyValue("date_end", ref fdate_end, value); }
            }
    
        
            private crm_case_section fsection_id;
            //FK FK_event_event_section_id
            [Custom("Caption", "Section Id")]
            public crm_case_section section_id {
                get { return fsection_id; }
                set { SetPropertyValue<crm_case_section>("section_id", ref fsection_id, value); }
            }
    
            private System.Int32 fregister_min;
            [Custom("Caption", "Register Min")]
            public System.Int32 register_min {
                get { return fregister_min; }
                set { SetPropertyValue("register_min", ref fregister_min, value); }
            }
    
            private System.Boolean fmail_auto_registr;
            [Custom("Caption", "Mail Auto registr")]
            public System.Boolean mail_auto_registr {
                get { return fmail_auto_registr; }
                set { SetPropertyValue("mail_auto_registr", ref fmail_auto_registr, value); }
            }
    
            private System.Int32 fregister_max;
            [Custom("Caption", "Register Max")]
            public System.Int32 register_max {
                get { return fregister_max; }
                set { SetPropertyValue("register_max", ref fregister_max, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String fmail_confirm;
            [Size(-1)]
            [Custom("Caption", "Mail Confirm")]
            public System.String mail_confirm {
                get { return fmail_confirm; }
                set { SetPropertyValue("mail_confirm", ref fmail_confirm, value); }
            }
    
            private System.String fmail_registr;
            [Size(-1)]
            [Custom("Caption", "Mail Registr")]
            public System.String mail_registr {
                get { return fmail_registr; }
                set { SetPropertyValue("mail_registr", ref fmail_registr, value); }
            }
    
        
            private event_type ftype;
            //FK FK_event_event_type
            [Custom("Caption", "Type")]
            public event_type type {
                get { return ftype; }
                set { SetPropertyValue<event_type>("type", ref ftype, value); }
            }
    
        
            private project_project fproject_id;
            //FK FK_event_event_project_id
            [Custom("Caption", "Project Id")]
            public project_project project_id {
                get { return fproject_id; }
                set { SetPropertyValue<project_project>("project_id", ref fproject_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public event_event(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

