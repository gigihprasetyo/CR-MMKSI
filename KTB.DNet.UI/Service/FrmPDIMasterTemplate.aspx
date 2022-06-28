<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPDIMasterTemplate.aspx.vb" Inherits="FrmPDIMasterTemplate" smartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PDI Master Template</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
    </script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	    <style type="text/css">
            .auto-style1 {
                width: 26%;
            }
            .auto-style2 {
                font-family: Sans-Serif, Arial;
                font-size: 11px;
                color: #000000;
                margin: 0px;
                font-weight: bold;
                text-align: left;
                height: 11px;
                width: 12%;
            }
            .auto-style4 {
                font-family: Sans-Serif, Arial;
                font-size: 11px;
                color: #000000;
                margin: 0px;
                font-weight: bold;
                text-align: left;
                width: 27%;
            }
            .auto-style5 {
                height: 11px;
                width: 27%;
            }
            .auto-style7 {
                width: 7%;
            }
            .auto-style8 {
                height: 11px;
                width: 7%;
            }
            .auto-style9 {
                width: 27%;
            }
            .auto-style10 {
                width: 12%;
            }
        </style>
	</HEAD>

<body onload="firstFocus()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">&nbsp;PRE DELIVERY INSPECTION&nbsp;-&nbsp;Master Template PDI</td>
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
                    <table id="Table2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblKategori" runat="server">Model </asp:Label></td>
                            <TD class="titleField" width="1%">:</TD>
                            <td width ="20%">
                                <asp:DropDownList ID="ddlModel" runat="server" Width="120px" Height="19px" AutoPostBack="True"></asp:DropDownList>
                                <asp:DropDownList ID="ddlKategori" runat="server" Width="120px" Height="19px" AutoPostBack="True"></asp:DropDownList></td>
                            
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblBerlaku" runat="server">Berlaku </asp:Label></td>
                            <TD class="titleField" width="1%">:</TD>
                            <td class="titleField" width="20%">
                                
                                            <cc1:IntiCalendar ID="icPaymentDateFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                       
                                 
                            
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblUpload" runat="server">Upload Template </asp:Label>:</td>
                           <TD class="titleField" width="1%">:</TD>
                            <td class="titleField" width="20%">

                                <input onkeypress="return false;" id="DataFile" style="height: 20px" type="file" size="29"
                                    name="File1" runat="server"> </td>
                                <td class="titleField" width="5%" >
                                     <asp:Button ID="btnUpload" runat="server" Width="65px" Visible="True" Text="Upload" OnClick="btnUpload_Click"></asp:Button>
                                </td>
                            <td class="titleField" width="5%" >
                                 <asp:Button ID="btnPreview" runat="server" Width="65px" Visible="false" Text="Preview" OnClick="btnPreview_Click"></asp:Button>
                            </td>

                             <td class="titleField" width="5%" >
                                 
                            </td>
                                
                        </tr>

                        <tr>
                            <td class="titleField" width="20%"></td>
                           <TD class="titleField" width="1%"></TD>
                             <td class="auto-style4" >
                                &nbsp;&nbsp;Template yang diupload harus berformat .docx
                                
                                </td>
                        </tr>

                        <tr>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblStatus" runat="server">Status </asp:Label>:</td>
                            <TD class="titleField" width="1%">:</TD>
                            <td class="auto-style9">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="129px" Height="19px">
                                    
                                </asp:DropDownList></td>
                        </tr>

                        <tr>
                            <td class="titleField" width="20%"></td>
                            <TD class="titleField" width="1%"></TD>
                            <td class="titleField" width="20%">
                                <asp:Button ID="btnSimpan" runat="server" Width="65px" Visible="True" Text="Simpan" OnClick="btnSimpan_Click" Enabled="false"></asp:Button>
                                <asp:Button ID="btnBatal" runat="server" Width="65px" Visible="True" Text="Batal" CausesValidation="False" OnClick="btnBatal_Click"></asp:Button>
                                <asp:Button ID="btnCari" Visible="true" runat="server" Width="75px" Text="Cari" OnClick="btnCari_Click"></asp:Button>
                                <asp:Button ID="btnDownload" runat="server" Width="152px" Visible="True" Text="Download Sample Template" OnClick="btnDownload_Click"></asp:Button>

                            </td>
                            <td>
                            </td>
                            <td class="titleField" width="5%"></td>
                            <td class="titleField" width="5%"></td>
                            <td class="titleField" width="5%"></td>
                        </tr>

                        <tr>
                            <td colspan="6" style="height: 11px">
                                <%--<asp:Label ID="lblTotalRow" Runat="server" />--%>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="titleField" style="width: 100%; height: 11px" colspan="6">
                                <div id="div1" style="overflow: auto; height: 300px">

                                    <asp:DataGrid ID="dgPDITemplate" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
                                        CellSpacing="1" GridLines="Vertical" AllowPaging="True" PageSize="10" 
                                        OnItemCommand="dgPDITemplate_ItemCommand" 
                                        OnPageIndexChanged="dgPDITemplate_PageIndexChanged"
                                        OnSortCommand="dgPDITemplate_SortCommand"
                                        OnItemDataBound="dgPDITemplate_ItemDataBound">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
										        <ItemTemplate>
											        <asp:Label id="lblNo" runat="server"></asp:Label>
										        </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Model" SortExpression="VechileModel.IndDescription">
                                                <HeaderStyle Width ="25%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblModel" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.VechileModel.IndDescription") + "(" + DataBinder.Eval(Container, "DataItem.VechileModel.VechileModelCode") + ")"%>' Text='<%# DataBinder.Eval(Container, "DataItem.VechileModel.IndDescription") + "(" + DataBinder.Eval(Container, "DataItem.VechileModel.VechileModelCode") + ")"%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Berlaku Mulai">
										        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id="lblBerlakuMulai" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.ValidFrom")%>' Text='<%# FormatDateTime(DataBinder.Eval(Container, "DataItem.ValidFrom"), Microsoft.VisualBasic.DateFormat.ShortDate) %>'>
											        </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>								
									        <asp:TemplateColumn HeaderText="Status">
										        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id=lblStatus runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status")%>'>
											        </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>
									        <asp:TemplateColumn HeaderText="Template">
                                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
	                                            <ItemTemplate>
                                                    <asp:HiddenField ID="hdFilename" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.FileName") %>' />
		                                             <asp:LinkButton id="lbDownload" runat="server" CommandName="download">
                                                         <img src="../images/download.gif" border="0" alt="Download File">
		                                             </asp:LinkButton>
	                                            </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
    </form>
    </body>
</html>
