using BlazorWasmTemplate.Client.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmTemplate.Client.Shared.Components;

public partial class Breadcrumbs
{
    // Demonstrates how a parent component can supply parameters
    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public List<BreadcrumbItem> BreadcrumbItems { get; set; } = new List<BreadcrumbItem>();
}