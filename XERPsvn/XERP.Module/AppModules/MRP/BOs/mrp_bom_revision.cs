
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
	[DefaultProperty("indice")]
    [Persistent("mrp_bom_revision")]
	public partial class mrp_bom_revision : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //mrp_bom_revision_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_mrp_bom_revision_create_uid
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
            //FK FK_mrp_bom_revision_write_uid
            [Custom("Caption", "Write Uid")]
            public res_users write_uid {
                get { return fwrite_uid; }
                set { SetPropertyValue<res_users>("write_uid", ref fwrite_uid, value); }
            }
    
            private System.String findice;
            [Size(16)]
            [Custom("Caption", "Indice")]
            public System.String indice {
                get { return findice; }
                set { SetPropertyValue("indice", ref findice, value); }
            }
    
            private System.String flast_indice;
            [Size(64)]
            [Custom("Caption", "Last Indice")]
            public System.String last_indice {
                get { return flast_indice; }
                set { SetPropertyValue("last_indice", ref flast_indice, value); }
            }
    
            private System.String fname;
            [Size(64)]
            [Custom("Caption", "Name")]
            public System.String name {
                get { return fname; }
                set { SetPropertyValue("name", ref fname, value); }
            }
    
        
            private mrp_bom fbom_id;
            //FK FK_mrp_bom_revision_bom_id
            [Custom("Caption", "Bom Id")]
            public mrp_bom bom_id {
                get { return fbom_id; }
                set { SetPropertyValue<mrp_bom>("bom_id", ref fbom_id, value); }
            }
    
        
            private res_users fauthor_id;
            //FK FK_mrp_bom_revision_author_id
            [Custom("Caption", "Author Id")]
            public res_users author_id {
                get { return fauthor_id; }
                set { SetPropertyValue<res_users>("author_id", ref fauthor_id, value); }
            }
    
            private System.String fdescription;
            [Size(-1)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public mrp_bom_revision(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

