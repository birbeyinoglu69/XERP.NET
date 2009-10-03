
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
    [Persistent("ir_act_window")]
	public partial class ir_act_window : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_act_window_id
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
    
            private System.Int32 fview_id;
            [Custom("Caption", "View Id")]
            public System.Int32 view_id {
                get { return fview_id; }
                set { SetPropertyValue("view_id", ref fview_id, value); }
            }
    
            private System.String fres_model;
            [Size(64)]
            [Custom("Caption", "Res Model")]
            public System.String res_model {
                get { return fres_model; }
                set { SetPropertyValue("res_model", ref fres_model, value); }
            }
    
            private System.String fview_type;
            [Size(16)]
            [Custom("Caption", "View Type")]
            public System.String view_type {
                get { return fview_type; }
                set { SetPropertyValue("view_type", ref fview_type, value); }
            }
    
            private System.String fdomain;
            [Size(250)]
            [Custom("Caption", "Domain")]
            public System.String domain {
                get { return fdomain; }
                set { SetPropertyValue("domain", ref fdomain, value); }
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
    
            private System.Int32 fauto_refresh;
            [Custom("Caption", "Auto Refresh")]
            public System.Int32 auto_refresh {
                get { return fauto_refresh; }
                set { SetPropertyValue("auto_refresh", ref fauto_refresh, value); }
            }
    
            private System.String fview_mode;
            [Size(250)]
            [Custom("Caption", "View Mode")]
            public System.String view_mode {
                get { return fview_mode; }
                set { SetPropertyValue("view_mode", ref fview_mode, value); }
            }
    
            private System.String ftarget;
            [Size(16)]
            [Custom("Caption", "Target")]
            public System.String target {
                get { return ftarget; }
                set { SetPropertyValue("target", ref ftarget, value); }
            }
    
            private System.String fsrc_model;
            [Size(64)]
            [Custom("Caption", "Src Model")]
            public System.String src_model {
                get { return fsrc_model; }
                set { SetPropertyValue("src_model", ref fsrc_model, value); }
            }
    
            private System.Int32 flimit;
            [Custom("Caption", "Limit")]
            public System.Int32 limit {
                get { return flimit; }
                set { SetPropertyValue("limit", ref flimit, value); }
            }
    
            private System.String fcontext;
            [Size(250)]
            [Custom("Caption", "Context")]
            public System.String context {
                get { return fcontext; }
                set { SetPropertyValue("context", ref fcontext, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_act_window(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

