
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
	[DefaultProperty("text_area")]
    [Persistent("wiki_wiki_history")]
	public partial class wiki_wiki_history : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //wiki_wiki_history_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_wiki_wiki_history_create_uid
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
            //FK FK_wiki_wiki_history_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String ftext_area;
            [Size(-1)]
            [Custom("Caption", "Text Area")]
            public System.String text_area {
                get { return ftext_area; }
                set { SetPropertyValue("text_area", ref ftext_area, value); }
            }
    
        
            private wiki_wiki fwiki_id;
            //FK FK_wiki_wiki_history_wiki_id
            [Custom("Caption", "Wiki Id")]
            public wiki_wiki wiki_id {
                get { return fwiki_id; }
                set { SetPropertyValue<wiki_wiki>("wiki_id", ref fwiki_id, value); }
            }
    
            private System.Boolean fminor_edit;
            [Custom("Caption", "Minor Edit")]
            public System.Boolean minor_edit {
                get { return fminor_edit; }
                set { SetPropertyValue("minor_edit", ref fminor_edit, value); }
            }
    
            private System.String fsummary;
            [Size(256)]
            [Custom("Caption", "Summary")]
            public System.String summary {
                get { return fsummary; }
                set { SetPropertyValue("summary", ref fsummary, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public wiki_wiki_history(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

