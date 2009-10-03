
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
	[DefaultProperty("info")]
    [Persistent("medical_drugs_recreational")]
	public partial class medical_drugs_recreational : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_drugs_recreational_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_drugs_recreational_create_uid
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
            //FK FK_medical_drugs_recreational_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String finfo;
            [Size(-1)]
            [Custom("Caption", "Info")]
            public System.String info {
                get { return finfo; }
                set { SetPropertyValue("info", ref finfo, value); }
            }
    
            private System.String flegal_status;
            [Size(16)]
            [Custom("Caption", "Legal Status")]
            public System.String legal_status {
                get { return flegal_status; }
                set { SetPropertyValue("legal_status", ref flegal_status, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fstreet_name;
            [Size(256)]
            [Custom("Caption", "Street Name")]
            public System.String street_name {
                get { return fstreet_name; }
                set { SetPropertyValue("street_name", ref fstreet_name, value); }
            }
    
            private System.String ftoxicity;
            [Size(16)]
            [Custom("Caption", "Toxicity")]
            public System.String toxicity {
                get { return ftoxicity; }
                set { SetPropertyValue("toxicity", ref ftoxicity, value); }
            }
    
            private System.String faddiction_level;
            [Size(16)]
            [Custom("Caption", "Addiction Level")]
            public System.String addiction_level {
                get { return faddiction_level; }
                set { SetPropertyValue("addiction_level", ref faddiction_level, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_drugs_recreational(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

