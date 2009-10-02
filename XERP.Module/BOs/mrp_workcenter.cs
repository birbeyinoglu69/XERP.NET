
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
	[DefaultProperty("code")]
    [Persistent("mrp_workcenter")]
	public partial class mrp_workcenter : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //mrp_workcenter_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_mrp_workcenter_create_uid
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
            //FK FK_mrp_workcenter_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fcode;
            [Size(16)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
            private System.Double ftime_stop;
            [Custom("Caption", "Time Stop")]
            public System.Double time_stop {
                get { return ftime_stop; }
                set { SetPropertyValue("time_stop", ref ftime_stop, value); }
            }
    
        
            private account_analytic_journal fcosts_journal_id;
            //FK FK_mrp_workcenter_costs_journal_id
            [Custom("Caption", "Costs Journal id")]
            public account_analytic_journal costs_journal_id {
                get { return fcosts_journal_id; }
                set { SetPropertyValue<account_analytic_journal>("costs_journal_id", ref fcosts_journal_id, value); }
            }
    
            private System.Double fcosts_hour;
            [Custom("Caption", "Costs Hour")]
            public System.Double costs_hour {
                get { return fcosts_hour; }
                set { SetPropertyValue("costs_hour", ref fcosts_hour, value); }
            }
    
            private System.Double fcosts_cycle;
            [Custom("Caption", "Costs Cycle")]
            public System.Double costs_cycle {
                get { return fcosts_cycle; }
                set { SetPropertyValue("costs_cycle", ref fcosts_cycle, value); }
            }
    
        
            private hr_timesheet_group ftimesheet_id;
            //FK FK_mrp_workcenter_timesheet_id
            [Custom("Caption", "Timesheet Id")]
            public hr_timesheet_group timesheet_id {
                get { return ftimesheet_id; }
                set { SetPropertyValue<hr_timesheet_group>("timesheet_id", ref ftimesheet_id, value); }
            }
    
        
            private account_analytic_account fcosts_cycle_account_id;
            //FK FK_mrp_workcenter_costs_cycle_account_id
            [Custom("Caption", "Costs Cycle account id")]
            public account_analytic_account costs_cycle_account_id {
                get { return fcosts_cycle_account_id; }
                set { SetPropertyValue<account_analytic_account>("costs_cycle_account_id", ref fcosts_cycle_account_id, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.Double ftime_cycle;
            [Custom("Caption", "Time Cycle")]
            public System.Double time_cycle {
                get { return ftime_cycle; }
                set { SetPropertyValue("time_cycle", ref ftime_cycle, value); }
            }
    
            private System.Double fcapacity_per_cycle;
            [Custom("Caption", "Capacity Per cycle")]
            public System.Double capacity_per_cycle {
                get { return fcapacity_per_cycle; }
                set { SetPropertyValue("capacity_per_cycle", ref fcapacity_per_cycle, value); }
            }
    
            private System.Double ftime_efficiency;
            [Custom("Caption", "Time Efficiency")]
            public System.Double time_efficiency {
                get { return ftime_efficiency; }
                set { SetPropertyValue("time_efficiency", ref ftime_efficiency, value); }
            }
    
            private System.Double ftime_start;
            [Custom("Caption", "Time Start")]
            public System.Double time_start {
                get { return ftime_start; }
                set { SetPropertyValue("time_start", ref ftime_start, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
        
            private account_analytic_account fcosts_hour_account_id;
            //FK FK_mrp_workcenter_costs_hour_account_id
            [Custom("Caption", "Costs Hour account id")]
            public account_analytic_account costs_hour_account_id {
                get { return fcosts_hour_account_id; }
                set { SetPropertyValue<account_analytic_account>("costs_hour_account_id", ref fcosts_hour_account_id, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
        
            private account_account fcosts_general_account_id;
            //FK FK_mrp_workcenter_costs_general_account_id
            [Custom("Caption", "Costs General account id")]
            public account_account costs_general_account_id {
                get { return fcosts_general_account_id; }
                set { SetPropertyValue<account_account>("costs_general_account_id", ref fcosts_general_account_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public mrp_workcenter(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

