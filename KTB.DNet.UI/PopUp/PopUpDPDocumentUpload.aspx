<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpDPDocumentUpload.aspx.vb" Inherits="PopUpDPDocumentUpload" %>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Upload Lampiran PO/SPK</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">
		    function checkTextAreaMaxLength(textBox, e, length) {
		        var mLen = textBox["MaxLength"];
		        if (null == mLen)
		            mLen = length;

		        var maxLength = parseInt(mLen);
		        if (!checkSpecialKeys(e)) {
		            if (textBox.value.length > maxLength - 1) {
		                if (window.event)//IE
		                    e.returnValue = false;
		                else//Firefox
		                    e.preventDefault();
		            }
		        }
		    }
		    function checkSpecialKeys(e) {
		        if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
		            return false;
		        else
		            return true;
		    }

		</script>
	</HEAD>
  
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
            <asp:HiddenField ID="hdnMode" runat="server" />
            <input id="hdnDiscountProposalHeaderID" type="hidden" value="0" runat="server">
			
            <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
                <asp:Label ID="Label1" runat="server" Text="Upload Dokumen Pendukung" Font-Size="15px" Font-Bold="True"></asp:Label>
            </div>
            <div>
                <hr />
                <table width="100%">
                    <tr>
                        <td class="titleField" width="25%">Nomor Registrasi&nbsp;</td>
                        <td style="text-align: center" width="2%">:</td>
                        <td>
                            <asp:Label ID="lblRegNumber" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="titleField" width="25%">Kode Dealer&nbsp;</td>
                        <td style="text-align: center" width="2%">:</td>
                        <td>
                            <asp:Label ID="lblDealerCode" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="titleField" width="20%">Nomor Aplikasi Dealer&nbsp;</td>
                        <td style="text-align: center" width="2%">:</td>
                        <td>
                            <asp:Label ID="lblDealerProposalNo" runat="server" ></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 50%">
                <asp:DataGrid ID="dgUploadFile" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                    AutoGenerateColumns="False" ShowFooter="True" PageSize="10000" AllowPaging="false" AllowSorting="false" AllowCustomPaging="false" >
                    <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                    <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                    <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                    <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="No.">
                            <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nama File">
                            <HeaderStyle CssClass="titleTableSales" Width="40%"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Path")%>'>
                                    <asp:Label ID="lblFileName" runat="server" alt="Download"></asp:Label>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <input id="UploadFile" onkeydown="return false;" style="width: 467px; height: 20px" type="file" size="25" name="File1" runat="server">
                                <asp:Label ID="lblFileUpload" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="Delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnAdd" TabIndex="40" CommandName="add" Text="Tambah" runat="server">
                                        <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
            <br />
            <div>
                <center>
                    <asp:Button ID="btnSave" runat="server" class="hideButtonOnPrint" Text="Simpan" Width="60px"></asp:Button>&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Batal" class="hideButtonOnPrint" Width="60px"></asp:Button>
                </center>
            </div>

            <asp:Panel ID="pnlRunCloseWindow" runat="server" Visible="False">
                <script language="javascript">
                    var hdnDiscountProposalHeaderID = document.getElementById("hdnDiscountProposalHeaderID");

                    var isUpload = '1';
                    alert('Simpan dokumen berhasil');
                    window.returnValue = isUpload + ';' + hdnDiscountProposalHeaderID.value;
                    window.close();
                    if (navigator.appName != "Microsoft Internet Explorer")
                    { opener.dialogWin.returnFunc(isUpload); }
                </script>
            </asp:Panel>
            <asp:Panel ID="pnlRunCloseWindow2" runat="server" Visible="False">
                <script language="javascript">
                    window.close();
                </script>
            </asp:Panel>
		</form>
	</body>
</HTML>
