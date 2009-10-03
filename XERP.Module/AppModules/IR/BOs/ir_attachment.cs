
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
	[DefaultProperty("description")]
    [Persistent("ir_attachment")]
	public partial class ir_attachment : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_attachment_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_ir_attachment_create_uid
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
            //FK FK_ir_attachment_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fdescription;
            [Size(-1)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
            private System.String fres_model;
            [Size(64)]
            [Custom("Caption", "Res Model")]
            public System.String res_model {
                get { return fres_model; }
                set { SetPropertyValue("res_model", ref fres_model, value); }
            }
    
            private System.String flink;
            [Size(256)]
            [Custom("Caption", "Link")]
            public System.String link {
                get { return flink; }
                set { SetPropertyValue("link", ref flink, value); }
            }
    
            private System.Int32 fres_id;
            [Custom("Caption", "Res Id")]
            public System.Int32 res_id {
                get { return fres_id; }
                set { SetPropertyValue("res_id", ref fres_id, value); }
            }
    
            private System.String fdatas_fname;
            [Size(64)]
            [Custom("Caption", "Datas Fname")]
            public System.String datas_fname {
                get { return fdatas_fname; }
                set { SetPropertyValue("datas_fname", ref fdatas_fname, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String ffile_type;
            [Size(32)]
            [Custom("Caption", "File Type")]
            public System.String file_type {
                get { return ffile_type; }
                set { SetPropertyValue("file_type", ref ffile_type, value); }
            }
    
            private System.Int32 ffile_size;
            [Custom("Caption", "File Size")]
            public System.Int32 file_size {
                get { return ffile_size; }
                set { SetPropertyValue("file_size", ref ffile_size, value); }
            }
    
        
            private res_partner fpartner_id;
            //FK FK_ir_attachment_partner_id
            [Custom("Caption", "Partner Id")]
            public res_partner partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue<res_partner>("partner_id", ref fpartner_id, value); }
            }
    
            private System.String fstore_method;
            [Size(16)]
            [Custom("Caption", "Store Method")]
            public System.String store_method {
                get { return fstore_method; }
                set { SetPropertyValue("store_method", ref fstore_method, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_ir_attachment_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String ftitle;
            [Size(64)]
            [Custom("Caption", "Title")]
            public System.String title {
                get { return ftitle; }
                set { SetPropertyValue("title", ref ftitle, value); }
            }
    
        
            private document_directory fparent_id;
            //FK FK_ir_attachment_parent_id
            [Custom("Caption", "Parent Id")]
            public document_directory parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<document_directory>("parent_id", ref fparent_id, value); }
            }
    
            private System.String fstore_fname;
            [Size(200)]
            [Custom("Caption", "Store Fname")]
            public System.String store_fname {
                get { return fstore_fname; }
                set { SetPropertyValue("store_fname", ref fstore_fname, value); }
            }
    
            private System.String findex_content;
            [Size(-1)]
            [Custom("Caption", "Index Content")]
            public System.String index_content {
                get { return findex_content; }
                set { SetPropertyValue("index_content", ref findex_content, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_attachment(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

