<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCcCSPerformanceClasification.aspx.vb" Inherits=".FrmCcCSPerformanceClasification" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    	<HEAD>
		<title>Master Assessment Parameter</title> 
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
            <script>

                function Confirm() {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_value";
                    if (confirm("Apakah Anda Yakin Menghapus Data Ini?")) {
                        confirm_value.value = "Yes";
                    } else {
                        confirm_value.value = "No";
                    }
                    document.forms[0].appendChild(confirm_value);
                }

            </script>
	</HEAD>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="FrmDealAssessmentParameterField" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="100%" border="0" >
            <tr>
                <td class="titlePage">CS Performance - Klasifikasi</td>
            </tr>
            <tr>
				<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
			</tr>
			<tr>
				<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
			</tr>
            <tr>
                <td align="left">
                    <TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            
                        

                        <tr valign="top">   
                            <td class="titleField">Nama Sub Parameter</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:Label ID="lblKodeSubParameter" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr valign="top">   
                            <td class="titleField">Kode Klasifikasi</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:Label ID="lblKodeKlasifikasi" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Nama Klasifikasi</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtParameterName" Width="200px"></asp:TextBox>
                                <asp:requiredfieldvalidator id="RequiredFieldValidator4" ValidationGroup="HeaderValidation" 
                                    EnableClientScript="false" runat="server" ErrorMessage="*" 
                                    ControlToValidate="txtParameterName"></asp:requiredfieldvalidator>
                            </td>
                        </tr>

                        

                        <tr valign="top">
                            <td class="titleField">Bobot</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="return numericOnlyUniv(event)" runat="server" ID="txtParameterWeight" Width="90px"> </asp:TextBox>
                                <asp:requiredfieldvalidator id="RequiredFieldValidator2" ValidationGroup="HeaderValidation" 
                                    EnableClientScript="false" runat="server" ErrorMessage="*" 
                                    ControlToValidate="txtParameterWeight"></asp:requiredfieldvalidator>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField"></td>
                            <td width="1%"></td>
                            <td style="width: 330px" width="262">
                                <asp:DropDownList AutoPostBack="true" runat="server" ID="ddlReferral" Width="200px">
                                    <asp:ListItem Text="Pilih" Value="0" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <%--<tr valign="top">
                            <td class="titleField">Nomor Urut</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="return numericOnlyUniv(event)" runat="server" ID="txtSequence" Width="200px">
                                </asp:TextBox>
                                 <asp:requiredfieldvalidator id="RequiredFieldValidator1" ValidationGroup="HeaderValidation" 
                                    EnableClientScript="false" runat="server" ErrorMessage="*" 
                                    ControlToValidate="txtSequence"></asp:requiredfieldvalidator>
                            </td>
                        </tr>
                            <tr valign="top">
                            <td class="titleField">Tipe</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:DropDownList runat="server" ID="ddlTipe" Width="200px">
                                </asp:DropDownList>
                                   <asp:requiredfieldvalidator id="RequiredFieldValidator3" ValidationGroup="HeaderValidation" 
                                    EnableClientScript="false" runat="server" ErrorMessage="*" 
                                    ControlToValidate="ddlTipe"></asp:requiredfieldvalidator>
                            </td>
                        </tr>
                                <tr vAlign="top">
                                <TD class="titleField" style="WIDTH: 88px" width="88">Level / Layer</TD>
								<TD width="1%">:</TD>
								<td style="WIDTH: 330px" width="262">
                                    <asp:DropDownList runat="server" ID="ddlLevelLayer" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlLevelLayer_SelectedIndexChanged">
                                        <asp:ListItem Text="0" Value="0" />
                                        <asp:ListItem Text="1" Value="1" />
                                    </asp:DropDownList>
								</td>
                            </tr>

                                <TR>
								<TD class="titleField" >Kode Master</TD>
								<TD width="1%">:</TD>
								<TD >
                                    <asp:DropDownList runat="server" ID="ddlKodeMaster" Width="200px" AutoPostBack="true"  OnSelectedIndexChanged="txtFormCode_SelectedIndexChanged" >
                                        <asp:ListItem Text="Pilih" Value="Pilih" />
                                    </asp:DropDownList>
								</TD>
							</TR>--%>

                       
                        <tr>
                            <td></td><td></td>
                            <td>
                                <asp:Button Text="Simpan" ID="btnSimpan" runat="server" Width="75px" />
                                &nbsp;
                                <asp:Button Text="Cari" ID="btnCari" runat="server" Width="75px" CausesValidation="False" />
                                &nbsp;
                                <asp:Button Text="Baru" ID="btnBaru" runat="server" Width="75px" CausesValidation="False" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button Text="Kembali" ID="btnKembali" runat="server" Width="75px" />
                            </td>
                        </tr>
                    </TABLE>
                </td>
            </tr>
            <tr><td></td></tr>
            <tr>
                <td valign="top" >
                    <div id="div1" style="OVERFLOW: auto; HEIGHT: 320px">
                        <asp:datagrid id="dtgCSPParameter" runat="server" Width="100%" PageSize="50" CellPadding="3"
								BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1"
								AllowSorting="True" Font-Names="Microsoft Sans Serif">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BorderColor="#E0E0E0"
									BackColor="#CC3333"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID">
									
									</asp:BoundColumn>
									
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									
									<asp:TemplateColumn HeaderText="Nama Sub Parameter">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
											<asp:Label ID="lblKodeParameter" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									
									<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="kode Klasifikasi">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>

                                    <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									
                                    <%--<asp:BoundColumn DataField="Sequence" SortExpression="Sequence" HeaderText="No.Urut">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>

                                     <asp:BoundColumn DataField="Type" SortExpression="Type" HeaderText="Tipe">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>--%>

                                     <asp:BoundColumn DataField="Weight" SortExpression="Weight" HeaderText="Bobot">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>

                                    <%-- <asp:BoundColumn DataField="Level" SortExpression="Level" HeaderText="Level">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>--%>

									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
                                            <asp:LinkButton id="lbtnGotoClasification" runat="server" Width="20px" Text="Sub Parameter" CausesValidation="False" CommandName="GotoClasification">
												<img src="../images/set.gif" border="0" alt="Assessment Field"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" OnClientClick = "Confirm();">
											    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>&nbsp;
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
                    </div>
                </td>
            </tr>

        </table>
    </form>
</body>
</html>
