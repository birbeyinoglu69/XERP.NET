
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
	[DefaultProperty("city")]
    [Persistent("res_partner_bank")]
	public partial class res_partner_bank : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_partner_bank_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_res_partner_bank_create_uid
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
            //FK FK_res_partner_bank_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fcity;
            [Size(128)]
            [Custom("Caption", "City")]
            public System.String city {
                get { return fcity; }
                set { SetPropertyValue("city", ref fcity, value); }
            }
    
            private System.String fowner_name;
            [Size(64)]
            [Custom("Caption", "Owner Name")]
            public System.String owner_name {
                get { return fowner_name; }
                set { SetPropertyValue("owner_name", ref fowner_name, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fzip;
            [Size(24)]
            [Custom("Caption", "Zip")]
            public System.String zip {
                get { return fzip; }
                set { SetPropertyValue("zip", ref fzip, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
        
            private res_country fcountry_id;
            //FK FK_res_partner_bank_country_id
            [Custom("Caption", "Country Id")]
            public res_country country_id {
                get { return fcountry_id; }
                set { SetPropertyValue<res_country>("country_id", ref fcountry_id, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String fstreet;
            [Size(128)]
            [Custom("Caption", "Street")]
            public System.String street {
                get { return fstreet; }
                set { SetPropertyValue("street", ref fstreet, value); }
            }
    
        
            private res_country_state1 fstate1_id;
            //FK FK_res_partner_bank_state1_id
            [Custom("Caption", "State1 Id")]
            public res_country_state1 state1_id {
                get { return fstate1_id; }
                set { SetPropertyValue<res_country_state1>("state1_id", ref fstate1_id, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_res_partner_bank_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
        
            private res_bank fbank;
            //FK FK_res_partner_bank_bank
            [Custom("Caption", "Bank")]
            public res_bank bank {
                get { return fbank; }
                set { SetPropertyValue<res_bank>("bank", ref fbank, value); }
            }
    
            private System.String facc_number;
            [Size(64)]
            [Custom("Caption", "Acc Number")]
            public System.String acc_number {
                get { return facc_number; }
                set { SetPropertyValue("acc_number", ref facc_number, value); }
            }
    
            private System.String fiban;
            [Size(34)]
            [Custom("Caption", "Iban")]
            public System.String iban {
                get { return fiban; }
                set { SetPropertyValue("iban", ref fiban, value); }
            }
    
            private System.String fpost_number;
            [Size(64)]
            [Custom("Caption", "Post Number")]
            public System.String post_number {
                get { return fpost_number; }
                set { SetPropertyValue("post_number", ref fpost_number, value); }
            }
    
            private System.String fdta_code;
            [Size(5)]
            [Custom("Caption", "Dta Code")]
            public System.String dta_code {
                get { return fdta_code; }
                set { SetPropertyValue("dta_code", ref fdta_code, value); }
            }
    
            private System.String fbvr_adherent_num;
            [Size(11)]
            [Custom("Caption", "Bvr Adherent num")]
            public System.String bvr_adherent_num {
                get { return fbvr_adherent_num; }
                set { SetPropertyValue("bvr_adherent_num", ref fbvr_adherent_num, value); }
            }
    
            private System.String fbvr_number;
            [Size(11)]
            [Custom("Caption", "Bvr Number")]
            public System.String bvr_number {
                get { return fbvr_number; }
                set { SetPropertyValue("bvr_number", ref fbvr_number, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_partner_bank(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

