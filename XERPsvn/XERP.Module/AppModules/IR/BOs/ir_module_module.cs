
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
	[DefaultProperty("website")]
    [Persistent("ir_module_module")]
	public partial class ir_module_module : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //ir_module_module_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_ir_module_module_create_uid
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
            //FK FK_ir_module_module_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fwebsite;
            [Size(256)]
            [Custom("Caption", "Website")]
            public System.String website {
                get { return fwebsite; }
                set { SetPropertyValue("website", ref fwebsite, value); }
            }
    
            private System.String fname;
            [Size(128)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
            private System.String fauthor;
            [Size(128)]
            [Custom("Caption", "Author")]
            public System.String author {
                get { return fauthor; }
                set { SetPropertyValue("author", ref fauthor, value); }
            }
    
            private System.String furl;
            [Size(128)]
            [Custom("Caption", "Url")]
            public System.String url {
                get { return furl; }
                set { SetPropertyValue("url", ref furl, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.String flatest_version;
            [Size(64)]
            [Custom("Caption", "Latest Version")]
            public System.String latest_version {
                get { return flatest_version; }
                set { SetPropertyValue("latest_version", ref flatest_version, value); }
            }
    
            private System.String fshortdesc;
            [Size(256)]
            [Custom("Caption", "Shortdesc")]
            public System.String shortdesc {
                get { return fshortdesc; }
                set { SetPropertyValue("shortdesc", ref fshortdesc, value); }
            }
    
        
            private ir_module_category fcategory_id;
            //FK FK_ir_module_module_category_id
            [Custom("Caption", "Category Id")]
            public ir_module_category category_id {
                get { return fcategory_id; }
                set { SetPropertyValue<ir_module_category>("category_id", ref fcategory_id, value); }
            }
    
            private System.String fcertificate;
            [Size(64)]
            [Custom("Caption", "Certificate")]
            public System.String certificate {
                get { return fcertificate; }
                set { SetPropertyValue("certificate", ref fcertificate, value); }
            }
    
            private System.String fdescription;
            [Size(-1)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
            private System.Boolean fdemo;
            [Custom("Caption", "Demo")]
            public System.Boolean demo {
                get { return fdemo; }
                set { SetPropertyValue("demo", ref fdemo, value); }
            }
    
            private System.String fmenus_by_module;
            [Size(-1)]
            [Custom("Caption", "Menus By module")]
            public System.String menus_by_module {
                get { return fmenus_by_module; }
                set { SetPropertyValue("menus_by_module", ref fmenus_by_module, value); }
            }
    
            private System.String fviews_by_module;
            [Size(-1)]
            [Custom("Caption", "Views By module")]
            public System.String views_by_module {
                get { return fviews_by_module; }
                set { SetPropertyValue("views_by_module", ref fviews_by_module, value); }
            }
    
            private System.String flicense;
            [Size(26)]
            [Custom("Caption", "License")]
            public System.String license {
                get { return flicense; }
                set { SetPropertyValue("license", ref flicense, value); }
            }
    
            private System.String fpublished_version;
            [Size(64)]
            [Custom("Caption", "Published Version")]
            public System.String published_version {
                get { return fpublished_version; }
                set { SetPropertyValue("published_version", ref fpublished_version, value); }
            }
    
            private System.String freports_by_module;
            [Size(-1)]
            [Custom("Caption", "Reports By module")]
            public System.String reports_by_module {
                get { return freports_by_module; }
                set { SetPropertyValue("reports_by_module", ref freports_by_module, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public ir_module_module(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

