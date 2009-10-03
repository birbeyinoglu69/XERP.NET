
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
    [Persistent("ir_model_data")]
	public partial class ir_model_data : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_model_data_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
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
    
            private System.Boolean fnoupdate;
            [Custom("Caption", "Noupdate")]
            public System.Boolean noupdate {
                get { return fnoupdate; }
                set { SetPropertyValue("noupdate", ref fnoupdate, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private DateTime? fdate_init;
            [Custom("Caption", "Date Init")]
            public DateTime? date_init {
                get { return fdate_init; }
                set { SetPropertyValue("date_init", ref fdate_init, value); }
            }
    
            private DateTime? fdate_update;
            [Custom("Caption", "Date Update")]
            public DateTime? date_update {
                get { return fdate_update; }
                set { SetPropertyValue("date_update", ref fdate_update, value); }
            }
    
            private System.String fmodule;
            [Size(64)]
            [Custom("Caption", "Module")]
            public System.String module {
                get { return fmodule; }
                set { SetPropertyValue("module", ref fmodule, value); }
            }
    
            private System.String fmodel;
            [Size(64)]
            [Custom("Caption", "Model")]
            public System.String model {
                get { return fmodel; }
                set { SetPropertyValue("model", ref fmodel, value); }
            }
    
            private System.Int32 fres_id;
            [Custom("Caption", "Res Id")]
            public System.Int32 res_id {
                get { return fres_id; }
                set { SetPropertyValue("res_id", ref fres_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_model_data(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

