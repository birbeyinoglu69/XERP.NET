
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
	[DefaultProperty("info")]
    [Persistent("account_tax_code")]
	public partial class account_tax_code : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_tax_code_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_tax_code_create_uid
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
            //FK FK_account_tax_code_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String finfo;
            [Size(-1)]
            [Custom("Caption", "Info")]
            public System.String info {
                get { return finfo; }
                set { SetPropertyValue("info", ref finfo, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private res_company fcompany_id;
            //FK FK_account_tax_code_company_id
            [Custom("Caption", "Company Id")]
            public res_company company_id {
                get { return fcompany_id; }
                set { SetPropertyValue<res_company>("company_id", ref fcompany_id, value); }
            }
    
            private System.Double fsign;
            [Custom("Caption", "Sign")]
            public System.Double sign {
                get { return fsign; }
                set { SetPropertyValue("sign", ref fsign, value); }
            }
    
            private System.Boolean fnotprintable;
            [Custom("Caption", "Notprintable")]
            public System.Boolean notprintable {
                get { return fnotprintable; }
                set { SetPropertyValue("notprintable", ref fnotprintable, value); }
            }
    
        
            private account_tax_code fparent_id;
            //FK FK_account_tax_code_parent_id
            [Custom("Caption", "Parent Id")]
            public account_tax_code parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<account_tax_code>("parent_id", ref fparent_id, value); }
            }
    
            private System.String fcode;
            [Size(512)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_tax_code(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

