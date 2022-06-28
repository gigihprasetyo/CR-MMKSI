<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSPLHeader.aspx.vb" Inherits="FrmSPLHeader" smartNavigation="False" MaintainScrollPositionOnPostback="true" %>
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
		<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
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
            function ShowPPCategoryDiscountSelection() {
                showPopUp('../General/../PopUp/PopUpDiscountMasterSelection.aspx', '', 500, 760, CategoryDiscountSelection);
            }
            function CategoryDiscountSelection(selectedCategory) {
                var hdnDiscountMasterID = document.getElementById("hdnDiscountMasterID");
                var txtDiscountCategorySelection = document.getElementById("txtDiscountCategory");
                var result = selectedCategory.split(';');
                hdnDiscountMasterID.value = result[0];
                txtDiscountCategorySelection.value = result[1];
            }
            setTimeout(function () {
                generateCheckBoxClick();
            }, 2000);
            function CheckAll(aspCheckBoxID) {
                var selectbox = getElement('input', 'chkbxAll')
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

                var table = document.getElementById('dgSPLHeader');
                var exitsno = '';
                for (i = 1; i < table.rows.length - 1; i++) {

                    if (table.rows[i].cells[1].getElementsByTagName("input")[0].checked == true)
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

                    if (inputs[i].id.indexOf('cbxDetail') > -1) {
                        if (inputs[i].type == 'checkbox') {

                            inputs[i].onclick = function () { setValueCheckBox(); };
                        }
                    }
                }
            }

            function setValueCheckBox() {
                var table = document.getElementById('dgSPLHeader');
                var stringlist = '';
                for (i = 1; i < table.rows.length - 1; i++) {
                    if (table.rows[i].cells[1].getElementsByTagName("input")[0] != undefined) {
                        if (table.rows[i].cells[1].getElementsByTagName("input")[0].checked == true)
                            stringlist = stringlist + ";" + i
                    }


                }
                var arrayCheck = getElement('input', 'arrayCheck')

                arrayCheck.value = stringlist
            }

        </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 17px">
						UMUM&nbsp;- Daftar Aplikasi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 17px" width="24%"><asp:label id="lblSPLNumber" runat="server">Nomor Aplikasi</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="25%">
									<asp:textbox id="txtSPLNumber" runat="server" MaxLength="20" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtSPLNumber','<>?*%$;')"></asp:textbox></TD>
								<TD class="titleField" style="HEIGHT: 17px" width="20%"><asp:label id="lblCustName" runat="server">Kategori Diskon</asp:label></TD>
								<TD style="HEIGHT: 17px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 17px" width="29%">
								    <asp:textbox id="txtDiscountCategory" runat="server"></asp:textbox>
                                                <%--<asp:requiredfieldvalidator id="RequiredFieldValidator7" runat="server" ControlToValidate="txtDiscountCategory" ErrorMessage="*"></asp:requiredfieldvalidator>--%>
                                                <asp:label id="lblPopUpCategoryDiscount" runat="server">
										            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									            </asp:label>
                                                <asp:HiddenField ID="hdnDiscountMasterID" runat="server" />	
                                </TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px" width="24%"><asp:label id="lblName" runat="server">Nama Dealer</asp:label></TD>
								<TD style="HEIGHT: 10px" width="1%"><asp:label id="Label1" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="25%">
									<asp:textbox id="txtDealerName" runat="server" MaxLength="20" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtDealerName','<>?*%$;')"></asp:textbox></TD>
								<TD class="titleField" style="HEIGHT: 10px" width="20%">
									<asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD style="HEIGHT: 10px" width="1%">
									<asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="29%">
									<asp:dropdownlist id="ddlStatus" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>
							<TR valign="top">
                                
                                
								<TD class="titleField" style="HEIGHT: 20px" width="24%">
                                    <asp:Label runat="server" style="margin-top:5px">Berlaku Pada</asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label runat="server" style="margin-top:5px">Nama Customer</asp:Label>
								</TD>
                                
                                <%--<TD class="titleField" style="HEIGHT: 20px" width="24%">Berlaku Pada</TD>--%>
								<TD style="HEIGHT: 20px" width="1%">
                                    <label>:</label>
                                    <br />
                                    <br />
                                    <label style="margin-top:5px">:</label>
								</TD>
								<TD style="HEIGHT: 20px" width="25%" nowrap>
									<asp:TextBox id="txtBerlakuPada" runat="server" MaxLength="6" Width="60px" onkeypress="return numericOnlyUniv(event)"></asp:TextBox>
									<asp:Label id="Label2" runat="server" ForeColor="Red">MMyyyy</asp:Label>
                                    <br />
                                    <%--<asp:TextBox id="txtCustomerName" runat="server" MaxLength="6" size="22" style="margin-top:5px"></asp:TextBox>--%>
                                    <asp:textbox id="txtCustName" runat="server" size="22" style="margin-top:5px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtCustName','<>?*%$;')"></asp:textbox>
								</TD>

                                <TD class="titleField" style="HEIGHT: 10px" width="20%">
									<asp:label id="Label4" runat="server">Status Persetujuan</asp:label></TD>
								<TD style="HEIGHT: 10px" width="1%">
									<asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD style="HEIGHT: 10px" width="29%">
                                    <asp:ListBox ID="lboxStatusPersetujuan" runat="server" Width="140" Rows="3" SelectionMode="Multiple"></asp:ListBox>
								</TD>
								
							</TR>
                            
						</TABLE>
					</TD>
				</TR>
                <tr valign="top" style="padding-top:10px">
                    <TD style="HEIGHT: 20px; text-align:center" width="100%">
									<asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button>
                                    <asp:HiddenField ID="arrayCheck" runat="server" />
								</TD>
                </tr>
                <tr style="padding-top:10px">
                    <TD vAlign="top" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px"><asp:datagrid id="dgSPLHeader" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
											BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True">
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <input type="checkbox" id="chkbxAll" onclick="CheckAll('cbxDetail')" />
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbxDetail" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="SPLNumber" SortExpression="SPLNumber" HeaderText="Nomor Aplikasi">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CustomerName" SortExpression="CustomerName" HeaderText="Nama Customer">
													<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Periode">
													<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblPeriode"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Status SPL" ItemStyle-HorizontalAlign="Center">
													<HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblStatusSPL"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Status Persetujuan" ItemStyle-HorizontalAlign="Center">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:Label Runat="server" ID="lblStatusPersetujuan"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>

												<asp:TemplateColumn>
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat SPL Detail"></asp:LinkButton>
														<asp:label id="lbtnDealer" runat="server" Width="20px" Text="Detail Dealer">
															<img src="../images/popup.gif" border="0" alt="Lihat Dealer"></asp:label>
														<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Edit SPL Detail"></asp:LinkButton>
														<asp:LinkButton id="lbtnDelete" visible="False" runat="server" Width="20px" Text="Ubah" CausesValidation="False"
															CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDownload" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
															CommandName="Download">
															<img src="../images/download.gif" border="0" alt="Lihat"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
                </tr>
				<TR>
					<TD style="HEIGHT: 8px">
                        <div>
                            <table>
                                <tr>
                                    <td>Status</td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatusProses" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnProses" runat="server" Text="Proses" Width="100px"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnTransfer" runat="server" Text="Transfer To Groupware" Width="200px"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDownload" runat="server" Text="Download" Width="100px"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </div>
					</TD>
				</TR>
			</TABLE>
		</form>
        <script language="javascript">
            if (window.parent == window) {
                if (!navigator.appName == "Microsoft Internet Explorer") {
                    self.opener = null;
                    self.close();
                }
                else {
                    this.name = "origWin";
                    origWin = window.open(window.location, "origWin");
                    window.opener = top;
                    window.close();
                }
            }
		</script>
	</body>
</HTML>
