
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
	[DefaultProperty("name")]
    [Persistent("crm_segmentation")]
	public partial class crm_segmentation : XPCustomObject
	{
		#region Properties
	    private System.Int32 fid;
        [Key(AutoGenerate = true), Browsable(false)]
        //crm_segmentation_id
		public System.Int32 id {
			get { return fid; }
			set { SetPropertyValue("id", ref fid, value); }
		}
        
            private res_users fcreate_uid;
            //FK FK_crm_segmentation_create_uid
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
            //FK FK_crm_segmentation_write_uid
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
    
            private System.Int32 fsom_interval_max;
            [Custom("Caption", "Som Interval max")]
            public System.Int32 som_interval_max {
                get { return fsom_interval_max; }
                set { SetPropertyValue("som_interval_max", ref fsom_interval_max, value); }
            }
    
            private System.Int32 fpartner_id;
            [Custom("Caption", "Partner Id")]
            public System.Int32 partner_id {
                get { return fpartner_id; }
                set { SetPropertyValue("partner_id", ref fpartner_id, value); }
            }
    
            private System.Boolean fexclusif;
            [Custom("Caption", "Exclusif")]
            public System.Boolean exclusif {
                get { return fexclusif; }
                set { SetPropertyValue("exclusif", ref fexclusif, value); }
            }
    
            private System.Int32 fsom_interval;
            [Custom("Caption", "Som Interval")]
            public System.Int32 som_interval {
                get { return fsom_interval; }
                set { SetPropertyValue("som_interval", ref fsom_interval, value); }
            }
    
            private System.String fstate1;
            [Size(16)]
            [Custom("Caption", "State1")]
            public System.String state1 {
                get { return fstate1; }
                set { SetPropertyValue("state1", ref fstate1, value); }
            }
    
            private System.Boolean fsales_purchase_active;
            [Custom("Caption", "Sales Purchase active")]
            public System.Boolean sales_purchase_active {
                get { return fsales_purchase_active; }
                set { SetPropertyValue("sales_purchase_active", ref fsales_purchase_active, value); }
            }
    
            private System.Double fsom_interval_default;
            [Custom("Caption", "Som Interval default")]
            public System.Double som_interval_default {
                get { return fsom_interval_default; }
                set { SetPropertyValue("som_interval_default", ref fsom_interval_default, value); }
            }
    
        
            private res_partner_category fcateg_id;
            //FK FK_crm_segmentation_categ_id
            [Custom("Caption", "Categ Id")]
            public res_partner_category categ_id {
                get { return fcateg_id; }
                set { SetPropertyValue<res_partner_category>("categ_id", ref fcateg_id, value); }
            }
    
            private System.Double fsom_interval_decrease;
            [Custom("Caption", "Som Interval decrease")]
            public System.Double som_interval_decrease {
                get { return fsom_interval_decrease; }
                set { SetPropertyValue("som_interval_decrease", ref fsom_interval_decrease, value); }
            }
    
            private System.String fdescription;
            [Size(-1)]
            [Custom("Caption", "Description")]
            public System.String description {
                get { return fdescription; }
                set { SetPropertyValue("description", ref fdescription, value); }
            }
    
            private System.Boolean fprofiling_active;
            [Custom("Caption", "Profiling Active")]
            public System.Boolean profiling_active {
                get { return fprofiling_active; }
                set { SetPropertyValue("profiling_active", ref fprofiling_active, value); }
            }
    
        
            private crm_segmentation fparent_id;
            //FK FK_crm_segmentation_parent_id
            [Custom("Caption", "Parent Id")]
            public crm_segmentation parent_id {
                get { return fparent_id; }
                set { SetPropertyValue<crm_segmentation>("parent_id", ref fparent_id, value); }
            }
    
		#endregion
	
		#region Collections
		#endregion
	
		#region Constructors
		public crm_segmentation(Session session) : base(session) { }
        #endregion

	}
}
//Generated for XERP

