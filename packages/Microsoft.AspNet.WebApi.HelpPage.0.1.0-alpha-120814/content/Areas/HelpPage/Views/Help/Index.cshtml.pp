@using System.Web.Http
@using System.Web.Http.Description
@using System.Collections.ObjectModel
@using $rootnamespace$.Areas.HelpPage.Models
@model Collection<ApiDescription>
           
@{
    ViewBag.Title = "ASP.NET Web API Help Page";
    
    // Group APIs by controller
    ILookup<string, ApiDescription> apiGroups = Model.ToLookup(api => api.ActionDescriptor.ControllerDescriptor.ControllerName);
}

<header>
    <div class="content-wrapper">
        <div class="float-left">
            <h1>@ViewBag.Title</h1>
        </div>
    </div>
</header>
<div id="body">
    <section class="featured">
        <div class="content-wrapper">
            <h2>Introduction</h2>
            <p>
                Provide a general description of your APIs here.
            </p>
        </div>
    </section>
    <section class="content-wrapper main-content clear-fix">
        @foreach (var group in apiGroups)
        {
            @Html.DisplayFor(m => group, "ApiGroup")
        }
    </section>
</div>

@section Scripts {
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/jquery-ui.min.js"></script>
    <link type="text/css" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/smoothness/jquery-ui.css" rel="stylesheet" />
}

