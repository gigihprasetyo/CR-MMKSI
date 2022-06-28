<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmFieldFixStockControl.aspx.vb" Inherits=".FrmFieldFixStockControl" %>

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

        var popupFillIndex = 0

        function ShowPPSparePartSelection() {
            showPopUp('../PopUp/PopUpSparepartSelectionOne.aspx', '', 500, 800, PartSelection);
        }

        function PartSelection(selectedPart) {
            var tempParam = selectedPart.split(';');
            var txtPartSelection = document.getElementById("txtPartNo");
            var lblPartName = document.getElementById("lblPartName");
            //var TextDealerName = document.getElementById("txtPartName");
            txtPartSelection.value = tempParam[0];
            lblPartName.innerHTML = tempParam[1];
            //TextDealerName.value = tempParam[1];
        }

        function ShowPPRecallCategorySelection() {
            showPopUp('../General/../PopUp/PopUpReacallCategorySelection.aspx', '', 500, 760, RecallCategorySelection);
        }

        function RecallCategorySelection(selectedRecallRegNo) {
            var RecallRegNo = document.getElementById("txtRecallRegNo");
            RecallRegNo.value = selectedRecallRegNo;
        }

        function ShowPPNoBulletinSelection() {
            showPopUp('../PopUp/PopUpServiceBulletinMultiSelection.aspx', '', 500, 600, InfoDokumenSBSelection);
        }

        function InfoDokumenSBSelection(selectedDoc) {
            var txtNoBuletin = document.getElementById("txtNoBuletin");
            txtNoBuletin.value = selectedDoc;
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
                                <td class="titlePage">SERVICE - Field Fix Campaign - Management Stock Control</td>
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
                                <td class="titleField" width="24%">Nomor Part</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtPartNo" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        onblur="omitSomeCharacter('txtPartName','<>?*%$')" Width="200px"></asp:TextBox>

                                    <asp:Label ID="lblSearchPart" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td class="titleField">Nama Part</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblPartName" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="24%">No Reg </td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtRecallRegNo" runat="server" size="22" onkeypress="alphaNumericExcept"
                                        onblur="omitSomeCharacter('txtRecallRegNo','<>?*%$')" Width="400px"></asp:TextBox>

                                    <asp:Label ID="lblRecallRegNo" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label>

                                </td>
                            </tr>

                            <tr>
                                <td class="titleField" width="24%">No Service Bulletin</td>
                                <td width="1%">:</td>
                                <td width="75%">                                    
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtNoBuletin" Width="400px"
                                        runat="server" ></asp:TextBox>

                                    <asp:Label ID="lblNoBulletin" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label>

                                </td>
                            </tr>

                            <tr>
                                <td class="titleField" width="24%">Stock (+)</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtStock" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')" Width="200px"></asp:TextBox>

                                </td>
                            </tr>

                            <tr>
                                <td class="titleField" width="24%">Maximal Order</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtMaxOrder" runat="server" size="22" onkeypress="return NumericOnlyWith(event,'');" Width="200px"></asp:TextBox>

                                </td>
                            </tr>
                           
                            <tr>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td colspan="2"></td>
                                <td width="75%">
                                    &nbsp; &nbsp;
                                    <asp:Button ID="btnSave" runat="server" Width="64px" Text="Simpan" CausesValidation="False"></asp:Button>
                                    <asp:Button ID="btnCancel" runat="server" Width="64px" Text="Batal" CausesValidation="False"></asp:Button>
									<asp:Button ID="btnSearch" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </tr>
                <tr valign="top">
                    <td>
                        <div id="div1" style="height: 360px; overflow: auto">
                            <asp:DataGrid ID="dtgStockPart" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="3" BackColor="#E0E0E0" AllowSorting="True" AllowCustomPaging="True" PageSize="50"
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


                                    <asp:TemplateColumn Visible="true" HeaderText="Nomor Part">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliPartNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparepartMaster.PartNumber")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="Nama Part">
                                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliPartName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparepartMaster.PartName")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="No Service Bulletin ">
                                        <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliNoBulletin" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoBulletinService")%>'>
                                            </asp:Label>
                                            <%--<asp:Label ID="lbliNoBulletin" runat="server"Text='<%# DataBinder.Eval(Container, "DataItem.NoBulletinService")%>'>
                                            </asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="No Reg">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliNoReg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoReCallCategory")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="MaxOrder" HeaderText="Maximal Order" Visible="true"  DataFormatString="{0:#,##0}" ItemStyle-HorizontalAlign="Right">
										<HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Right"></HeaderStyle>
									</asp:BoundColumn>
                                                                         
                                    <asp:TemplateColumn Visible="true" HeaderText="Initial Stock">
                                        <HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Right"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>   
                                        <ItemTemplate>
                                            <asp:Label ID="lblInitialStock" runat="server" DataFormatString="{0:#,##0}">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="Total Pengiriman ke Dealer">
                                        <HeaderStyle Width="8%" CssClass="titleTableService" HorizontalAlign="Right" ></HeaderStyle>  
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblSendQty" runat="server"   DataFormatString="{0:#,##0}"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <%--<asp:BoundColumn  HeaderText="Initial Stock" Visible="true"  DataFormatString="{0:#,##0}" ItemStyle-HorizontalAlign="Right">
										<HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Right"></HeaderStyle>
									</asp:BoundColumn>

                                    <asp:BoundColumn DataField="Stock" HeaderText="Total Pengiriman ke Dealer" Visible="true"  DataFormatString="{0:#,##0}" ItemStyle-HorizontalAlign="Right">
										<HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Right"></HeaderStyle>
									</asp:BoundColumn>--%>

                                    <asp:BoundColumn DataField="Stock" HeaderText="Sisa Stock" Visible="true"  DataFormatString="{0:#,##0}" ItemStyle-HorizontalAlign="Right">
										<HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Right"></HeaderStyle>
									</asp:BoundColumn>

                                    <asp:TemplateColumn Visible="true">
                                        <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False"
                                                CommandName="Edit">
													<img src="../images/edit.gif" border="0" alt="Ubah" ></asp:LinkButton>
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
                <%--<tr>
                    <td><asp:Button ID="btnDownload" runat="server" Text="Download" Visible="false"/></td>
                </tr>--%>
            </table>
        </div>
    </form>
</body>
</html>
