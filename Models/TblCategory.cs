using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reader.Models;

[Table("tblCategories")]
public partial class TblCategory
{
    [Key]
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }
    public string? Thumb { get; set; }

    public int? ParentId { get; set; }

    public int? Levels { get; set; }

    public int? CategoryOrder { get; set; }

    public bool HomeFlag { get; set; }
    public bool IsActive { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

}
