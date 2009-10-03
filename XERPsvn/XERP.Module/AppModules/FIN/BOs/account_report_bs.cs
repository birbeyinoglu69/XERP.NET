
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
    [Persistent("account_report_bs")]
	public partial class account_report_bs : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //account_report_bs_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_account_report_bs_create_uid
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
            //FK FK_account_report_bs_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
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
    
            private System.String fnote;
            [Size(-1)]
            [Custom("Caption", "Note")]
            public System.String note {
                get { return fnote; }
                set { SetPropertyValue("note", ref fnote, value); }
            }
    
        
            private account_report_bs fparent_id;
            //FK FK_account_report_bs_parent_id
            [Custom("Caption", "Parent Id")]
            public account_report_bs parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<account_report_bs>("parent_id", ref fparent_id, value); }
            }
    
            private System.String ffont_style;
            [Size(19)]
            [Custom("Caption", "Font Style")]
            public System.String font_style {
                get { return ffont_style; }
                set { SetPropertyValue("font_style", ref ffont_style, value); }
            }
    
            private System.String freport_type;
            [Size(16)]
            [Custom("Caption", "Report Type")]
            public System.String report_type {
                get { return freport_type; }
                set { SetPropertyValue("report_type", ref freport_type, value); }
            }
    
        
            private color_rml fcolor_back;
            //FK FK_account_report_bs_color_back
            [Custom("Caption", "Color Back")]
            public color_rml color_back {
                get { return fcolor_back; }
                set { SetPropertyValue<color_rml>("color_back", ref fcolor_back, value); }
            }
    
        
            private color_rml fcolor_font;
            //FK FK_account_report_bs_color_font
            [Custom("Caption", "Color Font")]
            public color_rml color_font {
                get { return fcolor_font; }
                set { SetPropertyValue<color_rml>("color_font", ref fcolor_font, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public account_report_bs(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

