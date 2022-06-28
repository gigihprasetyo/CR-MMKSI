<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDealerSystems.aspx.vb" Inherits=".FrmDealerSystems" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmDealerSystems</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" type="text/javascript">
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
        <div>
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage">ADMIN SISTEM - Dealer Systems</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1">
                        <img alt="" height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10">
                        <img alt="" height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td align="left">
                        <table id="tblSearch" cellspacing="1" cellpadding="2" width="100%" border="0">
                             <tr>
                                <td class="titleField" style="text-decoration-line:underline; text-transform:uppercase;" width="10%">Search / Add</td>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="10%">Dealer Code</td>
                                <td width="1%">:</td>
                                <td width="89%">
                                     <asp:TextBox ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" TextMode="MultiLine" Width="150px" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblSearchDealer" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="10%">System</td>
                                <td width="1%">:</td>
                                <td width="89%">
                                     <asp:DropDownList ID="ddlSystemID" runat="server" Width="150px" AutoPostBack="false"></asp:DropDownList> </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="10%"></td>
                                <td width="1%"></td>
                                <td width="89%">
                                    <asp:Button ID="btnSearch" runat="server" Text="Cari" Width="60px"></asp:Button>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" Width="60px" CausesValidation="False"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        
                        <hr />

                        <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td class="titleField" style="text-decoration-line:underline; text-transform:uppercase;" width="10%">View / Edit</td>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="10%">Dealer Code</td>
                                <td width="1%">:</td>
                                <td width="20%">
                                    <asp:HiddenField id="hdnID" runat="server"/>
                                    <asp:Label ID="lblDealer" runat="server"></asp:Label></td>

                                <td class="titleField" width="70%"><asp:CheckBox ID="chkIsSPKMatchFaktur" runat="server" Text="SPK Match Faktur"></asp:CheckBox></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="10%">System</td>
                                <td width="1%">:</td>
                                <td width="20%">
                                    <asp:DropDownList ID="ddlDetailSystemId" runat="server" Width="150px" AutoPostBack="false"></asp:DropDownList>
                                    &nbsp;<asp:RequiredFieldValidator ID="ValidateDescription" runat="server" ControlToValidate="ddlDetailSystemId" Height="16px"
                                            ErrorMessage="Deskripsi Kategori Harus Diisi!" Width="8px" EnableClientScript="False">*</asp:RequiredFieldValidator></td>

                                <td class="titleField" width="70%"><asp:CheckBox ID="chkIsOnlyUploadPhotoTenagaPenjual" runat="server" Text="Upload Tenaga Penjual"></asp:CheckBox></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="10%">Go Live Date/td>
                                <td width="1%">:</td>
                                <td width="20%">
                                    <cc1:inticalendar id="icGoLiveDate" runat="server" TextBoxWidth="110"></cc1:inticalendar>
                                </td>

                                <td class="titleField" width="70%"><asp:CheckBox ID="chkIsSPKDNet" runat="server" Text="SPK DNet"></asp:CheckBox></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="10%"></td>
                                <td width="1%"></td>
                                <td width="20%">
                                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="60px" Visible="false"></asp:Button>
                                    <asp:Button ID="btnBatal" runat="server" Text="Batal" Width="60px" CausesValidation="False" Visible="false"></asp:Button></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="5"></td>
                </tr>
                <tr>
                    <td valign="top">
                        <div id="div1" style="height: 390px; overflow: auto">
                            <asp:DataGrid ID="dtgDealerSystems" runat="server" Width="100%" PageSize="10" AllowSorting="True" CellSpacing="1"
                                AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD"
                                CellPadding="3" GridLines="None">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="1%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Dealer Code" SortExpression="Dealer.DealerCode">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDealerCode"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="System" SortExpression="SystemID">
                                        <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSystem"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="isSPKMatchFaktur" HeaderText="SPK Match Faktur" SortExpression="isSPKMatchFaktur">
                                        <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="isOnlyUploadPhotoTenagaPenjual" HeaderText="Upload Tenaga Penjual" SortExpression="isOnlyUploadPhotoTenagaPenjual">
                                        <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="isSPKDNet" HeaderText="SPK DNet" SortExpression="isSPKDNet">
                                        <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn SortExpression="GoLiveDate" HeaderText="Go Live Date">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# IIf(DataBinder.Eval(Container, "DataItem.GoLiveDate", "{0:dd/MM/yyyy}") = "01/01/1753", "", DataBinder.Eval(Container, "DataItem.GoLiveDate", "{0:dd/MM/yyyy}"))%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnView" runat="server" Text="Lihat" CommandName="View" CausesValidation="False">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>                                            
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td height="40"></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
