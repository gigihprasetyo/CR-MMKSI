<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmRecallCategoryDetail.aspx.vb" Inherits=".FrmRecallCategoryDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        .div-inline {
            display: inline;
        }
    </style>
    <title></title>

    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script type="text/javascript">
        function removeItemData(evt) {
            var dataId = evt.parentElement.parentElement.childNodes[7].childNodes[0].value;
            var serviceBulletinNo = document.getElementById('lblServiceBulletinNo').innerText;

            var typeCode = document.getElementById('txtTypeCode').value;
            var positionCode = document.getElementById('txtPositionCode').value;
            //var typeCode = document.getElementById('lblTypeCode').innerText;
            //var typeCode = document.getElementById('txtTypeCode').value;
            var typeCode = evt.parentElement.parentElement.childNodes[3].childNodes[0].innerText;
            //var positionCode = document.getElementById('txtPositionCode').value;
            var positionCode = evt.parentElement.parentElement.childNodes[4].childNodes[0].innerText;
            //var workCode = document.getElementById('txtWorkCode').value;
            var workCode = evt.parentElement.parentElement.childNodes[5].childNodes[0].innerText;
            var serviceBulletinNo = document.getElementById('lblServiceBulletinNo').innerText;

            var url = '/Service/FrmRecallCategoryDetail.aspx?';
            url += 'typeCode=' + typeCode;
            url += '&positionCode=' + positionCode;
            url += '&workCode=' + workCode;
            url += '&actionMode=Delete';
            url += '&strId=' + serviceBulletinNo;
            url += '&recallCategoryDetailId=' + dataId;
            url += '&strId=' + serviceBulletinNo;

            var baseUrl = window.location.href.split('/Service')[0];
            var apiUrl = baseUrl + url;
            window.location = apiUrl;
        }

        function addRecallCategoryDetail() {

            var typeCode = document.getElementById('txtTypeCode').value;
            var positionCode = document.getElementById('txtPositionCode').value;
            var workCode = document.getElementById('txtWorkCode').value;
            var serviceBulletinNo = document.getElementById('lblServiceBulletinNo').innerText;
            //var debug = document.getElementById('lblTypeCode').innerText;

            var url = '/Service/FrmRecallCategoryDetail.aspx?';
            url += 'typeCode=' + typeCode;
            url += '&positionCode=' + positionCode;
            url += '&workCode=' + workCode;
            url += '&actionMode=Add';
            url += '&strId=' + serviceBulletinNo;

            var baseUrl = window.location.href.split('/Service')[0];
            var apiUrl = baseUrl + url;
            window.location = apiUrl;
        }
        function getSelectedCategory(selectedCategory) {
            var index = GetCurrentInputIndex("dgRecallCategoryDetail");
            var dgRecallCategoryDetail = document.getElementById("dgRecallCategoryDetail");
            var tempParams = selectedCategory.split(';');
            var code = dgRecallCategoryDetail.rows[index].getElementsByTagName("INPUT")[1];
            var description = dgRecallCategoryDetail.rows[index].getElementsByTagName("SPAN")[0];

            code.value = tempParams[0];
            description.innerHTML = tempParams[1];
        }

        function getSelectedTypeCode(selectedTypeCode) {
            var index = GetCurrentInputIndex("dgRecallCategoryDetail");
            var dgRecallCategoryDetail = document.getElementById("dgRecallCategoryDetail");
            var tempParams = selectedTypeCode.split(';');
            var code = dgRecallCategoryDetail.rows[index].getElementsByTagName("INPUT")[1];
            var description = dgRecallCategoryDetail.rows[index].getElementsByTagName("SPAN")[0];

            code.value = tempParams[0];
            description.innerHTML = tempParams[1];
        }

        function getSelectedPosition(selectedPosition) {
            var index = GetCurrentInputIndex("dgRecallCategoryDetail");
            var dgRecallCategoryDetail = document.getElementById("dgRecallCategoryDetail");
            var tempParams = selectedPosition.split(';');
            var code = dgRecallCategoryDetail.rows[index].getElementsByTagName("INPUT")[2];

            code.value = tempParams[0];
        }

        function getSelectedWork(selectedWork) {
            var index = GetCurrentInputIndex("dgRecallCategoryDetail");
            var dgRecallCategoryDetail = document.getElementById("dgRecallCategoryDetail");
            var tempParams = selectedWork.split(';');
            var code = dgRecallCategoryDetail.rows[index].getElementsByTagName("INPUT")[3];

            code.value = tempParams[0];
        }

        function GetCurrentInputIndex(GridName) {
            var dtgDamageCode = document.getElementById(GridName);
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;

            for (index = 0; index < dtgDamageCode.rows.length; index++) {
                inputs = dtgDamageCode.rows[index].getElementsByTagName("INPUT");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }
            return -1;
        }

        function getElementId(elementId) {
            var rows = 10 + document.getElementById('dgRecallCategoryDetail').getElementsByTagName("tbody")[0].getElementsByTagName("tr").length
            return 'dgRecallCategoryDetail__ct' + rows + '_' + elementId;
        }

        function showPopupSearchTypeCode(evt) {
            var index = GetCurrentInputIndex("dgRecallCategoryDetail");
            var dgRecallCategoryDetail = document.getElementById("dgRecallCategoryDetail");
            var ddlCategory = dgRecallCategoryDetail.rows[index].getElementsByTagName("SELECT")[0];
            var categoryID = ddlCategory.value;
            showPopUp('../PopUp/PopUpVechileType.aspx?categoryID=' + categoryID, '', 500, 760, getSelectedTypeCode);
        }

        function showPopupSearchPositionCode(evt) {
            var index = GetCurrentInputIndex("dgRecallCategoryDetail");
            var dgRecallCategoryDetail = document.getElementById("dgRecallCategoryDetail");
            var code = dgRecallCategoryDetail.rows[index].getElementsByTagName("INPUT")[1];

            var typeCode = code.value;
            showPopUp('../PopUp/PopUpPositionCodeSelectionWSC.aspx?typeCode=' + typeCode, '', 500, 760, getSelectedPosition);
        }

        function showPopupSearchWorkCode(evt) {
            var index = GetCurrentInputIndex("dgRecallCategoryDetail");
            var dgRecallCategoryDetail = document.getElementById("dgRecallCategoryDetail");
            var code = dgRecallCategoryDetail.rows[index].getElementsByTagName("INPUT")[1];
            var position = dgRecallCategoryDetail.rows[index].getElementsByTagName("INPUT")[2];

            var typeCode = code.value;
            var positionCode = position.value;
            showPopUp('../PopUp/PopUpWorkCode.aspx?positionCode=' + positionCode + '&typeCode=' + typeCode, '', 500, 760, getSelectedWork);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" ms_positioning="GridLayout">
        <div>
            <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">

                <tbody>
                    <tr>
                        <td class="titlePage" colspan="3">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="titlePage">SERVICE – Field Fix Campaign – Assign Kode Posisi</td>
                                </tr>
                                <tr>
                                    <td height="1" background="../images/bg_hor.gif">
                                        <img border="0" src="../images/bg_hor.gif" height="1"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td class="titleField" style="font-size: x-small; font-size: 11px; font-weight: bold; font-family: Sans-Serif, Arial;">Nomor Service Buletin</td>
                                    <td>: </td>
                                    <td>
                                        <asp:Label runat="server" name="lblServiceBulletinNo" ID="lblServiceBulletinNo" Text="Label" Font-Size="Small"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleField" style="font-size: x-small; font-size: 11px; font-weight: bold; font-family: Sans-Serif, Arial;">Deskripsi</td>
                                    <td>: </td>
                                    <td>
                                        <asp:Label ID="lblDescription" runat="server" Text="Label" Font-Size="Small"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleField" style="font-size: x-small; font-size: 11px; font-weight: bold; font-family: Sans-Serif, Arial;">Upload</td>
                                    <td>: </td>
                                    <td>
                                        <input id="infWSCData" style="width: 293px" type="file" size="29" name="File1" runat="server" />
                                        <asp:Button ID="btnUpload" runat="server" Width="70px" Text="Upload"></asp:Button></td>
                                </tr>
                                <tr>
                                    <td class="titleField"></td>
                                    <td></td>
                                    <td>
                                        <asp:LinkButton ID="LnkTemplate" runat="server">Download Template</asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div id="div1" style="overflow: auto; height: 200px">
            <asp:DataGrid ID="dgRecallUpload" TabIndex="99" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CDCDCD"
                CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White" AllowSorting="false">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                <ItemStyle ForeColor="#4A3C8C" BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="Tipe Kendaraan">
                        <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblTypeCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleTypeCodeTemp")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Kode Posisi">
                        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblKodePosisi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PositionCode")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Kerja">
                        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle></ItemStyle>
                        <ItemTemplate>
                            <asp:Label id="lblKodeKerja" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkCode")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:BoundColumn DataField="ErrorMessage" HeaderText="Pesan">
                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                    </asp:BoundColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
            <asp:DataGrid ID="dgRecallCategoryDetail" TabIndex="99" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CDCDCD"
                CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
                <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle></ItemStyle>
                        <FooterTemplate>
                        </FooterTemplate>


                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Model/Tipe">
                        <HeaderStyle Width="200px" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:HiddenField ID="hidRecallCategoryDetailId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ID")%>' />
                            <asp:Label ID="lblModelType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleTypeDescription")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False"></FooterStyle>
                        <FooterTemplate>
                            <input type="hidden" runat="server" id="txtModelType"></input>
                            <asp:Label ID="lblModelTypeDesc" runat="server" Text=''>
                            </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kategori">
                        <HeaderStyle Width="108px" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CategoryMaster.CategoryCode") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList runat="server" ID="ddlModelType" name="SELECT">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Tipe">
                        <HeaderStyle Width="108px" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTypeCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VehicleType.VechileTypeCode")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTypeCode" name="txtTypeCode" Width="84px" runat="server"></asp:TextBox>


                            <asp:Label ID="lblSearchTypeCode" runat="server" TabIndex="0" onclick="showPopupSearchTypeCode();">
												<img src="../images/popup.gif" style="cursor:pointer;" border="0" alt="Klik popup"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Posisi">
                        <HeaderStyle Width="108px" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPositionCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LaborMaster.LaborCode") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPositionCode" name="txtPositionCode" Width="84px" runat="server"></asp:TextBox>


                            <label id="lblSearchPositionCode" tabindex="0" onclick="showPopupSearchPositionCode();">
                                <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Kerja">
                        <HeaderStyle Width="108px" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblWorkCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LaborMaster.WorkCode") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtWorkCode" name="txtWorkCode" Width="84px" runat="server"></asp:TextBox>


                            <label id="lblSearchWorkCode" tabindex="0" onclick="showPopupSearchWorkCode();">
                                <img src="../images/popup.gif" style="cursor: pointer;" border="0" alt="Klik popup"></label>
                        </FooterTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnDeleteCategoryDetail" runat="server" CausesValidation="true" CommandName="delete">

								<img src="../images/trash.gif" border="0" alt="Hapus">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" TabIndex="40" CommandName="add" Text="Tambah" runat="server">

                                    <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <%--
                    <asp:TemplateColumn HeaderText="" Visible="True">
                        <HeaderStyle Width="1px" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:HiddenField   ID="lblRecallCategoryDetailId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
                            </asp:HiddenField>
                        </ItemTemplate>
                        <FooterStyle Wrap="False"></FooterStyle>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                </Columns>
                --%>
                <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
        <p>
            <asp:Button ID="buttonSave" runat="server" Text="Simpan" />
            <asp:Button ID="buttonTambah" runat="server" Text="Tambahkan" />
            <asp:Button ID="buttonBack" runat="server" Text="Kembali" />
        </p>
    </form>

</body>
</html>
