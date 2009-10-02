
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
	[DefaultProperty("observations")]
    [Persistent("medical_vaccination")]
	public partial class medical_vaccination : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //medical_vaccination_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_medical_vaccination_create_uid
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
            //FK FK_medical_vaccination_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private DateTime? fdate;
            [Custom("Caption", "Date")]
            public DateTime? date {
                get { return fdate; }
                set { SetPropertyValue("date", ref fdate, value); }
            }
    
            private System.Int32 fdose;
            [Custom("Caption", "Dose")]
            public System.Int32 dose {
                get { return fdose; }
                set { SetPropertyValue("dose", ref fdose, value); }
            }
    
            private System.String fobservations;
            [Size(128)]
            [Custom("Caption", "Observations")]
            public System.String observations {
                get { return fobservations; }
                set { SetPropertyValue("observations", ref fobservations, value); }
            }
    
        
            private product_product fname;
            //FK FK_medical_vaccination_name
            [Custom("Caption", "Name")]
            public product_product name {
                get { return fname; }
                set { SetPropertyValue<product_product>("name", ref fname, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public medical_vaccination(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

