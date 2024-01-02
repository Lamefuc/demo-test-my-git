using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reader.Models;

[Table("tblSliders")]
public partial class TblSlider
{
    [Key]
    public int SliderId { get; set; }

    public int? CategoryId { get; set; }

    public string? Title { get; set; }

    public string? Abstract { get; set; }

    public bool IsActive { get; set; }

    public string? Images { get; set; }

    public int? Position { get; set; }

    public int? SliderOrder { get; set; }

    public string? Description { get; set; }

    public TblCategory? Category { get; set; }
}
