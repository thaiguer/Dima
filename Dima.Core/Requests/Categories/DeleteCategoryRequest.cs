using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class DeleteCategoryRequest : BaseRequest
{
    [Required]
    public long Id { get; set; }
}
