
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
	[DefaultProperty("model")]
    [Persistent("wkf_triggers")]
	public partial class wkf_triggers : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //wkf_triggers_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private wkf_instance finstance_id;
            //FK FK_wkf_triggers_instance_id
            [Custom("Caption", "Instance Id")]
            public wkf_instance instance_id {
                get { return finstance_id; }
                set { SetPropertyValue<wkf_instance>("instance_id", ref finstance_id, value); }
            }
    
        
            private wkf_workitem fworkitem_id;
            //FK FK_wkf_triggers_workitem_id
            [Custom("Caption", "Workitem Id")]
            public wkf_workitem workitem_id {
                get { return fworkitem_id; }
                set { SetPropertyValue<wkf_workitem>("workitem_id", ref fworkitem_id, value); }
            }
    
            private System.String fmodel;
            [Size(128)]
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
		public wkf_triggers(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

