<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPRegistrationUpload.aspx.vb" Inherits=".FrmMSPRegistrationUpload" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
    <head>
		<title>Upload Registrasi MSP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script type="text/javascript">
            function ShowPPDealerSelection() {
                showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            }

            function DealerSelection(selectedDealer) {
                var txtDealerSelection = document.getElementById("txtKodeDealer");
                txtDealerSelection.value = selectedDealer;
            }

            function CheckAll(aspCheckBoxID, checkVal) {
                re = new RegExp(':' + aspCheckBoxID + '$')
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    elm = document.forms[0].elements[i]
                    if (elm.type == 'checkbox') {
                        if (re.test(elm.name)) {
                            elm.checked = checkVal
                        }
                    }
                }
            }

            function InputPasswordPlease() {
                showPPPassword();
            }

            function showPPPassword() {
                showPopUp('../PopUp/PopupInputPassword.aspx', '', 200, 600, GotPassword);
            }
            function GotPassword(result) {
                var txtUser = document.getElementById("txtUser");
                var txtPwd = document.getElementById("txtPass");
                var btn = document.getElementById("btnSave");
                var str = result;
                var username = '', pwd = '';

                username = str.split(';')[0];
                pwd = str.split(';')[1];

                txtUser.value = username;
                txtPwd.value = pwd;
                btn.click();
            }

            function promptPassword() {
                var txt = document.getElementById("txtPass");
                var div = document.getElementById("divPassword");

                if (txt.value)

                    div.style.display = "inherit";
                alert("Please, Enter Your SAP Password First!")
                txt.focus();
            }
            function onLoad() {
                var div = document.getElementById("divPassword");
                div.style.display = "none";
            }

		</script>
	</head>
<body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
			<td class="titlePage">MSP - Upload Registrasi MSP</td>
		</tr>
		<tr>
			<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
		</tr>
		<tr>
			<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
		</tr>
        <tr>
			<td vAlign="top" align="left">
                <table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
					<tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="lblDealer" runat="server">Template File Upload</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:LinkButton ID="lbtnUploadTemplate" runat="server">Template Upload Excel</asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            Minimum Excel 2007 (*.xls/*.xlsx)
						</td>
						<td width="17%">
                            
						</td>
						<td width="1%"></td>
						<td width="32%">
                            
						</td>
					</tr>
                    <tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="Label1" runat="server">File Upload</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<input id="UploadFile" onkeydown="return false;" style="WIDTH: 240px" type="file" name="File1" runat="server">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" />
						</td>
						<td width="17%">
                            
						</td>
						<td width="1%"></td>
						<td width="32%">
                            
						</td>
					</tr>
                    
                     <tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                           <asp:Label ID="Label2" runat="server">Filter Data</asp:Label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							 <asp:dropdownlist id="ddlFilter" runat="server" autopostback="true">
							    <asp:ListItem Value="0" Selected="True">Pilih Semua</asp:ListItem>
							    <asp:ListItem Value="Valid">Valid</asp:ListItem>
							    <asp:ListItem Value="TidakValid">Tidak Valid</asp:ListItem>
						    </asp:dropdownlist>
						</td>
						<td width="17%">
                            
						</td>
						<td width="1%"></td>
						<td width="32%">
                            
						</td>
					</tr>
                   
                    <tr>
						<td class="titleField" width="14%"></td>
						<td width="1%"></td>
						<td style="WIDTH: 262px" width="35%">
							<asp:button id="btnSave" runat="server" Text="Simpan" width="70px" Visible="false"></asp:button>
                            <asp:button id="btnMigrasi" runat="server" Text="Migrasi" width="70px" Visible="false"></asp:button>
						</td>
					</tr>
                </table>
            </td>
        </tr>
        <tr>
            <td vAlign="top">
				<div id="div1" style="OVERFLOW: auto">
                    <asp:datagrid id="dtgMSPRegUpload" runat="server" Width="100%" CellSpacing="1" AllowSorting="True" PageSize="15" AllowPaging="false" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="false">
						<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
						<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
						<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
						<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
						<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
						<Columns>
                            <asp:TemplateColumn HeaderText="No">
								<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblNo" runat="server"></asp:Label>
								</ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label id="lblNo" runat="server"></asp:Label>
                                </EditItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Dealer">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblDealer" runat="server"></asp:Label>
								</ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtDealer"></asp:TextBox>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblName" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtName" MaxLength="50"></asp:TextBox>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="No KTP">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblKTP" runat="server"></asp:Label>
								</ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtKTP" MaxLength="16" onblur="numericOnlyBlur(txtKTPNo)" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="No MSP Lama">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblOldMSP" runat="server"></asp:Label>
								</ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtOldMSP" MaxLength="10"></asp:TextBox>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Alamat">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblAddress" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtAddress" MaxLength="100"></asp:TextBox>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Propinsi">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblProvince" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:dropdownlist runat="server" ID="ddlProvince" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack = "true"></asp:dropdownlist>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kota">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblCity" runat="server"></asp:Label>
								</ItemTemplate>
                                <EditItemTemplate>
                                    <asp:dropdownlist runat="server" ID="ddlCity"></asp:dropdownlist>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="Kabupaten">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblPreArea" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:dropdownlist id="ddlPreArea" runat="server">
							            <asp:ListItem Value="blank" Selected="True">Silahkan Pilih</asp:ListItem>
							            <asp:ListItem Value="KAB">KAB</asp:ListItem>
							            <asp:ListItem Value="KODYA">KODYA</asp:ListItem>
							            <asp:ListItem Value="KABUPATEN">KABUPATEN</asp:ListItem>
							            <asp:ListItem Value="KOTA MADYA">KOTAMADYA</asp:ListItem>
							            <asp:ListItem Value="KOTA">KOTA</asp:ListItem>
						            </asp:dropdownlist>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No Tlp">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblPhoneNo" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtPhoneNo" MaxLength="15" onblur="numericOnlyBlur(txtPhoneNo)" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
                                </EditItemTemplate>
							</asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Email">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblEmail" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtEmail" MaxLength="50"></asp:TextBox>
                                 </EditItemTemplate>
							</asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="No Rangka">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblChassisNumber" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtChassisNumber" AutoPostBack="true" OnTextChanged="txtChassisNumber_TextChanged"></asp:TextBox>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                                                       
                            <asp:TemplateColumn HeaderText="Tgl Buka Faktur">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblPKTDate" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:Label id="lblPKTDateEdit" runat="server"></asp:Label>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe MSP">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblMSPType" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:dropdownlist runat="server" ID="ddlMSPType" OnSelectedIndexChanged="ddlMSPType_SelectedIndexChanged" AutoPostBack = "true"></asp:dropdownlist>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Durasi">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblDuration" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:dropdownlist runat="server" ID="ddlDuration"></asp:dropdownlist>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                                                        
                            <asp:TemplateColumn HeaderText="Tipe Pengajuan">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblRequestType" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:dropdownlist runat="server" ID="ddlRequestType" OnSelectedIndexChanged="ddlRequestType_SelectedIndexChanged" AutoPostBack = "true">
                                         <asp:ListItem Value="-1" Selected="True">Silahkan Pilih</asp:ListItem>
							            <asp:ListItem Value="Paid">Paid</asp:ListItem>
							            <asp:ListItem Value="Promo">Promo</asp:ListItem>
                                    </asp:dropdownlist>
                                </EditItemTemplate>
							</asp:TemplateColumn>

                             <asp:TemplateColumn HeaderText="Tipe Benefit">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblBenefitType" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtBenefitType"></asp:TextBox>
                                </EditItemTemplate>
							</asp:TemplateColumn>

                             <asp:TemplateColumn HeaderText="Tgl Pengajuan">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblRequestDate" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <cc1:inticalendar id="txtRequestDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="Dijual Oleh">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblSoldBy" runat="server"></asp:Label>
								</ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:dropdownlist runat="server" ID="ddlSoldBy">
                                        <asp:ListItem Value="-1" Selected="True">Silahkan Pilih</asp:ListItem>
							            <asp:ListItem Value="SALES">Sales</asp:ListItem>
							            <asp:ListItem Value="SERVICE">Service</asp:ListItem>
                                    </asp:dropdownlist>
                                </EditItemTemplate>
							</asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="Keterangan" ItemStyle-Width="300">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
                                    <asp:Label id="lblDescription" runat="server"></asp:Label>                                   
								</ItemTemplate>
                                 <EditItemTemplate>
                                        <asp:Label id="lblDescriptionEdit" runat="server"></asp:Label>                                                                    
                                 </EditItemTemplate>
							</asp:TemplateColumn>

                            <asp:TemplateColumn>
                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>  
                                    <div style="width:45px;text-align:center">
                                                                              
									    <asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
										    <img src="../images/edit.gif" border="0" alt="Ubah">
									    </asp:LinkButton>
									    <asp:LinkButton id="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False" CommandName="Delete">
										    <img src="../images/trash.gif" border="0" alt="Hapus">
									    </asp:LinkButton>
                                                                              
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="width:45px;text-align:center">
                                        <asp:LinkButton id="lbtnSave" tabIndex="40" Runat="server" CausesValidation="True" CommandName="Update"
										text="Simpan">
										<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
									<asp:LinkButton id="lbtnCancel" tabIndex="50" Runat="server" CausesValidation="True" CommandName="Cancel"
										text="Batal">
										<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </div>

								</EditItemTemplate>
                                            
                            </asp:TemplateColumn>
                        </Columns>
                        <%--<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>--%>
                    </asp:datagrid>
                </div>
            </td>
        </tr>
        <tr>
            <td align="left">
                <br />
                <div id="divPassword" style="display:none;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td>SAP Password</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtUser" runat="server"   Width="171px"></asp:TextBox>
                                <asp:TextBox ID="txtPass" runat="server" TextMode="Password"  Width="171px"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                </div>
            </td>
        </tr>
      </table>
    </form>
</body>
</html>
