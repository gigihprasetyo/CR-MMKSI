<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEntryGyro2.aspx.vb" Inherits="FrmEntryGyro2" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEntryGyro</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script>
			function AssignmentChanged(ddl)
			{
				if(ddl.id.length==28)
				{
					var sPre = ddl.id.substring(0,ddl.id.length-14);
					var sLast = ddl.id.substring(ddl.id.length-1);
					
					var btn=document.getElementById(sPre+'btnAssign'+sLast);
					btn.click();
				}				
			}
			function CheckGyroLength(TriggerControl)
			{
				var txtGyroNumber = document.getElementById("txtGyroNumber");
				var ddlBank  = document.getElementById("ddlBank");
				var sBank = '';
				var nRemainChar=16;
				
				if(ddlBank.selectedIndex>0)
				{
					sBank = ddlBank.options[ddlBank.selectedIndex].value;
				}
				if(sBank!='')
				{
					nRemainChar=nRemainChar-sBank.length-1;
					if(txtGyroNumber.value.length>nRemainChar)
					{
						if(TriggerControl=="txtGyroNumber")
						{
							alert("No Gyro maksimal " + nRemainChar+" karakter");
							txtGyroNumber.value=txtGyroNumber.value.substring(0,nRemainChar);	
						}
						else
						{
							alert("Kode Bak + No Gyro maksimal 16 karakter");
							ddlBank.selectedIndex=0;
						}
						
					}
				}
			}
			function EnableButtonSave()
			{
				var btnSave = document.getElementById("btnSave");
				//alert(btnSave);	
				btnSave.disabled=false;
			}			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 16px">
						&nbsp;
						<asp:label id="lblTitle" runat="server">Label</asp:label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td style="WIDTH: 155px; HEIGHT: 27px"><b>Kode Dealer</b></td>
								<td style="WIDTH: 9px; HEIGHT: 27px">:</td>
								<td style="WIDTH: 198px; HEIGHT: 27px"><asp:label id="lblDealerCode" runat="server"></asp:label></td>
								<td style="WIDTH: 125px; HEIGHT: 27px"><STRONG><B>Jenis Entry</B></STRONG></td>
								<td style="WIDTH: 8px; HEIGHT: 27px">:</td>
								<td style="WIDTH: 138px; HEIGHT: 27px" align="left"><asp:dropdownlist id="ddlEntryType" runat="server" Width="152px" AutoPostBack="True"></asp:dropdownlist></td>
								<TD style="HEIGHT: 27px"></TD>
							</tr>
							<TR>
								<TD style="WIDTH: 155px"><STRONG><STRONG><STRONG> No. Reg. Gyro</STRONG></STRONG></STRONG></TD>
								<TD style="WIDTH: 9px">
									<asp:label id="lblColonKategori" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 198px">
									<asp:label id="lblRegNumber" runat="server"></asp:label></TD>
								<TD style="WIDTH: 125px"><STRONG><asp:label id="lblBank" runat="server">Nama Bank</asp:label></STRONG></TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD style="WIDTH: 138px" align="left"><asp:dropdownlist id="ddlBank" runat="server" Width="278px" AutoPostBack="True" OnChange="CheckGyroLength(this.id);"></asp:dropdownlist></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 155px; HEIGHT: 25px"><STRONG>
										<asp:label id="lblKategori" runat="server">Kategori</asp:label></STRONG></TD>
								<TD style="WIDTH: 9px; HEIGHT: 25px">:</TD>
								<TD style="WIDTH: 198px; HEIGHT: 25px"><asp:dropdownlist id="ddlCategory" runat="server" Width="142px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 125px; HEIGHT: 25px"><STRONG>No Gyro</STRONG></TD>
								<TD style="WIDTH: 8px; HEIGHT: 25px">:</TD>
								<TD style="WIDTH: 138px; HEIGHT: 25px" align="left"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtGyroNumber" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										onkeyup="CheckGyroLength(this.id);" runat="server" Width="152px" onchange="EnableButtonSave();"></asp:textbox></TD>
								<TD style="HEIGHT: 25px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 155px; HEIGHT: 28px"><STRONG><STRONG><STRONG><STRONG>Tanggal Permintaan Kirim</STRONG></STRONG></STRONG></STRONG></TD>
								<TD style="WIDTH: 9px; HEIGHT: 28px">:</TD>
								<TD style="WIDTH: 198px; HEIGHT: 28px"><cc1:inticalendar id="calReqDate" runat="server" CanPostBack="True" TextBoxWidth="70"></cc1:inticalendar><asp:button id="btnCheckGyroDate" Text="Check" Runat="server" Visible="False"></asp:button></TD>
								<TD style="WIDTH: 125px; HEIGHT: 28px"><STRONG><STRONG>
											<asp:label id="lblGyroDate" runat="server">Tanggal Gyro</asp:label></STRONG></STRONG></TD>
								<TD style="WIDTH: 8px; HEIGHT: 28px">:</TD>
								<TD style="WIDTH: 138px; HEIGHT: 28px" align="left"><cc1:inticalendar id="calBaseline" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
								<TD style="HEIGHT: 28px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 155px; HEIGHT: 13px"><STRONG><STRONG><STRONG><STRONG>Tujuan Entry</STRONG></STRONG></STRONG></STRONG></TD>
								<TD style="WIDTH: 9px; HEIGHT: 13px">:</TD>
								<TD style="WIDTH: 198px; HEIGHT: 13px">
									<asp:dropdownlist id="ddlGyroType" runat="server" AutoPostBack="True" Width="152px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 125px; HEIGHT: 13px"><STRONG><STRONG>
											<asp:label id="lblRef" runat="server">Referensi</asp:label></STRONG></STRONG></TD>
								<TD style="WIDTH: 8px; HEIGHT: 13px">
									<asp:label id="lblColonRef" runat="server">:</asp:label></TD>
								<TD style="WIDTH: 138px; HEIGHT: 13px" align="left">
									<asp:label id="lblRefSlipNumber" runat="server"></asp:label>
								<TD style="HEIGHT: 13px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 155px; HEIGHT: 5px"><B><STRONG>Tujuan Pembayaran</STRONG></B></TD>
								<TD style="WIDTH: 9px; HEIGHT: 5px">:</TD>
								<TD style="WIDTH: 198px; HEIGHT: 5px">
									<asp:dropdownlist id="ddlPaymentPurpose" runat="server" AutoPostBack="True" Width="142px" Height="32px"></asp:dropdownlist></TD>
								<TD style="WIDTH: 125px; HEIGHT: 5px"><STRONG><STRONG><STRONG><asp:label id="lblAcc" runat="server">Tanggal Percepatan</asp:label></STRONG></STRONG></STRONG></TD>
								<td style="WIDTH: 8px; HEIGHT: 5px">
									<asp:label id="lblColonAccelerate" runat="server">:</asp:label></td>
								<td style="WIDTH: 138px; HEIGHT: 5px" align="left">
									<table cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td>
												<cc1:inticalendar id="calAccelerated" runat="server"></cc1:inticalendar>
											</td>
											<td>
												<asp:Button Runat="server" ID="btnUpdateDTG" Text="Update Selisih &amp; PPh" Visible="False"></asp:Button>
											</td>
										</tr>
									</table>
								</td>
								<TD style="HEIGHT: 5px"><asp:label id="lblAcceleratedDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 155px"><STRONG>Total Gyro Amount</STRONG></TD>
								<TD style="WIDTH: 9px">:</TD>
								<TD style="WIDTH: 198px"><asp:label id="lblTotalGyro" runat="server" Font-Bold="True">0</asp:label></TD>
								<TD style="WIDTH: 125px"></TD>
								<TD style="WIDTH: 8px"></TD>
								<TD style="WIDTH: 138px" align="right"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD colSpan="7"><BR>
								</TD>
							</TR>
							<TR>
								<TD colSpan="7">
									<DIV id="divHidden" style="OVERFLOW: auto; WIDTH: 800px; HEIGHT: 290px">
										<asp:datagrid id="dtgMain" runat="server" Width="100%" CellSpacing="1" GridLines="Horizontal"
											CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
											AutoGenerateColumns="False" PageSize="25" AllowCustomPaging="true" AllowPaging="false" AllowSorting="True"
											DataKeyField="ID" ShowFooter="True">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Dealer PO Number">
													<HeaderStyle ForeColor="White" Width="150px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblAssignment"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:DropDownList Runat="server" ID="ddlAssignmentE" Width="150px" OnChange="AssignmentChanged(this);"></asp:DropDownList>
														<asp:Button Runat="server" ID="btnAssignE" CommandName="AssignmentChangeE" style="visibility:hidden;"></asp:Button>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:DropDownList Runat="server" ID="ddlAssignmentF" Width="150px" OnChange="AssignmentChanged(this);"></asp:DropDownList>
														<asp:Button Runat="server" ID="btnAssignF" CommandName="AssignmentChangeF" style="visibility:hidden;"></asp:Button>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Assignment">
													<HeaderStyle ForeColor="White" Width="150px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblPO"></asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:Label Runat="server" ID="lblPOE"></asp:Label>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:Label Runat="server" ID="lblPOF"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="VH">
													<HeaderStyle ForeColor="White" Width="50px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblVH">&nbsp;</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:Label runat="server" ID="lblVHE">&nbsp;</asp:Label>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblVHF">&nbsp;</asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="PP">
													<HeaderStyle ForeColor="White" Width="50px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblPP">&nbsp;</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:Label runat="server" ID="lblPPE">&nbsp;</asp:Label>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblPPF">&nbsp;</asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="IT">
													<HeaderStyle ForeColor="White" Width="50px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblIT">&nbsp;</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:Label runat="server" ID="lblITE">&nbsp;</asp:Label>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblITF">&nbsp;</asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Gyro Amount">
													<HeaderStyle ForeColor="White" Width="150px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblAmount">&nbsp;</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox Runat="server" ID="txtAmountE" Width="150px" onkeypress="return numericOnlyUniv(event)"
															CssClass="textRight" onkeyup="AutoThousandSeparator(this,event);"></asp:TextBox>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox Runat="server" ID="txtAmountF" Width="150px" onkeypress="return numericOnlyUniv(event)"
															CssClass="textRight" onkeyup="AutoThousandSeparator(this,event);"></asp:TextBox>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Selisih">
													<HeaderStyle ForeColor="White" Width="50px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblDiffInt">&nbsp;</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:Label runat="server" ID="lblDiffIntE">&nbsp;</asp:Label>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblDiffIntF">&nbsp;</asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="PPh">
													<HeaderStyle ForeColor="White" Width="50px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblPPh">&nbsp;</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:Label runat="server" ID="lblPPhE">&nbsp;</asp:Label>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblPPhF">&nbsp;</asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Ref 1">
													<HeaderStyle ForeColor="White" Width="150px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblRef1">&nbsp;</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox Runat="server" ID="txtRef1E" Width="150px"></asp:TextBox>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblRef1F"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Ref 2">
													<HeaderStyle ForeColor="White" Width="150px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblRef2">&nbsp;</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox Runat="server" ID="txtRef2E" Width="150px"></asp:TextBox>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblRef2F"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Ref 3">
													<HeaderStyle ForeColor="White" Width="150px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblRef3">&nbsp;</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox Runat="server" ID="txtRef3E" Width="150px"></asp:TextBox>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblRef3F"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reason">
													<HeaderStyle ForeColor="White" Width="150px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ID="lblReason">&nbsp;</asp:Label>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox Runat="server" ID="txtReasonE" Width="150px"></asp:TextBox>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:Label runat="server" ID="lblReasonF"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
													CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
													EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:EditCommandColumn>
												<asp:TemplateColumn>
													<HeaderStyle ForeColor="White" Width="10px" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Del">
															<img src="../images/trash.gif" border="0" alt="Tambah">
														</asp:LinkButton>
													</ItemTemplate>
													<FooterTemplate>
														<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah">
														</asp:LinkButton>
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></DIV>
								</TD>
							</TR>
							<TR>
								<TD background="../images/bg_hor.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="7"><BR>
									<asp:button id="btnBack" runat="server" Width="60px" Text="Kembali"></asp:button>&nbsp;
									<asp:button id="btnSave" runat="server" Width="60px" Text="Simpan"></asp:button></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			<cc1:inticalendar id="calTest" runat="server" CanPostBack="True" style="VISIBILITY:hidden"></cc1:inticalendar>
		</form>
		<script>
			CheckAutoBack();
			function CheckAutoBack(){
					
				var btnBack = document.getElementById("btnBack");
				if(btnBack){
					if(btnBack.value=="autoback"){
						alert('Simpan Data Berhasil');
						btnBack.click();
					}
				}
			}
		</script>
	</body>
</HTML>
