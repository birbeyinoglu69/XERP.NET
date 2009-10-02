
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
	[DefaultProperty("graph_mode")]
    [Persistent("base_report_creator_report_fields")]
	public partial class base_report_creator_report_fields : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //base_report_creator_report_fields_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_base_report_creator_report_fields_create_uid
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
            //FK FK_base_report_creator_report_fields_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fgraph_mode;
            [Size(16)]
            [Custom("Caption", "Graph Mode")]
            public System.String graph_mode {
                get { return fgraph_mode; }
                set { SetPropertyValue("graph_mode", ref fgraph_mode, value); }
            }
    
            private System.String fcalendar_mode;
            [Size(16)]
            [Custom("Caption", "Calendar Mode")]
            public System.String calendar_mode {
                get { return fcalendar_mode; }
                set { SetPropertyValue("calendar_mode", ref fcalendar_mode, value); }
            }
    
            private System.String fgroup_method;
            [Size(16)]
            [Custom("Caption", "Group Method")]
            public System.String group_method {
                get { return fgroup_method; }
                set { SetPropertyValue("group_method", ref fgroup_method, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
        
            private ir_model_fields ffield_id;
            //FK FK_base_report_creator_report_fields_field_id
            [Custom("Caption", "Field Id")]
            public ir_model_fields field_id {
                get { return ffield_id; }
                set { SetPropertyValue<ir_model_fields>("field_id", ref ffield_id, value); }
            }
    
        
            private base_report_creator_report freport_id;
            //FK FK_base_report_creator_report_fields_report_id
            [Custom("Caption", "Report Id")]
            public base_report_creator_report report_id {
                get { return freport_id; }
                set { SetPropertyValue<base_report_creator_report>("report_id", ref freport_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public base_report_creator_report_fields(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

