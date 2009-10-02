
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
    [Persistent("ir_act_url")]
	public partial class ir_act_url : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_act_url_id
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
    
            private System.String furl;
            [Size(-1)]
            [Custom("Caption", "Url")]
            public System.String url {
                get { return furl; }
                set { SetPropertyValue("url", ref furl, value); }
            }
    
            private System.String ftarget;
            [Size(64)]
            [Custom("Caption", "Target")]
            public System.String target {
                get { return ftarget; }
                set { SetPropertyValue("target", ref ftarget, value); }
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
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_act_url(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

