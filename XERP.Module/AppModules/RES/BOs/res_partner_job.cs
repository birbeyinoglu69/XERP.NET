
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
	[DefaultProperty("extension")]
    [Persistent("res_partner_job")]
	public partial class res_partner_job : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_partner_job_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_res_partner_job_create_uid
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
            //FK FK_res_partner_job_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Int32 fsequence_partner;
            [Custom("Caption", "Sequence Partner")]
            public System.Int32 sequence_partner {
                get { return fsequence_partner; }
                set { SetPropertyValue("sequence_partner", ref fsequence_partner, value); }
            }
    
            private System.Int32 fsequence_contact;
            [Custom("Caption", "Sequence Contact")]
            public System.Int32 sequence_contact {
                get { return fsequence_contact; }
                set { SetPropertyValue("sequence_contact", ref fsequence_contact, value); }
            }
    
            private System.String fextension;
            [Size(64)]
            [Custom("Caption", "Extension")]
            public System.String extension {
                get { return fextension; }
                set { SetPropertyValue("extension", ref fextension, value); }
            }
    
        
            private res_partner_address faddress_id;
            //FK FK_res_partner_job_address_id
            [Custom("Caption", "Address Id")]
            public res_partner_address address_id {
                get { return faddress_id; }
                set { SetPropertyValue<res_partner_address>("address_id", ref faddress_id, value); }
            }
    
            private System.String ffax;
            [Size(64)]
            [Custom("Caption", "Fax")]
            public System.String fax {
                get { return ffax; }
                set { SetPropertyValue("fax", ref ffax, value); }
            }
    
        
            private res_partner_contact fcontact_id;
            //FK FK_res_partner_job_contact_id
            [Custom("Caption", "Contact Id")]
            public res_partner_contact contact_id {
                get { return fcontact_id; }
                set { SetPropertyValue<res_partner_contact>("contact_id", ref fcontact_id, value); }
            }
    
            private System.String fphone;
            [Size(64)]
            [Custom("Caption", "Phone")]
            public System.String phone {
                get { return fphone; }
                set { SetPropertyValue("phone", ref fphone, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String fother;
            [Size(64)]
            [Custom("Caption", "Other")]
            public System.String other {
                get { return fother; }
                set { SetPropertyValue("other", ref fother, value); }
            }
    
        
            private res_partner_function ffunction_id;
            //FK FK_res_partner_job_function_id
            [Custom("Caption", "Function Id")]
            public res_partner_function function_id {
                get { return ffunction_id; }
                set { SetPropertyValue<res_partner_function>("function_id", ref ffunction_id, value); }
            }
    
            private System.String femail;
            [Size(240)]
            [Custom("Caption", "Email")]
            public System.String email {
                get { return femail; }
                set { SetPropertyValue("email", ref femail, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_partner_job(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

