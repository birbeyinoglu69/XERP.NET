
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
	[DefaultProperty("name")]
    [Persistent("crm_case_log")]
	public partial class crm_case_log : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //crm_case_log_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_crm_case_log_create_uid
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
            //FK FK_crm_case_log_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private crm_case fcase_id;
            //FK FK_crm_case_log_case_id
            [Custom("Caption", "Case Id")]
            public crm_case case_id {
                get { return fcase_id; }
                set { SetPropertyValue<crm_case>("case_id", ref fcase_id, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_crm_case_log_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private res_partner_canal fcanal_id;
            //FK FK_crm_case_log_canal_id
            [Custom("Caption", "Canal Id")]
            public res_partner_canal canal_id {
                get { return fcanal_id; }
                set { SetPropertyValue<res_partner_canal>("canal_id", ref fcanal_id, value); }
            }
    
            private DateTime? fdate;
            [Custom("Caption", "Date")]
            public DateTime? date {
                get { return fdate; }
                set { SetPropertyValue("date", ref fdate, value); }
            }
    
        
            private res_partner_som fsom;
            //FK FK_crm_case_log_som
            [Custom("Caption", "Som")]
            public res_partner_som som {
                get { return fsom; }
                set { SetPropertyValue<res_partner_som>("som", ref fsom, value); }
            }
    
        
            private crm_case_section fsection_id;
            //FK FK_crm_case_log_section_id
            [Custom("Caption", "Section Id")]
            public crm_case_section section_id {
                get { return fsection_id; }
                set { SetPropertyValue<crm_case_section>("section_id", ref fsection_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public crm_case_log(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

