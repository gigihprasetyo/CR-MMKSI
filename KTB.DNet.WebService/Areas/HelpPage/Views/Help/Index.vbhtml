@Imports System.Web.Http
@Imports System.Web.Http.Description
@Imports System.Collections.ObjectModel
@Imports KTB.DNet.WebService.Areas.HelpPage
@ModelType Collection(Of ApiDescription)

@Code
    ViewData("Title") = "DNET Web API Help Page"
    
    ' Group APIs by controller
    Dim apiGroups As ILookup(Of String, ApiDescription) = Model.ToLookup(Function(api) api.ActionDescriptor.ControllerDescriptor.ControllerName)
End Code

<header>
    <div class="content-wrapper">
        <div class="float-left">
            <h1>@ViewData("Title")</h1>
        </div>
    </div>
</header>
<div id="body">
    <section class="featured">
        <div class="content-wrapper">
            <h2>Introduction</h2>
            <p>
                Provide a general description of your APIs here. ahoy
            </p>
        </div>
    </section>
    <section class="content-wrapper main-content clear-fix">
        @For Each group As IGrouping(Of String, ApiDescription) In apiGroups
            @Html.DisplayFor(Function(m) group, "ApiGroup")
        Next
    </section>
</div>

@Section Scripts
    <link type="text/css" href="~/Areas/HelpPage/HelpPage.css" rel="stylesheet" />
End Section