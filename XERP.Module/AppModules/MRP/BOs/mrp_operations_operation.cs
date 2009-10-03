
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
    [Persistent("mrp_operations_operation")]
	public partial class mrp_operations_operation : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //mrp_operations_operation_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_mrp_operations_operation_create_uid
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
            //FK FK_mrp_operations_operation_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private mrp_production fproduction_id;
            //FK FK_mrp_operations_operation_production_id
            [Custom("Caption", "Production Id")]
            public mrp_production production_id {
                get { return fproduction_id; }
                set { SetPropertyValue<mrp_production>("production_id", ref fproduction_id, value); }
            }
    
        
            private mrp_operations_operation_code fcode_id;
            //FK FK_mrp_operations_operation_code_id
            [Custom("Caption", "Code Id")]
            public mrp_operations_operation_code code_id {
                get { return fcode_id; }
                set { SetPropertyValue<mrp_operations_operation_code>("code_id", ref fcode_id, value); }
            }
    
        
            private mrp_workcenter fworkcenter_id;
            //FK FK_mrp_operations_operation_workcenter_id
            [Custom("Caption", "Workcenter Id")]
            public mrp_workcenter workcenter_id {
                get { return fworkcenter_id; }
                set { SetPropertyValue<mrp_workcenter>("workcenter_id", ref fworkcenter_id, value); }
            }
    
            private DateTime? fdate_finished;
            [Custom("Caption", "Date Finished")]
            public DateTime? date_finished {
                get { return fdate_finished; }
                set { SetPropertyValue("date_finished", ref fdate_finished, value); }
            }
    
            private DateTime? fdate_start;
            [Custom("Caption", "Date Start")]
            public DateTime? date_start {
                get { return fdate_start; }
                set { SetPropertyValue("date_start", ref fdate_start, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public mrp_operations_operation(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

