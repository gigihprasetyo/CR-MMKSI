<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpRedemptionDetail2.aspx.vb" Inherits="PopUpRedemptionDetail2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Redemption Detail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self"></base>
		
		<style>
			.but_lock { BORDER-BOTTOM: #c9c9c9 1px solid; TEXT-ALIGN: right; BORDER-LEFT: #c9c9c9 1px solid; BACKGROUND-COLOR: #9fb2ea; BORDER-TOP: #c9c9c9 1px solid; CURSOR: pointer; BORDER-RIGHT: #c9c9c9 1px solid }
			.but_lock_focus { BORDER-BOTTOM: #c9c9c9 1px solid; TEXT-ALIGN: right; BORDER-LEFT: #c9c9c9 1px solid; BACKGROUND-COLOR: #6284ea; BORDER-TOP: #c9c9c9 1px solid; CURSOR: pointer; FONT-WEIGHT: bold; BORDER-RIGHT: #c9c9c9 1px solid }
			.info_lock { TEXT-ALIGN: right; FONT-WEIGHT: bold; background-colorX: #9fb2ea }
		</style>
		<script>
			function SetButtonLock(div,IsFocused){
				if(IsFocused==1){ div.className="but_lock_focus";}
				else {div.className="but_lock";}
			}
			function UpdateData()
			{
				var txtsDay = document.getElementById("txtsDay");
				var txtsTotRow = document.getElementById("txtsTotRow");
				var txtsTotCol = document.getElementById("txtsTotCol");				
				var sReturn ='';
				
				sReturn=txtsDay.value+'#'+txtsTotRow.value+'#'+txtsTotCol.value;
				
				if(navigator.appName == "Microsoft Internet Explorer")
				{
					window.returnValue = sReturn;// 1;
				}
				else
				{
					window.opener.dialogWin.returnFunc(sReturn);//1);
				}
				window.close();
			}
			function UpdateParent(sDay,sTotRow,sTotCol){
				var txtTest = document.getElementById("txtTest");
				var prnDoc = window.dialogArguments;
				txtTest.value=prnDoc.getElementById("txtIsUpdated");
				return false;
				alert(window.opener.document.getElementById("txtIsUpdated"));
				window.opener.document.getElementById("txtsDay").value="1000";
				window.opener.document.getElementById("txtIsUpdated").value="1";
				
			}
			function UpdateDisplay(sDay, sTotRow, sTotCol){
				//window.returnValue=sDay;window.close();return false;
				//document.getElementById("txtTest").value = sDay;return false;
				var txtsDay = document.getElementById("txtsDay");
				var txtsTotRow = document.getElementById("txtsTotRow");
				var txtsTotCol = document.getElementById("txtsTotCol");
				
				txtsDay.value=sDay;
				txtsTotRow.value=sTotRow;
				txtsTotCol.value=sTotCol;
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none">
				<asp:TextBox Runat="server" ID="txtsDay" Text="" />
				<asp:TextBox Runat="server" ID="txtsTotRow" Text="" />
				<asp:TextBox Runat="server" ID="txtsTotCol" Text="" />
			</div>
			<TABLE id="Table2" style="WIDTH: 397px; HEIGHT: 316px" cellSpacing="0" cellPadding="0"
				width="397" align="center" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px" align="center"><asp:label id="lblVehicle" Runat="server" text="Vehicle Type"></asp:label><br>
						Permintaan Kirim
						<asp:label id="lblPeriod" Runat="server" text="21 April 2010"></asp:label></td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" style="WIDTH: 448px; HEIGHT: 240px" cellSpacing="1" cellPadding="2"
							width="448" border="0">
							<tr>
								<td>
									<table width="100%">
										<tr>
											<td>Harga Satuan :</td>
											<td><asp:label id="lblPrice" runat="server" width="150px">0</asp:label></td>
											<td align="right">
												<div class="but_lock" onclick="document.getElementById('btnSet').click();" onmouseover="SetButtonLock(this,1);"
													onmouseout="SetButtonLock(this,0);" style="DISPLAY:none">
													<table>
														<tr>
															<td><asp:Label Runat="server" ID="lblInfoLock">Kunci dari perhitungan otomatis</asp:Label></td>
															<td><img runat="server" id="imgSet" src="../images/lock.gif" width="16" height="16">&nbsp;</td>
														</tr>
													</table>
												</div>
												<asp:Button Runat="server" ID="btnSet" Text="Set" stylex="DISPLAY:none" Width="198px"></asp:Button>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<TR>
								<TD class="titleField" align="center" colSpan="6">
									<div id="div1" style="WIDTH:100%; OVERFLOW:auto"><asp:datagrid id="dtgMain" runat="server" Width="504px" BackColor="#E0E0E0" AutoGenerateColumns="False"
											BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="True">
											<FooterStyle ForeColor="#003399" VerticalAlign="Top" BackColor="White"></FooterStyle>
											<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle VerticalAlign="Top"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2px" CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="2" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblNo" runat="server" text= '<%# container.itemindex+1 %>'>
														</asp:Label>
														<asp:TextBox ID="txtID" Runat="server" Text="0" style="visibility:hidden;"></asp:TextBox>
													</ItemTemplate>
													<FooterStyle Font-Size="Small"></FooterStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Cara Pembayaran">
													<HeaderStyle Width="80px" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Width="13%" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblTOP" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:DropDownList ID="ddlFooterTOP" Runat="server"></asp:DropDownList>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:DropDownList ID="ddlEditTOP" Runat="server"></asp:DropDownList>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Jml Unit">
													<HeaderStyle Width="10px" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right" Width="10px" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblQty" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterQty" runat="server" Width="30" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtEditQty" runat="server" Width="30" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="JK">
													<HeaderStyle Width="10px" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Right" Width="10px" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblJK" Runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<asp:TextBox id="txtFooterJK" runat="server" Width="30" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtEditJK" runat="server" Width="30" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Total Harga">
													<HeaderStyle Width="10px" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label id="lblTotal" runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:label ID="lblFooterTotal" Runat="server"></asp:label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:Label ID="lblEditTotal" Runat="server"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Ceiling">
													<HeaderStyle Width="10px" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label id="lblCeiling" runat="server"></asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:label ID="lblFooterCeiling" Runat="server"></asp:label>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:Label ID="lblEditCeiling" Runat="server"></asp:Label>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Info">
													<HeaderStyle Width="10px" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
													<ItemTemplate>
														<asp:Label id="lblInfo" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>												
												<asp:TemplateColumn>
													<HeaderStyle Width="20px" CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="true" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus">
														</asp:LinkButton>
														<asp:Label id="lblViewPK" runat="server" Visible="False">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="PK Header">
														</asp:Label>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Left"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="Linkbutton1" runat="server" CausesValidation="true" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah">
														</asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<table>
															<tr>
																<td>
																	<asp:LinkButton id="lbtnSave" tabIndex="40" Runat="server" CausesValidation="True" CommandName="Update"
																		text="Simpan">
																		<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
																</td>
																<td>
																	<asp:LinkButton id="lbtnCancel" tabIndex="50" Runat="server" CausesValidation="True" CommandName="Cancel"
																		text="Batal">
																		<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
																</td>
															</tr>
														</table>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<tr>
								<td>
									<table style="WIDTH: 100%; HEIGHT: 57px" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td style="WIDTH: 103px; HEIGHT: 14px"><b>Jumlah Unit (RD)</b></td>
											<td style="WIDTH: 8px; HEIGHT: 14px"><b>:</b></td>
											<td style="HEIGHT: 14px"><b><asp:label id="lblTotalQty" Runat="server" Text="0" Width="150px" style="TEXT-ALIGN:right"></asp:label></b></td>
											<TD style="HEIGHT: 14px"></TD>
											<td style="WIDTH: 20px; HEIGHT: 14px">
												<div class="info_lock">
													<table>
														<tr>
															<td>Status:&nbsp;</td>
															<td><asp:Label Runat="server" ID="lblStatusLock" Font-Bold="True">TERBUKA</asp:Label></td>
															<td><img runat="server" id="imgStatus" src="../images/lock.gif" width="26" height="26"></td>
														</tr>
													</table>
												</div>
											</td>
										</tr>
										<TR>
											<TD><STRONG>Jumlah Unit (JK)</STRONG></TD>
											<TD>:</TD>
											<TD>
												<b></VAR><asp:label id="lblTotalJK" style="TEXT-ALIGN: right" Runat="server" Width="150px" Text="0">0</asp:label></b></TD>
											<TD></TD>
											<TD>
											</TD>
										</TR>
										<tr>
											<td style="WIDTH: 103px; HEIGHT: 17px"><b>Total Harga</b></td>
											<td style="WIDTH: 8px; HEIGHT: 17px"><b>:</b></td>
											<td style="HEIGHT: 17px"><b><asp:Label ID="lblTotalPrice" Runat="server" Text="0" Width="150px" style="TEXT-ALIGN:right"></asp:Label></b></td>
											<TD style="HEIGHT: 17px"></TD>
											<td style="WIDTH: 20px; HEIGHT: 17px"></td>
										</tr>
										<TR>
											<TD colspan="5" style="WIDTHx: 103px; HEIGHT: 17px">
												<hr>
											</TD>
											<!--
											<TD style="WIDTH: 8px; HEIGHT: 17px"></TD>
											<TD style="HEIGHT: 17px"></TD>
											<TD style="HEIGHT: 17px"></TD>
											<TD style="WIDTH: 20px; HEIGHT: 17px"></TD>
											-->
										</TR>
										<TR>
											<TD style="WIDTH: 103px; HEIGHT: 17px"><STRONG>Ceiling RTGS</STRONG></TD>
											<TD style="WIDTH: 8px; HEIGHT: 17px"><STRONG>:</STRONG></TD>
											<TD style="HEIGHT: 17px"><STRONG>
													<asp:label id="lblCeilingRTGS" style="Z-INDEX: 0; VISIBILITY: visible" runat="server" Height="1px">0</asp:label></STRONG></TD>
											<TD style="HEIGHT: 17px"></TD>
											<TD style="WIDTH: 20px; HEIGHT: 17px"></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 103px; HEIGHT: 17px"><STRONG>Ceiling COD</STRONG></TD>
											<TD style="WIDTH: 8px; HEIGHT: 17px"><STRONG>:</STRONG></TD>
											<TD style="HEIGHT: 17px"><STRONG>
													<asp:label id="lblCeilingCOD" style="Z-INDEX: 0; VISIBILITY: visible" runat="server" Height="1px">0</asp:label></STRONG></TD>
											<TD style="HEIGHT: 17px"></TD>
											<TD style="WIDTH: 20px; HEIGHT: 17px"></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 103px; HEIGHT: 17px"><STRONG>Ceiling TOP</STRONG></TD>
											<TD style="WIDTH: 8px; HEIGHT: 17px"><STRONG>:</STRONG></TD>
											<TD style="HEIGHT: 17px"><STRONG>
													<asp:label id="lblCeilingTOP" style="Z-INDEX: 0; VISIBILITY: visible" runat="server" Height="1px">0</asp:label></STRONG></TD>
											<TD style="HEIGHT: 17px"></TD>
											<TD style="WIDTH: 20px; HEIGHT: 17px"></TD>
										</TR>
										<tr>
											<td colspan="5">
												<hr>
											</td>
										</tr>
										<tr style="DISPLAY:none">
											<td colspan="4">
												<asp:Button ID="btnCalculate" Runat="server" Text="Hitung"></asp:Button>
											</td>
											<TD style="WIDTH: 26px"></TD>
										</tr>
									</table>
								</td>
							</tr>
							<TR style="DISPLAY:none">
								<TD class="titleField" style="HEIGHT: 11px" align="center" colSpan="6">
									TOP: COD: RTGS:
									<asp:TextBox id="txtCeilingInfo" runat="server" Width="392px" Height="58px" TextMode="MultiLine"></asp:TextBox>
								</TD>
							</TR>
							<tr>
								<td align="center">
									<input type="button" value="OK" onclick="UpdateData();" width="70" style="WIDTH: 93px; HEIGHT: 21px">
								</td>
							</tr>
							<TR style="VISIBILITY: hidden">
								<TD class="titleField" style="HEIGHT: 11px" align="center" colSpan="6">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td align="right"><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" style="VISIBILITY: hidden"></asp:button></td>
											<td align="left"><asp:button id="btnBack" runat="server" Width="60px" Text="Kembali" CausesValidation="False"
													style="VISIBILITY: hidden"></asp:button></td>
										</tr>
									</table>
								</TD>
							</TR>
							
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
