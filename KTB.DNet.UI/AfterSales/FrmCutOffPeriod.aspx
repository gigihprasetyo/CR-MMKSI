<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCutOffPeriod.aspx.vb" Inherits="FrmCutOffPeriod" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCutOffPeriod</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
                <script language="javascript">

                    function ShowPPDealerSelection() {
                        showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
                    }

                    function DealerSelection(selectedDealer) {
                        var data = selectedDealer.split(";");
                        var txtDealerCodeSelection = document.getElementById("txtDealerCode");
                        txtDealerCodeSelection.value = data[0];
                        var lblDealerName = document.getElementById("lblDealerName");
                        lblDealerName.innerHTML = data[1];
                    }

                    //function ChangeType() {
                        
                    //    var chkType = document.getElementById("chkType");
                    //    var txtDealerCodeSelection = document.getElementById("txtDealerCode");
                    //    var lblDealerName = document.getElementById("lblDealerName");
                    //    var lblPopUpDealer = document.getElementById("lblPopUpDealer");
                    //    if (chkType.checked)
                    //    {
                    //        txtDealerCodeSelection.value = '';
                    //        txtDealerCodeSelection.readOnly = true;
                    //        lblDealerName.innerHTML = '';
                    //        lblPopUpDealer.style.display = "none";
                    //    }
                    //    else
                    //    {
                    //        txtDealerCodeSelection.value = '';
                    //        txtDealerCodeSelection.readOnly = false;
                    //        lblDealerName.innerHTML = '';
                    //        lblPopUpDealer.style.display = "inline";
                    //    }
                    //}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Master - Tanggal Cut Off</td>
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
<%--							 <TR>
								<TD class="titleField" Visible="false">Cut Off All Dealer</TD>
								<td>:</td>
								<TD>
									<asp:CheckBox id="chkType" runat="server" Text=""></asp:CheckBox></TD>
							</TR>--%>
                            <TR>
                                <TD class="titleField" style="WIDTH: 5%">Kode Dealer</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD class="titleField"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                        runat="server" ToolTip="Dealer Search 1" Width="128px"></asp:textbox><asp:label id="lblDealerCode" runat="server"></asp:label><asp:label id="lblPopUpDealer" runat="server" width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:label></TD>
                            </TR>
                            <TR>
                                <TD class="titleField" style="WIDTH: 5%">Nama Dealer</TD>
                                <TD style="WIDTH: 2px">:</TD>
                                <TD class="titleField"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
                            </TR>
                            <TR>
								<TD class="titleField" width="5%">Bulan</TD>
								<TD width="1%">:</TD>
								<TD width="30%"><asp:dropdownlist id="ddlMonth" runat="server" Width="100px"></asp:dropdownlist></TD><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlMonth" InitialValue=" "
										Width="16px" EnableClientScript="False" Display="Dynamic"></asp:requiredfieldvalidator></td>
							</TR>
                             <TR>
								<TD class="titleField" width="5%">Tahun</TD>
								<TD width="1%">:</TD>
								<TD width="30%"><asp:dropdownlist id="ddlYear" runat="server" Width="100px"></asp:dropdownlist></TD><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlYear" InitialValue=" "
										Width="16px" EnableClientScript="False" Display="Dynamic"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="titleField" width="5%"></TD>
								<TD></TD>
								<td>
									<asp:Button id="btnCari" runat="server" Text=" Cari " width="60px" CausesValidation="False"></asp:Button>&nbsp;
                                    <asp:button id="btnSimpan" runat="server" Text="Simpan" width="60px" Visible="false"></asp:button>
							</TR>
                            <TR>
								<TD class="titleField" width="5%"></TD>
								<TD></TD>
								<td>
                                    <asp:Button id="btnActivateAll" runat="server" Text=" Aktifkan Semua " width="120px" CausesValidation="False"></asp:Button>&nbsp;
                                    <asp:Button id="btnInactiveAll" runat="server" Text=" Non Aktifkan Semua" width="120px" CausesValidation="False"></asp:Button>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgCutOffPeriod" runat="server" Width="100%" PageSize="25" AllowCustomPaging="True"
								AllowPaging="True" CellSpacing="1" ToolTip="Cut Off Period" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
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
									<asp:TemplateColumn SortExpression="Month" HeaderText="Periode">
                                        <ItemStyle horizontalalign="Center"/>
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
											<asp:Label id=Label4 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Period")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="40%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")%>' ID="Label2" NAME="Label2">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="CutOffDate" HeaderText="Tgl Cut Off">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle horizontalalign="Center"/>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.CutOffDate") ,"dd/MM/yyyy")%>' ID="Label3" NAME="Label2">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Action">
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
<%--											<asp:LinkButton id="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete" >
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>--%>
                                            <asp:LinkButton id="linkButonActive" runat="server" Width="16px" CausesValidation="False" CommandName="Activate">
												<img src="../images/in-aktif.gif" border="0" title="Klik untuk Aktifkan data"></asp:LinkButton>
											<asp:LinkButton id="LinkButtonNonActive" runat="server" Width="16px" CausesValidation="False" CommandName="Deactivate">
												<img src="../images/aktif.gif" border="0" title="Klik untuk non-Aktifkan data"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
            <INPUT id="hdnValActive" type="hidden" value="-1" name="hdnValActive" runat="server">
            <INPUT id="hdnValInActive" type="hidden" value="-1" name="hdnValInActive" runat="server">
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
