@using System.Web.Http
@using System.Web.Http.Description
@using $rootnamespace$.Areas.HelpPage
@using $rootnamespace$.Areas.HelpPage.Models
@model IGrouping<string, ApiDescription>

<h2 id="@Model.Key">@Model.Key</h2>
<table>
    <thead class="ui-widget-header">
        <tr><th>API</th><th>Description</th></tr>
    </thead>
    <tbody class="ui-widget-content">
    @foreach (var api in Model)
    {
        <tr>
            <td><a href="@Url.Action("Api", "Help", new { apiId = api.GetFriendlyId() })">@api.HttpMethod.Method @api.RelativePath</a></td>
            <td>
            @if (api.Documentation != null)
            {
                <p>@api.Documentation</p>
            }
            else
            {
                <p>No documentation available.</p>
            }
            </td>
        </tr>
    }
    </tbody>
</table>