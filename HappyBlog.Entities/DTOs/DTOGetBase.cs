using HappyBlog.Shared.Utilities.Results.ComplexTypes;

namespace HappyBlog.Entities.DTOs
{
   public abstract class DTOGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; }
    }
}
