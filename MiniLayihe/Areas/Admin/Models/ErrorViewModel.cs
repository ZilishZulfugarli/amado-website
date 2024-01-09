using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MiniLayihe.Areas.Admin.Models;
[Authorize(Roles = "Admin")]
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

