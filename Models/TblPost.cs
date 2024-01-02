using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reader.Models;

[Table("tblPosts")]
public partial class TblPost
{
    [Key]
    public int PostId { get; set; }

    public int? AccountId { get; set; }

    public int? CategoryId { get; set; }

    public string? Title { get; set; }

    public string? SubContents { get; set; }

    public string? Contents { get; set; }

    public string? Thumb { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsHot { get; set; }

    public bool IsNewfeed { get; set; }

    public int PostOrder { get; set; }

    public int Status { get; set; }

    public int? Sview { get; set; }

    public string? Description { get; set; }

    public TblCategory? Category { get; set; }

}
