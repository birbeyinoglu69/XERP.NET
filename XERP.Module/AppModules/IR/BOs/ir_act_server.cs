
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
    [Persistent("ir_act_server")]
	public partial class ir_act_server : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_act_server_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String ftype;
            [Size(32)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.String fusage;
            [Size(32)]
            [Custom("Caption", "Usage")]
            public System.String usage {
                get { return fusage; }
                set { SetPropertyValue("usage", ref fusage, value); }
            }
    
            private System.Int32 fcreate_uid;
            [Custom("Caption", "Create Uid")]
            public System.Int32 create_uid {
                get { return fcreate_uid; }
                set { SetPropertyValue("create_uid", ref fcreate_uid, value); }
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
    
            private System.Int32 fwrite_uid;
            [Custom("Caption", "Write Uid")]
            public System.Int32 write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue("write_uid", ref fwrite_uid, value); }
            }
    
        
            private ir_model fmodel_id;
            //FK FK_ir_act_server_model_id
            [Custom("Caption", "Model Id")]
            public ir_model model_id {
                get { return fmodel_id; }
                set { SetPropertyValue<ir_model>("model_id", ref fmodel_id, value); }
            }
    
            private System.String fcode;
            [Size(-1)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
            private System.String fwrite_id;
            [Size(256)]
            [Custom("Caption", "Write Id")]
            public System.String write_id {
                get { return fwrite_id; }
                set { SetPropertyValue("write_id", ref fwrite_id, value); }
            }
    
        
            private ir_model fsrcmodel_id;
            //FK FK_ir_act_server_srcmodel_id
            [Custom("Caption", "Srcmodel Id")]
            public ir_model srcmodel_id {
                get { return fsrcmodel_id; }
                set { SetPropertyValue<ir_model>("srcmodel_id", ref fsrcmodel_id, value); }
            }
    
            private System.String fmessage;
            [Size(-1)]
            [Custom("Caption", "Message")]
            public System.String message {
                get { return fmessage; }
                set { SetPropertyValue("message", ref fmessage, value); }
            }
    
            private System.String ftrigger_name;
            [Size(128)]
            [Custom("Caption", "Trigger Name")]
            public System.String trigger_name {
                get { return ftrigger_name; }
                set { SetPropertyValue("trigger_name", ref ftrigger_name, value); }
            }
    
            private System.String fcondition;
            [Size(256)]
            [Custom("Caption", "Condition")]
            public System.String condition {
                get { return fcondition; }
                set { SetPropertyValue("condition", ref fcondition, value); }
            }
    
            private System.String fsubject;
            [Size(1024)]
            [Custom("Caption", "Subject")]
            public System.String subject {
                get { return fsubject; }
                set { SetPropertyValue("subject", ref fsubject, value); }
            }
    
        
            private ir_act_server floop_action;
            //FK FK_ir_act_server_loop_action
            [Custom("Caption", "Loop Action")]
            public ir_act_server loop_action {
                get { return floop_action; }
                set { SetPropertyValue<ir_act_server>("loop_action", ref floop_action, value); }
            }
    
        
            private ir_model_fields ftrigger_obj_id;
            //FK FK_ir_act_server_trigger_obj_id
            [Custom("Caption", "Trigger Obj id")]
            public ir_model_fields trigger_obj_id {
                get { return ftrigger_obj_id; }
                set { SetPropertyValue<ir_model_fields>("trigger_obj_id", ref ftrigger_obj_id, value); }
            }
    
            private System.String fmobile;
            [Size(512)]
            [Custom("Caption", "Mobile")]
            public System.String mobile {
                get { return fmobile; }
                set { SetPropertyValue("mobile", ref fmobile, value); }
            }
    
            private System.String fexpression;
            [Size(512)]
            [Custom("Caption", "Expression")]
            public System.String expression {
                get { return fexpression; }
                set { SetPropertyValue("expression", ref fexpression, value); }
            }
    
            private System.String fsms;
            [Size(160)]
            [Custom("Caption", "Sms")]
            public System.String sms {
                get { return fsms; }
                set { SetPropertyValue("sms", ref fsms, value); }
            }
    
        
            private ir_model fwkf_model_id;
            //FK FK_ir_act_server_wkf_model_id
            [Custom("Caption", "Wkf Model id")]
            public ir_model wkf_model_id {
                get { return fwkf_model_id; }
                set { SetPropertyValue<ir_model>("wkf_model_id", ref fwkf_model_id, value); }
            }
    
            private System.String fstate1;
            [Size(32)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
        
            private ir_model_fields frecord_id;
            //FK FK_ir_act_server_record_id
            [Custom("Caption", "Record Id")]
            public ir_model_fields record_id {
                get { return frecord_id; }
                set { SetPropertyValue<ir_model_fields>("record_id", ref frecord_id, value); }
            }
    
            private System.String femail;
            [Size(512)]
            [Custom("Caption", "Email")]
            public System.String email {
                get { return femail; }
                set { SetPropertyValue("email", ref femail, value); }
            }
    
            private System.Int32 faction_id;
            [Custom("Caption", "Action Id")]
            public System.Int32 action_id {
                get { return faction_id; }
                set { SetPropertyValue("action_id", ref faction_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_act_server(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

