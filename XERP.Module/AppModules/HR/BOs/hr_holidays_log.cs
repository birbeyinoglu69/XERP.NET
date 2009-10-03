
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
	[DefaultProperty("holiday_req_id")]
    [Persistent("hr_holidays_log")]
	public partial class hr_holidays_log : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //hr_holidays_log_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_hr_holidays_log_create_uid
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
            //FK FK_hr_holidays_log_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fholiday_req_id;
            [Size(64)]
            [Custom("Caption", "Holiday Req id")]
            public System.String holiday_req_id {
                get { return fholiday_req_id; }
                set { SetPropertyValue("holiday_req_id", ref fholiday_req_id, value); }
            }
    
        
            private hr_employee femployee_id;
            //FK FK_hr_holidays_log_employee_id
            [Custom("Caption", "Employee Id")]
            public hr_employee employee_id {
                get { return femployee_id; }
                set { SetPropertyValue<hr_employee>("employee_id", ref femployee_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private DateTime? fdate;
            [Custom("Caption", "Date")]
            public DateTime? date {
                get { return fdate; }
                set { SetPropertyValue("date", ref fdate, value); }
            }
    
        
            private hr_holidays_status fholiday_status;
            //FK FK_hr_holidays_log_holiday_status
            [Custom("Caption", "Holiday Status")]
            public hr_holidays_status holiday_status {
                get { return fholiday_status; }
                set { SetPropertyValue<hr_holidays_status>("holiday_status", ref fholiday_status, value); }
            }
    
            private System.Double fnb_holidays;
            [Custom("Caption", "Nb Holidays")]
            public System.Double nb_holidays {
                get { return fnb_holidays; }
                set { SetPropertyValue("nb_holidays", ref fnb_holidays, value); }
            }
    
        
            private hr_holidays_per_user fholiday_user_id;
            //FK FK_hr_holidays_log_holiday_user_id
            [Custom("Caption", "Holiday User id")]
            public hr_holidays_per_user holiday_user_id {
                get { return fholiday_user_id; }
                set { SetPropertyValue<hr_holidays_per_user>("holiday_user_id", ref fholiday_user_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public hr_holidays_log(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

