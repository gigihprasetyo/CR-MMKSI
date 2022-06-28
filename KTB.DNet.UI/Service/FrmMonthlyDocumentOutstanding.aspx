<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMonthlyDocumentOutstanding.aspx.vb" Inherits=".FrmMonthlyDocumentOutstanding" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Daftar Dokumen Service</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/jquery-1.10.2.js" type="text/javascript"></script>
    <script language="javascript">

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }


        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function UploadFaktureEvidance() {
            
        }
       
    </script>

    <script language="javascript" type="text/javascript">
        
        function on_create_form_clicked(lnk) {
            try {
                var row = lnk.parentNode.parentNode;
                var message = "Row Index: " + (row.rowIndex - 1);
                //alert(message);
                var tbl = document.getElementById('dtgMonthlyDocument');
                var value = row.cells[0].getElementsByTagName("SPAN")[0].innerHTML
                
                showPopUp('../General/../PopUp/PopUpUploadFaktureEvidance.aspx?IDmonth=' + String(value) + '&mode=input', '', 500, 760, UploadFaktureEvidance);
            } catch (e) {
                alert(e.message);
            }

        }
        
        function on_create_form_clicked_view(lnk) {
            try {
                var row = lnk.parentNode.parentNode;
                var message = "Row Index: " + (row.rowIndex - 1);
                //alert(message);
                var tbl = document.getElementById('dtgMonthlyDocument');
                var value = row.cells[0].getElementsByTagName("SPAN")[0].innerHTML

                showPopUp('../General/../PopUp/PopUpUploadFaktureEvidance.aspx?IDmonth=' + String(value) + '&mode=view', '', 500, 760, UploadFaktureEvidance);
            } catch (e) {
                alert(e.message);
            }

        }

</script>
</head>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="titlePage" colSpan="2">
                        
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">UMUM - Daftar Dokumen Service Outstanding</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td class="titleField" style="height: 16px" width="24%">
                                    <asp:Label ID="lblDealer" runat="server">Dealer</asp:Label></td>
                                <td style="height: 16px" width="1%">:</td>
                                <td style="height: 16px" width="75%">
                                    <asp:TextBox ID="txtKodeDealer" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                        onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="lblPeriode" runat="server"> Periode</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlPeriode" runat="server" Width="120px"></asp:DropDownList><asp:DropDownList ID="ddlPeriodeYear" runat="server" Width="120px"></asp:DropDownList>
                                    &nbsp; s.d &nbsp;
                                    <asp:DropDownList ID="ddlPeriodeTo" runat="server" Width="120px"></asp:DropDownList><asp:DropDownList ID="ddlPeriodeYearTo" runat="server" Width="120px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="lblJenisDokumen" runat="server">Jenis Dokumen</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlJenisDokumen" runat="server" Width="240px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    <asp:Label ID="lblStatusDownload" runat="server">Status Download</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlStatusDownload" runat="server" Width="120px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField">Produk</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField">No Accounting</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtNoAccounting" runat="server" Width="174px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="titleField">Billing No</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtBillingNo" runat="server" Width="174px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField"></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnCari" runat="server" Width="64px" Text="Cari" Style="z-index: 0"></asp:Button></td>
                            </tr>
                        </table>
                    </td>
                    <td width="40%">
                        <asp:Label ID="lblAlert" runat="server" ForeColor="#ff6666" Font-Bold="True">
							<b style='mso-bidi-font-weight:normal'>
								<span style='font-size:11.0pt;line-height:115%;font-family:Calibri;mso-fareast-font-family:
									Calibri;mso-bidi-font-family:"Times New Roman";mso-ansi-language:EN-US;
									mso-fareast-language:EN-US;mso-bidi-language:AR-SA'>
								</span>
							</b>
                        </asp:Label>
                    </td>
                </tr>
				<TR>
					<TD vAlign="top" colSpan="2"><div id="div1" style="HEIGHT: 310px; OVERFLOW: auto">
                        <asp:datagrid id="dtgMonthlyDocument" runat="server" Width="100%" 
								AutoGenerateColumns="False" CellPadding="3" BackColor="#E0E0E0" AllowSorting="True" AllowCustomPaging="True" PageSize="50"
								AllowPaging="True" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#ededed"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
                                            <asp:Label  ID="lblID" style="display:none" runat="server" ></asp:Label>
											<asp:Label id="lblDownload" runat="server">
												<img src="../images/red.gif" border="0" alt="Belum Didownload"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblNo runat="server" NAME="lblNo" text="<%# container.itemindex+1 %>">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn SortExpression="Kind" HeaderText="Jenis Dokumen">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Periode">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" SortExpression="FileName" HeaderText="NamaFile">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="FileSize" HeaderText="Ukuran File (kb)">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" HeaderText="Tanggal Dibuat" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="LastDownloadBy" HeaderText="Didownload Oleh">
										<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="LastDownloadDate" HeaderText="Tanggal Download" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>


                                    <asp:BoundColumn DataField="AccountingNo" SortExpression="AccountingNo" HeaderText="Nomor Accounting">
										<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="BillingNo" SortExpression="BillingNo" HeaderText="Nomor Billing">
										<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>

								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#CCCCCC" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>

                <tr>
                    <td style="height: 40px" align="left">
                        <asp:Button ID="btnDownload" runat="server" Width="100px" Text="Download"></asp:Button>
                        &nbsp;&nbsp;<asp:Label ID="lblLoading" ForeColor="Red" runat="server"></asp:Label>
                    </td>                    
                </tr>  

				<%--<TR id="OpClient" runat="server">
					<TD vAlign="middle" colSpan="2"><asp:textbox id="txtDownload" runat="server"></asp:textbox></TD>
				</TR>--%>
			</TABLE>
		</form>
		<script language="javascript">
			document.getElementById("txtDownload").style.visibility="hidden";			
			if (document.getElementById("txtDownload").value != "")
			{
				 var downloadURL = document.getElementById("txtDownload").value;
				document.getElementById("txtDownload").value = "";
				document.location.href="../download.aspx?file="+downloadURL;	 
				 		
			}
		</script>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>
