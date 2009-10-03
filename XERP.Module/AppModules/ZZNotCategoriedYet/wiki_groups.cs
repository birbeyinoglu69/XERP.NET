
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
    [Persistent("wiki_groups")]
	public partial class wiki_groups : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //wiki_groups_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_wiki_groups_create_uid
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
            //FK FK_wiki_groups_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fname;
            [Size(256)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fnotes;
            [Size(-1)]
            [Custom("Caption", "Notes")]
            public System.String notes {
                get { return fnotes; }
                set { SetPropertyValue("notes", ref fnotes, value); }
            }
    
        
            private wiki_groups fparent_id;
            //FK FK_wiki_groups_parent_id
            [Custom("Caption", "Parent Id")]
            public wiki_groups parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<wiki_groups>("parent_id", ref fparent_id, value); }
            }
    
            private System.String ftemplate;
            [Size(-1)]
            [Custom("Caption", "Template")]
            public System.String template {
                get { return ftemplate; }
                set { SetPropertyValue("template", ref ftemplate, value); }
            }
    
        
            private wiki_wiki fhome;
            //FK FK_wiki_groups_home
            [Custom("Caption", "Home")]
            public wiki_wiki home {
                get { return fhome; }
                set { SetPropertyValue<wiki_wiki>("home", ref fhome, value); }
            }
    
            private System.Boolean fsection;
            [Custom("Caption", "Section")]
            public System.Boolean section {
                get { return fsection; }
                set { SetPropertyValue("section", ref fsection, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public wiki_groups(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

