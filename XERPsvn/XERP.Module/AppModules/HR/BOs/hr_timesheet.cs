
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
	[DefaultProperty("dayofweek")]
    [Persistent("hr_timesheet")]
	public partial class hr_timesheet : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //hr_timesheet_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_hr_timesheet_create_uid
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
            //FK FK_hr_timesheet_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fdayofweek;
            [Size(16)]
            [Custom("Caption", "Dayofweek")]
            public System.String dayofweek {
                get { return fdayofweek; }
                set { SetPropertyValue("dayofweek", ref fdayofweek, value); }
            }
    
            private System.Double fhour_from;
            [Custom("Caption", "Hour From")]
            public System.Double hour_from {
                get { return fhour_from; }
                set { SetPropertyValue("hour_from", ref fhour_from, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private hr_timesheet_group ftgroup_id;
            //FK FK_hr_timesheet_tgroup_id
            [Custom("Caption", "Tgroup Id")]
            public hr_timesheet_group tgroup_id {
                get { return ftgroup_id; }
                set { SetPropertyValue<hr_timesheet_group>("tgroup_id", ref ftgroup_id, value); }
            }
    
            private System.Double fhour_to;
            [Custom("Caption", "Hour To")]
            public System.Double hour_to {
                get { return fhour_to; }
                set { SetPropertyValue("hour_to", ref fhour_to, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public hr_timesheet(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

