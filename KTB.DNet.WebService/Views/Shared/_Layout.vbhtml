<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>@ViewData("Title")</title>
    @Styles.Render("~/Content/css")
    @Styles.Render("~/Content/themes/base/css")
    @Styles.Render("~/Content/plugin")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/jqueryui")
    @Scripts.Render("~/bundles/plugin")
    @RenderSection("_ScriptsHeader", required:=False)

</head>
<body>
    <header>
        <div class="content-wrapper">
            <div class="float-left">
                <p class="site-title">
                   MMC DNet Web API
                </p>
            </div>
            <div class="float-right">
                <nav>
                    <ul id="menu">
                        @*<li>@Html.ActionLink("Home", "Index", "Home", New With {.area = ""}, Nothing)</li>
                        <li>@Html.ActionLink("API", "Index", "Help", New With {.area = ""}, Nothing)</li>*@
                        <li>@Html.ActionLink("Submit Data WS", "WsSubmiter", "WsLog", New With {.area = ""}, Nothing)</li>
                        <li>@Html.ActionLink("List WSlog", "Index", "WsLog", New With {.area = ""}, Nothing)</li>
                        <li>@Html.ActionLink("List BAPI log", "Index", "BapiLog", New With {.area = ""}, Nothing)</li>
                        <li>@Html.ActionLink("Send File", "SendFile", "WsLog", New With {.area = ""}, Nothing)</li>
                    </ul>
                </nav>
            </div>
        </div>
    </header>
    @RenderBody()

   
    @RenderSection("scripts", required:=False)

</body>
</html>
