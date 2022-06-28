<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEntryHargaPasar.aspx.vb" Inherits="FrmEntryHargaPasar" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEntryHargaPasar</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
			function GetCurrentInputIndex()
			{
				var dg = document.getElementById("dgCompetitor");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dg.rows.length; index++)
				{
					inputs = dg.rows[index].getElementsByTagName("INPUT");
					
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
			
			function ShowPPMerk()
			{
				showPopUp('../PopUp/PopUpCarMerk.aspx','',500,760,MerkSelection);
			}
				
			function ShowPPType()
			{
				var indek = GetCurrentInputIndex();
				var dg = document.getElementById("dgCompetitor");
				var Merk = dg.rows[indek].getElementsByTagName("INPUT")[0];	
				if (Merk.value=='')
				{
					alert('Merk belum diisi');
					return;
				}	
				else
				{	
					showPopUp('../PopUp/PopUpCarType.aspx?Merk='+Merk.value,'',500,760,TypeSelection);
				}
			}	
			
			function MerkSelection(selectedVal)
			{
				var tempParam= selectedVal;				
				var indek = GetCurrentInputIndex();
				var dg = document.getElementById("dgCompetitor");
				var Merk = dg.rows[indek].getElementsByTagName("INPUT")[0];				
				Merk.value = tempParam;
			}
			
			function TypeSelection(selectedVal)
			{
				var tempParam= selectedVal.split(';');				
				var indek = GetCurrentInputIndex();
				var dg = document.getElementById("dgCompetitor");
				var Tipe = dg.rows[indek].getElementsByTagName("INPUT")[1];
				var Name = dg.rows[indek].getElementsByTagName("SPAN")[2];				
				Tipe.value = tempParam[0];
				Name.innerHTML = tempParam[1];
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">TRANSAKSI - Masukkan Harga</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Dealer</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="lblDealer" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Perwakilan Area</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:label id="lblPerwakilan" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px; HEIGHT: 23px">Harga Berlaku</TD>
								<TD style="WIDTH: 2px; HEIGHT: 23px">:</TD>
								<TD class="titleField" style="HEIGHT: 23px"><asp:dropdownlist id="ddlPeriod" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 146px">Kategori</TD>
								<TD style="WIDTH: 2px">:</TD>
								<TD class="titleField"><asp:dropdownlist id="ddlCategory" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 340px"><asp:datagrid id="dgCompetitor" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellSpacing="1"
								CellPadding="3" BorderColor="Gainsboro" BackColor="#CDCDCD" BorderWidth="0px" Width="100%">
								<AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#F5F1EE"></FooterStyle>
								<Columns>
									<asp:BoundColumn ReadOnly="True" HeaderText="No">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Tanggal">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label1 runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.ValidDate"),"dd") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:DropDownList id="ddlDateF" runat="server"></asp:DropDownList>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:DropDownList id="ddlDate" runat="server"></asp:DropDownList>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Merk">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblMerk" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompetitorType.CompetitorBrand.Description") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:TextBox id="txtFMerk" tabIndex="10" runat="server" width="70" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
												onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
											<asp:Label id="lblFPopUpMerk" tabIndex="20" runat="server" height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Merk" src="../images/popup.gif"
													border="0"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="txtEMerk" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" tabIndex=10 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompetitorType.CompetitorBrand.Code") %>' width="70">
											</asp:TextBox>
											<asp:Label id="lblEPopUpMerk" tabIndex="20" runat="server" height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Merk" src="../images/popup.gif"
													border="0"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Tipe">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompetitorType.Code") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:TextBox id="txtFType" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"
												tabIndex="10" runat="server" width="70"></asp:TextBox>
											<asp:Label id="lblFType" tabIndex="20" runat="server" height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Type" src="../images/popup.gif"
													border="0"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="txtEType" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" tabIndex=10 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompetitorType.Code") %>' width="70">
											</asp:TextBox>
											<asp:Label id="lblEType" tabIndex="20" runat="server" height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Merk" src="../images/popup.gif"
													border="0"></asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Kendaraan">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompetitorType.Description") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblFName" runat="server"></asp:Label>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:Label id="lblEName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompetitorType.Description") %>'>
											</asp:Label>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="On The Road (Rp)">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblOnTheRoad" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.OnTheRoadPrice"),"#,###") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox id="lblFOnTheRoad" runat="server" CssClass="textRight" onkeypress="return NumericOnlyWith(event,'');"
												onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" MaxLength="12" />
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="lblEOnTheRoad" runat="server" CssClass="textRight" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Text='<%# Format(DataBinder.Eval(Container, "DataItem.OnTheRoadPrice"),"#,###") %>' Width="100px" MaxLength="12" />
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="BBN (Rp)">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblBBN" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.BBN"),"#,###") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox id="lblFBBN" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')"
												runat="server" CssClass="textRight" Width="100px" MaxLength="12"></asp:TextBox>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="lblEBBN" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.BBN"),"#,###") %>' CssClass="textRight" Width="100px" MaxLength="12">
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Keterangan">
										<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblItemOtherInfo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OtherInfo") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:TextBox id="txtFooterOtherInfo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OtherInfo") %>'>
											</asp:TextBox>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:TextBox id="txtEditOtherInfo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OtherInfo") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle HorizontalAlign="Center" CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" text="Ubah" Runat="server">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" text="Hapus" Runat="server">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="lbtnAdd" CommandName="add" text="Tambah" Runat="server">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
										</FooterTemplate>
										<EditItemTemplate>
											<asp:LinkButton id="lbtnSave" tabIndex="40" CommandName="save" text="Simpan" Runat="server">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
											<asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="cancel" text="Batal" Runat="server">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD><asp:button id="btnSave" runat="server" Text="Simpan"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
