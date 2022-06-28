<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadClaim.aspx.vb" Inherits="FrmUploadClaim" smartNavigation="False"%>
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
		    function getElement(tipeElement, IdElement) {
		        var selectbox;
		        var inputs = document.getElementsByTagName(tipeElement);

		        for (var i = 0; i < inputs.length; i++) {
		            if (inputs[i].id.indexOf(IdElement) > -1) {
		                selectbox = inputs[i]
		                break;
		            }
		        }
		        return selectbox;
		    }
		    function CheckAll(aspCheckBoxID) {
		        var selectbox = getElement('input', 'chkAllItems')
		        var inputs = document.getElementsByTagName("input");
		        var stringlist = ""
		        for (var i = 0; i < inputs.length; i++) {
		            if (inputs[i].id.indexOf(aspCheckBoxID) > -1) {
		                if (inputs[i].type == 'checkbox') {
		                    if (selectbox.checked == true) {
		                        inputs[i].checked = "checked"

		                    }

		                    else
		                        inputs[i].checked = ""
		                }
		            }
		        }

		        var table = document.getElementById('dgTable');
		        var exitsno = '';
		        for (i = 1; i < table.rows.length - 1; i++) {

		            //stringlist = stringlist + ";" + table.rows[i].cells[0].getElementsByTagName("input")[0].checked;
		            if (table.rows[i].cells[0].getElementsByTagName("input")[0].checked == true)
		                stringlist = stringlist + ";" + i

		        }

		        var arrayCheck = getElement('input', 'arrayCheck')
		        if (selectbox.checked == true) {
		            arrayCheck.value = stringlist
		        } else arrayCheck.value = ""
		    }

		    function generateCheckBoxClick() {
		        var inputs = document.getElementsByTagName("input");
		        var stringlist = ""
		        for (var i = 0; i < inputs.length; i++) {

		            if (inputs[i].id.indexOf('cbAllGrid') > -1) {
		                if (inputs[i].type == 'checkbox') {

		                    inputs[i].onclick = function () { setValueCheckBox(); };
		                }
		            }
		        }
		    }
		    function setValueCheckBox() {
		        var table = document.getElementById('dgTable');
		        var stringlist = '';
		        for (i = 1; i < table.rows.length - 1; i++) {

		            if (table.rows[i].cells[0].getElementsByTagName("input")[0].checked == true)
		                stringlist = stringlist + ";" + i

		        }
		        var arrayCheck = getElement('input', 'arrayCheck')

		        arrayCheck.value = stringlist

		    }
		    setTimeout(function () {
		      //  generateCheckBoxClick();

		       

		    }, 2000);

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="5" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px" colSpan="2">SALES CAMPAIGN - Upload Leasing</td>
				</tr>
                <tr>
					<td background="../images/bg_hor.gif" colSpan="2" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" colSpan="2" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
                <tr>
					<td colSpan="2">
                        <table>
                            <tr>
					            <td class="titleField" width="20%">Leasing&nbsp;</td>
					            <td>
                                      <asp:DropDownList ID="ddlLeasing" runat="server">     
                                        </asp:DropDownList>  

					            </td>
                                <td colspan="2"></td>
					           
				            </tr>

                            <tr>
					            <td class="titleField" width="20%">Upload File&nbsp;</td>
					            <td>
                                     <asp:FileUpload ID="fileUploadExcel" runat="server"  />
                                      <asp:Button ID="btnUpload" runat="server" Text="Upload"   />

                                     <asp:HiddenField ID="arrayCheck" runat="server" />


					            </td>
                                <td class="titleField" width="20%"></td>
					            <td>
                                                   
					            </td>
				            </tr>
				           
                            <tr>
					            <td class="titleField" width="20%">Total Unit Upload &nbsp;</td>
					            <td>
                                    
                                    <asp:Label ID="lblTotalUnit" runat="server" Text=""></asp:Label>
                                                               
					            </td>
                                <td class="titleField" width="20%">&nbsp;</td>
					            <td>                              
					            </td>
				            </tr>

                            
				          


                        </table>

					</td>
				</tr>


				
							
				<tr>
					<td class="titleField" width="20%">
                         

                        <asp:LinkButton ID="LinkDownload" runat="server">Template  Upload Excel</asp:LinkButton>
					</td>
					<td>                        
                        <asp:button id="btnSimpan" runat="server" Text="Simpan" width="60px" Visible="false"></asp:button>&nbsp;
						
					</td>
				</tr>
				<TR>
					<TD colSpan="2">
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; max-height: 440px">
                                        <asp:datagrid id="dgTable" runat="server" Width="100%"  AllowSorting="True"
											 AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
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
                                                											
												<asp:TemplateColumn HeaderText="Kode Dealer">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
                                                        <asp:Label ID="lblDealer" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Leasing Company">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
                                                        <asp:Label ID="lblLeasing" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No Claim Reg">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNoClaimReg" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nomor Rangka">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNoRangka" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Deskripsi Kendaraan">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblDeskripsi" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nomor Mesin">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label ID="lblNoMesin" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Faktur Open Date">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
                                                        <asp:Label ID="lblFakturDate" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tanggal Validasi Faktur">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
                                                        <asp:Label ID="lblValidasiDate" Runat="server"></asp:Label>                                                      
													</ItemTemplate>
												</asp:TemplateColumn>
                                                 <asp:TemplateColumn HeaderText="Nama Customer">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
                                                        <asp:Label ID="lblCustomer" Runat="server"></asp:Label>                                                      
													</ItemTemplate>
												</asp:TemplateColumn>
                                                 <asp:TemplateColumn HeaderText="Kota Customer">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
                                                       <asp:Label ID="lblKota" Runat="server"></asp:Label>                                                   
													</ItemTemplate>
												</asp:TemplateColumn>
                                                 <asp:TemplateColumn HeaderText="Checking Status">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
                                                        <asp:Label ID="lblCheck" Runat="server"></asp:Label>                                                  
													</ItemTemplate>
												</asp:TemplateColumn>
                                                
											
											</Columns>
											
										</asp:datagrid></div>

                                     <asp:Panel ID="PanelError" runat="server" >
                                        <asp:Label ID="lblpanelError" runat="server" Text="" Width="100%"></asp:Label>
                                    </asp:Panel>

								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>

                <!--
                <tr>
					<td class="titleField" width="20%">Mengubah Status&nbsp;</td>
					<td>         
                        <asp:DropDownList ID="ddlStatusLeasing" runat="server">                                        
                            <asp:ListItem Value="" Text="-Silakan Pilih-"></asp:ListItem>
                            <asp:ListItem Value="1" Text="OK"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Double Claim "></asp:ListItem>
                            <asp:ListItem Value="3" Text="Beda Leasing"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Claim tidak terdaftar"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Tanggal Faktur tidak valid"></asp:ListItem>
                        </asp:DropDownList>                
                        <asp:button id="btnProses" runat="server" Text="Proses" width="60px"></asp:button>&nbsp;
						
					</td>
				</tr>
                    -->

			</TABLE>
		</form>
	</body>
</HTML>
