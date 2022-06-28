<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmFieldFixPartOrderConfirmationList.aspx.vb" Inherits=".FrmFieldFixPartOrderConfirmationList" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daftar Kategori Field Fix Campaign</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerCode = document.getElementById("txtKdDealer");
            txtDealerCode.value = selectedDealer;
        }

        function ShowPPSparePartSelection() {
            showPopUp('../PopUp/PopUpPartConfirmationList.aspx', '', 500, 800, PartSelection);
        }

        function PartSelection(selectedPart) {
            var tempParam = selectedPart.split(';');
            var txtPartSelection = document.getElementById("txtPartNo");
            var lblPartName = document.getElementById("lblPartName");
            //var TextDealerName = document.getElementById("txtPartName");
            txtPartSelection.value = tempParam;
            lblPartName.innerHTML = tempParam;
            //TextDealerName.value = tempParam[1];
        }

        function ShowPPPOSelection() {
            showPopUp('../PopUp/PopUpPOConfirmationList.aspx', '', 500, 800, POSelection);
        }

        function POSelection(selectedPo) {
            var tempParam = selectedPo.split(';');
            var txtNoPO = document.getElementById("txtNoPO");
            txtNoPO.value = tempParam;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" ms_positioning="GridLayout">
        <div>
            <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="titlePage">Field Fix Campaign - List Permintaan</td>
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
                            <tbody>

                                <tr>
                                    <td class="titleField">Kode Dealer</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:TextBox ID="txtKdDealer" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                            Width="200px"></asp:TextBox>

                                        <asp:Label ID="Label1" runat="server" onclick="ShowPPDealerSelection();">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField" width="24%">Nomor Part</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:TextBox ID="txtPartNo" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                            onblur="omitSomeCharacter('txtPartName','<>?*%$')" Width="200px"></asp:TextBox>

                                        <asp:Label ID="lblSearchPart" runat="server" onclick="ShowPPSparePartSelection();">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField" width="24%">Nomor PO</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:TextBox ID="txtNoPO" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                            onblur="omitSomeCharacter('txtNoPO','<>?*%$')" Width="200px"></asp:TextBox>

                                        <asp:Label ID="Label2" runat="server" onclick="ShowPPPOSelection();">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label>
                                    </td>
                                </tr>

                                <tr style="display: none;">
                                    <td class="titleField">Nama Part</td>
                                    <td width="1%">:</td>
                                    <td>
                                        <asp:Label ID="lblPartName" runat="server"></asp:Label></td>
                                    <td></td>
                                    <td></td>
                                    <td width="25%"></td>
                                </tr>

                                <tr>
                                    <td class="titleField">Tanggal Permintaan</td>
                                    <td width="1%">:</td>

                                    <td style="height: 20px; white-space: nowrap;" width="75%">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkTglReq" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    <cc1:IntiCalendar ID="icStartDateReq" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                <td>
                                                &nbsp;s.d&nbsp;            
                                                <td>
                                                    <cc1:IntiCalendar ID="icEndDateReq" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField">Tanggal Pengiriman</td>
                                    <td width="1%">:</td>

                                    <td style="height: 20px; white-space: nowrap;" width="75%">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkTglKirim" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    <cc1:IntiCalendar ID="icStartDateKirim" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                <td>
                                                &nbsp;s.d&nbsp;            
                                                <td>
                                                    <cc1:IntiCalendar ID="icEndDateKirim" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField">Status</td>
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" AutoPostBack="true" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td width="75%">
                                        <asp:Button ID="btnCancel" runat="server" Width="64px" Text="Batal" CausesValidation="False"></asp:Button>
                                        <asp:Button ID="btnSearch" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:Button>
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value="-1" />

                                    </td>
                                </tr>
                            </tbody>

                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div id="div1" style="height: 360px; overflow: auto">
                            <asp:DataGrid ID="dtgSentPart" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="3" BackColor="#E0E0E0"
                                AllowSorting="false" AllowCustomPaging="True" PageSize="50"
                                AllowPaging="True" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                                <FooterStyle ForeColor="Black" BackColor="#ededed"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label runat="server" ID="lblNoEdit"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Kode Dealer">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliKdDealer" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbliKdDealerEdit" runat="server">
                                            </asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Nomor Part">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliPartNo" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbliPartNoEdit" runat="server">
                                            </asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Nama Part">
                                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliPartName" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbliPartNameEdit" runat="server">
                                            </asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="No PO ">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNOPo" runat="server"> </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblNOPoEdit" runat="server">
                                            </asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="No Service Bulletin ">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliNoBulletin" runat="server"> </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbliNoBulletinEdit" runat="server">
                                            </asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Tgl Permintaan">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequestDate" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblRequestDateEdit" runat="server">
                                            </asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Jumlah Permintaan">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequestAmount" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblRequestAmountEdit" runat="server">
                                            </asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Jumlah Disetujui">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblJumlahDisetujui" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtJumlahDisetujuiEdit" runat="server" onkeypress="return NumericOnlyWith(event,'');">
                                            </asp:TextBox>
                                            <asp:Label ID="lblJumlahDisetujuiEdit" runat="server">
                                            </asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Status">
                                        <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliStatus" runat="server"> </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbliStatusEdit" runat="server"> </asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Tanggal Pengiriman">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPengirimanDate" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <cc1:IntiCalendar ID="icPengirimanDateEdit" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            <asp:Label ID="lblPengirimanDateEdit" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No AWB">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoAWB" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNoAWBEdit" runat="server">
                                            </asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="50px" ForeColor="White" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">
												            <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnInactive" CausesValidation="False" runat="server" CommandName="Inactive" ToolTip="Batalkan Permintaan" OnClientClick="return confirm('Anda yakin mau membatalkan permintaan?');" >
												<img border="0" src="../images/in-aktif.gif" alt="Batalkan Permintaan" style="cursor:hand"></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="lnkbtnSave" runat="server" CommandName="Save" CausesValidation="False">
												            <img src="../images/download.gif" border="0" alt="Simpan"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" TabIndex="50" CommandName="Cancel" Text="Batal" runat="server">
                                                            <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px" align="left"></td>
                </tr>
                <tr>
                    <td width="75%">
                        <asp:Button ID="btnDownload" runat="server" Text="Download" Enabled="true"></asp:Button>&nbsp;
                    </td>

                </tr>
            </table>
        </div>
    </form>
</body>
</html>
