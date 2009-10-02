﻿
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
    [Persistent("medical_appointment")]
	public partial class medical_appointment : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_appointment_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_appointment_create_uid
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
            //FK FK_medical_appointment_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_partner fpatient;
            //FK FK_medical_appointment_patient
            [Custom("Caption", "Patient")]
            public res_partner patient {
                get { return fpatient; }
                set { SetPropertyValue<res_partner>("patient", ref fpatient, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private DateTime? fappointment_date;
            [Custom("Caption", "Appointment Date")]
            public DateTime? appointment_date {
                get { return fappointment_date; }
                set { SetPropertyValue("appointment_date", ref fappointment_date, value); }
            }
    
        
            private res_partner fdoctor;
            //FK FK_medical_appointment_doctor
            [Custom("Caption", "Doctor")]
            public res_partner doctor {
                get { return fdoctor; }
                set { SetPropertyValue<res_partner>("doctor", ref fdoctor, value); }
            }
    
        
            private medical_specialty fspecialty;
            //FK FK_medical_appointment_specialty
            [Custom("Caption", "Specialty")]
            public medical_specialty specialty {
                get { return fspecialty; }
                set { SetPropertyValue<medical_specialty>("specialty", ref fspecialty, value); }
            }
    
            private System.String fcomments;
            [Size(-1)]
            [Custom("Caption", "Comments")]
            public System.String comments {
                get { return fcomments; }
                set { SetPropertyValue("comments", ref fcomments, value); }
            }
    
        
            private res_partner finstitution;
            //FK FK_medical_appointment_institution
            [Custom("Caption", "Institution")]
            public res_partner institution {
                get { return finstitution; }
                set { SetPropertyValue<res_partner>("institution", ref finstitution, value); }
            }
    
            private System.String furgency;
            [Size(16)]
            [Custom("Caption", "Urgency")]
            public System.String urgency {
                get { return furgency; }
                set { SetPropertyValue("urgency", ref furgency, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_appointment(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

