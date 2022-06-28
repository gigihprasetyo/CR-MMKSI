<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmWarrantyActivation.aspx.vb" Inherits=".frmWarrantyActivation" smartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<HEAD>
		<title>Aktivasi Warranti</title>
		    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"></script>
    <script language="javascript" type="text/javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtbox = document.getElementById("txtDealerCode");
            txtbox.value = data[0] + " - " + data[1];
        }
        function ClosePreview() {
            $("#PreviewImage").hide("slow");
        }
        function ShowPreview() {
            $("#PreviewImage").show("slow");
        }

    </script>
	    
	</HEAD>
    <body onload="firstFocus()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
           <tr>
                <td class="titlePage">&nbsp;Umum&nbsp;-&nbsp;Aktivasi Warranti</td>
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
                <td valign="top" align="left">
                     <table cellspacing="0" cellpadding="3" border="0" style="width: 1037px; height: 64px">
                        <tr>
                            <td class="titleField" width="20%">Kode Dealer</td>
                            <TD width="1%">:</TD>
                            <td width="20%">
                                <asp:TextBox ID="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                    runat="server" Width="180px"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server" onclick="ShowPPDealerSelection();">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                            </td>
                            <td width ="2%"></td>
                            <td width="40%">
                            </td>
                        </tr>

                         

                        <tr>
                            <td class="titleField">Nomor Rangka</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                                <asp:TextBox ID="txtNomorRangka" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td width ="2%"></td>
                            <td width="40%">
                            </td>
                        </tr>

                         <tr>
                            <td class="titleField">Nama Customer Request</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                                <asp:TextBox ID="txtCustName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td width ="2%"></td>
                            <td width="40%">
                            </td>
                        </tr>

                         <tr>
                            <td class="titleField">Nomor Polisi</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                                <asp:TextBox ID="txtNomorPolisi" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td width ="2%"></td>
                            <td width="40%">
                            </td>
                        </tr>

                         <%--<tr>
                            <td class="titleField">Nomor Telepon</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                                <asp:TextBox ID="txtNoTelp" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td width ="2%"></td>
                            <td width="40%">
                            </td>
                        </tr>--%>

                         <tr>
                            <td class="titleField">Tanggal Permintaan</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="ccTglPermintaan" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="ccTglPermintaanEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                         <tr>
                            <td class="titleField">Tanggal PDI</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="ccTglPDIMulai" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="ccTglPDIAkhir" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                         <tr>
                            <td class="titleField">Tanggal PKT</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                               <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="ccTglPKTMulai" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="ccTglPKTAkhir" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                         <tr>
                            <td class="titleField">Status</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                               
                                </asp:DropDownList>
                            </td>
                             <td width ="2%"></td>
                             <td width="40%">
                                
                            </td>
                        </tr>
                         <tr>
                            <td class="titleField"></td>
                             <TD width="1%"> </TD>
                            <td width="20%">
                                <asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px" OnClick="btnCari_Click" />
                            </td>
                             <td width ="2%"></td>
                             <td width="40%">
                                
                            </td>
                        </tr>

                         <tr>
                            <td class="titleField">
                                <%--<asp:Label ID="lblTotalRow" Runat="server" />--%>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </td>
                             <TD width="1%"> </TD>
                            <td width="20%">
                                
                            </td>
                             <td width ="2%"></td>
                             <td width="40%">
                                
                            </td>
                        </tr>

                        <tr>
                            <td style="height: 11px" colspan="6">
                                <div id="div1" style="overflow: auto; height: 300px; width: 1042px;">

                                    <asp:DataGrid ID="dgWarrantyActivation" runat="server" Width="100%" CellPadding="1" BorderWidth="0px"
                                        CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="10"
                                        AllowPaging="True" 
                                        OnItemCommand="dgWarrantyActivation_ItemCommand"
                                        OnPageIndexChanged="dgWarrantyActivation_PageIndexChanged"
                                        OnSortCommand="dgWarrantyActivation_SortCommand" 
                                        OnItemDataBound="dgWarrantyActivation_ItemDataBound">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
										        <ItemTemplate>
											        <asp:Label id="lblNo" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ID")%>' />
										        </ItemTemplate>
                                            </asp:TemplateColumn>        
                                           
                                            <asp:TemplateColumn HeaderText="Nomor Rangka" SortExpression="ChassisMaster.ChassisNumber">
                                                <HeaderStyle Width ="15%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNomorRangka" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Nama Customer" SortExpression="CustomerName">
                                                <HeaderStyle Width ="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaCustomer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Nomor Polisi" SortExpression="PlateNumber">
                                                <HeaderStyle Width ="8%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNomorPolisi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PlateNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <%--<asp:TemplateColumn HeaderText="Nomor Telpon" SortExpression="TelpNo">
                                                <HeaderStyle Width ="6%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNomorTelp" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TelpNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>--%>

                                            <asp:TemplateColumn HeaderText="Tanggal Permintaan" SortExpression="ChassisMaster.ChassisNumber">
										        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id="lblTanggalermintaan" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.WADate")%>' Text='<%# FormatDateTime(DataBinder.Eval(Container, "DataItem.WADate"), Microsoft.VisualBasic.DateFormat.ShortDate) %>'>
											        </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tanggal PDI" SortExpression="PDI.PDIDate">
										        <HeaderStyle Width="10%" CssClass="titleTableService" ></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id="lblTanggalPDI" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.PDI.PDIDate")%>' Text='<%# FormatDateTime(DataBinder.Eval(Container, "DataItem.PDI.PDIDate"), Microsoft.VisualBasic.DateFormat.ShortDate) %>'>
											        </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Dealer Pelaksana PDI" SortExpression="PDI.Dealer.DealerName">
                                                <HeaderStyle Width ="15%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerPDI" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PDI.Dealer.DealerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tanggal PKT" SortExpression="ChassisMasterPKT.PKTDate">
										        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id="lblTanggalPKT" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.ChassisMasterPKT.PKTDate")%>' Text='<%# FormatDateTime(DataBinder.Eval(Container, "DataItem.ChassisMasterPKT.PKTDate"), Microsoft.VisualBasic.DateFormat.ShortDate) %>'>
											        </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Dealer Pelaksana PKT" Visible="false">
                                                <HeaderStyle Width ="15%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerPKT" runat="server"></asp:Label> 
                                                </ItemTemplate> 
                                            </asp:TemplateColumn>								
									        <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
										        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id=lblStatus runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status")%>'>
											        </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>
									        <asp:TemplateColumn HeaderText="">
                                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
	                                            <ItemTemplate>
		                                             <asp:LinkButton id="lbDownload" runat="server" CommandName="download">
                                                         <img src="../images/download.gif" border="0" alt="Download File">
		                                             </asp:LinkButton>
	                                            </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                         <tr>
                                                         <td width="30%">
                                <asp:Button ID="btnDownload" runat="server" Text="Download" Width="68px" OnClick="btnDownload_Click" />
                            </td>

                         </tr>

                    </table>
                </td>
            </tr>
            
        </table>
    </form>
    </body>

</html>
