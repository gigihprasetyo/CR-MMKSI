@ModelType KTB.DNet.Domain.WsLog

@Code
    ViewData("Title") = "View Detail"
    Layout = "~/Views/Shared/_Layout.vbhtml"

End Code


@code
    Dim strapi As String = "api/saplistener/SendData"
    Dim strapi2 As String = "api/saplistenerdsk/SendData"
    Dim IsProduction As Boolean = False
    Dim str As String = System.Configuration.ConfigurationManager.AppSettings("LoginMode")
    Dim Addr As String = ""

End Code

<div id="body">

    <section class="content-wrapper main-content clear-fix">
        @If Not IsNothing(ViewBag.IP) Then
            @<select id="apiurl">
                <option value="@ViewBag.IP@strapi">@ViewBag.IP@strapi</option>
                <option value="@ViewBag.IP@strapi2">@ViewBag.IP@strapi2</option>
            </select>

        End If
        <div>

            <fieldset>

                <input type="password" autocomplete="off" placeholder="Password" id="txtPassword" />
                <button id="BtnResend">Resend this Data</button>



            </fieldset>
        </div>

        <div>
            <dl class="dl-horizontal">

                <dt>
                    @Html.DisplayNameFor(Function(Model) Model.CreatedTime)

                </dt>
                <dd>
                    @Html.DisplayFor(Function(Model) Model.CreatedTime)
                </dd>


                <dt>
                    @Html.DisplayNameFor(Function(Model) Model.Source)
                </dt>

                <dd>
                    @Html.DisplayFor(Function(Model) Model.Source)
                </dd>

                <dt>
                    @Html.DisplayNameFor(Function(Model) Model.Status)
                </dt>

                <dd>
                    @Html.DisplayFor(Function(Model) Model.Status)
                </dd>

                <dt>
                    @Html.DisplayNameFor(Function(Model) Model.Message)
                </dt>

                <dd>
                    @Html.DisplayFor(Function(Model) Model.Message)
                </dd>

                <dt>
                    <span>Raw Body</span>
                </dt>

                <dd>
                    <textarea id="idData" style="width:100%; height:150px;">@Model.Body</textarea>

                </dd>

                <dt>
                    <span>  Body</span>
                </dt>

                <dd>
                    @Html.ToTable(Model.Body)

                </dd>

            </dl>
        </div>

    </section>
</div>

<div id="Footer">
    <section class="content-wrapper main-content clear-fix"></section>
</div>
<p>
    <button onclick="location.href='@Url.Action("Index")';return false;">Back to List</button>
    @*  <button id="btnBack"> @Html.ActionLink("Back to List", "Index")</button> *@
    @*<button>  @Html.ActionLink("Back to List", Nothing, Nothing, Nothing, New With {Key .href = Request.UrlReferrer })</button>*@
</p>

@section scripts
    <script type="text/javascript">

        $(function () {


            $("#idData").attr('readOnly', 'true');

            $("#BtnResend").click(function () {
                var content = $('#idData').val();
                var vPassword = $('#txtPassword').val();
                var Addr = "@ViewBag.IP@strapi";




                if (vPassword == "") {
                    alert("Masukan Password");
                    $('#txtPassword').focus();
                    return;
                }
                var vapiurl = $('#apiurl').val();
                $.ajax({
                    type: "POST",
                    Origin: Addr,
                    headers: { 'x-pass-header': vPassword },
                    cache: false,
                    url: vapiurl,
                    crossDomain: true,
                    data: content
                }).done(function (str) {
                    alert('success. Status = ' + str.Status + '. Message = ' + str.Message);
                });

            });



        });


    </script>
End Section
