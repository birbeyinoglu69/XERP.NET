
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
	[DefaultProperty("res_type")]
    [Persistent("wkf_logs")]
	public partial class wkf_logs : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //wkf_logs_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
            private System.String fres_type;
            [Size(128)]
            [Custom("Caption", "Res Type")]
            public System.String res_type {
                get { return fres_type; }
                set { SetPropertyValue("res_type", ref fres_type, value); }
            }
    
            private System.Int32 fres_id;
            [Custom("Caption", "Res Id")]
            public System.Int32 res_id {
                get { return fres_id; }
                set { SetPropertyValue("res_id", ref fres_id, value); }
            }
    
        
            private res_users fuid;
            //FK FK_wkf_logs_uid
            [Custom("Caption", "Uid")]
            public res_users uid {
                get { return fuid; }
                set { SetPropertyValue<res_users>("uid", ref fuid, value); }
            }
    
        
            private wkf_activity fact_id;
            //FK FK_wkf_logs_act_id
            [Custom("Caption", "Act Id")]
            public wkf_activity act_id {
                get { return fact_id; }
                set { SetPropertyValue<wkf_activity>("act_id", ref fact_id, value); }
            }
    
            private System.String finfo;
            [Size(128)]
            [Custom("Caption", "Info")]
            public System.String info {
                get { return finfo; }
                set { SetPropertyValue("info", ref finfo, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public wkf_logs(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

