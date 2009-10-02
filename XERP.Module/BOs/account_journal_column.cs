
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
	[DefaultProperty("field")]
    [Persistent("account_journal_column")]
	public partial class account_journal_column : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_journal_column_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_journal_column_create_uid
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
            //FK FK_account_journal_column_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String ffield;
            [Size(32)]
            [Custom("Caption", "Field")]
            public System.String field {
                get { return ffield; }
                set { SetPropertyValue("field", ref ffield, value); }
            }
    
            private System.Boolean freadonly1;
            [Custom("Caption", "Readonly")]
            public System.Boolean readonly1 {
                get { return freadonly1; }
                set { SetPropertyValue("readonly1", ref freadonly1, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Int32 fsequence;
            [Custom("Caption", "Sequence")]
            public System.Int32 sequence {
                get { return fsequence; }
                set { SetPropertyValue("sequence", ref fsequence, value); }
            }
    
        
            private account_journal_view fview_id;
            //FK FK_account_journal_column_view_id
            [Custom("Caption", "View Id")]
            public account_journal_view view_id {
                get { return fview_id; }
                set { SetPropertyValue<account_journal_view>("view_id", ref fview_id, value); }
            }
    
            private System.Boolean frequired;
            [Custom("Caption", "Required")]
            public System.Boolean required {
                get { return frequired; }
                set { SetPropertyValue("required", ref frequired, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_journal_column(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

