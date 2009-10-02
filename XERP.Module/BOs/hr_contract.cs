
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
    [Persistent("hr_contract")]
	public partial class hr_contract : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //hr_contract_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_hr_contract_create_uid
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
            //FK FK_hr_contract_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_partner_function ffunction;
            //FK FK_hr_contract_function
            [Custom("Caption", "Function")]
            public res_partner_function function {
                get { return ffunction; }
                set { SetPropertyValue<res_partner_function>("function", ref ffunction, value); }
            }
    
        
            private hr_contract_wage_type fwage_type_id;
            //FK FK_hr_contract_wage_type_id
            [Custom("Caption", "Wage Type id")]
            public hr_contract_wage_type wage_type_id {
                get { return fwage_type_id; }
                set { SetPropertyValue<hr_contract_wage_type>("wage_type_id", ref fwage_type_id, value); }
            }
    
        
            private hr_employee femployee_id;
            //FK FK_hr_contract_employee_id
            [Custom("Caption", "Employee Id")]
            public hr_employee employee_id {
                get { return femployee_id; }
                set { SetPropertyValue<hr_employee>("employee_id", ref femployee_id, value); }
            }
    
            private System.String fname;
            [Size(30)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Double fwage;
            [Custom("Caption", "Wage")]
            public System.Double wage {
                get { return fwage; }
                set { SetPropertyValue("wage", ref fwage, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private System.Int32 fworking_hours_per_day;
            [Custom("Caption", "Working Hours per day")]
            public System.Int32 working_hours_per_day {
                get { return fworking_hours_per_day; }
                set { SetPropertyValue("working_hours_per_day", ref fworking_hours_per_day, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public hr_contract(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

