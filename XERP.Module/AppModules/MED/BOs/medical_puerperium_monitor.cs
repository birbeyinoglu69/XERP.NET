
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
    [Persistent("medical_puerperium_monitor")]
	public partial class medical_puerperium_monitor : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_puerperium_monitor_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_puerperium_monitor_create_uid
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
            //FK FK_medical_puerperium_monitor_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Int32 fdiastolic;
            [Custom("Caption", "Diastolic")]
            public System.Int32 diastolic {
                get { return fdiastolic; }
                set { SetPropertyValue("diastolic", ref fdiastolic, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String flochia_odor;
            [Size(16)]
            [Custom("Caption", "Lochia Odor")]
            public System.String lochia_odor {
                get { return flochia_odor; }
                set { SetPropertyValue("lochia_odor", ref flochia_odor, value); }
            }
    
            private System.Int32 ffrequency;
            [Custom("Caption", "Frequency")]
            public System.Int32 frequency {
                get { return ffrequency; }
                set { SetPropertyValue("frequency", ref ffrequency, value); }
            }
    
            private System.Int32 fsystolic;
            [Custom("Caption", "Systolic")]
            public System.Int32 systolic {
                get { return fsystolic; }
                set { SetPropertyValue("systolic", ref fsystolic, value); }
            }
    
            private DateTime? fdate;
            [Custom("Caption", "Date")]
            public DateTime? date {
                get { return fdate; }
                set { SetPropertyValue("date", ref fdate, value); }
            }
    
            private System.String flochia_color;
            [Size(16)]
            [Custom("Caption", "Lochia Color")]
            public System.String lochia_color {
                get { return flochia_color; }
                set { SetPropertyValue("lochia_color", ref flochia_color, value); }
            }
    
            private System.Int32 futerus_involution;
            [Custom("Caption", "Uterus Involution")]
            public System.Int32 uterus_involution {
                get { return futerus_involution; }
                set { SetPropertyValue("uterus_involution", ref futerus_involution, value); }
            }
    
            private System.String flochia_amount;
            [Size(16)]
            [Custom("Caption", "Lochia Amount")]
            public System.String lochia_amount {
                get { return flochia_amount; }
                set { SetPropertyValue("lochia_amount", ref flochia_amount, value); }
            }
    
            private System.Double ftemperature;
            [Custom("Caption", "Temperature")]
            public System.Double temperature {
                get { return ftemperature; }
                set { SetPropertyValue("temperature", ref ftemperature, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_puerperium_monitor(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

