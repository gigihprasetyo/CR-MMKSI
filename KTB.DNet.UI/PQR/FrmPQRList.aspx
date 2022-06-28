<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPQRList.aspx.vb" Inherits="FrmPQRList" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmPQRList</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">

        function ShowPPDealerBranchSelection() {
            var lblDealer = document.getElementById("lblKodeDealer");
            var dealerText = lblDealer.textContent || lblDealer.innerText;
            if (dealerText == '') {
                var txtDealerSelection = document.getElementById("txtKodeDealer");
                dealerText = txtDealerSelection.value;
            }
            var dealerCode = dealerText.split("-")[0].replace(/\s/g, '');
            showPopUp('../General/../PopUp/PopUpDealerBranchMultipleSelection.aspx?DealerCode=' + dealerCode, '', 500, 760, DealerBranchSelection);
        }

        function DealerBranchSelection(selectedDealerBranch) {
            var txtDealerBranchSelection = document.getElementById("txtKodeDealerBranch");
            txtDealerBranchSelection.value = selectedDealerBranch;
        }

        function ShowPPDealerSelection() {
            showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam[0];
        }
        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
            }
        }
        function PopUpHistory() {
        }


        function GetSelectedPartsCode(selectedCode) {
            var tempParam = selectedCode.split(';');
            var txtPartSelection = document.getElementById("txtKodePart");
            var txtDescPartSelection = document.getElementById("lblDescPart");
            txtPartSelection.value = tempParam[0];
            txtDescPartSelection.innerHTML = tempParam[1];
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">PRODUCT QUALITY REPORT -&nbsp; Daftar PQR</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblDealerSearch" runat="server">Dealer</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="34%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
                                    runat="server" MaxLength="10"></asp:TextBox><asp:Label ID="lblKodeDealer" runat="server"></asp:Label><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label></td>
                            <td class="titleField" width="19%">
                                <asp:Label ID="lblStat" runat="server">Status</asp:Label></td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Cabang</td>
                            <td></td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealerBranch" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
                                    runat="server" MaxLength="10"></asp:TextBox><asp:Label ID="lblDealerBranch" runat="server"></asp:Label><asp:Label ID="lblPopUpDealerBranch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label></td>
                            <td class="titleField">Kategori</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlKategori" AutoPostBack="true" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">
                                <asp:Label ID="lblPQRNoSearch" runat="server">No PQR</asp:Label></td>
                            <td style="height: 18px">:</td>
                            <td style="height: 18px">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtPQRNo" onblur="omitSomeCharacter('txtPQRNo','<>?*%$;')"
                                    runat="server" MaxLength="25"></asp:TextBox></td>
                            <td class="titleField" style="height: 18px">Sub Kategori</td>
                            <td style="height: 18px">:</td>
                            <td style="height: 18px">
                                <asp:DropDownList ID="ddlTipe" runat="server" Width="120px"></asp:DropDownList></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" height="20">
                                <asp:Label ID="lblTglApplySearch" runat="server">Tgl Pembuatan</asp:Label></td>
                            <td>:</td>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="top">
                                        <td>
                                            <asp:CheckBox ID="chkFilterTanggal" runat="server" Checked="True"></asp:CheckBox></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icTglApplyDari" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                        <td valign="bottom">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icTglApplySampai" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="titleField" style="width: 120px; height: 24px">Kode Part</td>
                            <td style="width: 2px; height: 24px">:</td>
                            <td style="height: 24px; width: 220px">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodePart" onblur="omitSomeCharacter('txtKodePart','<>?*%$;')"
                                    runat="server" MaxLength="20"></asp:TextBox><asp:Label ID="lblSearchPart" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label><asp:Label ID="lblDescPart" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Kode Posisi</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtKodePosisi" onblur="omitSomeCharacter('txtKodePosisi','<>?*%$;')"
                                    runat="server" MaxLength="50" Width="132px"></asp:TextBox></td>

                            <td class="titleField">Dibuat Oleh</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlCreator" runat="server"></asp:DropDownList><br />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblProcessBy" runat="server">Di Proses Oleh</asp:Label></td>
                            <td>
                                <asp:Label ID="lbltitik2" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtProcessBy" onblur="omitSomeCharacter('txtProcessBy','<>?*%$;')"
                                    runat="server" MaxLength="50"></asp:TextBox></td>
                            <td class="titleField" style="height: 18px">&nbsp;</td>
                            <td style="height: 18px">&nbsp;</td>
                            <td style="height: 18px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label1" runat="server">Jenis PQR</asp:Label></td>
                            <td>:</td>
                            <td style="height: 18px">
                                <asp:DropDownList ID="ddlPqrType" runat="server"></asp:DropDownList></td>
                            <td class="titleField" style="height: 18px" colspan="3"></td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgListPQR" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="50" AllowPaging="True" BorderStyle="None" DataKeyField="ID">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableService"></HeaderStyle>
                        <HeaderTemplate>
                            <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkSelection',
														document.forms[0].chkAllItems.checked)" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelection" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="RowStatus" HeaderText="Status">
                        <HeaderStyle ForeColor="White" CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")  %>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' ID="lblDealer" NAME="lblDealer">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Cabang">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.Name")  %>' Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.DealerBranchCode")%>' ID="lblDealerBranch" NAME="lblDealerBranch">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="PQRNo" SortExpression="PQRNo" HeaderText="Nomor PQR">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="PQRType" HeaderText="Jenis PQR">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPQRType" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="DocumentDate" SortExpression="DocumentDate" HeaderText="Tgl Pembuatan"
                        DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn SortExpression="FinishDate" HeaderText="Tgl Selesai">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTglSelesai"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="No Rangka">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>' ID="lblNoChassis" NAME="lblNoChassis">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="Category.CategoryCode" HeaderText="Model">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.CategoryCode") %>' ID="lblCategory" NAME="lblCategory">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="Subject" SortExpression="Subject" HeaderText="Subject">
                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Kode Posisi">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblKodePosisi"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Kode Part">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblKodePart"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn SortExpression="ConfirmBy" HeaderText="Diproses Oleh">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblProcessUser"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="CreatedBy" HeaderText="Dibuat Oleh">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCreatedUser"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Pesan">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnAdditionalInfoIcon" runat="server" Width="20px" CausesValidation="False" CommandName="AdditionalInfo" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                <img id="img" runat="server" src="../images/edit.gif" border="0">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Buat WSC">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEditWSC" runat="server" Width="20px" CausesValidation="False" CommandName="BWSC" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                <img alt="" id="imgEditWSC" runat="server" src="../images/dok.gif" border="0" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="PDF" Visible="false">
                        <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownloadPDF" runat="server" CommandName="lnkDownloadPDF" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                <img src="../images/download.gif" style="cursor:hand" border="0" alt="Download PDF">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Lampiran">
                        <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownloadLampiran" runat="server" CommandName="lnkDownloadLampiran" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                <img src="../images/download.gif" style="cursor:hand" border="0" alt="Download Lampiran">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Edit">
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
									<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
									<img alt="Lihat" src="../images/detail.gif" border="0"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Hapus" Visible="false" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
									<img alt="Hapus" src="../images/trash.gif" border="0"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblHistoryStatus" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="History Perubahan Status"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
        <br>
        <asp:Button ID="btnDownload" runat="server" Width="72px" Text="Download"></asp:Button><asp:Panel class="titleField" ID="pnlChangeStatus" Visible="False" runat="server" Height="1px">
            Mengubah Status : 
            <asp:DropDownList ID="ddlStatus2" runat="server" Width="140" Visible="False"></asp:DropDownList>
            <asp:Button ID="btnSave" runat="server" Width="60px" Text="Simpan" Visible="False"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp; 
        </asp:Panel>
    </form>
</body>
</html>
