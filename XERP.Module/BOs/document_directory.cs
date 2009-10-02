
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
	[DefaultProperty("domain")]
    [Persistent("document_directory")]
	public partial class document_directory : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //document_directory_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_document_directory_create_uid
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
            //FK FK_document_directory_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fdomain;
            [Size(128)]
            [Custom("Caption", "Domain")]
            public System.String domain {
                get { return fdomain; }
                set { SetPropertyValue("domain", ref fdomain, value); }
            }
    
        
            private ir_model fressource_type_id;
            //FK FK_document_directory_ressource_type_id
            [Custom("Caption", "Ressource Type id")]
            public ir_model ressource_type_id {
                get { return fressource_type_id; }
                set { SetPropertyValue<ir_model>("ressource_type_id", ref fressource_type_id, value); }
            }
    
            private System.String ffile_type;
            [Size(32)]
            [Custom("Caption", "File Type")]
            public System.String file_type {
                get { return ffile_type; }
                set { SetPropertyValue("file_type", ref ffile_type, value); }
            }
    
            private System.Boolean fressource_tree;
            [Custom("Caption", "Ressource Tree")]
            public System.Boolean ressource_tree {
                get { return fressource_tree; }
                set { SetPropertyValue("ressource_tree", ref fressource_tree, value); }
            }
    
        
            private res_users fuser_id;
            //FK FK_document_directory_user_id
            [Custom("Caption", "User Id")]
            public res_users user_id {
                get { return fuser_id; }
                set { SetPropertyValue<res_users>("user_id", ref fuser_id, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.Int32 fressource_id;
            [Custom("Caption", "Ressource Id")]
            public System.Int32 ressource_id {
                get { return fressource_id; }
                set { SetPropertyValue("ressource_id", ref fressource_id, value); }
            }
    
        
            private document_directory fparent_id;
            //FK FK_document_directory_parent_id
            [Custom("Caption", "Parent Id")]
            public document_directory parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<document_directory>("parent_id", ref fparent_id, value); }
            }
    
        
            private ir_model fressource_parent_type_id;
            //FK FK_document_directory_ressource_parent_type_id
            [Custom("Caption", "Ressource Parent type id")]
            public ir_model ressource_parent_type_id {
                get { return fressource_parent_type_id; }
                set { SetPropertyValue<ir_model>("ressource_parent_type_id", ref fressource_parent_type_id, value); }
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
		public document_directory(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

