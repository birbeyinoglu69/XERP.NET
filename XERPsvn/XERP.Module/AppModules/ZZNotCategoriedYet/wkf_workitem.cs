
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
	[DefaultProperty("state1")]
    [Persistent("wkf_workitem")]
	public partial class wkf_workitem : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //wkf_workitem_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private wkf_activity fact_id;
            //FK FK_wkf_workitem_act_id
            [Custom("Caption", "Act Id")]
            public wkf_activity act_id {
                get { return fact_id; }
                set { SetPropertyValue<wkf_activity>("act_id", ref fact_id, value); }
            }
    
        
            private wkf_instance finst_id;
            //FK FK_wkf_workitem_inst_id
            [Custom("Caption", "Inst Id")]
            public wkf_instance inst_id {
                get { return finst_id; }
                set { SetPropertyValue<wkf_instance>("inst_id", ref finst_id, value); }
            }
    
        
            private wkf_instance fsubflow_id;
            //FK FK_wkf_workitem_subflow_id
            [Custom("Caption", "Subflow Id")]
            public wkf_instance subflow_id {
                get { return fsubflow_id; }
                set { SetPropertyValue<wkf_instance>("subflow_id", ref fsubflow_id, value); }
            }
    
            private System.String fstate1;
            [Size(64)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public wkf_workitem(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

