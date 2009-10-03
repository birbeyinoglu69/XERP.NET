
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
    [Persistent("medical_lab_hepatogram")]
	public partial class medical_lab_hepatogram : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_lab_hepatogram_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_lab_hepatogram_create_uid
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
            //FK FK_medical_lab_hepatogram_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private medical_lab fname;
            //FK FK_medical_lab_hepatogram_name
            [Custom("Caption", "Name")]
            public medical_lab name {
                get { return fname; }
                set { SetPropertyValue<medical_lab>("name", ref fname, value); }
            }
    
            private System.Decimal fast;
            [Custom("Caption", "Ast")]
            public System.Decimal ast {
                get { return fast; }
                set { SetPropertyValue("ast", ref fast, value); }
            }
    
            private System.Decimal fbilirubin;
            [Custom("Caption", "Bilirubin")]
            public System.Decimal bilirubin {
                get { return fbilirubin; }
                set { SetPropertyValue("bilirubin", ref fbilirubin, value); }
            }
    
            private System.Decimal fldl;
            [Custom("Caption", "Ldl")]
            public System.Decimal ldl {
                get { return fldl; }
                set { SetPropertyValue("ldl", ref fldl, value); }
            }
    
            private System.Decimal fbilirubin_bc;
            [Custom("Caption", "Bilirubin Bc")]
            public System.Decimal bilirubin_bc {
                get { return fbilirubin_bc; }
                set { SetPropertyValue("bilirubin_bc", ref fbilirubin_bc, value); }
            }
    
            private System.Decimal fcholesterol;
            [Custom("Caption", "Cholesterol")]
            public System.Decimal cholesterol {
                get { return fcholesterol; }
                set { SetPropertyValue("cholesterol", ref fcholesterol, value); }
            }
    
            private System.Decimal falt;
            [Custom("Caption", "Alt")]
            public System.Decimal alt {
                get { return falt; }
                set { SetPropertyValue("alt", ref falt, value); }
            }
    
            private System.Decimal fhdl;
            [Custom("Caption", "Hdl")]
            public System.Decimal hdl {
                get { return fhdl; }
                set { SetPropertyValue("hdl", ref fhdl, value); }
            }
    
            private System.Decimal ftriglycerides;
            [Custom("Caption", "Triglycerides")]
            public System.Decimal triglycerides {
                get { return ftriglycerides; }
                set { SetPropertyValue("triglycerides", ref ftriglycerides, value); }
            }
    
            private System.Decimal falp;
            [Custom("Caption", "Alp")]
            public System.Decimal alp {
                get { return falp; }
                set { SetPropertyValue("alp", ref falp, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_lab_hepatogram(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

