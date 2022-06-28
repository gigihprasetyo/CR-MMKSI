<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadSPPOExcel.aspx.vb" Inherits="FrmUploadSPPOExcel" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEntrySparePartPO</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		    function ShowPQRNoSelection() {
		        showPopUp('../PopUp/PopUpPQRSelectionSingle.aspx', '', 500, 760, PQRNoSelection);
		    }
		    function PQRNoSelection(selectedPQRNo) {
		        var txtPQRNo = document.getElementById("txtPQRNo");
		        txtPQRNo.value = selectedPQRNo;
		        __doPostBack("txtPQRNo", "");
		    }

		    function GetCurrentInputIndex()
			{
				var dgPODetail = document.getElementById("dtgPODetail");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dgPODetail.rows.length; index++)
				{
					inputs = dgPODetail.rows[index].getElementsByTagName("INPUT");
					
					if (inputs != null && inputs.length > 0)
					{
						for (indexInput = 0; indexInput < inputs.length; indexInput++)
						{	
							if (inputs[indexInput].type != "hidden")
								return index;
						}
					}
				}
				
				return -1;
			}
			
		    function ShowPopUpSparePart(intPQRHeaderID) {
		        showPopUp('../PopUp/PopUpSparePart.aspx?PQRHeaderID=' + intPQRHeaderID, '', 510, 700, SparePart);
		    }

		    function SparePart(selectedCode)
			{
				var tempParam = selectedCode.split(';');	
				var indek = GetCurrentInputIndex();
				var dgPODetail = document.getElementById("dtgPODetail");
				var partCode = dgPODetail.rows[indek].getElementsByTagName("INPUT")[0];
				var partName = dgPODetail.rows[indek].getElementsByTagName("SPAN")[1];
				var partPrice = dgPODetail.rows[indek].getElementsByTagName("SPAN")[2];	
				var partAmount = dgPODetail.rows[indek].getElementsByTagName("SPAN")[3];
				var partQTY = dgPODetail.rows[indek].getElementsByTagName("INPUT")[1];
				
					partCode.value = tempParam[0];
					partName.innerHTML = tempParam[1];				
					if  (tempParam[2]=="")
						partPrice.innerHTML = "N.A";
					else
						partPrice.innerHTML = parseFloat(tempParam[2]);
					partAmount.innerHTML="0";		
				
				partQTY.value="";
				partQTY.focus();
			}
			
			function CalculateAmount()
			{	var indek = GetCurrentInputIndex();
				var dgPODetail = document.getElementById("dtgPODetail");
				var partQTY =parseInt(dgPODetail.rows[indek].getElementsByTagName("INPUT")[1].value);
				var partAmount = dgPODetail.rows[indek].getElementsByTagName("SPAN")[3];
					var partPrice = parseFloat(dgPODetail.rows[indek].getElementsByTagName("SPAN")[2].innerHTML);
					if (isNaN(partPrice)|| partPrice=="")	
					{
						partPrice=0;
					}									
					var amount=parseInt(partQTY)*parseFloat(partPrice);
					partAmount.innerHTML=amount;				
			}
			
			
			
			function QtyValidate(source, arguments)
			{
				var indek = GetCurrentInputIndex();	
				var dgPODetail = document.getElementById("dtgPODetail");
				var partQTY =parseInt(dgPODetail.rows[indek].getElementsByTagName("INPUT")[1].value);							
				if (partQTY >0)
				{
					arguments.IsValid = true;
				}
				else
				{					
					arguments.IsValid = false;					
				}
			}
			
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">PEMESANAN - Upload&nbsp;Melalui File Excel</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="0" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<tr>
								<td class="titleField" width="24%">Kode Dealer</td>
								<td width="1%">:</td>
								<TD width="75%"><asp:label id="lblDealerCode" runat="server">Label</asp:label></TD>
							</tr>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<td>:</td>
								<TD><asp:label id="lblDealerName" runat="server">Label</asp:label>/
									<asp:label id="lblDealerTerm" runat="server">Label</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Tipe Order</TD>
								<td>:</td>
								<TD><asp:dropdownlist  AutoPostBack="True" id="ddlOrderType" runat="server" Width="140px" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR id="trPQRNo" runat="server" visible="false">
								<TD class="titleField">Nomor PQR</TD>
								<td width="1%">:</td>
								<TD colSpan="2">
                                    <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}');" id="txtPQRNo"
										onblur="omitSomeCharacter('txtPQRNo','<>?*%^():|\@#$;+=`~{}');" runat="server" Width="150px"></asp:textbox>&nbsp;
                                    <asp:Label ID="lblSearchPQRNo" runat="server">
										    <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Nomor /Tanggal PO</TD>
								<td>:</td>
								<TD>
									<table cellSpacing="0" cellPadding="2" border="0">
										<tr>
											<td><asp:textbox id="txtPONumber" runat="server" Width="140px" ReadOnly="True">[Dibuat oleh sistem]</asp:textbox></td>
											<td><cc1:inticalendar id="icOrderDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
							</TR>
							<tr>
								<td class="titleField">Nilai Pemesanan</td>
								<td>:</td>
								<td><B>Rp
										<asp:Label id="lblPOAmount" runat="server"></asp:Label><asp:label id="lblTest" runat="server"></asp:label></B></td>
							</tr>
                            <tr>
                                <td class="titleField">Cara Pembayaran</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                                </td>
                            </tr>
							<TR>
								<TD class="titleField">Lokasi File</TD>
								<td>:</td>
								<TD><INPUT id="DataFile" type="file" size="40" name="File1" runat="server">
									<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid" ControlToValidate="DataFile"
										ValidationExpression="^(([a-zA-Z]:\\)|(\\\\))(\w[\w].*)">*</asp:RegularExpressionValidator>&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btnUpload" runat="server" Width="70px" Text="Upload" CausesValidation="False"></asp:button> &nbsp;
                                    <asp:button id="btnDownloadSample" runat="server" Width="110px" Text="Download Sample"></asp:Button></TD>
							</TR>
						</TABLE>
						<asp:datagrid id="dtgPODetail" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
							BorderColor="Gainsboro" BackColor="#CDCDCD" ShowFooter="True" AutoGenerateColumns="False"
							AllowSorting="True">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
									<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="PartNumberTemp" HeaderText="Nomor Barang">
									<HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNumberTemp") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtFPartNumber" runat="server" size="10" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" 
                                            onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
										<asp:Label id="lblFPopUpSparePart" runat="server" width="10">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=txtEPartNumber runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNumberTemp") %>' size="10">
										</asp:TextBox>
										<asp:Label id="lblEPopUpSparePart" runat="server" Text="..." width="10">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="PartNameTemp" HeaderText="Nama Barang">
									<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=lblPartname runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNameTemp") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="lblFPartName" runat="server"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id=lblEPartName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNameTemp") %>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Quantity" HeaderText="Jumlah">
									<HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' CssClass="textRight">
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox onkeypress="return numericOnlyUniv(event)" id="txtFQTY" runat="server" size="5"
											CssClass="textRight" MaxLength="6" onchange="CalculateAmount()"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox onkeypress="return numericOnlyUniv(event)" id=txtEQTY runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' size="5" CssClass="textRight" MaxLength="6" onchange="CalculateAmount()">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="RetailPrice" HeaderText="Harga Eceran (Rp)">
									<HeaderStyle HorizontalAlign="Right" Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblRetailPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RetailPrice","{0:#,##0}") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<asp:Label id="lblFRetailPrice" runat="server"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id=lblERetailPrice runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RetailPrice","{0:###}") %>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Amount" HeaderText="Total Harga (Rp)">
									<HeaderStyle HorizontalAlign="Right" Width="8%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id=lblPOAmount runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount","{0:#,##0}") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<asp:Label id="lblFPOAmount" runat="server"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id=lblEPOAmount runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount","{0:#,##0}") %>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Aksi">
									<HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnEdit" CausesValidation="False" Runat="server" text="Ubah" CommandName="edit">
											<img border="0" src="../images/edit.gif" alt="Ubah" style="cursor:hand"></asp:LinkButton>
										<asp:LinkButton id="lbtnDelete" CausesValidation="False" Runat="server" text="Hapus" CommandName="delete">
											<img src="../images/trash.gif" alt="Hapus" border="0" style="cursor:hand"></asp:LinkButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="lbtnAdd" Runat="server" text="Tambah" CommandName="add">
											<img src="../images/add.gif" border="0" alt="Tambah" align="center" align="middle" style="Cursor:hand"></asp:LinkButton>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:LinkButton id="lbtnSave" tabIndex="40" CommandName="save" text="Simpan" Runat="server">
											<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
										<asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="cancel" text="Batal" Runat="server">
											<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ErrorMessage" SortExpression="ErrorMessage" ReadOnly="True" HeaderText="Pesan">
									<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD height="40"><asp:button id="btnNew" runat="server" Width="60px" Text="Baru" CausesValidation="False"></asp:button><asp:button id="btnCancel" runat="server" Width="60px" Text="Batal" CausesValidation="False"
							Enabled="False"></asp:button><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" CausesValidation="False"></asp:button><asp:button id="btnPrint" runat="server" Width="60px" Text="Cetak" CausesValidation="False"
							Enabled="False"></asp:button><asp:button id="btnSubmit" runat="server" Width="60px" Text="Kirim" CausesValidation="False"
							Enabled="False"></asp:button></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
			<INPUT id="hid_f" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 432px" type="hidden"
				value="0" name="hid_f" runat="server">
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
