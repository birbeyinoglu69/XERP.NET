
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
	[DefaultProperty("name")]
    [Persistent("mrp_production_workcenter_line")]
	public partial class mrp_production_workcenter_line : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //mrp_production_workcenter_line_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_mrp_production_workcenter_line_create_uid
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
            //FK FK_mrp_production_workcenter_line_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private mrp_workcenter fworkcenter_id;
            //FK FK_mrp_production_workcenter_line_workcenter_id
            [Custom("Caption", "Workcenter Id")]
            public mrp_workcenter workcenter_id {
                get { return fworkcenter_id; }
                set { SetPropertyValue<mrp_workcenter>("workcenter_id", ref fworkcenter_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Decimal fhour;
            [Custom("Caption", "Hour")]
            public System.Decimal hour {
                get { return fhour; }
                set { SetPropertyValue("hour", ref fhour, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
        
            private mrp_production fproduction_id;
            //FK FK_mrp_production_workcenter_line_production_id
            [Custom("Caption", "Production Id")]
            public mrp_production production_id {
                get { return fproduction_id; }
                set { SetPropertyValue<mrp_production>("production_id", ref fproduction_id, value); }
            }
    
            private System.Decimal fcycle;
            [Custom("Caption", "Cycle")]
            public System.Decimal cycle {
                get { return fcycle; }
                set { SetPropertyValue("cycle", ref fcycle, value); }
            }
    
            private DateTime? fdate_finnished;
            [Custom("Caption", "Date Finnished")]
            public DateTime? date_finnished {
                get { return fdate_finnished; }
                set { SetPropertyValue("date_finnished", ref fdate_finnished, value); }
            }
    
            private DateTime? fdate_planned;
            [Custom("Caption", "Date Planned")]
            public DateTime? date_planned {
                get { return fdate_planned; }
                set { SetPropertyValue("date_planned", ref fdate_planned, value); }
            }
    
            private DateTime? fdate_start;
            [Custom("Caption", "Date Start")]
            public DateTime? date_start {
                get { return fdate_start; }
                set { SetPropertyValue("date_start", ref fdate_start, value); }
            }
    
            private System.Double fdelay;
            [Custom("Caption", "Delay")]
            public System.Double delay {
                get { return fdelay; }
                set { SetPropertyValue("delay", ref fdelay, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public mrp_production_workcenter_line(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

