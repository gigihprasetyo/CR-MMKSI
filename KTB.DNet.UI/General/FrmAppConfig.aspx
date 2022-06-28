<%@ page language="vb" autoeventwireup="false" codebehind="FrmAppConfig.aspx.vb" inherits="FrmAppConfig" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Maintanance - AppConfig</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js" type="text/javascript"></script>
    <script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
    <style type="text/css">
        .CustomGrid td {
            max-width: 320px;
            word-break: break-all;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <thead>
                <tr>
                    <th class="titlePage" style="text-align: left">App Config - Setting Key Config</th>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1">
                        <img height="1" src="/images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td valign="top" align="left">
                        <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="titleField" width="24%">Nama</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtName" onblur="HtmlCharBlur(txtName)"
                                            runat="server" maxlength="100" width="300px"></asp:textbox>
                                        <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" controltovalidate="txtName" errormessage="*"></asp:requiredfieldvalidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleField" width="24%">Nilai</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtValue" runat="server" width="400px"
                                            maxlength="2000"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleField" width="24%">AppID</td>
                                    <td width="1%">:</td>
                                    <td width="75%">

                                        <asp:dropdownlist id="DDAppID" runat="server" width="40%">
                                                <asp:ListItem value="">Pilih</asp:ListItem>
                                                <asp:ListItem value="KTB.DNet.UI">KTB.DNET.UI</asp:ListItem>
                                            <asp:ListItem value="KTB.DNet.Service">KTB.DNet.Service</asp:ListItem>
                                             <asp:ListItem value="KTB.DNet.SapListener">KTB.DNet.SapListener</asp:ListItem>
                                            <asp:ListItem value="KTB.DNet.UI.QA">KTB.DNet.UI.QA</asp:ListItem>
                                            <asp:ListItem value="KTB.DNET.WSMChecker">KTB.DNET.WSMChecker</asp:ListItem>
                                            <asp:ListItem value="KTB.DNet.TokenAlert">KTB.DNet.TokenAlert</asp:ListItem>
                                           
                                            </asp:dropdownlist>
                                        <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" controltovalidate="DDAppID" errormessage="*"></asp:requiredfieldvalidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titleField" width="24%">Status</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist>
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
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div id="div1" style="height: 360px; overflow: auto">
                            <asp:datagrid id="dtgAppConfig" runat="server" width="100%" autogeneratecolumns="False" borderstyle="None"
                                borderwidth="0px" backcolor="#CDCDCD" gridlines="None" bordercolor="#CDCDCD" cellpadding="3"
                                pagesize="50" allowcustompaging="True" allowpaging="True" allowsorting="True" cellspacing="1"
                                font-names="Microsoft Sans Serif" cssclass="CustomGrid">
									<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
									<ItemStyle ForeColor="Black" BackColor="#FDF1F2" Wrap="True"></ItemStyle>
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BorderColor="#E0E0E0"
										BackColor="#CC3333"></HeaderStyle>
									<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
											<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
											<ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn HeaderText="No">
											<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
											<ItemTemplate>
												<asp:Label Runat="server" ID="lblNo"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Key Name">
											<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Value" SortExpression="Value" HeaderText="Key Values">
											<HeaderStyle Width="140px" CssClass="titleTableGeneral" Wrap="True"></HeaderStyle>
										</asp:BoundColumn>

                                        <asp:BoundColumn DataField="AppID" SortExpression="AppID" HeaderText="AppID">
											<HeaderStyle Width="140px" CssClass="titleTableGeneral" Wrap="True"></HeaderStyle>
										</asp:BoundColumn>

										<asp:TemplateColumn HeaderText="Status Aktif">
											<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
											<ItemTemplate>
												<asp:Label runat="server" Text="" ID="LblStatus"></asp:Label>
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:TemplateColumn>
											<HeaderStyle Width="32%" CssClass="titleTableGeneral"></HeaderStyle>
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
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
								</asp:datagrid>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
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
</html>
