<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmUploadServiceStandardTime.aspx.vb" Inherits="frmUploadServiceStandardTime" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<HEAD>
		<title>FrmSalesChannel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
        <Script>
           
            function openWindowUpload() {
                var h = document.getElementById('hUploadURLtemp');
                var url = h.value
              
                var childwindow = window.open(url, 'AssistUpload', 'directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,width=350,height=200 , top=' + 250 + ', left=' + 450);  

                if (window.focus) {
                    childwindow.focus();
                }

                var timer = setInterval(function () {
                    if (childwindow.closed) {
                        clearInterval(timer);
                        document.getElementById('btnCari').click();
                    }
                }, 1000);

            }

            function openWindowDownload() {
                var h = document.getElementById('hDownloadURL');
                var url = h.value
                //alert("baru saja hit url download : \n" + url);
                window.location.assign(url);
            }

         </Script>
	</HEAD>
    <body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
            <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Service - Upload Service Standard Time</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
                <TR>
					<TD>
						<TABLE id="Table2" cellSpacing="2" cellPadding="2">
							<TR>
								<TD class="titleField" width="24%">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:label id="lblDealerCode" runat="server"></asp:label></td>
							</TR>
							
                            <tr>
                                <td class="titleField">Assist Service Type</td>
                                <td style="width: 2px" valign="top">:</td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlAssistServiceType" runat="server" AutoPostBack="false"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td class="titleField">Jenis Kegiatan</td>
                                <td style="width: 2px" valign="top">:</td>
                                <td valign="top">
                                    <asp:DropDownList ID="ddlJenisKegiatan" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    <asp:LinkButton ID="LnkDownloadTemplate" runat="server" ToolTip="Download Template Excell">Download Template</asp:LinkButton>
                                    
                                </td>
                            </tr>

                            
                            <tr>
                                <td class="titleField">Upload File</td>
                                <td style="width: 2px" valign="top">:</td>
                                <td valign="top">
                                    <input onkeypress="return false;" id="DataFile" style="height: 20px" type="file" size="29"
                                    name="File1" runat="server">
                                &nbsp;&nbsp;Minimum Excel 2007 (*.xls / *.xlsx)
                                    
                                </td>
                            </tr>
                       
                            <tr>
                            <td class="titleField" style="width: 24%"></td>
                            <td style="width: 1%"></td>
                            <td style="width: 75%">
                                <asp:Button ID="btnUpload" runat="server" Width="88px" Text="Upload" OnClick="btnUpload_Click"></asp:Button>&nbsp;
                                <asp:Button ID="btnBatal" runat="server" Width="88px" Text="Batal" OnClick ="btnBatal_Click"></asp:Button>&nbsp;
						        <asp:Button ID="btnSimpan" runat="server" Width="88px" Text="Simpan" OnClick="btnSimpan_Click" Enabled ="false"></asp:Button>
                            </td>
            </tr>

                         </TABLE>
                        
                            
					</TD>
            </TR>

                <tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left"  class="titleField"><asp:label id="lblMessage" Runat="server" Visible="false"></asp:label></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>

                <TR>
					<TD vAlign="top">
                        <div id="div1" style="OVERFLOW: auto; HEIGHT: 390px">
                        <asp:DataGrid ID="dtgServiceStandardTime" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
							BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="false" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
											AllowPaging="True">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
                                
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <%--<asp:TemplateColumn>
													<HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<asp:CheckBox id="cbAll" runat="server" onclick="CheckAll('cbItem',this.checked)"></asp:CheckBox>
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="cbItem" runat="server"></asp:CheckBox>
														<asp:CheckBox id="cbInitial" style="display:none" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
                                <%--<asp:TemplateColumn HeaderText="Kode Dealer">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbKodeDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>--%>
                                <asp:TemplateColumn HeaderText="Assist Service Type">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbAssistServiceType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssistServiceTypeCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Tipe Kendaraan">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbKodeTipeKendaraan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.VechileTypeCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Deskripsi Kendaraan">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbTipeKendaraan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Kegiatan">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbJenisKegiatan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceTypeID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Service">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbJenisService" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KindCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Standard Waktu Dealer (Jam)">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbStandardDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerStandardTime", "{0:0.00}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Standard Waktu System (Jam)">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbStandardSystem" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SystemStandardTime", "{0:0.00}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>                               
                                

                        <asp:BoundColumn DataField="ErrorMessage" HeaderText="Keterangan">
										<HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>                               
						    </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
					                 </div>
                        
                        
                        
					</TD>
				</TR>

            </TABLE>
            
        </form>
    </body>
</html>
