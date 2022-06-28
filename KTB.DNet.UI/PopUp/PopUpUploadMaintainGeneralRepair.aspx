<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpUploadMaintainGeneralRepair.aspx.vb" Inherits=".PopUpUploadMaintainGeneralRepair" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PopUpUploadMaintainGeneralRepair</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
	<meta content="JavaScript" name="vs_defaultClientScript"/>
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link href="../WebResources/scheduler_traditional.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/css/jquery-ui.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/jquery-clockpicker.min.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery.1.11.0.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-ui.min.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-clockpicker.min.js"></script>

	<base target="_self" />
	<script type="text/javascript" language="javascript">
	    function onSuccess(result) {
	        if (navigator.appName == "Microsoft Internet Explorer")
	            window.returnValue = "Success Upload.";
	        else {
	            window.close();
	            window.opener.dialogWin.returnFunc("Success Upload.");
	        }

	        window.close();
	    };
	</script>
</head>
<body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table2" cellspacing="3" cellpadding="3" width="100%" border="0">
            <tr>
                <td class="titleTableParts3" colspan="3">Popup Maintain General Repair</td>
            </tr>

            <tr>
                <td class="titleField">Upload File</td>
                <td style="width: 2px" valign="top">:</td>
                <td valign="top">
                    <input onkeypress="return false;" id="DataFile" style="height: 20px" type="file" size="29"
                    name="File1" runat="server">
                &nbsp;&nbsp;Minimum Excel 2007 (*.xls / *.xlsx)
                                    
                </td>
            </tr>

            <tr>
                <td class="titleField" style="width: 24%"></td>
                <td style="width: 1%"></td>
                <td style="width: 75%">
                    <asp:Button ID="btnUpload" runat="server" Width="88px" Text="Upload" OnClick="btnUpload_Click" ></asp:Button>&nbsp;
                    <asp:Button ID="btnBatal" runat="server" Width="88px" Text="Batal" OnClick="btnBatal_Click" ></asp:Button>&nbsp;
					<asp:Button ID="btnSimpan" runat="server" Width="88px" Text="Simpan"  Enabled ="false" OnClick="btnSimpan_Click"></asp:Button>
                </td>
            </tr>
            <tr id="trCost" runat="server">
                <td colspan="4" class="titleField">
                    <asp:DataGrid ID="dtgServiceStandardTime" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
							BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="false" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
						OnSortCommand="dtgServiceStandardTime_SortCommand"	
                        				AllowPaging="True">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
                                
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="GRKindID" Visible="false">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGRKindID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GRKindID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Variant">
                                            <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbVariant" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Variants")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Service">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbJenisService" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kind Code">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbKindCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jasa Service">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbJasaService" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LaborCost")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Durasi Service">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbDurasiService" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LaborDuration")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Mulai Berlaku">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbMulaiBerlaku" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ValidFrom")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                        <asp:BoundColumn DataField="ErrorMessage" HeaderText="Keterangan">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>                               
						    </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                </td>
            </tr>

        <tr>

        <tr id="tr1" runat="server">
                <td colspan="4" class="titleField">
                    <asp:DataGrid ID="dtgServiceStandardTimeDetail" runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
							BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="false" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
						OnSortCommand="dtgServiceStandardTimeDetail_SortCommand"	
                        				AllowPaging="True">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
									<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
                                <asp:TemplateColumn Visible ="false">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceTemplateGRId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceTemplateGRLaborID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible ="false">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSparepartMasterId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMasterID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Variant">
                                            <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbVariantDetail" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Service">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbJenisServiceDetail" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                 <asp:TemplateColumn HeaderText="Kind Code">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbKindCodeDetail" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama Sparepart">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbNamaSaprepart" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Sparepart">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbKodeSparepart" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Harga Satuan">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbHargaSatuan" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jumlah Unit">
                                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbJumlahUnit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartQuantity")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                        <asp:BoundColumn DataField="ErrorMessage" HeaderText="Keterangan">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>                               
						    </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                </td>
            </tr>

        </tr>
    </table>
    </form>
</body>
</html>
