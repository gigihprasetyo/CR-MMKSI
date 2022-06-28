<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmExpenseService.aspx.vb" Inherits="FrmExpenseService" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<HTML>
	<HEAD>
		<title>FrmExpenseService</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript" src="../WebResources/FormFunctions.js"></script>
        <script>
            function ShowPPDealerSelection() {
                showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
            }

            function DealerSelection(selectedDealer) {
                var data = selectedDealer.split(";");
                var txtDealerCodeSelection = document.getElementById("txtKodeDealer");
                txtDealerCodeSelection.value = data[0];
            }

            function openWindowDownload() {
                var h = document.getElementById('hDownloadURL');
                var url = h.value
                window.location = url;
            }
        </script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Data Upload - Dealer Expense Service</td>
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
								<TD class="titleField" width="150px"><asp:label id="lblKodeDealerDetail" Text="Kode Dealer" runat="server"/></TD>
								<TD width="1%"><asp:label id="lblKodeDealerDetailSeparator" Text=":" runat="server"/></TD>
								<td width="250px"><asp:label id="lblDealerCode" runat="server"></asp:label></td>
							</TR>
							<TR>
								<TD class="titleField" width="150px"><asp:label id="lblNamaDealerDetail" Text="Nama Dealer" runat="server"/></TD>
								<TD><asp:label id="lblNamaDealerDetailSeparator" Text=":" runat="server"/></TD>
								<td>
									<P><asp:label id="lblDealerName" runat="server"></asp:label><asp:HiddenField ID="hAssistUploadLogID" runat="server" /> </P>                                   
                                     <asp:HiddenField ID="hDownloadURL" runat="server" />     
                                    <asp:HiddenField ID="hQuery" runat="server" />  
								</td>
							</TR>
                            <TR >
								<TD class="titleField" style="WIDTH: 150px"><asp:label id="lblKodeDealerMenu" Text="Kode Dealer" runat="server"></asp:label></TD>
								<TD width="1%"><asp:label id="lblKodeDealerSeparator" Text=":" runat="server"/></TD>
								<td style="WIDTH: 250px"><asp:textbox id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server"
										Width="152px" ></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></td>
							</TR>
                             <TR>
								<TD class="titleField" width="150px">Bulan</TD>
								<TD width="1%">:</TD>
								<TD width=250px><asp:dropdownlist id="ddlMonth" runat="server" Width="100px"></asp:dropdownlist></TD><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlMonth" InitialValue=" "
										Width="16px" EnableClientScript="False" Display="Dynamic"></asp:requiredfieldvalidator></td>
                                
							</TR>
                             <TR>
								<TD class="titleField" width="150px">Tahun</TD>
								<TD width="1%">:</TD>
								<TD width="250px"><asp:dropdownlist id="ddlYear" runat="server" Width="100px"></asp:dropdownlist></TD><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlYear" InitialValue=" "
										Width="16px" EnableClientScript="False" Display="Dynamic"></asp:requiredfieldvalidator></td>							
							</TR>

                            <TR>
								<TD class="titleField" width="150px"><u>Monitoring Upload Data</u></TD>
							</TR>
                            <TR>
								<TD width="150px">I. Total Biaya Karyawan</TD>
								<TD width="1%">:</TD>
								<TD><asp:label id="lblTotalBiayaKaryawan" runat="server" Font-Bold="True"></asp:label></TD>
                                <TD style="WIDTH: 150px"><asp:label id="Label3" runat="server">IV. Total Biaya Penyusutan</asp:label>&nbsp;</TD>
								<TD style="WIDTH: 12px">:</TD>
                                <TD><asp:label id="lblTotalBiayaPenyusutan" runat="server" Font-Bold="True"></asp:label></TD>
							</TR>
                             <TR>
								<TD width="150px">II. Total Biaya Operasional</TD>
								<TD width="1%">:</TD>
								<TD><asp:label id="lblTotalBiayaOperasional" runat="server" Font-Bold="True"></asp:label></TD>
                                <TD style="WIDTH: 150px"><asp:label id="Label2" runat="server">V. Total Biaya Umum</asp:label>&nbsp;</TD>
								<TD style="WIDTH: 12px">:</TD>
                                <TD><asp:label id="lblTotalBiayaUmum" runat="server" Font-Bold="True"></asp:label></TD>
							</TR>
                             <TR>
								<TD width="150px">III. Total Biaya Perawatan</TD>
								<TD width="1%">:</TD>
								<TD><asp:label id="lblTotalBiayaPerawatan" runat="server" Font-Bold="True"></asp:label></TD>
                                
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td>
									<asp:Button id="btnCari" runat="server" Text=" Cari " width="60px" CausesValidation="False"></asp:Button>
                                    <asp:Button id="btnDownload" runat="server" Text=" Download " width="80px" CausesValidation="False"></asp:Button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
                        <br>
                        <b>LAMPIRAN TARGET SERVICE</b><br>
                            A. TARGET DEALER
                        <div id="div1" ><asp:datagrid id="dtgTargetDealer" runat="server" Width="45%" PageSize="100" AllowCustomPaging="True"
								AllowPaging="True" CellSpacing="1" ToolTip="Module" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
								BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
											<asp:Label id=lblMonth runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="UnitType" HeaderText="">
										<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitType")%>' ID="Label5" NAME="Label5">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                 
                                    <asp:TemplateColumn SortExpression="Value" HeaderText="">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Right"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.Value") ,"0.00")%>' ID="lblValue" NAME="lblValue">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages" Visible ="false"></PagerStyle>
							</asp:datagrid>
                        </div>
                        <br>
					</TD>
				</TR>
            <TR>
                <TD>
                    <br>
                        <b>LAMPIRAN REALISASI BIAYA SERVICE</b><br>
                            I. BIAYA KARYAWAN
                            <div id="divBiayaKaryawan"   ><asp:datagrid id="dtgBiayaKaryawan" runat="server" Width="45%"  PageSize="100" AllowCustomPaging="True"
								AllowPaging="True" CellSpacing="1" ToolTip="Module" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
								BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
											<asp:Label id=lblMonth runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="UnitType" HeaderText="">
										<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitType")%>' ID="Label5" NAME="Label5">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                 
                                    <asp:TemplateColumn SortExpression="Value" HeaderText="">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Right"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.Value") ,"0.00")%>' ID="lblValue" NAME="lblValue">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
                    <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages" Visible ="false"></PagerStyle>
							</asp:datagrid></div>
					</TD>

            </TR>
            <TR>
                <TD>
                    <br>
                            II. BIAYA OPERASIONAL
                            <div id="divOperasional"><asp:datagrid id="dtgBiayaOperasional" runat="server" Width="45%"  PageSize="100" AllowCustomPaging="True"
								AllowPaging="True" CellSpacing="1" ToolTip="Module" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
								BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
											<asp:Label id=lblMonth runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="UnitType" HeaderText="">
										<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitType")%>' ID="Label5" NAME="Label5">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                 
                                    <asp:TemplateColumn SortExpression="Value" HeaderText="">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Right"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.Value") ,"0.00")%>' ID="lblValue" NAME="lblValue">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
                    <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages" Visible ="false"></PagerStyle>
							</asp:datagrid></div>
					</TD>

            </TR>
            <TR>
                <TD>
                    <br>
                            III. BIAYA PERAWATAN
                            <div id="divPerawatan"   ><asp:datagrid id="dtgBiayaPerawatan" runat="server" Width="45%"  PageSize="100" AllowCustomPaging="True"
								AllowPaging="True" CellSpacing="1" ToolTip="Module" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
								BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
											<asp:Label id=lblMonth runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="UnitType" HeaderText="">
										<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitType")%>' ID="Label5" NAME="Label5">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                 
                                    <asp:TemplateColumn SortExpression="Value" HeaderText="">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Right"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.Value") ,"0.00")%>' ID="lblValue" NAME="lblValue">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
                    <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages" Visible ="false"></PagerStyle>
							</asp:datagrid></div>
					</TD>

            </TR>
            <TR>
                <TD>
                    <br>
                            IV. BIAYA PENYUSUTAN
                            <div id="divPenyusutan"   ><asp:datagrid id="dtgBiayaPenyusutan" runat="server" Width="45%"  PageSize="100" AllowCustomPaging="True"
								AllowPaging="True" CellSpacing="1" ToolTip="Module" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
								BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
											<asp:Label id=lblMonth runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="UnitType" HeaderText="">
										<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitType")%>' ID="Label5" NAME="Label5">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                 
                                    <asp:TemplateColumn SortExpression="Value" HeaderText="">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Right"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.Value") ,"0.00")%>' ID="lblValue" NAME="lblValue">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
                    <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages" Visible ="false"></PagerStyle>
							</asp:datagrid></div>
					</TD>

            </TR>
            <TR>
                <TD>
                    <br>
                            V. BIAYA UMUM
                            <div id="divUmum"   ><asp:datagrid id="dtgBiayaUmum" runat="server" Width="45%"  PageSize="100" AllowCustomPaging="True"
								AllowPaging="True" CellSpacing="1" ToolTip="Module" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
								BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
											<asp:Label id=lblMonth runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="UnitType" HeaderText="">
										<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitType")%>' ID="Label5" NAME="Label5">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                 
                                    <asp:TemplateColumn SortExpression="Value" HeaderText="">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Right"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.Value") ,"0.00")%>' ID="lblValue" NAME="lblValue">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
                    <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages" Visible ="false"></PagerStyle>
							</asp:datagrid></div>
					</TD>

            </TR>
            <TR>
									<TD colSpan="6" valign="bottom">
										<TABLE id="tblOperator" style="WIDTH: 192px; HEIGHT: 46px" cellSpacing="1" cellPadding="1"
											width="192" border="0" runat="server">
											<TR>
												<TD valign="top"><asp:button id="btnBack" Text="Kembali" Runat="server"></asp:button></TD>
											</TR>
										</TABLE>
									</TD>
									<TD></TD>
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
