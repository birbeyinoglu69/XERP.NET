
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
	[DefaultProperty("notes")]
    [Persistent("hr_holidays_per_user")]
	public partial class hr_holidays_per_user : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //hr_holidays_per_user_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_hr_holidays_per_user_create_uid
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
            //FK FK_hr_holidays_per_user_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private hr_employee femployee_id;
            //FK FK_hr_holidays_per_user_employee_id
            [Custom("Caption", "Employee Id")]
            public hr_employee employee_id {
                get { return femployee_id; }
                set { SetPropertyValue<hr_employee>("employee_id", ref femployee_id, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_hr_holidays_per_user_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private System.Double fmax_leaves;
            [Custom("Caption", "Max Leaves")]
            public System.Double max_leaves {
                get { return fmax_leaves; }
                set { SetPropertyValue("max_leaves", ref fmax_leaves, value); }
            }
    
            private System.Double fleaves_taken;
            [Custom("Caption", "Leaves Taken")]
            public System.Double leaves_taken {
                get { return fleaves_taken; }
                set { SetPropertyValue("leaves_taken", ref fleaves_taken, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
        
            private hr_holidays_status fholiday_status;
            //FK FK_hr_holidays_per_user_holiday_status
            [Custom("Caption", "Holiday Status")]
            public hr_holidays_status holiday_status {
                get { return fholiday_status; }
                set { SetPropertyValue<hr_holidays_status>("holiday_status", ref fholiday_status, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public hr_holidays_per_user(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

