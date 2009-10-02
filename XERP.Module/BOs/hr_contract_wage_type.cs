
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
	[DefaultProperty("type")]
    [Persistent("hr_contract_wage_type")]
	public partial class hr_contract_wage_type : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //hr_contract_wage_type_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_hr_contract_wage_type_create_uid
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
            //FK FK_hr_contract_wage_type_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private hr_contract_wage_type_period fperiod_id;
            //FK FK_hr_contract_wage_type_period_id
            [Custom("Caption", "Period Id")]
            public hr_contract_wage_type_period period_id {
                get { return fperiod_id; }
                set { SetPropertyValue<hr_contract_wage_type_period>("period_id", ref fperiod_id, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.String fname;
            [Size(50)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Decimal ffactor_type;
            [Custom("Caption", "Factor Type")]
            public System.Decimal factor_type {
                get { return ffactor_type; }
                set { SetPropertyValue("factor_type", ref ffactor_type, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public hr_contract_wage_type(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

