
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
    [Persistent("wiki_groups_link")]
	public partial class wiki_groups_link : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //wiki_groups_link_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_wiki_groups_link_create_uid
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
            //FK FK_wiki_groups_link_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
        
            private wiki_groups fgroup_id;
            //FK FK_wiki_groups_link_group_id
            [Custom("Caption", "Group Id")]
            public wiki_groups group_id {
                get { return fgroup_id; }
                set { SetPropertyValue<wiki_groups>("group_id", ref fgroup_id, value); }
            }
    
        
            private ir_ui_menu faction_id;
            //FK FK_wiki_groups_link_action_id
            [Custom("Caption", "Action Id")]
            public ir_ui_menu action_id {
                get { return faction_id; }
                set { SetPropertyValue<ir_ui_menu>("action_id", ref faction_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public wiki_groups_link(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

