using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Categories;

public class CreateCategoryRequest : BaseRequest
{
    [Required()]
    [MaxLength(80)]
    public string Title { get; set; } = string.Empty;

    [Required()]
    public string Description { get; set; } = string.Empty;
}