
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
    [Persistent("res_partner_bank_type_field")]
	public partial class res_partner_bank_type_field : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_partner_bank_type_field_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_res_partner_bank_type_field_create_uid
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
            //FK FK_res_partner_bank_type_field_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private res_partner_bank_type fbank_type_id;
            //FK FK_res_partner_bank_type_field_bank_type_id
            [Custom("Caption", "Bank Type id")]
            public res_partner_bank_type bank_type_id {
                get { return fbank_type_id; }
                set { SetPropertyValue<res_partner_bank_type>("bank_type_id", ref fbank_type_id, value); }
            }
    
            private System.Boolean freadonly1;
            [Custom("Caption", "Readonly")]
            public System.Boolean readonly1 {
                get { return freadonly1; }
                set { SetPropertyValue("readonly1", ref freadonly1, value); }
            }
    
            private System.Boolean frequired;
            [Custom("Caption", "Required")]
            public System.Boolean required {
                get { return frequired; }
                set { SetPropertyValue("required", ref frequired, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Int32 fsize;
            [Custom("Caption", "Size")]
            public System.Int32 size {
                get { return fsize; }
                set { SetPropertyValue("size", ref fsize, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_partner_bank_type_field(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

