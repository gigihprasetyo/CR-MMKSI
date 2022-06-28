<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadReport.aspx.vb" Inherits="FrmUploadReport" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<HTML>
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
					<td class="titlePage">Transaksi - Upload Report</td>
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
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD>:</TD>
								<td>
									<P><asp:label id="lblDealerName" runat="server"></asp:label></P>
                                    <%--Hidden Variable--%>
                                    <asp:HiddenField ID="hUploadURL" runat="server" />  
                                    <asp:HiddenField ID="hUploadURLtemp" runat="server" />  
                                    <asp:HiddenField ID="hDownloadURL" runat="server" />                                    
								</td>
							</TR>
                            <TR>
								<TD class="titleField" width="24%">Bulan Periode</TD>
								<TD width="1%">:</TD>
								<TD width="30%"><asp:dropdownlist id="ddlMonth" runat="server" Width="100px"></asp:dropdownlist></TD><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlMonth" InitialValue=" "
										Width="16px" EnableClientScript="False" Display="Dynamic"></asp:requiredfieldvalidator>
							</TR>
                             <TR>
								<TD class="titleField" width="24%">Tahun Periode</TD>
								<TD width="1%">:</TD>
								<TD width="30%"><asp:dropdownlist id="ddlYear" runat="server" Width="100px"></asp:dropdownlist></TD><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlYear" InitialValue=" "
										Width="16px" EnableClientScript="False" Display="Dynamic"></asp:requiredfieldvalidator>
							</TR>

                         </TABLE>

					</TD>
            </TR>
                
           <tr>
			    <td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
		    </tr>
            <TR> 
            <TD>
                           <TABLE id="Table3" cellSpacing="2" cellPadding="2">
                            <TR>
								<TD class="titleField" width="24%"><u>File Upload</u></TD>
							</TR>
                               
                               <TR>
								<TD  width="24%">Tipe Pelaporan Data</TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:dropdownlist id="ddlTipePelaporan" runat="server" Width="280px">
								                </asp:dropdownlist><asp:Button id="btnDownloadTemplate" runat="server" Text=" Download Template " width="120px" CausesValidation="False"></asp:Button>
								</TD><asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlTipePelaporan" InitialValue=" "
										Width="16px" EnableClientScript="False" Display="Dynamic"></asp:requiredfieldvalidator>
                               
							</TR>
                            <TR>
								<TD class="titleField" width="24%">Upload</TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:Button id="btnUpload" runat="server" Text=" Open Upload File" width="100px" CausesValidation="False" ></asp:Button>&nbsp; &nbsp;<asp:Label runat="server" ID="lblfile"></asp:Label> </td>
                                <%--OnClientClick="openWindow()"--%>
							</TR>
                            </TABLE>
                </TD>
				</TR>
              <tr>
					        <td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
			   </tr>
             <TR>
                <TABLE id="Table4" cellSpacing="2" cellPadding="2">
                            <TR>
								<TD class="titleField" width="24%"><u>Filter Data</u></TD>
							</TR>        
                            <TR>
								<TD ><asp:label id="lblTglUpload" runat="server">Tanggal Upload</asp:label></TD>
								<TD>:</TD>
								<TD style="WIDTH: 328px">
									<table>
										<tr>
											<td><cc1:inticalendar id="periodeFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s.d&nbsp;</td>
											<td><cc1:inticalendar id="periodeTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>        
                            <TR>
								<TD  width="24%">Status</TD>
								<TD width="1%">:</TD>
								<TD width="70%"><asp:dropdownlist id="ddlStatus" runat="server" Width="180px"></asp:dropdownlist></TD><asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" ErrorMessage="*" ControlToValidate="ddlStatus" InitialValue=" "
										Width="16px" EnableClientScript="False" Display="Dynamic"></asp:requiredfieldvalidator>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td>
									<asp:Button id="btnCari" runat="server" Text=" Cari " width="60px" CausesValidation="False"></asp:Button></td>
							</TR>
						</TABLE>
                </TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgUploadReport" runat="server" Width="100%" PageSize="25" AllowCustomPaging="True"
								AllowPaging="True" CellSpacing="1" ToolTip="Module" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
								BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="KodeDealer" HeaderText="Kode Dealer">
										<HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
                                        <ItemTemplate>
											<asp:Label id=Label4 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ModuleID" HeaderText="Tipe Pelaporan Data">
										<HeaderStyle Width="18%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssistModule.Name")%>' ID="Label2" NAME="Label2">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="UploadTime" HeaderText="Tanggal Upload">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.UploadTime") ,"dd/MM/yyyy")%>' ID="Label3" NAME="Label3">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="UploadTime" HeaderText="Waktu Upload">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.UploadTime") ,"HH:mm")%>' ID="Label11" NAME="Label11">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ErrorRatio" HeaderText="Error Ratio">
										<HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# String.Concat( DataBinder.Eval(Container, "DataItem.ErrorRatio")," %")%>' ID="Label8" NAME="Label8">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Performance" HeaderText="Performance">
										<HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# String.Concat( DataBinder.Eval(Container, "DataItem.Performance")," %")%>' ID="Label5" NAME="Label5">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="OriginalFileName" HeaderText="Original File">
										<HeaderStyle Width="18%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OriginalFileName")%>' ID="Label6" NAME="Label6">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Download Summary">
										<HeaderStyle Width="9%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle horizontalalign="Center"/>
                                         <ItemTemplate>
											<asp:LinkButton id="btnDownload" runat="server" Text="Download" CausesValidation="False" CommandName="Download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                                            <asp:Label runat="server" visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AssistModule.TemplateFileName")%>' ID="lblTemplateFileName" NAME="lblTemplateFileName">
											</asp:Label>
                                            
										</ItemTemplate>
									</asp:TemplateColumn>
                                     <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                         <ItemStyle  Wrap="false" />
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StatusDescription")%>' ID="Label7" NAME="Label7">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>

		</form>
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
