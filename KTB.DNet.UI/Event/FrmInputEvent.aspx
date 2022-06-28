<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputEvent.aspx.vb" Inherits=".FrmInputEvent" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmInputEvent</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPopUpTO() {
            var txtDealerCode = document.getElementById("txtDealerCode");
            var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            showPopUp('../PopUp/PopUpDealerBranchEventMultipleSelection.aspx?DealerCode=' + txtDealerCode.value + '&DealerBranchCode=' + txtTemporaryOutlet.value + '&f=event', '', 430, 800, TemporaryOutlet);
        }

        function ShowPopUpDealer() {
            var ddlEventCategory = document.getElementById("ddlEventCategory");
            var hdnMode = document.getElementById("hdnMode");
            var hdnEventDealerHeaderID = document.getElementById("hdnEventDealerHeaderID");
            var index = ddlEventCategory.selectedIndex;
            var valCategory = ddlEventCategory.options[index].value;
            var txtDealerCode = document.getElementById("txtDealerCode");
            showPopUp('../PopUp/PopUpDealerSelectionEvent.aspx?DealerCode=' + txtDealerCode.value + '&Category=' + valCategory + '&Mode=' + hdnMode.value + '&EventDealerHeaderID=' + hdnEventDealerHeaderID.value, '', 430, 800, GetDealer);
        }

        function TemporaryOutlet(selectedRefNumber) {
            //var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            //var hdnTemporaryOutlet = document.getElementById("hdnTemporaryOutlet");
            //hdnTemporaryOutlet.value = selectedRefNumber;

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnTemporaryOutlet.blur();
            //}
            //else {
            //    hdnTemporaryOutlet.onchange();
            //}
            var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            txtTemporaryOutlet.value = selectedRefNumber;
        }

        function GetDealer(selectedRefNumber) {
            var hdnDealerCode = document.getElementById("hdnDealerCode");
            hdnDealerCode.value = selectedRefNumber;

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnDealerCode.blur();
            //}
            //else {
            //    hdnDealerCode.onchange();
            //}
            var txtDealerCode = document.getElementById("txtDealerCode");
            txtDealerCode.value = selectedRefNumber;
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="hdnMode" Value="New" runat="server" />
        <asp:HiddenField ID="hdnEventDealerHeaderID" Value="0" runat="server" />
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 18px">
                    <asp:Label ID="lblTitle" runat="server" Text="EVENT - INPUT EVENT DEALER"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" width="50%" border="0">
                        <tr>
                            <td class="titleField">Kategori Dealer</td>
                            <td width="4px">
                                <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                            <td style="height: 10px">
                                <asp:DropDownList ID="ddlEventCategory" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 30px; width: 30%">Kode Dealer</td>
                            <td style="height: 10px" width="4px">
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td style="height: 10px">
                                <asp:TextBox ID="txtDealerCode" runat="server" Width="200px" TextMode="MultiLine" Height="30px" Enabled="false"></asp:TextBox>
                                <asp:HiddenField ID="hdnDealerCode" runat="server" />
                                <asp:label ID="lblPopUpDealer" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 30px">Temporary Outlet</td>
                            <td style="height: 10px" width="4px">
                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                            <td style="height: 10px">
                                <asp:TextBox ID="txtTemporaryOutlet" runat="server" Width="200px" TextMode="MultiLine" Height="30px" Enabled="false"></asp:TextBox>
                                <asp:HiddenField ID="hdnTemporaryOutlet" runat="server" />
                                <asp:label ID="lblPopUpTO" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 30px">Periode</td>
                            <td style="height: 10px" width="4px">
                                <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                            <td style="height: 10px">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icDatePeriodeStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icDatePeriodeAkhir" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 30px">Nama Event</td>
                            <td style="height: 10px" width="4px">
                                <asp:Label ID="Label2" runat="server">:</asp:Label>
                            </td>
                            <td style="height: 10px">
                                <asp:TextBox ID="txtEventName" runat="server" Width="128px" />
                            </td>
                        </tr>
                        <tr id="trJmlSubsidi" runat="server">
                            <td class="titleField" style="height: 30px">Jumlah Subsidi</td>
                            <td style="height: 10px" width="4px">
                                <asp:Label ID="Label4" runat="server">:</asp:Label>
                            </td>
                            <td style="height: 10px">
                                <asp:TextBox ID="txtTargetBudget" Style="text-align: right" runat="server" Width="128px" 
                                    onkeypress="return NumericOnlyWith(event,'');" 
                                    onkeyup="pic(this,this.value,'9999999999','N')" />
                            </td>
                        </tr>
                        <tr id="trMaxSubsidi" runat="server">
                            <td class="titleField" style="height: 30px">Max Subsidi</td>
                            <td style="height: 10px" width="4px">
                                <asp:Label ID="Label5" runat="server">:</asp:Label>
                            </td>
                            <td style="height: 10px">
                                <asp:TextBox ID="txtMaxSubsidy" MaxLength="5" Style="text-align: right" runat="server" Width="45px" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" />
                                <asp:Label ID="Label6" runat="server" style="vertical-align: central; margin-left: 5px" Height="20px">%</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td class="titleField" style="height: 30px;" colspan="3">Petunjuk Pelaksanaan Acara :</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:DataGrid ID="dgEventDealerDocument" runat="server" Width="70%" AutoGenerateColumns="False" ShowFooter="True">
                        <ItemStyle ForeColor="Black" BackColor="White" Height="30px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        <FooterStyle ForeColor="Black" BackColor="White" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle ForeColor="White" Width="4%" CssClass="titleTablePromo" HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="EditlblNo" runat="server"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Dokumen">
                                <HeaderStyle ForeColor="White" Width="30%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblFileName" runat="server" Text="FileName"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="EditlblFileName" runat="server" Width="95%"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFileName" runat="server" Width="95%"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle ForeColor="White" Width="35%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.FileName")%>'>
                                        <asp:Label ID="lblFile" runat="server" Text="File Cache"></asp:Label>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblEditFileUploadEvent" runat="server" Text="File Cache"></asp:Label>
                                    <input id="EditFileUploadEvent" type="file" runat="server" style="width: 95%;" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <input id="FileUploadEvent" type="file" runat="server" style="width: 95%;" />
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">
												            <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Save" CausesValidation="False">
												            <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkbtnAdd" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="height: 30px;" colspan="3">Dokumen Laporan Acara yang Harus diUpload :</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:DataGrid ID="dgEventDealerRequiredDoc" runat="server" Width="50%" AutoGenerateColumns="False" ShowFooter="True">
                        <ItemStyle ForeColor="Black" BackColor="White" Height="30px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        <FooterStyle ForeColor="Black" BackColor="White" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle ForeColor="White" Width="3%" CssClass="titleTablePromo" HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Dokumen">
                                <HeaderStyle ForeColor="White" Width="45%" CssClass="titleTablePromo" HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblFileEvent" runat="server" Text="File Cache"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFileEvent" runat="server" Width="95%"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle ForeColor="White" Width="3%" CssClass="titleTablePromo" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnDeleteParts" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkbtnAddParts" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												            <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSimpan" OnClientClick="return confirm('Anda yakin mau simpan?');" Text="Simpan" runat="server" Style="margin-top: 20px" />
                    <asp:Button ID="btnBack" runat="server" Text="Kembali" Visible="false"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
