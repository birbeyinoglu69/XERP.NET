
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
	[DefaultProperty("condition")]
    [Persistent("wkf_transition")]
	public partial class wkf_transition : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //wkf_transition_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private wkf_activity fact_from;
            //FK FK_wkf_transition_act_from
            [Custom("Caption", "Act From")]
            public wkf_activity act_from {
                get { return fact_from; }
                set { SetPropertyValue<wkf_activity>("act_from", ref fact_from, value); }
            }
    
        
            private wkf_activity fact_to;
            //FK FK_wkf_transition_act_to
            [Custom("Caption", "Act To")]
            public wkf_activity act_to {
                get { return fact_to; }
                set { SetPropertyValue<wkf_activity>("act_to", ref fact_to, value); }
            }
    
            private System.String fcondition;
            [Size(128)]
            [Custom("Caption", "Condition")]
            public System.String condition {
                get { return fcondition; }
                set { SetPropertyValue("condition", ref fcondition, value); }
            }
    
            private System.String ftrigger_type;
            [Size(128)]
            [Custom("Caption", "Trigger Type")]
            public System.String trigger_type {
                get { return ftrigger_type; }
                set { SetPropertyValue("trigger_type", ref ftrigger_type, value); }
            }
    
            private System.String ftrigger_expr_id;
            [Size(128)]
            [Custom("Caption", "Trigger Expr id")]
            public System.String trigger_expr_id {
                get { return ftrigger_expr_id; }
                set { SetPropertyValue("trigger_expr_id", ref ftrigger_expr_id, value); }
            }
    
            private System.String fsignal;
            [Size(64)]
            [Custom("Caption", "Signal")]
            public System.String signal {
                get { return fsignal; }
                set { SetPropertyValue("signal", ref fsignal, value); }
            }
    
        
            private res_roles frole_id;
            //FK FK_wkf_transition_role_id
            [Custom("Caption", "Role Id")]
            public res_roles role_id {
                get { return frole_id; }
                set { SetPropertyValue<res_roles>("role_id", ref frole_id, value); }
            }
    
        
            private res_users fcreate_uid;
            //FK FK_wkf_transition_create_uid
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
            //FK FK_wkf_transition_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String ftrigger_model;
            [Size(128)]
            [Custom("Caption", "Trigger Model")]
            public System.String trigger_model {
                get { return ftrigger_model; }
                set { SetPropertyValue("trigger_model", ref ftrigger_model, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public wkf_transition(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

