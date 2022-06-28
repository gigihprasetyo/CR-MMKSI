﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmUploadScoreCSPerformance.aspx.vb" Inherits=".FrmUploadScoreCSPerformance" %>

<html>
    <head>
    <title>Upload  Score Sub Parameter CS Performance</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
	<script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        
    </script>
    </head>

    <body MS_POSITIONING="GridLayout">
        <form id="form1" runat="server">
            <table id="table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
                <tr>
                    <td>
                        <table id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                            <tr>
                                <td class="titlePage" style="HEIGHT: 18px" colSpan="3">CS Performance - Upload Score Sub Parameter</td>
                            </tr>
                            <tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor_sales.gif" colSpan="3" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
                            <tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
                            <tr>
                                <td class="titleField">
                                    Periode</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList SortExpression="PeriodID" ID="ddlPeriode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPeriode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator4" ValidationGroup="HeaderValidation" EnableClientScript="false" runat="server" ErrorMessage="*" ControlToValidate="ddlPeriode"></asp:requiredfieldvalidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">
                                    Pelayanan</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList SortExpression="ID" ID="ddlPelayanan" AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="*" ValidationGroup="HeaderValidation" EnableClientScript="false" ControlToValidate="ddlPelayanan"></asp:requiredfieldvalidator>
                                </td>
                            </tr>
                            <tr id="trJenisKendaraan" runat="server" visible="false">
                                <td class="titleField">
                                    Jenis Kendaraan</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList SortExpression="ID" ID="DdlJenisKendaraan" runat="server">
                                    </asp:DropDownList>
                                    <%--<asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ErrorMessage="*" ValidationGroup="HeaderValidation" EnableClientScript="false" ControlToValidate="ddlJenisKendaraan"></asp:requiredfieldvalidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="HEIGHT: 22px" width="20%">Master</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlMaster" AutoPostBack="true" OnSelectedIndexChanged="ddlMaster_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ErrorMessage="*" ValidationGroup="HeaderValidation" EnableClientScript="false" ControlToValidate="ddlMaster"></asp:requiredfieldvalidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="HEIGHT: 22px" width="20%">Parameter</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlParameter" AutoPostBack="true" OnSelectedIndexChanged="ddlParameter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="*" ValidationGroup="HeaderValidation" EnableClientScript="false" ControlToValidate="ddlParameter"></asp:requiredfieldvalidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="HEIGHT: 22px" width="20%">Sub Parameter</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlSubParameter">
                                    </asp:DropDownList>
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ValidationGroup="HeaderValidation" EnableClientScript="false" ControlToValidate="ddlSubParameter"></asp:requiredfieldvalidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="HEIGHT: 22px" width="20%">Sub Function</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtSubFunction" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="HEIGHT: 22px" width="20%">File To Upload</td>
                                <td>:</td>
                                <td>
                                   <asp:FileUpload ID="fileUploadBudget" runat="server"/>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
                                    <asp:LinkButton ID="btnLinkDownloadTemplate" Text="Download Template" runat="server" />
                                </td><td></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td></td><td></td>
                                <td>
                                    <asp:Button Text="Upload" ID="btnUpload" Width="70px" runat="server" ValidationGroup="HeaderValidation" /> &nbsp;
                                    <asp:Button Text="Simpan" ID="btnSimpan" Width="70px" runat="server"  /> &nbsp;
                                    <asp:Button Text="Batal" ID="btnBatal" Width="70px" runat="server"  /> &nbsp;
                                    <asp:Button Enabled="false" Text="Cari" Visible="false" ID="btnCari" Width="70px" runat="server"  /> &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table Width="100%" >
                <tr>
                    <td class="titleField" colspan="6">
                        <div id="div1" style="OVERFLOW: auto; HEIGHT: 330px" >
                            <asp:datagrid id="dtgUploadCSPerformanceScore" runat="server" Width="100%"  AllowSorting="True"
											 AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
											CellPadding="3" DataKeyField="ID" >
											<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>

<%--												<asp:BoundColumn DataField="DiscountMasterID" SortExpression="DiscountMasterID" HeaderText="Nama Diskon">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>--%>

                                     <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID">
									</asp:BoundColumn>
									
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                             <asp:TemplateColumn HeaderText="Kode Dealer">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodeDealer" runat="server" NAME="lblKodeDealer">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblKodeDealer" runat="server" NAME="lblKodeDealer">
                                                    </asp:Label>
                                                </EditItemTemplate>
                                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtFooterDiscountMasterID" runat="server" size="2" BackColor="White" MaxLength="4"></asp:TextBox>
                                                    <asp:Label ID="lblFooterDiscountMasterID" runat="server">
														<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText="Nama Dealer">
                                                <HeaderStyle Width="22%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaDealer" runat="server" NAME="lblNamaDealer">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblNamaDealer" runat="server" NAME="lblNamaDealer">
                                                    </asp:Label>
                                                </EditItemTemplate>
                                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Score">
                                                <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblScore" runat="server" NAME="lblScore" Text='<%# DataBinder.Eval(Container.DataItem, "ParameterScore")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtEditScore" runat="server" size="2" BackColor="White" MaxLength="4"></asp:TextBox>
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEditScore" runat="server" size="2" BackColor="White" Text='<%# DataBinder.Eval(Container.DataItem, "ParameterScore")%>' Visible="True">
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Keterangan" ItemStyle-Wrap="true">
                                                <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKeterangan" runat="server" NAME="lblKeterangan">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                     <asp:Label ID="lblKeterangan" runat="server" NAME="lblKeterangan">
                                                    </asp:Label>
                                                </EditItemTemplate>
                                                <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                            </asp:TemplateColumn>

                                             <asp:TemplateColumn HeaderText="Aksi">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" CausesValidation="False" runat="server" Text="Ubah" CommandName="Edit" Visible="true">
											             <img border="0" src="../images/edit.gif" alt="Ubah" style="cursor:hand"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" CausesValidation="False" runat="server" Text="Hapus" CommandName="Delete">
											             <img src="../images/trash.gif" onclick="return confirm('Yakin ingin menghapus data ini?');" alt="Hapus" border="0" style="cursor:hand"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnAdd" runat="server" Text="Tambah" CommandName="Add">
											<img src="../images/add.gif" border="0" alt="Tambah" align="center" align="middle" style="Cursor:hand"></asp:LinkButton>
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="lbtnSave" TabIndex="40" CommandName="Save" Text="Simpan" runat="server">
											<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnCancel" TabIndex="50" CommandName="Cancel" Text="Batal" runat="server">
											<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>

											</Columns>
										</asp:datagrid>
                        </div>
                    </td>
                </tr>
            </table>

        </form>
    </body>
</html>
