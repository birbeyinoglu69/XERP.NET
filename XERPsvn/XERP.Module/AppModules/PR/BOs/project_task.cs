
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
	[DefaultProperty("description")]
    [Persistent("project_task")]
	public partial class project_task : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //project_task_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_project_task_create_uid
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
            //FK FK_project_task_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Decimal fremaining_hours;
            [Custom("Caption", "Remaining Hours")]
            public System.Decimal remaining_hours {
                get { return fremaining_hours; }
                set { SetPropertyValue("remaining_hours", ref fremaining_hours, value); }
            }
    
            private System.String fdescription;
            [Size(-1)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
            private System.Double feffective_hours;
            [Custom("Caption", "Effective Hours")]
            public System.Double effective_hours {
                get { return feffective_hours; }
                set { SetPropertyValue("effective_hours", ref feffective_hours, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.Double fplanned_hours;
            [Custom("Caption", "Planned Hours")]
            public System.Double planned_hours {
                get { return fplanned_hours; }
                set { SetPropertyValue("planned_hours", ref fplanned_hours, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_project_task_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private System.Double fdelay_hours;
            [Custom("Caption", "Delay Hours")]
            public System.Double delay_hours {
                get { return fdelay_hours; }
                set { SetPropertyValue("delay_hours", ref fdelay_hours, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_project_task_user_id
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
    
            private DateTime? fdate_deadline;
            [Custom("Caption", "Date Deadline")]
            public DateTime? date_deadline {
                get { return fdate_deadline; }
                set { SetPropertyValue("date_deadline", ref fdate_deadline, value); }
            }
    
            private System.Double ftotal_hours;
            [Custom("Caption", "Total Hours")]
            public System.Double total_hours {
                get { return ftotal_hours; }
                set { SetPropertyValue("total_hours", ref ftotal_hours, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private DateTime? fdate_start;
            [Custom("Caption", "Date Start")]
            public DateTime? date_start {
                get { return fdate_start; }
                set { SetPropertyValue("date_start", ref fdate_start, value); }
            }
    
            private DateTime? fdate_close;
            [Custom("Caption", "Date Close")]
            public DateTime? date_close {
                get { return fdate_close; }
                set { SetPropertyValue("date_close", ref fdate_close, value); }
            }
    
            private System.String fpriority;
            [Size(16)]
            [Custom("Caption", "Priority")]
            public System.String priority {
                get { return fpriority; }
                set { SetPropertyValue("priority", ref fpriority, value); }
            }
    
        
            private project_task fparent_id;
            //FK FK_project_task_parent_id
            [Custom("Caption", "Parent Id")]
            public project_task parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<project_task>("parent_id", ref fparent_id, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.Double fprogress;
            [Custom("Caption", "Progress")]
            public System.Double progress {
                get { return fprogress; }
                set { SetPropertyValue("progress", ref fprogress, value); }
            }
    
        
            private project_project fproject_id;
            //FK FK_project_task_project_id
            [Custom("Caption", "Project Id")]
            public project_project project_id {
                get { return fproject_id; }
                set { SetPropertyValue<project_project>("project_id", ref fproject_id, value); }
            }
    
        
            private project_task_type ftype;
            //FK FK_project_task_type
            [Custom("Caption", "Type")]
            public project_task_type type {
                get { return ftype; }
                set { SetPropertyValue<project_task_type>("type", ref ftype, value); }
            }
    
        
            private project_gtd_context fcontext_id;
            //FK FK_project_task_context_id
            [Custom("Caption", "Context Id")]
            public project_gtd_context context_id {
                get { return fcontext_id; }
                set { SetPropertyValue<project_gtd_context>("context_id", ref fcontext_id, value); }
            }
    
        
            private project_gtd_timebox ftimebox_id;
            //FK FK_project_task_timebox_id
            [Custom("Caption", "Timebox Id")]
            public project_gtd_timebox timebox_id {
                get { return ftimebox_id; }
                set { SetPropertyValue<project_gtd_timebox>("timebox_id", ref ftimebox_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public project_task(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

