
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
	[DefaultProperty("classification")]
    [Persistent("medical_surgery")]
	public partial class medical_surgery : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_surgery_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_surgery_create_uid
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
            //FK FK_medical_surgery_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private medical_procedure fname;
            //FK FK_medical_surgery_name
            [Custom("Caption", "Name")]
            public medical_procedure name {
                get { return fname; }
                set { SetPropertyValue<medical_procedure>("name", ref fname, value); }
            }
    
            private System.String fclassification;
            [Size(16)]
            [Custom("Caption", "Classification")]
            public System.String classification {
                get { return fclassification; }
                set { SetPropertyValue("classification", ref fclassification, value); }
            }
    
            private System.String fage;
            [Size(3)]
            [Custom("Caption", "Age")]
            public System.String age {
                get { return fage; }
                set { SetPropertyValue("age", ref fage, value); }
            }
    
            private System.String fdescription;
            [Size(128)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
            private DateTime? fdate;
            [Custom("Caption", "Date")]
            public DateTime? date {
                get { return fdate; }
                set { SetPropertyValue("date", ref fdate, value); }
            }
    
        
            private medical_pathology fpathology;
            //FK FK_medical_surgery_pathology
            [Custom("Caption", "Pathology")]
            public medical_pathology pathology {
                get { return fpathology; }
                set { SetPropertyValue<medical_pathology>("pathology", ref fpathology, value); }
            }
    
        
            private medical_physician fsurgeon;
            //FK FK_medical_surgery_surgeon
            [Custom("Caption", "Surgeon")]
            public medical_physician surgeon {
                get { return fsurgeon; }
                set { SetPropertyValue<medical_physician>("surgeon", ref fsurgeon, value); }
            }
    
            private System.String fextra_info;
            [Size(-1)]
            [Custom("Caption", "Extra Info")]
            public System.String extra_info {
                get { return fextra_info; }
                set { SetPropertyValue("extra_info", ref fextra_info, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_surgery(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

