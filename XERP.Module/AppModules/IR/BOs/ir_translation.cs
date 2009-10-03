
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
	[DefaultProperty("lang")]
    [Persistent("ir_translation")]
	public partial class ir_translation : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_translation_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
            private System.String flang;
            [Size(5)]
            [Custom("Caption", "Lang")]
            public System.String lang {
                get { return flang; }
                set { SetPropertyValue("lang", ref flang, value); }
            }
    
            private System.String fsrc;
            [Size(-1)]
            [Custom("Caption", "Src")]
            public System.String src {
                get { return fsrc; }
                set { SetPropertyValue("src", ref fsrc, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.Int32 fres_id;
            [Custom("Caption", "Res Id")]
            public System.Int32 res_id {
                get { return fres_id; }
                set { SetPropertyValue("res_id", ref fres_id, value); }
            }
    
            private System.String fvalue;
            [Size(-1)]
            [Custom("Caption", "Value")]
            public System.String value {
                get { return fvalue; }
                set { SetPropertyValue("value", ref fvalue, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_translation(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

