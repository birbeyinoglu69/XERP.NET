
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
	[DefaultProperty("evaluation_type")]
    [Persistent("medical_patient_evaluation")]
	public partial class medical_patient_evaluation : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_patient_evaluation_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_patient_evaluation_create_uid
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
            //FK FK_medical_patient_evaluation_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Double fweight;
            [Custom("Caption", "Weight")]
            public System.Double weight {
                get { return fweight; }
                set { SetPropertyValue("weight", ref fweight, value); }
            }
    
            private System.String fevaluation_type;
            [Size(16)]
            [Custom("Caption", "Evaluation Type")]
            public System.String evaluation_type {
                get { return fevaluation_type; }
                set { SetPropertyValue("evaluation_type", ref fevaluation_type, value); }
            }
    
            private System.Int32 fldl;
            [Custom("Caption", "Ldl")]
            public System.Int32 ldl {
                get { return fldl; }
                set { SetPropertyValue("ldl", ref fldl, value); }
            }
    
            private System.String fchief_complaint;
            [Size(128)]
            [Custom("Caption", "Chief Complaint")]
            public System.String chief_complaint {
                get { return fchief_complaint; }
                set { SetPropertyValue("chief_complaint", ref fchief_complaint, value); }
            }
    
            private System.Int32 ftag;
            [Custom("Caption", "Tag")]
            public System.Int32 tag {
                get { return ftag; }
                set { SetPropertyValue("tag", ref ftag, value); }
            }
    
            private System.String fsigns;
            [Size(-1)]
            [Custom("Caption", "Signs")]
            public System.String signs {
                get { return fsigns; }
                set { SetPropertyValue("signs", ref fsigns, value); }
            }
    
        
            private medical_appointment fevaluation_date;
            //FK FK_medical_patient_evaluation_evaluation_date
            [Custom("Caption", "Evaluation Date")]
            public medical_appointment evaluation_date {
                get { return fevaluation_date; }
                set { SetPropertyValue<medical_appointment>("evaluation_date", ref fevaluation_date, value); }
            }
    
            private System.Int32 floc;
            [Custom("Caption", "Loc")]
            public System.Int32 loc {
                get { return floc; }
                set { SetPropertyValue("loc", ref floc, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_medical_patient_evaluation_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.Double ftemperature;
            [Custom("Caption", "Temperature")]
            public System.Double temperature {
                get { return ftemperature; }
                set { SetPropertyValue("temperature", ref ftemperature, value); }
            }
    
        
            private medical_appointment fnext_evaluation;
            //FK FK_medical_patient_evaluation_next_evaluation
            [Custom("Caption", "Next Evaluation")]
            public medical_appointment next_evaluation {
                get { return fnext_evaluation; }
                set { SetPropertyValue<medical_appointment>("next_evaluation", ref fnext_evaluation, value); }
            }
    
        
            private medical_patient fname;
            //FK FK_medical_patient_evaluation_name
            [Custom("Caption", "Name")]
            public medical_patient name {
                get { return fname; }
                set { SetPropertyValue<medical_patient>("name", ref fname, value); }
            }
    
            private System.String fsymptoms;
            [Size(-1)]
            [Custom("Caption", "Symptoms")]
            public System.String symptoms {
                get { return fsymptoms; }
                set { SetPropertyValue("symptoms", ref fsymptoms, value); }
            }
    
            private System.Double fglycemia;
            [Custom("Caption", "Glycemia")]
            public System.Double glycemia {
                get { return fglycemia; }
                set { SetPropertyValue("glycemia", ref fglycemia, value); }
            }
    
            private System.String finfo_diagnosis;
            [Size(-1)]
            [Custom("Caption", "Info Diagnosis")]
            public System.String info_diagnosis {
                get { return finfo_diagnosis; }
                set { SetPropertyValue("info_diagnosis", ref finfo_diagnosis, value); }
            }
    
        
            private medical_physician fderived_from;
            //FK FK_medical_patient_evaluation_derived_from
            [Custom("Caption", "Derived From")]
            public medical_physician derived_from {
                get { return fderived_from; }
                set { SetPropertyValue<medical_physician>("derived_from", ref fderived_from, value); }
            }
    
            private System.Int32 floc_verbal;
            [Custom("Caption", "Loc Verbal")]
            public System.Int32 loc_verbal {
                get { return floc_verbal; }
                set { SetPropertyValue("loc_verbal", ref floc_verbal, value); }
            }
    
            private System.Int32 floc_motor;
            [Custom("Caption", "Loc Motor")]
            public System.Int32 loc_motor {
                get { return floc_motor; }
                set { SetPropertyValue("loc_motor", ref floc_motor, value); }
            }
    
            private System.Double fbmi;
            [Custom("Caption", "Bmi")]
            public System.Double bmi {
                get { return fbmi; }
                set { SetPropertyValue("bmi", ref fbmi, value); }
            }
    
        
            private medical_physician fderived_to;
            //FK FK_medical_patient_evaluation_derived_to
            [Custom("Caption", "Derived To")]
            public medical_physician derived_to {
                get { return fderived_to; }
                set { SetPropertyValue<medical_physician>("derived_to", ref fderived_to, value); }
            }
    
            private System.String fdirections;
            [Size(-1)]
            [Custom("Caption", "Directions")]
            public System.String directions {
                get { return fdirections; }
                set { SetPropertyValue("directions", ref fdirections, value); }
            }
    
            private System.Double fcholesterol_total;
            [Custom("Caption", "Cholesterol Total")]
            public System.Double cholesterol_total {
                get { return fcholesterol_total; }
                set { SetPropertyValue("cholesterol_total", ref fcholesterol_total, value); }
            }
    
            private System.Double fheight;
            [Custom("Caption", "Height")]
            public System.Double height {
                get { return fheight; }
                set { SetPropertyValue("height", ref fheight, value); }
            }
    
            private System.Int32 fmood;
            [Custom("Caption", "Mood")]
            public System.Int32 mood {
                get { return fmood; }
                set { SetPropertyValue("mood", ref fmood, value); }
            }
    
            private DateTime? fevaluation_endtime;
            [Custom("Caption", "Evaluation Endtime")]
            public DateTime? evaluation_endtime {
                get { return fevaluation_endtime; }
                set { SetPropertyValue("evaluation_endtime", ref fevaluation_endtime, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private System.Int32 fbpm;
            [Custom("Caption", "Bpm")]
            public System.Int32 bpm {
                get { return fbpm; }
                set { SetPropertyValue("bpm", ref fbpm, value); }
            }
    
            private System.Int32 floc_eyes;
            [Custom("Caption", "Loc Eyes")]
            public System.Int32 loc_eyes {
                get { return floc_eyes; }
                set { SetPropertyValue("loc_eyes", ref floc_eyes, value); }
            }
    
            private System.Double fabdominal_circ;
            [Custom("Caption", "Abdominal Circ")]
            public System.Double abdominal_circ {
                get { return fabdominal_circ; }
                set { SetPropertyValue("abdominal_circ", ref fabdominal_circ, value); }
            }
    
            private System.Int32 fsystolic;
            [Custom("Caption", "Systolic")]
            public System.Int32 systolic {
                get { return fsystolic; }
                set { SetPropertyValue("systolic", ref fsystolic, value); }
            }
    
        
            private medical_pathology fdiagnosis;
            //FK FK_medical_patient_evaluation_diagnosis
            [Custom("Caption", "Diagnosis")]
            public medical_pathology diagnosis {
                get { return fdiagnosis; }
                set { SetPropertyValue<medical_pathology>("diagnosis", ref fdiagnosis, value); }
            }
    
            private System.String fnotes_complaint;
            [Size(-1)]
            [Custom("Caption", "Notes Complaint")]
            public System.String notes_complaint {
                get { return fnotes_complaint; }
                set { SetPropertyValue("notes_complaint", ref fnotes_complaint, value); }
            }
    
            private System.Int32 fhdl;
            [Custom("Caption", "Hdl")]
            public System.Int32 hdl {
                get { return fhdl; }
                set { SetPropertyValue("hdl", ref fhdl, value); }
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
		public medical_patient_evaluation(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

