<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpCostEstimation.aspx.vb" Inherits=".PopUpCostEstimation" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PopUpCostEstimation</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
	<meta content="JavaScript" name="vs_defaultClientScript"/>
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link href="../WebResources/scheduler_traditional.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/css/jquery-ui.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/jquery-clockpicker.min.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery.1.11.0.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-ui.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-clockpicker.min.js"></script>

	<base target="_self" />
	<script type="text/javascript" language="javascript">
	    function onSuccess(result)
	    {
	        if (navigator.appName == "Microsoft Internet Explorer")
	            window.returnValue = "Service Booking Dibatalkan.";
	        else {
	            window.close();
	            window.opener.dialogWin.returnFunc("Service Booking Dibatalkan.");
	        }

	        window.close();
	    }
	    var rbtnResponses = document.getElementById('rbtnResponses')
	    rbtnResponses.addEventListener('change', function () {
	        alert("berubah");
	    });
	</script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table2" cellspacing="3" cellpadding="3" width="100%" border="0">
            <tr>
                <td class="titleTableParts3" colspan="3">Popup Cost Estimation</td>
            </tr>
            <tr id="trCost" runat="server">
                <td colspan="4" class="titleField">
                    <asp:datagrid id="dgCost" runat="server" Width="100%" CellPadding="3" BorderWidth="1px"
	                        BorderColor="#CDCDCD" BackColor="White" AutoGenerateColumns="False" ShowFooter="True" OnItemDataBound="dgCost_ItemDataBound">
	                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
	                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
	                        <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
	                        <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
	                        <Columns>

                                <asp:TemplateColumn HeaderText="No">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService" Width="2%"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Kegiatan">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJnsKegiatan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.JenisKegiatan")%>'>
                                    </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Service">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
				                        <asp:Label ID="lblJnsService" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.JenisService")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
				                        <FooterTemplate>
                                            <asp:Label ID="lblGrand" runat="server" Text="Grand Total">
                                            </asp:Label>
			                        </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Subtotal">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubtotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.JasaService")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
				                        <FooterTemplate>
                                            <asp:Label ID="lblGrandTotal" runat="server">
                                            </asp:Label>
			                        </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:datagrid>
                </td>
            </tr>
            <tr id="trCost2" runat="server">
                <td colspan="4" class="titleField">
                    <asp:datagrid id="dgSparePart" runat="server" Width="100%" CellPadding="3" BorderWidth="1px"
	                    BorderColor="#CDCDCD" BackColor="White" AutoGenerateColumns="False" ShowFooter="True" OnItemDataBound="dgSparePart_ItemDataBound">
	                    <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
	                    <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
	                    <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
	                    <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
	                    <Columns>

                            <asp:TemplateColumn HeaderText="No">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <HeaderStyle CssClass="titleTableService" Width="2%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Part">
                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNamaPart" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartName")%>'>
                                </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Harga Satuan">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
								    <asp:Label ID="lblHargaSatuan" Runat="server" Text = '<%# DataBinder.Eval(Container, "DataItem.RetalPrice")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Jumlah">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblJumlah" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartQuantity")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Diskon">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDiskon" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DiscountAmount")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
								    <FooterTemplate>
                                        <asp:Label ID="lblGrand" runat="server" Text="Grand Total">
                                        </asp:Label>
								</FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Subtotal">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSubtotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Subtotal")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
								    <FooterTemplate>
                                        <asp:Label ID="lblGrandTotal" runat="server">
                                        </asp:Label>
								</FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                       <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:datagrid>
                </td>
            </tr>
        
    </table>
    </form>
</body>
</html>
