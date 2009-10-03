
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
    [Persistent("base_report_creator_report")]
	public partial class base_report_creator_report : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //base_report_creator_report_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_base_report_creator_report_create_uid
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
            //FK FK_base_report_creator_report_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.String fview_graph_type;
            [Size(16)]
            [Custom("Caption", "View Graph type")]
            public System.String view_graph_type {
                get { return fview_graph_type; }
                set { SetPropertyValue("view_graph_type", ref fview_graph_type, value); }
            }
    
            private System.String fview_type2;
            [Size(16)]
            [Custom("Caption", "View Type2")]
            public System.String view_type2 {
                get { return fview_type2; }
                set { SetPropertyValue("view_type2", ref fview_type2, value); }
            }
    
            private System.String fview_type1;
            [Size(16)]
            [Custom("Caption", "View Type1")]
            public System.String view_type1 {
                get { return fview_type1; }
                set { SetPropertyValue("view_type1", ref fview_type1, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String fview_type3;
            [Size(16)]
            [Custom("Caption", "View Type3")]
            public System.String view_type3 {
                get { return fview_type3; }
                set { SetPropertyValue("view_type3", ref fview_type3, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.String fsql_query;
            [Size(-1)]
            [Custom("Caption", "Sql Query")]
            public System.String sql_query {
                get { return fsql_query; }
                set { SetPropertyValue("sql_query", ref fsql_query, value); }
            }
    
            private System.String fview_graph_orientation;
            [Size(16)]
            [Custom("Caption", "View Graph orientation")]
            public System.String view_graph_orientation {
                get { return fview_graph_orientation; }
                set { SetPropertyValue("view_graph_orientation", ref fview_graph_orientation, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public base_report_creator_report(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

