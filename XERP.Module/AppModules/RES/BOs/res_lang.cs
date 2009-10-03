
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
	[DefaultProperty("date_format")]
    [Persistent("res_lang")]
	public partial class res_lang : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //res_lang_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_res_lang_create_uid
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
            //FK FK_res_lang_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String fdate_format;
            [Size(64)]
            [Custom("Caption", "Date Format")]
            public System.String date_format {
                get { return fdate_format; }
                set { SetPropertyValue("date_format", ref fdate_format, value); }
            }
    
            private System.String fdirection;
            [Size(16)]
            [Custom("Caption", "Direction")]
            public System.String direction {
                get { return fdirection; }
                set { SetPropertyValue("direction", ref fdirection, value); }
            }
    
            private System.String fcode;
            [Size(5)]
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
    
            private System.String fthousands_sep;
            [Size(64)]
            [Custom("Caption", "Thousands Sep")]
            public System.String thousands_sep {
                get { return fthousands_sep; }
                set { SetPropertyValue("thousands_sep", ref fthousands_sep, value); }
            }
    
            private System.Boolean ftranslatable;
            [Custom("Caption", "Translatable")]
            public System.Boolean translatable {
                get { return ftranslatable; }
                set { SetPropertyValue("translatable", ref ftranslatable, value); }
            }
    
            private System.String ftime_format;
            [Size(64)]
            [Custom("Caption", "Time Format")]
            public System.String time_format {
                get { return ftime_format; }
                set { SetPropertyValue("time_format", ref ftime_format, value); }
            }
    
            private System.String fdecimal_point;
            [Size(64)]
            [Custom("Caption", "Decimal Point")]
            public System.String decimal_point {
                get { return fdecimal_point; }
                set { SetPropertyValue("decimal_point", ref fdecimal_point, value); }
            }
    
            private System.Boolean factive;
            [Custom("Caption", "Active")]
            public System.Boolean active {
                get { return factive; }
                set { SetPropertyValue("active", ref factive, value); }
            }
    
            private System.String fgrouping;
            [Size(64)]
            [Custom("Caption", "Grouping")]
            public System.String grouping {
                get { return fgrouping; }
                set { SetPropertyValue("grouping", ref fgrouping, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public res_lang(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

