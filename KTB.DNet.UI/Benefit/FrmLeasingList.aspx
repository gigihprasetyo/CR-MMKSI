<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmLeasingList.aspx.vb" Inherits="FrmLeasingList" smartNavigation="False"%>
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
		    function clearform() {
		        document.getElementById('txtBenefitName').value = '';
		      
		    }
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="5" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px" colSpan="2">SALES CAMPAIGN - LEASING</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="2" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" colSpan="2" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>

                <tr>
					<td colspan="2">
                        <asp:Panel ID="formLeasing" runat="server" >
                            <table>
                                <tr style="display:none">
					                <td class="titleField" width="20%">Id&nbsp;</td>
					                <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="hfID" Enabled="false"
							                runat="server" Width="242px"></asp:textbox></td>
				                </tr>
                                
                                 <tr>
					                <td class="titleField" width="20%">Kode Leasing&nbsp;</td>
					                <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtVendorId" 
							                runat="server" Width="242px"></asp:textbox></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Nama Leasing&nbsp;</td>
					                <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtLeasingName" 
							                runat="server" Width="242px"></asp:textbox></td>
				                </tr>

				               			
				                <tr>
					                <td class="titleField" width="20%">&nbsp;</td>
					                <td>    
                                             <asp:button id="btnCari" runat="server" Text="Cari" width="60px"></asp:button>&nbsp;          
                                        <asp:button id="btnSimpan" runat="server" Text="Simpan" width="60px"></asp:button>&nbsp;
                                      <!--  <input type="button" value="Batal"  style="width:60px" onclick="clearform()" /> -->
						                <asp:button id="btnBatal" runat="server" Text="Batal" width="60px" CausesValidation="False"></asp:button>
					                </td>
				                </tr>

                            </table>

                        </asp:Panel>
                    </td>
				</tr>
                 <tr>
					<td colspan="2">
                         
                        <asp:Panel ID="formGrid" runat="server"  Visible="true">
                            <asp:button id="btnTambah" Visible="false" runat="server" Text="Tambah" width="60px" CausesValidation="False"></asp:button>
                            	<div id="div1" style="OVERFLOW: auto; HEIGHT: 440px">
                                        <asp:datagrid id="dgTable" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
											PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3" DataKeyField="ID">
                                             <SelectedItemStyle Font-Bold="True" BackColor="#738A9C"></SelectedItemStyle>
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
												
                                                <asp:TemplateColumn HeaderText="Kode Leasing">
													<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblVendorId" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nama Leasing">
													<HeaderStyle Width="80%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblLeasingName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												
												<asp:TemplateColumn>
													<HeaderStyle  CssClass="titleTableSales"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>														
														<asp:LinkButton id="lnkbtnView" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
															<img alt="Detail"  title="Detail" src="../images/Detail.gif" border="0"/></asp:LinkButton>
														<asp:LinkButton id="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' >
															<img alt="Ubah"  title="Ubah" src="../images/edit.gif" border="0" /></asp:LinkButton>
														<asp:LinkButton ID="lnkbtnDelete" Runat = "server" Width = "20px" Text = "Hapus" CausesValidation = "False" CommandName ="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img  onclick="return confirm('Yakin ingin menghapus data ini?');" src="../images/trash.gif"  
																border="0" alt="Hapus" title="Hapus"/></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>

                        </asp:Panel>
                    </td>
				</tr>
				
				
			</TABLE>
		</form>
	</body>
</HTML>
