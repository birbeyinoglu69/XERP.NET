
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
	[DefaultProperty("split_mode")]
    [Persistent("wkf_activity")]
	public partial class wkf_activity : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //wkf_activity_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private wkf fwkf_id;
            //FK FK_wkf_activity_wkf_id
            [Custom("Caption", "Wkf Id")]
            public wkf wkf_id {
                get { return fwkf_id; }
                set { SetPropertyValue<wkf>("wkf_id", ref fwkf_id, value); }
            }
    
        
            private wkf fsubflow_id;
            //FK FK_wkf_activity_subflow_id
            [Custom("Caption", "Subflow Id")]
            public wkf subflow_id {
                get { return fsubflow_id; }
                set { SetPropertyValue<wkf>("subflow_id", ref fsubflow_id, value); }
            }
    
            private System.String fsplit_mode;
            [Size(3)]
            [Custom("Caption", "Split Mode")]
            public System.String split_mode {
                get { return fsplit_mode; }
                set { SetPropertyValue("split_mode", ref fsplit_mode, value); }
            }
    
            private System.String fjoin_mode;
            [Size(3)]
            [Custom("Caption", "Join Mode")]
            public System.String join_mode {
                get { return fjoin_mode; }
                set { SetPropertyValue("join_mode", ref fjoin_mode, value); }
            }
    
            private System.String fkind;
            [Size(16)]
            [Custom("Caption", "Kind")]
            public System.String kind {
                get { return fkind; }
                set { SetPropertyValue("kind", ref fkind, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fsignal_send;
            [Size(32)]
            [Custom("Caption", "Signal Send")]
            public System.String signal_send {
                get { return fsignal_send; }
                set { SetPropertyValue("signal_send", ref fsignal_send, value); }
            }
    
            private System.Boolean fflow_start;
            [Custom("Caption", "Flow Start")]
            public System.Boolean flow_start {
                get { return fflow_start; }
                set { SetPropertyValue("flow_start", ref fflow_start, value); }
            }
    
            private System.Boolean fflow_stop;
            [Custom("Caption", "Flow Stop")]
            public System.Boolean flow_stop {
                get { return fflow_stop; }
                set { SetPropertyValue("flow_stop", ref fflow_stop, value); }
            }
    
            private System.String faction;
            [Size(-1)]
            [Custom("Caption", "Action")]
            public System.String action {
                get { return faction; }
                set { SetPropertyValue("action", ref faction, value); }
            }
    
        
            private res_users fcreate_uid;
            //FK FK_wkf_activity_create_uid
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
            //FK FK_wkf_activity_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private ir_act_server faction_id;
            //FK FK_wkf_activity_action_id
            [Custom("Caption", "Action Id")]
            public ir_act_server action_id {
                get { return faction_id; }
                set { SetPropertyValue<ir_act_server>("action_id", ref faction_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public wkf_activity(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

