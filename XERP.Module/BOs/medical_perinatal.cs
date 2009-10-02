
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
	[DefaultProperty("fetus_presentation")]
    [Persistent("medical_perinatal")]
	public partial class medical_perinatal : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_perinatal_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_perinatal_create_uid
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
            //FK FK_medical_perinatal_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Int32 fprenatal_evaluations;
            [Custom("Caption", "Prenatal Evaluations")]
            public System.Int32 prenatal_evaluations {
                get { return fprenatal_evaluations; }
                set { SetPropertyValue("prenatal_evaluations", ref fprenatal_evaluations, value); }
            }
    
            private System.Int32 fgestational_weeks;
            [Custom("Caption", "Gestational Weeks")]
            public System.Int32 gestational_weeks {
                get { return fgestational_weeks; }
                set { SetPropertyValue("gestational_weeks", ref fgestational_weeks, value); }
            }
    
            private System.Boolean fabortion;
            [Custom("Caption", "Abortion")]
            public System.Boolean abortion {
                get { return fabortion; }
                set { SetPropertyValue("abortion", ref fabortion, value); }
            }
    
            private System.Boolean fplacenta_retained;
            [Custom("Caption", "Placenta Retained")]
            public System.Boolean placenta_retained {
                get { return fplacenta_retained; }
                set { SetPropertyValue("placenta_retained", ref fplacenta_retained, value); }
            }
    
            private System.Boolean fvaginal_tearing;
            [Custom("Caption", "Vaginal Tearing")]
            public System.Boolean vaginal_tearing {
                get { return fvaginal_tearing; }
                set { SetPropertyValue("vaginal_tearing", ref fvaginal_tearing, value); }
            }
    
            private System.String ffetus_presentation;
            [Size(16)]
            [Custom("Caption", "Fetus Presentation")]
            public System.String fetus_presentation {
                get { return ffetus_presentation; }
                set { SetPropertyValue("fetus_presentation", ref ffetus_presentation, value); }
            }
    
            private System.String fstart_labor_mode;
            [Size(16)]
            [Custom("Caption", "Start Labor mode")]
            public System.String start_labor_mode {
                get { return fstart_labor_mode; }
                set { SetPropertyValue("start_labor_mode", ref fstart_labor_mode, value); }
            }
    
            private System.Boolean fplacenta_incomplete;
            [Custom("Caption", "Placenta Incomplete")]
            public System.Boolean placenta_incomplete {
                get { return fplacenta_incomplete; }
                set { SetPropertyValue("placenta_incomplete", ref fplacenta_incomplete, value); }
            }
    
            private System.Int32 fgestational_days;
            [Custom("Caption", "Gestational Days")]
            public System.Int32 gestational_days {
                get { return fgestational_days; }
                set { SetPropertyValue("gestational_days", ref fgestational_days, value); }
            }
    
            private System.Boolean fepisiotomy;
            [Custom("Caption", "Episiotomy")]
            public System.Boolean episiotomy {
                get { return fepisiotomy; }
                set { SetPropertyValue("episiotomy", ref fepisiotomy, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
            private DateTime? fadmission_date;
            [Custom("Caption", "Admission Date")]
            public DateTime? admission_date {
                get { return fadmission_date; }
                set { SetPropertyValue("admission_date", ref fadmission_date, value); }
            }
    
            private System.Boolean fforceps;
            [Custom("Caption", "Forceps")]
            public System.Boolean forceps {
                get { return fforceps; }
                set { SetPropertyValue("forceps", ref fforceps, value); }
            }
    
            private DateTime? fdismissed;
            [Custom("Caption", "Dismissed")]
            public DateTime? dismissed {
                get { return fdismissed; }
                set { SetPropertyValue("dismissed", ref fdismissed, value); }
            }
    
            private System.Int32 fgravida_number;
            [Custom("Caption", "Gravida Number")]
            public System.Int32 gravida_number {
                get { return fgravida_number; }
                set { SetPropertyValue("gravida_number", ref fgravida_number, value); }
            }
    
            private System.Boolean fdied_at_delivery;
            [Custom("Caption", "Died At delivery")]
            public System.Boolean died_at_delivery {
                get { return fdied_at_delivery; }
                set { SetPropertyValue("died_at_delivery", ref fdied_at_delivery, value); }
            }
    
            private System.Boolean fdied_being_transferred;
            [Custom("Caption", "Died Being transferred")]
            public System.Boolean died_being_transferred {
                get { return fdied_being_transferred; }
                set { SetPropertyValue("died_being_transferred", ref fdied_being_transferred, value); }
            }
    
            private System.Boolean fdied_at_the_hospital;
            [Custom("Caption", "Died At the hospital")]
            public System.Boolean died_at_the_hospital {
                get { return fdied_at_the_hospital; }
                set { SetPropertyValue("died_at_the_hospital", ref fdied_at_the_hospital, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_perinatal(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

