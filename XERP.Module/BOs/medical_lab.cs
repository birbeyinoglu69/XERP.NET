
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
    [Persistent("medical_lab")]
	public partial class medical_lab : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_lab_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_lab_create_uid
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
            //FK FK_medical_lab_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private medical_patient fpatient;
            //FK FK_medical_lab_patient
            [Custom("Caption", "Patient")]
            public medical_patient patient {
                get { return fpatient; }
                set { SetPropertyValue<medical_patient>("patient", ref fpatient, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private medical_physician fpathologist;
            //FK FK_medical_lab_pathologist
            [Custom("Caption", "Pathologist")]
            public medical_physician pathologist {
                get { return fpathologist; }
                set { SetPropertyValue<medical_physician>("pathologist", ref fpathologist, value); }
            }
    
            private System.String fresults;
            [Size(-1)]
            [Custom("Caption", "Results")]
            public System.String results {
                get { return fresults; }
                set { SetPropertyValue("results", ref fresults, value); }
            }
    
        
            private medical_physician frequestor;
            //FK FK_medical_lab_requestor
            [Custom("Caption", "Requestor")]
            public medical_physician requestor {
                get { return frequestor; }
                set { SetPropertyValue<medical_physician>("requestor", ref frequestor, value); }
            }
    
            private System.String fdiagnosis;
            [Size(-1)]
            [Custom("Caption", "Diagnosis")]
            public System.String diagnosis {
                get { return fdiagnosis; }
                set { SetPropertyValue("diagnosis", ref fdiagnosis, value); }
            }
    
        
            private medical_test ftest;
            //FK FK_medical_lab_test
            [Custom("Caption", "Test")]
            public medical_test test {
                get { return ftest; }
                set { SetPropertyValue<medical_test>("test", ref ftest, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_lab(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

