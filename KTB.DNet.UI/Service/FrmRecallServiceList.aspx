<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmRecallServiceList.aspx.vb" Inherits=".FrmRecallServiceList" SmartNavigation="false" %>

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
            var txtDealerCode = document.getElementById("txtDealerCode");
            txtDealerCode.value = selectedDealer;
        }


        function ShowPPRecallCategorySelection() {
            showPopUp('../General/../PopUp/PopUpReacallCategorySelection.aspx', '', 500, 760, RecallCategorySelection);
        }

        function RecallCategorySelection(selectedRecallRegNo) {
            var RecallRegNo = document.getElementById("txtRecallRegNo");
            RecallRegNo.value = selectedRecallRegNo;
        }

        function ShowPPNoRangkaSelection() {
            showPopUp('../General/../PopUp/PopUpChassisMasterMultiSelection.aspx', '', 500, 760, NoRangkaSelection);
        }

        function NoRangkaSelection(selectedNoRangka) {
            var txtNoRangka = document.getElementById("txtNorangka");
            txtNoRangka.value = selectedNoRangka;
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <div>

            <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                <tr>
                    <td class="titlePage" colspan="2">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="titlePage">SERVICE - Field Fix Campaign - Daftar</td>
                            </tr>
                            <tr>
                                <td background="../images/bg_hor.gif" height="1">
                                    <img height="1" src="/images/bg_hor.gif" border="0"></td>
                            </tr>
                            <tr>
                                <td height="10">
                                    <img height="1" src="../images/dot.gif" border="0"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tbody>

                            <tr>
                                <td class="titleField" width="24%">Kode Dealer</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtDealerCode" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        onblur="omitSomeCharacter('txtDealerName','<>?*%$')" Width="200px"></asp:TextBox>

                                    <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label>

                                </td>
                            </tr>

                            <tr>
                                <td class="titleField" width="24%">Field Fix Reg No</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtRecallRegNo" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        onblur="omitSomeCharacter('txtRecallRegNo','<>?*%$')" Width="200px"></asp:TextBox>

                                    <asp:Label ID="lblRecallRegNo" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label>

                                </td>
                            </tr>



                            <tr>
                                <td class="titleField" width="24%">No Rangka</td>
                                <td width="1%">:</td>
                                <td width="75%">                                    
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtNorangka" Width="400px"
                                        runat="server" ></asp:TextBox>

                                    <asp:Label ID="lblNoRangka" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label>

                                    <asp:CheckBox ID="chkChassisBB" Checked="true" runat="server" Text="Chassis BB"/> 

                                </td>
                            </tr>


                            <tr>
                                <td class="titleField" width="24%"><asp:CheckBox ID="chkServiceDate" Checked="true" runat="server" Text="Tanggal Service"/> </td>
                                <td width="1%">:</td>

                                <td style="height: 20px; white-space: nowrap;" width="75%">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <cc1:inticalendar id="icStartDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                            <td>&nbsp;s.d&nbsp;</td>
                                            <td>
                                                <cc1:inticalendar id="icEndDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        </tr>
                                    </table>


                                </td>
                            </tr>

                             <tr  >
                                <td class="titleField" width="24%" ><asp:CheckBox ID="chkInputDate" Checked="false" runat="server" Text="Tanggal Input"/></td>
                                <td width="1%">:</td>

                                <td style="height: 20px; white-space: nowrap;" width="75%">
                                    <asp:HiddenField ID="hdnOK" Value="false" runat="server" />
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <cc1:inticalendar id="ccStart" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                            <td>&nbsp;s.d&nbsp;</td>
                                            <td>
                                                <cc1:inticalendar id="ccEnd" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        </tr>
                                    </table>


                                </td>
                            </tr>


                           
                            <tr>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td colspan="2"></td>
                                <td width="75%">
                                    &nbsp;
											&nbsp;
											<asp:Button ID="btnSearch" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </tr>
                <tr valign="top">
                    <td>
                        <div id="div1" style="height: 360px; overflow: auto">
                            <asp:DataGrid ID="dtgRecallService" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="3" BackColor="#E0E0E0" AllowSorting="True" AllowCustomPaging="True" PageSize="50"
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
                                    <asp:TemplateColumn Visible="false">
                                        <HeaderStyle Width="3%" CssClass="titleTableService" ></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderTemplate>
                                            <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxesNew('ChkExport', document.all.chkAllItems.checked)"
                                                type="checkbox" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkExport" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="No" >
                                        <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>


                                    <asp:TemplateColumn Visible="true" HeaderText="Kode Dealer">
                                        <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliKodeDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="Nama Dealer">
                                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="No Rangka ">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblinoRangka" runat="server" >
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>


                                    <asp:TemplateColumn Visible="true" HeaderText="Field Fix Reg No">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblinoreg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallChassisMaster.RecallCategory.RecallRegNo")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="Deskripsi">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallChassisMaster.RecallCategory.Description")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                     
                                    <asp:BoundColumn DataField="MileAge" HeaderText="Jarak Tempuh (KM)" Visible="true"  DataFormatString="{0:#,##0}" ItemStyle-HorizontalAlign="Right">
										<HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Right"></HeaderStyle>
									</asp:BoundColumn>

                                        <asp:BoundColumn ItemStyle-HorizontalAlign="Center" DataField="ServiceDate" HeaderText="Tanggal Service" Visible="true"  DataFormatString="{0:dd-MM-yyyy}">
										<HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>

                                       <asp:BoundColumn DataField="CreatedTime" ItemStyle-HorizontalAlign="Center" HeaderText="Tanggal Input" Visible="true"  DataFormatString="{0:dd-MM-yyyy}">
										<HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>



                                    <asp:TemplateColumn Visible="true">
                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
                                                CommandName="View" visible="false">
													<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" visible="false">
													<img src="../images/edit.gif" border="0" alt="Ubah" ></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False"
                                                CommandName="Delete">
													<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td><input type="hidden" id="hdn1" /></td>
                </tr>
                <tr>
                    <td><asp:Button ID="btnDownload" runat="server" Text="Download" Visible="false"/></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
