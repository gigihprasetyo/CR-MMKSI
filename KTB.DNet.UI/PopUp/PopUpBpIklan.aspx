<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpBpIklan.aspx.vb" Inherits="PopUpBpIklan" smartNavigation="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>PopUpBpIklan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
        <script language="javascript">
        
            function DoRefresh()
            {
                //window.returnValue = "DoRefresh"
                window.close();
            }
        
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
							elm.checked = checkVal
						}
					}
				}
			}
        
        </script>
    </HEAD>
    <body>
        <form id="Form1" method="post" runat="server">
            <DIV style="TEXT-ALIGN: left">
                <TABLE id="Table1" cellSpacing="1" cellPadding="1" border="0">
                    <TR>
                        <TD>Kode Dealer</TD>
                        <TD>:</TD>
                        <TD>
                            <asp:Label id="lblDealerCode" runat="server"></asp:Label></TD>
                    </TR>
                    <TR>
                        <TD>Nama Dealer</TD>
                        <TD>:</TD>
                        <TD>
                            <asp:Label id="lblDealerName" runat="server"></asp:Label></TD>
                    </TR>
                    <TR>
                        <TD>No Pengajuan</TD>
                        <TD>:</TD>
                        <TD>
                            <asp:Label id="lblNoPengajuan" runat="server"></asp:Label></TD>
                    </TR>
                    <TR>
                        <TD>Tanggal Pengajuan</TD>
                        <TD>:</TD>
                        <TD>
                            <asp:Label id="lblTglPengajuan" runat="server"></asp:Label></TD>
                    </TR>
                    <TR>
                        <TD>Jenis Kegiatan</TD>
                        <TD>:</TD>
                        <TD>
                            <asp:Label id="lblJenisKegiatan" runat="server"></asp:Label></TD>
                    </TR>
                    <TR>
                        <TD>Jumlah Pengajuan</TD>
                        <TD>:</TD>
                        <TD>
                            <asp:label id="lblTotalBiayaIklan" Font-Bold="True" Text="Total Biaya : Rp. 0" Runat="server">Rp. 0</asp:label></TD>
                    </TR>
                </TABLE>
            </DIV>
            <DIV style="TEXT-ALIGN: right">
                <asp:datagrid id="dgIklan" runat="server" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" Width="100%" GridLines="None"
                    DataKeyField="ID">
                    <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" ForeColor="White" BackColor="LightGrey"></HeaderStyle>
                    <FooterStyle ForeColor="#4A3C8C" BackColor="#DEDEDE"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="Media">
                            <HeaderStyle ForeColor="White" BackColor="LightGrey"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id="lblMedia" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nama Media">
                            <HeaderStyle ForeColor="White" BackColor="LightGrey" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id="lblNamaMedia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MediaName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tgl Tayang Iklan">
                            <HeaderStyle ForeColor="White" BackColor="LightGrey" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblStartDate" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.StartDate"),"dd/MM/yyyy") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tgl Selesai">
                            <HeaderStyle ForeColor="White" BackColor="LightGrey" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblEndDate" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.EndDate"),"dd/MM/yyyy") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Produk Kategori">
                            <HeaderStyle ForeColor="White" BackColor="LightGrey" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id="lblProductCatIklan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.Description") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Produk Display">
                            <HeaderStyle ForeColor="White" BackColor="LightGrey" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label id="lblKendDisplay" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Biaya Iklan (Rp)">
                            <HeaderStyle ForeColor="White" BackColor="LightGrey" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label id="lblCost" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Expense"),"#,##0") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Persetujuan MMKSI">
                            <HeaderStyle ForeColor="White" BackColor="LightGrey" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtPersetujuanKTB" MaxLength="13" Runat="server" Width="80" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Text='<%# Format(DataBinder.Eval(Container, "DataItem.KTBApprovalAmount"),"#,##0") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:LinkButton Runat="server" ID="lnkReject" CommandName="Reject" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>Reject</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:datagrid>
                <asp:Button id="btnSave" runat="server" Text="Save" CommandName="UpdateBpIklan"></asp:Button>
                <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close();" type="button" value="Close" name="btnCancel">
                </DIV>
        </form>
    </body>
</HTML>
