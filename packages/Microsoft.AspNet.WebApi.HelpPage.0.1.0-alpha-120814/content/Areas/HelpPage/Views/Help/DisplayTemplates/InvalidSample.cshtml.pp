@using $rootnamespace$.Areas.HelpPage
@model InvalidSample

@if (HttpContext.Current.IsDebuggingEnabled)
{
    <pre class="wrapped ui-state-error">@Model.ErrorMessage</pre>
}
else
{
    <p>Sample not available.</p>
}
