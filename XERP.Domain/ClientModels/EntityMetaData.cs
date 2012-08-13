using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XERP.Domain.ClientModels
{
    public class EntityMetaData
    {
        public long ID { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public int? MaxLength { get; set; }
    }
}
