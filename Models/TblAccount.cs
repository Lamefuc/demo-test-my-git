using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reader.Models;

[Table("tblAccounts")]
public partial class TblAccount
{
    [Key]
    public int AccountId { get; set; }

    public int? RoleId { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public DateTime? BirthDay { get; set; }

    public string? Address { get; set; }

    public string? Thumb { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Description { get; set; }
    public TblRole? Role { get; set; }
}
