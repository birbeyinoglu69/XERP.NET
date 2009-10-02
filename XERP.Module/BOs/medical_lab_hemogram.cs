
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
    [Persistent("medical_lab_hemogram")]
	public partial class medical_lab_hemogram : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_lab_hemogram_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_lab_hemogram_create_uid
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
            //FK FK_medical_lab_hemogram_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Decimal freticulocytes;
            [Custom("Caption", "Reticulocytes")]
            public System.Decimal reticulocytes {
                get { return freticulocytes; }
                set { SetPropertyValue("reticulocytes", ref freticulocytes, value); }
            }
    
            private System.Decimal fmch;
            [Custom("Caption", "Mch")]
            public System.Decimal mch {
                get { return fmch; }
                set { SetPropertyValue("mch", ref fmch, value); }
            }
    
            private System.Decimal ftct;
            [Custom("Caption", "Tct")]
            public System.Decimal tct {
                get { return ftct; }
                set { SetPropertyValue("tct", ref ftct, value); }
            }
    
            private System.Int32 fplatelets;
            [Custom("Caption", "Platelets")]
            public System.Int32 platelets {
                get { return fplatelets; }
                set { SetPropertyValue("platelets", ref fplatelets, value); }
            }
    
            private System.Decimal finr;
            [Custom("Caption", "Inr")]
            public System.Decimal inr {
                get { return finr; }
                set { SetPropertyValue("inr", ref finr, value); }
            }
    
            private System.Decimal fhct;
            [Custom("Caption", "Hct")]
            public System.Decimal hct {
                get { return fhct; }
                set { SetPropertyValue("hct", ref fhct, value); }
            }
    
            private System.Int32 fbt;
            [Custom("Caption", "Bt")]
            public System.Int32 bt {
                get { return fbt; }
                set { SetPropertyValue("bt", ref fbt, value); }
            }
    
            private System.Decimal fbasophils;
            [Custom("Caption", "Basophils")]
            public System.Decimal basophils {
                get { return fbasophils; }
                set { SetPropertyValue("basophils", ref fbasophils, value); }
            }
    
            private System.Decimal faptt;
            [Custom("Caption", "Aptt")]
            public System.Decimal aptt {
                get { return faptt; }
                set { SetPropertyValue("aptt", ref faptt, value); }
            }
    
            private System.Decimal fhb;
            [Custom("Caption", "Hb")]
            public System.Decimal hb {
                get { return fhb; }
                set { SetPropertyValue("hb", ref fhb, value); }
            }
    
            private System.Decimal fmcv;
            [Custom("Caption", "Mcv")]
            public System.Decimal mcv {
                get { return fmcv; }
                set { SetPropertyValue("mcv", ref fmcv, value); }
            }
    
            private System.Decimal fmonocytes_pct;
            [Custom("Caption", "Monocytes Pct")]
            public System.Decimal monocytes_pct {
                get { return fmonocytes_pct; }
                set { SetPropertyValue("monocytes_pct", ref fmonocytes_pct, value); }
            }
    
            private System.Decimal fbneutrophils;
            [Custom("Caption", "Bneutrophils")]
            public System.Decimal bneutrophils {
                get { return fbneutrophils; }
                set { SetPropertyValue("bneutrophils", ref fbneutrophils, value); }
            }
    
            private System.Decimal feosinophils_pct;
            [Custom("Caption", "Eosinophils Pct")]
            public System.Decimal eosinophils_pct {
                get { return feosinophils_pct; }
                set { SetPropertyValue("eosinophils_pct", ref feosinophils_pct, value); }
            }
    
        
            private medical_lab fname;
            //FK FK_medical_lab_hemogram_name
            [Custom("Caption", "Name")]
            public medical_lab name {
                get { return fname; }
                set { SetPropertyValue<medical_lab>("name", ref fname, value); }
            }
    
            private System.Decimal feosinophils;
            [Custom("Caption", "Eosinophils")]
            public System.Decimal eosinophils {
                get { return feosinophils; }
                set { SetPropertyValue("eosinophils", ref feosinophils, value); }
            }
    
            private System.Decimal fbasophils_pct;
            [Custom("Caption", "Basophils Pct")]
            public System.Decimal basophils_pct {
                get { return fbasophils_pct; }
                set { SetPropertyValue("basophils_pct", ref fbasophils_pct, value); }
            }
    
            private System.Decimal fneutrophils_pct;
            [Custom("Caption", "Neutrophils Pct")]
            public System.Decimal neutrophils_pct {
                get { return fneutrophils_pct; }
                set { SetPropertyValue("neutrophils_pct", ref fneutrophils_pct, value); }
            }
    
            private System.Decimal fpt;
            [Custom("Caption", "Pt")]
            public System.Decimal pt {
                get { return fpt; }
                set { SetPropertyValue("pt", ref fpt, value); }
            }
    
            private System.Decimal fhbg;
            [Custom("Caption", "Hbg")]
            public System.Decimal hbg {
                get { return fhbg; }
                set { SetPropertyValue("hbg", ref fhbg, value); }
            }
    
            private System.Decimal ffibrinogen;
            [Custom("Caption", "Fibrinogen")]
            public System.Decimal fibrinogen {
                get { return ffibrinogen; }
                set { SetPropertyValue("fibrinogen", ref ffibrinogen, value); }
            }
    
            private System.Decimal fbneutrophils_pct;
            [Custom("Caption", "Bneutrophils Pct")]
            public System.Decimal bneutrophils_pct {
                get { return fbneutrophils_pct; }
                set { SetPropertyValue("bneutrophils_pct", ref fbneutrophils_pct, value); }
            }
    
            private System.Decimal flymphocytes_pct;
            [Custom("Caption", "Lymphocytes Pct")]
            public System.Decimal lymphocytes_pct {
                get { return flymphocytes_pct; }
                set { SetPropertyValue("lymphocytes_pct", ref flymphocytes_pct, value); }
            }
    
            private System.Decimal fwbc;
            [Custom("Caption", "Wbc")]
            public System.Decimal wbc {
                get { return fwbc; }
                set { SetPropertyValue("wbc", ref fwbc, value); }
            }
    
            private System.Decimal fmonocytes;
            [Custom("Caption", "Monocytes")]
            public System.Decimal monocytes {
                get { return fmonocytes; }
                set { SetPropertyValue("monocytes", ref fmonocytes, value); }
            }
    
            private System.Decimal flymphocytes;
            [Custom("Caption", "Lymphocytes")]
            public System.Decimal lymphocytes {
                get { return flymphocytes; }
                set { SetPropertyValue("lymphocytes", ref flymphocytes, value); }
            }
    
            private System.Decimal fneutrophils;
            [Custom("Caption", "Neutrophils")]
            public System.Decimal neutrophils {
                get { return fneutrophils; }
                set { SetPropertyValue("neutrophils", ref fneutrophils, value); }
            }
    
            private System.Decimal frbc;
            [Custom("Caption", "Rbc")]
            public System.Decimal rbc {
                get { return frbc; }
                set { SetPropertyValue("rbc", ref frbc, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_lab_hemogram(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

