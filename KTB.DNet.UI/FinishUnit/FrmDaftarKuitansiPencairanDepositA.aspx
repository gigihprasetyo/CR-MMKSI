<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDaftarKuitansiPencairanDepositA.aspx.vb" Inherits="FrmDaftarKuitansiPencairanDepositA" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDaftarKuitansiPencairanDepositA</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<script language="javascript">
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
			}

			function DealerSelection(selectedDealer)
			{
				var tempParam= selectedDealer;
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = tempParam;
			}

		function GetSelectedKuitansi()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dgDaftarPengajuanKuitansiDepositA");
			var Kuitansi ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (Kuitansi == '')
						{
							Kuitansi = replace(table.rows[i].cells[1].innerText,' ','');
						}
						else
						{
							Kuitansi = Kuitansi + ';' + replace(table.rows[i].cells[1].innerText,' ','');
						}
					window.returnValue = Kuitansi;
					bcheck=true;
					}
					else
					{
						if (Kuitansi == '')
						{
							Kuitansi = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							Kuitansi = Kuitansi + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						bcheck=true;
					}
				}
			}
			if (bcheck)
			  {
					//window.close();
					
					if(navigator.appName != "Microsoft Internet Explorer")
					{	//opener.dialogWin.returnFunc(Kuitansi);
						showPopUp('../PopUp/PopUpUpload2SAP.aspx?KuitansiID=' + Kuitansi ,'',500,760,UploadStatus);
					}
			  }
			else
			  {
				alert("Silahkan Pilih Kuitansi terlebih dahulu");	
			  }
		}

		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
						elm.checked = checkVal
					}
				}
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" width="140%" border="0">
				<tr>
					<td class="titlePage">Sales - DepositA - Daftar Kuitansi Pencairan</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td>
						<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="titleField" style="WIDTH: 152px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="lblCode" Runat="server">Kode Dealer*</asp:label></td>
								<td style="WIDTH: 2px">:</td>
								<td style="WIDTH: 186px"><asp:textbox id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="CURSOR:hand" border="0" alt="Klik popup">
									</asp:label></td>
								<td class="titleField" style="WIDTH: 146px"><asp:radiobutton id="rbPeriodeKuitansi" Runat="server" AutoPostBack="True" GroupName="KuitansiSearch"></asp:radiobutton>Tanggal 
									Kuitansi</td>
								<td style="WIDTH: 2px">:
								</td>
								<td class="titleField">
									<table>
										<tr>
											<td><cc1:inticalendar id="icPeriodeFromKuitansi" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>s/d</td>
											<td><cc1:inticalendar id="icPeriodeToKuitansi" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 152px; HEIGHT: 44px"><asp:radiobutton id="rbNoKuitansi" Runat="server" AutoPostBack="True" GroupName="KuitansiSearch"></asp:radiobutton>Nomor 
									Kuitansi</td>
								<td style="WIDTH: 2px; HEIGHT: 44px">:</td>
								<td class="titleField" style="WIDTH: 186px; HEIGHT: 44px"><asp:textbox id="txtNoKuitansi" Runat="server" MaxLength="18"></asp:textbox></td>
								<td class="titleField" style="WIDTH: 146px"><asp:radiobutton id="rbPeriodePengajuan" Runat="server" AutoPostBack="True" GroupName="KuitansiSearch"></asp:radiobutton>Tanggal 
									Pengajuan</td>
								<td style="WIDTH: 2px">:</td>
								<td class="titleField">
									<table>
										<tr>
											<td><cc1:inticalendar id="icPeriodeFromPengajuan" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>s/d</td>
											<td><cc1:inticalendar id="icPeriodeToPengajuan" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="titleField" style="WIDTH: 152px"><asp:radiobutton id="rbNoPengajuan" Runat="server" AutoPostBack="True" GroupName="KuitansiSearch"></asp:radiobutton>No. 
									Ref Surat Pengajuan</td>
								<td style="WIDTH: 2px">:</td>
								<td class="titleField" style="WIDTH: 186px"><asp:textbox id="txtNoPengajuan" runat="server" MaxLength="18"></asp:textbox></td>
								<td class="titleField" style="WIDTH: 146px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="Label2" Runat="server">Tipe Pengajuan</asp:label></td>
								<td style="WIDTH: 2px">:</td>
								<td class="titleField"><asp:dropdownlist id="ddlTipePengajuan" runat="server" Width="112px"></asp:dropdownlist></td>
							</tr>
							<TR>
								<TD class="titleField" style="WIDTH: 152px; HEIGHT: 19px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label1" Runat="server">Status</asp:label></TD>
								<TD style="WIDTH: 2px; HEIGHT: 19px">:</TD>
								<TD class="titleField" style="WIDTH: 186px; HEIGHT: 19px"><asp:dropdownlist id="ddlJenisStatus" runat="server" Width="112px">
										<asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
										<asp:ListItem Value="0">Baru</asp:ListItem>
										<asp:ListItem Value="1">Validasi</asp:ListItem>
										<asp:ListItem Value="10">Konfirmasi</asp:ListItem>
										<asp:ListItem Value="12">Selesai</asp:ListItem>
										<asp:ListItem Value="15">Cancel JV</asp:ListItem>
                                    <asp:ListItem Value="16">Cair</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="titleField" style="WIDTH: 146px; HEIGHT: 19px"><asp:checkbox id="chkTglPencairan" runat="server"></asp:checkbox><asp:label id="Label3" Runat="server">Tgl Pencairan</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD style="WIDTH: 2px; HEIGHT: 19px">:</TD>
								<TD class="titleField" style="HEIGHT: 19px">&nbsp;
									<cc1:inticalendar id="icTglPencairan" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</TR>
                            
                             <tr>

                                 <td class="titleField" style="width: 152px; HEIGHT: 19px"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Produk</td>
                           <TD style="WIDTH: 2px; HEIGHT: 19px">:</TD>
                            <td   class="titleField" style="WIDTH: 186px; HEIGHT: 19px" >

                                <asp:DropDownList Style="z-index: 0" ID="ddlProductCategory" runat="server" Width="140px"></asp:DropDownList>
                            </td>
                               <TD class="titleField"  >NoReg</TD>
                                 <td style="WIDTH: 2px; ">:</td>
                                <td  > <asp:TextBox runat="server"  ID="txtNoReg" placeholder="NoReg"></asp:TextBox> </td>
                            </tr>
                           
							<TR>
								<TD class="titleField" style="WIDTH: 152px; HEIGHT: 19px"></TD>
								<TD style="WIDTH: 2px; HEIGHT: 19px"></TD>
								<TD class="titleField" style="WIDTH: 186px; HEIGHT: 19px"></TD>
								<TD class="titleField" style="WIDTH: 146px; HEIGHT: 19px"></TD>
								<TD style="WIDTH: 2px; HEIGHT: 19px"></TD>
								<TD class="titleField" style="HEIGHT: 19px"></TD>
							</TR>
							<tr>
								<td colSpan="2"></td>
								<td style="WIDTH: 186px"><asp:button id="btnSearch" runat="server" Width="72px" Text="Cari"></asp:button></td>
								<td colSpan="3"></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 270px"><asp:datagrid id="dgDaftarPengajuanKuitansiDepositA" runat="server" Width="100%" DataKeyField="ID"
								AllowSorting="True" PageSize="15" AllowCustomPaging="True" AllowPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro"
								CellSpacing="1" CellPadding="3" BorderWidth="0px">
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked', document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No.">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tipe">
										<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTipe" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblDealer runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="ProductCategory.Code" HeaderText="Produk">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id=lblProdukDetail runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCategory.Code") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

									<asp:TemplateColumn HeaderText="No. Reg">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNoReg" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ReceiptNumber" ReadOnly="True" HeaderText="Nomor Kuitansi">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NoSurat" ReadOnly="True" HeaderText="No.Ref Pengajuan">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TglPencairan" ReadOnly="True" HeaderText="Tanggal Pencairan" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NoJV" ReadOnly="True" HeaderText="No JV">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DNNumber" ReadOnly="True" HeaderText="Nomor DebitNote">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AssignmentNumber" ReadOnly="True" HeaderText="Nomor SO">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TotalAmount" ReadOnly="True" HeaderText="Uang Sejumlah" DataFormatString="{0:#,###}">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" ReadOnly="True" HeaderText="Untuk Pembayaran">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbViewDetail" runat="server" CommandName="ViewDetail" Visible="True" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/detail.gif" border="0" style="cursor:hand">
											</asp:LinkButton>
											<asp:LinkButton id="lbViewFlow" runat="server" CommandName="lbViewFlow" Visible="True">
												<img src="../images/alur_flow.gif" border="0" style="cursor:hand">
											</asp:LinkButton>
											<asp:LinkButton id="lbViewStatus" runat="server" CommandName="lbViewStatus" Visible="True">
												<img src="../images/popup.gif" border="0" style="cursor:hand">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<td>
						<table>
							<TR>
								<TD><asp:label id="lblUbahStatus" runat="server" Font-Bold="True" Font-Italic="True"> Mengubah Status</asp:label></TD>
								<td>:
								</td>
								<td><asp:dropdownlist id="ddlAction" runat="server">
										<asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>
										<asp:ListItem Value="Konfirmasi">Konfirmasi</asp:ListItem>
										<asp:ListItem Value="BatalKonfirmasi">Batal Konfirmasi</asp:ListItem>
										<asp:ListItem Value="Setuju">Setuju</asp:ListItem>
										<asp:ListItem Value="Tolak">Tolak</asp:ListItem>
									</asp:dropdownlist></td>
								<td><asp:button id="btnProses" runat="server" Text="Proses"></asp:button></td>
							</TR>
							<tr>
								<td colspan="3">
									<asp:button id="btnTransferSAP" runat="server" Width="100" text="Transfer ke SAP"></asp:button>
									<asp:button id="btnDownload" runat="server" Width="100" text="Download"></asp:button>
								</td>
							</tr>
						</table>
					</td>
				</TR>
			</table>
		</form>
	</body>
</HTML>
