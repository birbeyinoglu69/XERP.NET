
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
    [Persistent("project_gtd_timebox")]
	public partial class project_gtd_timebox : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //project_gtd_timebox_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_project_gtd_timebox_create_uid
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
            //FK FK_project_gtd_timebox_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Boolean fcol_priority;
            [Custom("Caption", "Col Priority")]
            public System.Boolean col_priority {
                get { return fcol_priority; }
                set { SetPropertyValue("col_priority", ref fcol_priority, value); }
            }
    
        
            private project_gtd_context fcontext6_id;
            //FK FK_project_gtd_timebox_context6_id
            [Custom("Caption", "Context6 Id")]
            public project_gtd_context context6_id {
                get { return fcontext6_id; }
                set { SetPropertyValue<project_gtd_context>("context6_id", ref fcontext6_id, value); }
            }
    
            private System.Boolean fcol_effective_hours;
            [Custom("Caption", "Col Effective hours")]
            public System.Boolean col_effective_hours {
                get { return fcol_effective_hours; }
                set { SetPropertyValue("col_effective_hours", ref fcol_effective_hours, value); }
            }
    
        
            private project_gtd_context fcontext2_id;
            //FK FK_project_gtd_timebox_context2_id
            [Custom("Caption", "Context2 Id")]
            public project_gtd_context context2_id {
                get { return fcontext2_id; }
                set { SetPropertyValue<project_gtd_context>("context2_id", ref fcontext2_id, value); }
            }
    
        
            private project_gtd_context fcontext3_id;
            //FK FK_project_gtd_timebox_context3_id
            [Custom("Caption", "Context3 Id")]
            public project_gtd_context context3_id {
                get { return fcontext3_id; }
                set { SetPropertyValue<project_gtd_context>("context3_id", ref fcontext3_id, value); }
            }
    
            private System.Boolean fcol_project;
            [Custom("Caption", "Col Project")]
            public System.Boolean col_project {
                get { return fcol_project; }
                set { SetPropertyValue("col_project", ref fcol_project, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_project_gtd_timebox_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
        
            private project_gtd_context fcontext5_id;
            //FK FK_project_gtd_timebox_context5_id
            [Custom("Caption", "Context5 Id")]
            public project_gtd_context context5_id {
                get { return fcontext5_id; }
                set { SetPropertyValue<project_gtd_context>("context5_id", ref fcontext5_id, value); }
            }
    
        
            private project_gtd_context fcontext1_id;
            //FK FK_project_gtd_timebox_context1_id
            [Custom("Caption", "Context1 Id")]
            public project_gtd_context context1_id {
                get { return fcontext1_id; }
                set { SetPropertyValue<project_gtd_context>("context1_id", ref fcontext1_id, value); }
            }
    
        
            private project_gtd_context fcontext4_id;
            //FK FK_project_gtd_timebox_context4_id
            [Custom("Caption", "Context4 Id")]
            public project_gtd_context context4_id {
                get { return fcontext4_id; }
                set { SetPropertyValue<project_gtd_context>("context4_id", ref fcontext4_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private project_gtd_timebox fparent_id;
            //FK FK_project_gtd_timebox_parent_id
            [Custom("Caption", "Parent Id")]
            public project_gtd_timebox parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<project_gtd_timebox>("parent_id", ref fparent_id, value); }
            }
    
            private System.Boolean fcol_planned_hours;
            [Custom("Caption", "Col Planned hours")]
            public System.Boolean col_planned_hours {
                get { return fcol_planned_hours; }
                set { SetPropertyValue("col_planned_hours", ref fcol_planned_hours, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.Boolean fcol_deadline;
            [Custom("Caption", "Col Deadline")]
            public System.Boolean col_deadline {
                get { return fcol_deadline; }
                set { SetPropertyValue("col_deadline", ref fcol_deadline, value); }
            }
    
            private System.Boolean fcol_date_start;
            [Custom("Caption", "Col Date start")]
            public System.Boolean col_date_start {
                get { return fcol_date_start; }
                set { SetPropertyValue("col_date_start", ref fcol_date_start, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public project_gtd_timebox(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

