<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmMasterKaroseriList.aspx.vb" Inherits="FrmMasterKaroseriList" smartNavigation="False"%>
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
					<td class="titlePage" style="HEIGHT: 17px" colSpan="2">UMUM - Master Karoseri</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" colSpan="2" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" colSpan="2" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>

                <tr>
					<td colspan="2">
                        <asp:Panel ID="formKaroseri" runat="server" >
                            <table>
                                <tr style="display:none">
					                <td class="titleField" width="20%">Id&nbsp;</td>
					                <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="hfID" Enabled="false"
							                runat="server" Width="242px"></asp:textbox></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Nama Perusahaan&nbsp;</td>
								    <TD width="1%">:</TD>
					                <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtName" 
							                runat="server" Width="242px"></asp:textbox></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Propinsi&nbsp;</td>
								    <TD width="1%">:</TD>
					                <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtProvince" 
							                runat="server" Width="242px"></asp:textbox></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Kota&nbsp;</td>
								    <TD width="1%">:</TD>
					                <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtCity" 
							                runat="server" Width="242px"></asp:textbox></td>
				                </tr>
                                <tr>
					                <td class="titleField" width="20%">Alamat&nbsp;</td>
								    <TD width="1%">:</TD>
					                <td><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtAlamat" 
							                runat="server" Width="242px"></asp:textbox></td>
				                </tr>
				                <tr>
					                <td class="titleField" width="20%">&nbsp;</td>
								    <TD width="1%"></TD>
					                <td>    
                                             <asp:button id="btnCari" runat="server" Text="Cari" width="60px"></asp:button>&nbsp;          
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
                                                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                                    <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableGeneral" ></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblNo" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Code" HeaderText="Kode">
													<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblCode" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Name" HeaderText="Nama Perusahaan">
													<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblName" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Alamat">
													<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblAlamat" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Kota">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblCity" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Propinsi">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblProvince" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>   
                                                <asp:TemplateColumn HeaderText="Status">
													<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblStatus" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>  
                                                <asp:TemplateColumn HeaderText="">
                                                    <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="center" Wrap="false"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDetail" runat="server" CommandName="View">
															    <img src="../images/detail.gif" border="0" style="cursor:hand" alt="Lihat detil"></asp:LinkButton>
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
