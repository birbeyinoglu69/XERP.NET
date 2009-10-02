
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
    [Persistent("medical_perinatal_monitor")]
	public partial class medical_perinatal_monitor : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_perinatal_monitor_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_perinatal_monitor_create_uid
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
            //FK FK_medical_perinatal_monitor_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Int32 fcontractions;
            [Custom("Caption", "Contractions")]
            public System.Int32 contractions {
                get { return fcontractions; }
                set { SetPropertyValue("contractions", ref fcontractions, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Boolean fbleeding;
            [Custom("Caption", "Bleeding")]
            public System.Boolean bleeding {
                get { return fbleeding; }
                set { SetPropertyValue("bleeding", ref fbleeding, value); }
            }
    
            private System.Int32 fdilation;
            [Custom("Caption", "Dilation")]
            public System.Int32 dilation {
                get { return fdilation; }
                set { SetPropertyValue("dilation", ref fdilation, value); }
            }
    
            private System.Boolean fmeconium;
            [Custom("Caption", "Meconium")]
            public System.Boolean meconium {
                get { return fmeconium; }
                set { SetPropertyValue("meconium", ref fmeconium, value); }
            }
    
            private System.String ffetus_position;
            [Size(16)]
            [Custom("Caption", "Fetus Position")]
            public System.String fetus_position {
                get { return ffetus_position; }
                set { SetPropertyValue("fetus_position", ref ffetus_position, value); }
            }
    
            private System.Int32 ffrequency;
            [Custom("Caption", "Frequency")]
            public System.Int32 frequency {
                get { return ffrequency; }
                set { SetPropertyValue("frequency", ref ffrequency, value); }
            }
    
            private System.Int32 ffundal_height;
            [Custom("Caption", "Fundal Height")]
            public System.Int32 fundal_height {
                get { return ffundal_height; }
                set { SetPropertyValue("fundal_height", ref ffundal_height, value); }
            }
    
            private DateTime? fdate;
            [Custom("Caption", "Date")]
            public DateTime? date {
                get { return fdate; }
                set { SetPropertyValue("date", ref fdate, value); }
            }
    
            private System.Int32 fsystolic;
            [Custom("Caption", "Systolic")]
            public System.Int32 systolic {
                get { return fsystolic; }
                set { SetPropertyValue("systolic", ref fsystolic, value); }
            }
    
            private System.Int32 ff_frequency;
            [Custom("Caption", "F_frequency")]
            public System.Int32 f_frequency {
                get { return ff_frequency; }
                set { SetPropertyValue("f_frequency", ref ff_frequency, value); }
            }
    
            private System.Int32 fdiastolic;
            [Custom("Caption", "Diastolic")]
            public System.Int32 diastolic {
                get { return fdiastolic; }
                set { SetPropertyValue("diastolic", ref fdiastolic, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_perinatal_monitor(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

