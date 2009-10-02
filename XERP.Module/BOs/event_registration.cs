
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
	[DefaultProperty("badge_title")]
    [Persistent("event_registration")]
	public partial class event_registration : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //event_registration_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_event_registration_create_uid
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
            //FK FK_event_registration_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Int32 fnb_register;
            [Custom("Caption", "Nb Register")]
            public System.Int32 nb_register {
                get { return fnb_register; }
                set { SetPropertyValue("nb_register", ref fnb_register, value); }
            }
    
            private System.String fbadge_title;
            [Size(128)]
            [Custom("Caption", "Badge Title")]
            public System.String badge_title {
                get { return fbadge_title; }
                set { SetPropertyValue("badge_title", ref fbadge_title, value); }
            }
    
        
            private event_event fevent_id;
            //FK FK_event_registration_event_id
            [Custom("Caption", "Event Id")]
            public event_event event_id {
                get { return fevent_id; }
                set { SetPropertyValue<event_event>("event_id", ref fevent_id, value); }
            }
    
        
            private account_invoice finvoice_id;
            //FK FK_event_registration_invoice_id
            [Custom("Caption", "Invoice Id")]
            public account_invoice invoice_id {
                get { return finvoice_id; }
                set { SetPropertyValue<account_invoice>("invoice_id", ref finvoice_id, value); }
            }
    
            private System.Double funit_price;
            [Custom("Caption", "Unit Price")]
            public System.Double unit_price {
                get { return funit_price; }
                set { SetPropertyValue("unit_price", ref funit_price, value); }
            }
    
            private System.String fbadge_partner;
            [Size(128)]
            [Custom("Caption", "Badge Partner")]
            public System.String badge_partner {
                get { return fbadge_partner; }
                set { SetPropertyValue("badge_partner", ref fbadge_partner, value); }
            }
    
        
            private crm_case fcase_id;
            //FK FK_event_registration_case_id
            [Custom("Caption", "Case Id")]
            public crm_case case_id {
                get { return fcase_id; }
                set { SetPropertyValue<crm_case>("case_id", ref fcase_id, value); }
            }
    
        
            private res_partner_contact fcontact_id;
            //FK FK_event_registration_contact_id
            [Custom("Caption", "Contact Id")]
            public res_partner_contact contact_id {
                get { return fcontact_id; }
                set { SetPropertyValue<res_partner_contact>("contact_id", ref fcontact_id, value); }
            }
    
            private System.String fbadge_name;
            [Size(128)]
            [Custom("Caption", "Badge Name")]
            public System.String badge_name {
                get { return fbadge_name; }
                set { SetPropertyValue("badge_name", ref fbadge_name, value); }
            }
    
        
            private res_partner fpartner_invoice_id;
            //FK FK_event_registration_partner_invoice_id
            [Custom("Caption", "Partner Invoice id")]
            public res_partner partner_invoice_id {
                get { return fpartner_invoice_id; }
                set { SetPropertyValue<res_partner>("partner_invoice_id", ref fpartner_invoice_id, value); }
            }
    
            private System.String finvoice_label;
            [Size(128)]
            [Custom("Caption", "Invoice Label")]
            public System.String invoice_label {
                get { return finvoice_label; }
                set { SetPropertyValue("invoice_label", ref finvoice_label, value); }
            }
    
            private System.Boolean ftobe_invoiced;
            [Custom("Caption", "Tobe Invoiced")]
            public System.Boolean tobe_invoiced {
                get { return ftobe_invoiced; }
                set { SetPropertyValue("tobe_invoiced", ref ftobe_invoiced, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public event_registration(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

