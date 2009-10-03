
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
    [Persistent("ir_act_report_xml")]
	public partial class ir_act_report_xml : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_act_report_xml_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String ftype;
            [Size(32)]
            [Custom("Caption", "Type")]
            public System.String type {
                get { return ftype; }
                set { SetPropertyValue("type", ref ftype, value); }
            }
    
            private System.String fusage;
            [Size(32)]
            [Custom("Caption", "Usage")]
            public System.String usage {
                get { return fusage; }
                set { SetPropertyValue("usage", ref fusage, value); }
            }
    
            private System.String fmodel;
            [Size(64)]
            [Custom("Caption", "Model")]
            public System.String model {
                get { return fmodel; }
                set { SetPropertyValue("model", ref fmodel, value); }
            }
    
            private System.String freport_name;
            [Size(64)]
            [Custom("Caption", "Report Name")]
            public System.String report_name {
                get { return freport_name; }
                set { SetPropertyValue("report_name", ref freport_name, value); }
            }
    
            private System.String freport_xsl;
            [Size(256)]
            [Custom("Caption", "Report Xsl")]
            public System.String report_xsl {
                get { return freport_xsl; }
                set { SetPropertyValue("report_xsl", ref freport_xsl, value); }
            }
    
            private System.String freport_xml;
            [Size(256)]
            [Custom("Caption", "Report Xml")]
            public System.String report_xml {
                get { return freport_xml; }
                set { SetPropertyValue("report_xml", ref freport_xml, value); }
            }
    
            private System.Boolean fauto;
            [Custom("Caption", "Auto")]
            public System.Boolean auto {
                get { return fauto; }
                set { SetPropertyValue("auto", ref fauto, value); }
            }
    
            private System.Int32 fcreate_uid;
            [Custom("Caption", "Create Uid")]
            public System.Int32 create_uid {
                get { return fcreate_uid; }
                set { SetPropertyValue("create_uid", ref fcreate_uid, value); }
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
    
            private System.Int32 fwrite_uid;
            [Custom("Caption", "Write Uid")]
            public System.Int32 write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue("write_uid", ref fwrite_uid, value); }
            }
    
            private System.Byte[] freport_rml_content_data;
            [Custom("Caption", "Report Rml content data")]
            public System.Byte[] report_rml_content_data {
                get { return freport_rml_content_data; }
                set { SetPropertyValue("report_rml_content_data", ref freport_rml_content_data, value); }
            }
    
            private System.Boolean fheader;
            [Custom("Caption", "Header")]
            public System.Boolean header {
                get { return fheader; }
                set { SetPropertyValue("header", ref fheader, value); }
            }
    
            private System.String freport_type;
            [Size(16)]
            [Custom("Caption", "Report Type")]
            public System.String report_type {
                get { return freport_type; }
                set { SetPropertyValue("report_type", ref freport_type, value); }
            }
    
            private System.Boolean fmulti;
            [Custom("Caption", "Multi")]
            public System.Boolean multi {
                get { return fmulti; }
                set { SetPropertyValue("multi", ref fmulti, value); }
            }
    
            private System.String freport_rml;
            [Size(256)]
            [Custom("Caption", "Report Rml")]
            public System.String report_rml {
                get { return freport_rml; }
                set { SetPropertyValue("report_rml", ref freport_rml, value); }
            }
    
            private System.String fattachment;
            [Size(128)]
            [Custom("Caption", "Attachment")]
            public System.String attachment {
                get { return fattachment; }
                set { SetPropertyValue("attachment", ref fattachment, value); }
            }
    
            private System.Byte[] freport_sxw_content_data;
            [Custom("Caption", "Report Sxw content data")]
            public System.Byte[] report_sxw_content_data {
                get { return freport_sxw_content_data; }
                set { SetPropertyValue("report_sxw_content_data", ref freport_sxw_content_data, value); }
            }
    
            private System.Boolean fattachment_use;
            [Custom("Caption", "Attachment Use")]
            public System.Boolean attachment_use {
                get { return fattachment_use; }
                set { SetPropertyValue("attachment_use", ref fattachment_use, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_act_report_xml(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

