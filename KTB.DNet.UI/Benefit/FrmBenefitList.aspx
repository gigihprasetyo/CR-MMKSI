<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmBenefitList.aspx.vb" Inherits="FrmBenefitList" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAplikasiHeader</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		    function ShowPPDealerSelection() {
		        //showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
		        showPopUp('../General/../Benefit/PopUpDealerSelectionBenefit.aspx', '', 500, 760, DealerSelection);
		    }

		    function DealerSelection(selectedDealer) {
		        var txtDealerSelection = document.getElementById("txtCodeDealer");
		        txtDealerSelection.value = selectedDealer;
		    }
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="5" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px" colSpan="2">SALES CAMPAIGN - Daftar Benefit</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="2" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" colSpan="2" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td class="titleField" width="20%">Kode Dealer&nbsp;</td>
					<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtCodeDealer" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
							runat="server" Width="100px"></asp:textbox>
                        &nbsp;<asp:label id="lblPopUpDealer" runat="server" width="16px">
							        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="titleField" width="20%">Benefit Reg No&nbsp;</td>
					<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtRegNo" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
							runat="server" Width="242px"></asp:textbox>                       
					</td>
				</tr>
				<tr>
					<td class="titleField" width="20%">No Surat&nbsp;</td>
					<td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" MaxLength="40" id="txtNoSurat" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
							runat="server" Width="242px"></asp:textbox>  

					</td>
				</tr>
                <tr>
					<td class="titleField" width="20%">Status&nbsp;</td>
					<td>
                        <asp:DropDownList ID="ddlStatus" runat="server">        
                             <asp:ListItem Value="" Text=""></asp:ListItem>    
                             <asp:ListItem Value="0" Text="Aktif"></asp:ListItem>                                
                            <asp:ListItem Value="1" Text="Tidak Aktif"></asp:ListItem>                            
                        </asp:DropDownList>  
					</td>
				</tr>				
				<tr>
					<td class="titleField" width="20%">&nbsp;</td>
					<td>                        
                        <asp:button id="btnSearch" runat="server" Text="Cari" width="60px"></asp:button>&nbsp;
						

                         <asp:button id="btnTambah" runat="server" Text="Tambah" width="60px" Visible="false"></asp:button>&nbsp;
					</td>
				</tr>
				<TR>
					<TD colSpan="2">
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" >
                                        <asp:datagrid id="dgTable" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
											PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3" DataKeyField="ID">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>      
                                                
                                   
                                                                                           
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableSales" ></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblNo" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>												
												<asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="">
													<HeaderStyle Width="50%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblKodeDealer" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												
                                                <asp:TemplateColumn HeaderText="No Surat" SortExpression="NomorSurat">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNoSurat" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Benefit Reg No" SortExpression="BenefitRegNo">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblRegNo" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Remark">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblRemark" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblStatus" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle  CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>														
														<asp:LinkButton id="lnkbtnView" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
															<img alt="Detail" src="../images/Detail.gif" border="0"></asp:LinkButton>
														<asp:LinkButton id="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
															<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
														<asp:LinkButton ID="lnkbtnDelete" Runat = "server" Width = "20px" Text = "Hapus" CausesValidation = "False" CommandName ="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img alt="Delete"  src="../images/trash.gif"
																border="0"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
               
			</TABLE>
		</form>
	</body>
</HTML>
