@ModelType KTB.DNet.Domain.BapiLog

@Code
    ViewData("Title") = "View Detail"
    Layout = "~/Views/Shared/_Layout.vbhtml"

End Code


<div id="body">

    <section class="content-wrapper main-content clear-fix">
        <div>
            <dl class="dl-horizontal">
                <dt>
                    @Html.DisplayNameFor(Function(Model) Model.SubmitDate)

                </dt>
                <dd>
                    @Html.DisplayFor(Function(Model) Model.SubmitDate)
                </dd>


                <dt>
                    User Bapi
                </dt>

                <dd>
                    @Html.DisplayFor(Function(Model) Model.UserName)
                </dd>

                <dt>
                    @Html.DisplayNameFor(Function(Model) Model.CreatedBy)
                </dt>

                <dd>
                    @Html.DisplayFor(Function(Model) Model.CreatedBy)
                </dd>

                <dt>
                    @Html.DisplayNameFor(Function(Model) Model.KindLog)
                </dt>

                <dd>
                    @Html.Action("GetEnumDesc", New With {.val = Model.KindLog})
                </dd>

                <dt>
                    @Html.DisplayNameFor(Function(Model) Model.Status)
                </dt>

                <dd>

                    @Html.DisplayFor(Function(Model) Model.Status)

                </dd>
                <dt>
                    @Html.DisplayNameFor(Function(Model) Model.Body)
                </dt>
                <dd>
                    <div class="jsonFormatter">
                        @Html.DisplayFor(Function(Model) Model.Body)
                    </div>
                </dd>

                <dt>
                    @Html.DisplayNameFor(Function(Model) Model.ResponseLog)
                </dt>
                <dd>
                    <div class="jsonFormatter">
                        @Html.DisplayFor(Function(Model) Model.ResponseLog)
                    </div>
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
</p>


@section scripts
    <script type="text/javascript">
        $(document).ready(function () {
            $('.jsonFormatter').jsonFormatter(); 
        });
    </script>
End Section