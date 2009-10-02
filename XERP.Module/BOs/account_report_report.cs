
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
	[DefaultProperty("code")]
    [Persistent("account_report_report")]
	public partial class account_report_report : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_report_report_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_report_report_create_uid
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
            //FK FK_account_report_report_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Decimal fgoodness_limit;
            [Custom("Caption", "Goodness Limit")]
            public System.Decimal goodness_limit {
                get { return fgoodness_limit; }
                set { SetPropertyValue("goodness_limit", ref fgoodness_limit, value); }
            }
    
            private System.Boolean fdisp_tree;
            [Custom("Caption", "Disp Tree")]
            public System.Boolean disp_tree {
                get { return fdisp_tree; }
                set { SetPropertyValue("disp_tree", ref fdisp_tree, value); }
            }
    
            private System.String fcode;
            [Size(64)]
            [Custom("Caption", "Code")]
            public System.String code {
                get { return fcode; }
                set { SetPropertyValue("code", ref fcode, value); }
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
    
            private System.String fexpression;
            [Size(240)]
            [Custom("Caption", "Expression")]
            public System.String expression {
                get { return fexpression; }
                set { SetPropertyValue("expression", ref fexpression, value); }
            }
    
            private System.Decimal fbadness_limit;
            [Custom("Caption", "Badness Limit")]
            public System.Decimal badness_limit {
                get { return fbadness_limit; }
                set { SetPropertyValue("badness_limit", ref fbadness_limit, value); }
            }
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
        
            private account_report_report fparent_id;
            //FK FK_account_report_report_parent_id
            [Custom("Caption", "Parent Id")]
            public account_report_report parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<account_report_report>("parent_id", ref fparent_id, value); }
            }
    
            private System.Boolean fdisp_graph;
            [Custom("Caption", "Disp Graph")]
            public System.Boolean disp_graph {
                get { return fdisp_graph; }
                set { SetPropertyValue("disp_graph", ref fdisp_graph, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.String ftype;
            [Size(16)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_report_report(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

