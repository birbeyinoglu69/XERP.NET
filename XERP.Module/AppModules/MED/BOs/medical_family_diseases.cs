
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
	[DefaultProperty("relative")]
    [Persistent("medical_family_diseases")]
	public partial class medical_family_diseases : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_family_diseases_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_family_diseases_create_uid
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
            //FK FK_medical_family_diseases_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String frelative;
            [Size(16)]
            [Custom("Caption", "Relative")]
            public System.String relative {
                get { return frelative; }
                set { SetPropertyValue("relative", ref frelative, value); }
            }
    
        
            private medical_pathology fname;
            //FK FK_medical_family_diseases_name
            [Custom("Caption", "Name")]
            public medical_pathology name {
                get { return fname; }
                set { SetPropertyValue<medical_pathology>("name", ref fname, value); }
            }
    
            private System.String fx_or_y;
            [Size(16)]
            [Custom("Caption", "X_or_y")]
            public System.String x_or_y {
                get { return fx_or_y; }
                set { SetPropertyValue("x_or_y", ref fx_or_y, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_family_diseases(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

