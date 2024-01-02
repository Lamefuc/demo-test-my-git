using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace Reader.Models;

[Table("tblPostStatus")]
public partial class TblPostStatus
{
    [Key]
    public int Status { get; set; }

    public string? StatusName { get; set; }

    public string? Description { get; set; }
}
