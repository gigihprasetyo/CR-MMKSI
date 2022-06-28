<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmRecallCategory.aspx.vb" Inherits=".FrmRecallCategory" SmartNavigation="false" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daftar Kategori Field Fix Campaign</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <div>

            <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                <tr>
                    <td class="titlePage" colspan="2">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="titlePage">SERVICE – Field Fix Campaign – Assign Kode Posisi</td>
                            </tr>
                            <tr>
                                <td background="../images/bg_hor.gif" height="1">
                                    <img height="1" src="/images/bg_hor.gif" border="0"></td>
                            </tr>
                            <tr>
                                <td height="10">
                                    <img height="1" src="../images/dot.gif" border="0"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr  valign="top" align="left">
                     <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="titleField" width="24%">Deskripsi</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDescription"   width="400px"
                                            runat="server" maxlength="100"  ></asp:textbox>
                                        
                                    </td>
                                </tr>
                                  <tr>
                                    <td class="titleField" width="24%">Nomor Service Buletin</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtBuletin"   width="200px"
                                            runat="server" maxlength="100"  ></asp:textbox>
                                        
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField" width="24%">Tanggal Berlaku Mulai</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                     
                                                <cc1:inticalendar id="icStartDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>
                                        
                                    </td>
                                </tr>


                                <tr>
                                    <td class="titleField" width="24%">Field Fix Campagin No</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:Label ID="lblREgCallNo" Text="[AUTONUMBER]" runat="server"></asp:Label>
                                        <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtRecallRegNo" runat="server" width="100px"
                                            maxlength="200" ReadOnly="true" Enabled="false" placeholder="Autonumber" Visible="false"></asp:textbox>
                                    </td>
                                </tr>
                               
                                <tr style="display:none;">
                                    <td class="titleField" width="24%">Status</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:dropdownlist id="ddlStatus" runat="server">
                                             <asp:ListItem value="">Pilih</asp:ListItem>
                                                <asp:ListItem value="1">aktif</asp:ListItem>
                                            <asp:ListItem value="0">Non Aktif</asp:ListItem>

                                        </asp:dropdownlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3"></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td width="75%">
                                        <asp:button id="btnSimpan" runat="server" width="60px" text="Simpan"></asp:button>
                                        &nbsp;
											<asp:button id="btnBatal" runat="server" width="60px" text="Batal" causesvalidation="False"></asp:button>
                                        &nbsp;
											<asp:button id="btnSearch" runat="server" width="64px" text="Cari" causesvalidation="False"></asp:button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                </tr>
                   <tr valign="top">
                    <td>
                        <div id="div1" style="height: 360px; overflow: auto">
                            <asp:datagrid id="dtgRecallCategory" runat="server" width="100%" AutoGenerateColumns="False" CellPadding="3" BackColor="#E0E0E0" AllowSorting="True" AllowCustomPaging="True" PageSize="50"
								AllowPaging="True" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#ededed"></FooterStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
											<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn HeaderText="No">
											<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
											<ItemTemplate>
												<asp:Label Runat="server" ID="lblNo"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="RecallRegNo" SortExpression="RecallRegNo" HeaderText="No Reg">
											<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
											<HeaderStyle Width="20%" CssClass="titleTableService" Wrap="True"></HeaderStyle>
										</asp:BoundColumn>

                                           <asp:BoundColumn DataField="ValidStartDate" SortExpression="ValidStartDate" HeaderText="Tanggal Berlaku Mulai" DataFormatString="{0:dd-MM-yyyy}">
											<HeaderStyle Width="140px" CssClass="titleTableService" Wrap="false" HorizontalAlign="Justify"></HeaderStyle>
										</asp:BoundColumn>

                                        <asp:BoundColumn DataField="BuletinDescription" SortExpression="BuletinDescription" HeaderText="Nomor Buletin"   >
											<HeaderStyle Width="100px" CssClass="titleTableService" Wrap="false"></HeaderStyle>
										</asp:BoundColumn>

                                     

                                       <asp:TemplateColumn HeaderText="Total Recall">
											<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label runat="server" Text="" ID="lblTotalRecall"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>

                                          <asp:TemplateColumn HeaderText="Total Service">
											<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label runat="server" Text="" ID="lblTotalService"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>

                                          <asp:TemplateColumn HeaderText="Persentase Service (%)">
											<HeaderStyle Width="22%" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label runat="server" Text="" ID="lblPersentase"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>

										<asp:TemplateColumn HeaderText="Status Aktif"  Visible="false">
											<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label runat="server" Text="" ID="LblStatus"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
													CommandName="View">
													<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
												<asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
													<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
												<asp:LinkButton id="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False"
													CommandName="Delete">
													<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                <asp:LinkButton id="lbAssignPositionCode" runat="server" Width="16px" Text="Hapus" CausesValidation="False"
													CommandName="AssignPositionCode">
													<img src="../images/set.gif" border="0" alt="Assign Kode Posisi"></asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
								</asp:datagrid>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
