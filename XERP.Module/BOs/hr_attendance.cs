
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
	[DefaultProperty("action")]
    [Persistent("hr_attendance")]
	public partial class hr_attendance : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //hr_attendance_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_hr_attendance_create_uid
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
            //FK FK_hr_attendance_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String faction;
            [Size(16)]
            [Custom("Caption", "Action")]
            public System.String action {
                get { return faction; }
                set { SetPropertyValue("action", ref faction, value); }
            }
    
        
            private hr_employee femployee_id;
            //FK FK_hr_attendance_employee_id
            [Custom("Caption", "Employee Id")]
            public hr_employee employee_id {
                get { return femployee_id; }
                set { SetPropertyValue<hr_employee>("employee_id", ref femployee_id, value); }
            }
    
            private DateTime? fname;
            [Custom("Caption", "Name")]
            public DateTime? name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private hr_action_reason faction_desc;
            //FK FK_hr_attendance_action_desc
            [Custom("Caption", "Action Desc")]
            public hr_action_reason action_desc {
                get { return faction_desc; }
                set { SetPropertyValue<hr_action_reason>("action_desc", ref faction_desc, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public hr_attendance(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

