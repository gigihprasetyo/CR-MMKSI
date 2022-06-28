@Code
    ViewData("Title") = "Daftar BAPI Log"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@section scripts
    <script type="text/javascript">
        //haloo
        $(function () {
            $(".DTPicker").datepicker({
                dateFormat: "yy-mm-dd"
            });

            $(".DTPicker").attr('readOnly', 'true');


            $("#GridArea").click(function () {

                RefreshRow();
            });

            $("#GridArea").blur(function () {

                RefreshRow();
            });

            setInterval(function () { RefreshRow(); }, 100);
            RefreshRow();
        });

        function RefreshRow() {
            $('.webgrid tr').each(function (index, element) {

                var Hd = $(element).find('input');

                if (Hd != undefined || Hd != null) {
                    if (Hd.val() == "False") {
                        $(element).addClass("warning");
                    }
                }

            });
        };
    </script>


End Section

@Section _ScriptsHeader
    <style type="text/css">
        .webgrid-row-style {
            /*background-color:red;*/
            font-size: 1em;
        }

        .warning {
            /*background-color:red;*/
            color: red;
            font-weight: bold;
        }

            .warning a {
                /*background-color:red;*/
                color: red;
                font-weight: bold;
            }

        .hd {
            display: none;
            /*background-color:red;*/
        }
    </style>

End Section
<div id="body">
    <section class="content-wrapper main-content clear-fix">
        @Code
            Dim grid = New WebGrid(canPage:=True, rowsPerPage:=10, ajaxUpdateContainerId:="GridArea")
            grid.Bind(Model, rowCount:=ViewBag.totalRow)
            grid.Pager(WebGridPagerModes.NextPrevious)

        End Code

        <fieldset>
            <form method="get">
                <table>
                    <tr>
                        <td>
                            <label>Submited From</label>
                            <input type="datetime" name="TXTDATEFROM" class="DTPicker" id="TXTDATEFROM" style="width:120px;" value="@ViewBag.TXTDATEFROM" />
                        </td>
                        <td>

                            <label>Submited To</label>
                            <input type="datetime" name="TXTDATETO" class="DTPicker" id="TXTDATETO" style="width:120px;" value="@ViewBag.TXTDATETO" />
                        </td>
                        <td>
                            <label>Kind Log</label>
                            <input list="datalist" name="TXTKEYNAME" id="TXTKEYNAME" style="width:160px;" value="@ViewBag.TXTKEYNAME">
                            <datalist id="datalist">
                                <option value="1">IR</option>
                                <option value="2">PO</option>
                                <option value="3">MSP</option>
                            </datalist>
                        </td>
                        <td>
                            <label>Status</label>
                            <select name="TXTSTATUS" id="TXTSTATUS" class="selMenu">
                                <option value="">ALL</option>
                                @If ViewBag.TXTSTATUS.ToString().ToUpper() = "TRUE" Then
                                    @<option value="1" selected>True</option>
                                Else
                                    @<option value="1">True</option>
                                End If

                                @If ViewBag.TXTSTATUS.ToString().ToUpper() = "FALSE" Then
                                    @<option value="0" selected>False</option>
                                Else
                                    @<option value="0">False</option>
                                End If

                            </select>
                        </td>
                        <td>
                            <label>Body</label>
                            <input type="text" id="TXTBODY" name="TXTBODY" value="@ViewBag.TXTBODY" />
                        </td>
                        <td>
                            <input type="submit" title="Search" value="Search" name="txtSeacrh" />
                        </td>
                    </tr>
                </table>
            </form>
        </fieldset>

        <fieldset>
            <div id="GridArea">
                @grid.GetHtml(tableStyle:="webgrid", alternatingRowStyle:="webgrid-row-style", rowStyle:="webgrid-row-style", _
                      columns:=grid.Columns(grid.Column(header:="No", format:=Function(item) (Html.Encode(item.WebGrid.Rows.IndexOf(item) + 1 + Math.Round(Convert.ToDouble(grid.TotalRowCount / grid.PageCount) / grid.RowsPerPage) * grid.RowsPerPage * grid.PageIndex))), _
                                            grid.Column("CreatedTime", "Tangal", format:=Function(item) Convert.ToDateTime(item.CreatedTime).ToString("dd/MM/yyyy HH:mm:ss")), _
                                            grid.Column("KindLog", "Log", Function(item) Html.Action("GetEnumDesc", New With {.val = item.KindLog})), _
                                            grid.Column("Status", "Status"), _
                                            grid.Column("ResponseLog", "ResponseLog", format:=Function(item) Html.Left(item.ResponseLog, 20)), _
                                           grid.Column("Body", "Body", format:=Function(item) Html.ActionLink((Html.Left(item.Body.ToString(), 100).ToString()), "ViewDetail", New With {.id = item.ID})), _
                                           grid.Column(style:="hd", format:=Function(item) Html.Raw("<input type='hidden' class='validasi' value='" & item.Status.ToString() & "'/>"))) _
                                       )
            </div>
        </fieldset>
    </section>
</div>
