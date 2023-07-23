using System;

namespace Cms.Backend.Api.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public bool IsActived { get; set; }
        public bool  IsDeleted { get; set; }     
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
        public string Remarks { get; set; }
        
    }
}