
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
	[DefaultProperty("res_type")]
    [Persistent("wkf_instance")]
	public partial class wkf_instance : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //wkf_instance_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private wkf fwkf_id;
            //FK FK_wkf_instance_wkf_id
            [Custom("Caption", "Wkf Id")]
            public wkf wkf_id {
                get { return fwkf_id; }
                set { SetPropertyValue<wkf>("wkf_id", ref fwkf_id, value); }
            }
    
            private System.Int32 fuid;
            [Custom("Caption", "Uid")]
            public System.Int32 uid {
                get { return fuid; }
                set { SetPropertyValue("uid", ref fuid, value); }
            }
    
            private System.Int32 fres_id;
            [Custom("Caption", "Res Id")]
            public System.Int32 res_id {
                get { return fres_id; }
                set { SetPropertyValue("res_id", ref fres_id, value); }
            }
    
            private System.String fres_type;
            [Size(64)]
            [Custom("Caption", "Res Type")]
            public System.String res_type {
                get { return fres_type; }
                set { SetPropertyValue("res_type", ref fres_type, value); }
            }
    
            private System.String fstate1;
            [Size(32)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public wkf_instance(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

