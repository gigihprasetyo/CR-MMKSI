<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmFieldFixRequestPart.aspx.vb" Inherits=".FrmFieldFixRequestPart" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">

        function showPopupSearchPartCode(evt) {
            var hdnIndexSelectedGridPart = document.getElementById("hdnIndexSelectedGridPart");
            var idx = getRowIndex(evt);
            hdnIndexSelectedGridPart.value = idx

            var index = GetCurrentInputIndex("dgMasterPart");
            var dgParts = document.getElementById("dgMasterPart");
            var codePart = dgParts.rows[index].getElementsByTagName("INPUT")[0];
            if (codePart.disabled == true || dgParts.disabled == true) { return; }
            showPopUp('../PopUp/PopUpPartsForecastMaster.aspx', '', 500, 760, getSelectedPartsCode);
        }

        function getRowIndex(el) {
            while ((el = el.parentNode) && el.nodeName.toLowerCase() !== 'tr');

            if (el)
                return el.rowIndex;
        }

        function getSelectedPartsCode(selectedPart) {
            var hdnIndexSelectedGridPart = document.getElementById("hdnIndexSelectedGridPart");
            var index = hdnIndexSelectedGridPart.value;
            //var index = GetCurrentInputIndex("dgMasterPart");
            var dgParts = document.getElementById("dgMasterPart");
            var tempParams = selectedPart.split(';');
            var codePart = dgParts.rows[index].getElementsByTagName("INPUT")[0];
            var namePart = dgParts.rows[index].getElementsByTagName("SPAN")[1];
            codePart.value = tempParams[0];
            namePart.innerHTML = tempParams[1];
            var noBulletin = dgParts.rows[index].getElementsByTagName("SPAN")[2];
            noBulletin.innerHTML = tempParams[2];
        }

        function GetCurrentInputIndex() {
            var dgPODetail = document.getElementById("dgMasterPart");
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;

            for (index = 0; index < dgPODetail.rows.length; index++) {
                inputs = dgPODetail.rows[index].getElementsByTagName("INPUT");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }

            return -1;
        }

        function SparePart(selectedCode) {
            var tempParam = selectedCode.split(';');
            var indek = GetCurrentInputIndex();
            var dgPODetail = document.getElementById("dgMasterPart");
            //var partCode = dgPODetail.rows[indek].getElementsByTagName("INPUT")[0];
            //var partName = dgPODetail.rows[indek].getElementsByTagName("SPAN")[1];
            var lblPartName = document.getElementById("lblNoPO");

            //partCode.value = tempParam[0];
            //partName.innerHTML = tempParam[1];
            lblPartName.innerHTML = tempParam[1];
            //	console.log(selectedCode);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" ms_positioning="GridLayout">
        <div>
            <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="titlePage">Field Fix Campaign - Request Part Order</td>
                </tr>
                <tr>
                    <td height="1" background="../images/bg_hor.gif">
                        <img border="0" src="../images/bg_hor.gif" height="1"></td>
                </tr>
                <tr>
                    <td height="10">
                        <img border="0" src="../images/dot.gif" height="1"></td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                        <table id="Table2" border="0" cellspacing="2" cellpadding="1" width="100%">
                            <tr>
                                <td class="titleField">Kode Dealer</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                                <asp:HiddenField ID="hdnIndexSelectedGridPart" runat="server" />
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField">Nama Dealer</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField">Alamat Lengkap</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblAlamat" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField">Kode Pos</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblKodePos" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField">PO Number</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblNoPO" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdID" runat="server" />
                                    <asp:HiddenField ID="HdDate" runat="server" />
                                    <asp:HiddenField ID="HDStatus" runat="server" />
                                    <asp:HiddenField ID="HdPo" runat="server" />
                                    <asp:HiddenField ID="HdDtStart" runat="server" />
                                    <asp:HiddenField ID="HdDtEnd" runat="server" />
                                </td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField">&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <div style="overflow: auto; height: 180px; width: 85%"  id="div1">
                            <asp:DataGrid ID="dgMasterPart" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="3" BackColor="#E0E0E0"
                                AllowSorting="false" AllowCustomPaging="True" PageSize="50"
                                AllowPaging="True" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Nomor Part">
                                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodeParts" runat="server" Visible="true">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodePartsFooter"
                                                runat="server" Width="80%" TabIndex="0" ></asp:TextBox>
                                            <asp:Label ID="lblSearchPartsFooter" runat="server" TabIndex="0" onclick="showPopupSearchPartCode(this);">
												            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Nama Part">
                                        <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNamaParts" runat="server" ></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Wrap="False"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:Label ID="lblNamaPartsFooter" runat="server"><span style="font-size: 8pt"></span></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="No Service Bulletin">
                                        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoBulletin" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>                                        
                                        <FooterStyle Wrap="False"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:Label ID="lblNoBulletinFooter" runat="server"><span style="font-size: 8pt"></span></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="Quantity" HeaderText="Jumlah Permintaan">
                                        <HeaderStyle Width="10%" CssClass="titleTableService" HorizontalAlign="Right"></HeaderStyle>
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblQtyItem" runat="server" HorizontalAlign="Right" Visible="false">
                                            </asp:Label>--%>
                                            <asp:TextBox ID="txtQtyItem" runat="server" onkeypress="return NumericOnlyWith(event,'');" Width="100%">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtQtyFooter" runat="server" onkeypress="return NumericOnlyWith(event,'');" 
                                                onkeyup="pic(this,this.value,'9999999999','N')" HorizontalAlign="Right" Width="100%">
                                            </asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">
												            <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkbtnDeleteParts" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                            <%--<asp:LinkButton ID="lnkbtnAddEditParts" runat="server" CommandName="AddEdit" CausesValidation="False" Visible="false">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>--%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnAddParts" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="False">
                                        <HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIDItem" runat="server"> </asp:Label>
                                            <asp:Label ID="lblDateItem" runat="server"> </asp:Label>
                                            <asp:Label ID="lblStatusItem" runat="server"> </asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblIDFooter" runat="server"> </asp:Label>
                                            <asp:Label ID="lblDateFooter" runat="server"> </asp:Label>
                                            <asp:Label ID="lblStatusFooter" runat="server"> </asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px" align="left"></td>
                </tr>
                <tr>
                    <td width="75%">
                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="60px" Enabled="False"></asp:Button>&nbsp;
                        <asp:Button ID="btnValidasi" runat="server" Text="Validasi" Width="60px" Enabled="False"></asp:Button>&nbsp;
                        <asp:Button ID="btnKembali" runat="server" Text="Kembali" Width="60px" Visible="False"></asp:Button>&nbsp;
                    </td>

                </tr>
            </table>
        </div>
    </form>
</body>
</html>
