
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
    [Persistent("hr_holidays")]
	public partial class hr_holidays : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //hr_holidays_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_hr_holidays_create_uid
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
            //FK FK_hr_holidays_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private hr_employee femployee_id;
            //FK FK_hr_holidays_employee_id
            [Custom("Caption", "Employee Id")]
            public hr_employee employee_id {
                get { return femployee_id; }
                set { SetPropertyValue<hr_employee>("employee_id", ref femployee_id, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_hr_holidays_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private DateTime? fdate_from;
            [Custom("Caption", "Date From")]
            public DateTime? date_from {
                get { return fdate_from; }
                set { SetPropertyValue("date_from", ref fdate_from, value); }
            }
    
        
            private hr_holidays_status fholiday_status;
            //FK FK_hr_holidays_holiday_status
            [Custom("Caption", "Holiday Status")]
            public hr_holidays_status holiday_status {
                get { return fholiday_status; }
                set { SetPropertyValue<hr_holidays_status>("holiday_status", ref fholiday_status, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private crm_case fcase_id;
            //FK FK_hr_holidays_case_id
            [Custom("Caption", "Case Id")]
            public crm_case case_id {
                get { return fcase_id; }
                set { SetPropertyValue<crm_case>("case_id", ref fcase_id, value); }
            }
    
        
            private hr_employee fmanager_id;
            //FK FK_hr_holidays_manager_id
            [Custom("Caption", "Manager Id")]
            public hr_employee manager_id {
                get { return fmanager_id; }
                set { SetPropertyValue<hr_employee>("manager_id", ref fmanager_id, value); }
            }
    
            private DateTime? fdate_to;
            [Custom("Caption", "Date To")]
            public DateTime? date_to {
                get { return fdate_to; }
                set { SetPropertyValue("date_to", ref fdate_to, value); }
            }
    
            private System.Double fnumber_of_days;
            [Custom("Caption", "Number Of days")]
            public System.Double number_of_days {
                get { return fnumber_of_days; }
                set { SetPropertyValue("number_of_days", ref fnumber_of_days, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
        
            private hr_holidays_per_user fholiday_user_id;
            //FK FK_hr_holidays_holiday_user_id
            [Custom("Caption", "Holiday User id")]
            public hr_holidays_per_user holiday_user_id {
                get { return fholiday_user_id; }
                set { SetPropertyValue<hr_holidays_per_user>("holiday_user_id", ref fholiday_user_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public hr_holidays(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

