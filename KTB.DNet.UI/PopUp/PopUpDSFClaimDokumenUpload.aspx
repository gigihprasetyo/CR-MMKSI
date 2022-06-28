<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpDSFClaimDokumenUpload.aspx.vb" Inherits="PopUpDSFClaimDokumenUpload" %>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DSF Upload Dokumen Pendukung</title>
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
            <input id="hdnDSFLeasingClaimID" type="hidden" value="0" runat="server">
			
            <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
                <asp:Label ID="Label1" runat="server" Text="Upload Dokumen Pendukung" Font-Size="15px" Font-Bold="True"></asp:Label>
            </div><hr />
            <div>
                <table>
                    <tr>
                        <td class="titleField" width="15%">Nomor Registrasi&nbsp;</td>
                        <td style="text-align: center" width="2%">:</td>
                        <td>
                            <asp:Label ID="lblRegNumber" Font-Bold="true" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="titleField" width="15%">Kode Dealer&nbsp;</td>
                        <td style="text-align: center" width="2%">:</td>
                        <td>
                            <asp:Label ID="lblDealerCode" Font-Bold="true" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trAlasanBatal">
                        <td class="titleField" width="15%">Alasan Batal&nbsp;</td>
                        <td style="text-align: center" width="2%">:</td>
						<td><asp:textbox id="txtAlasanBatal" runat="server" MaxLength="100" Width="250px" TextMode="MultiLine"
                            onkeyDown="checkTextAreaMaxLength(this,event,'100');"></asp:textbox>&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%">
                <asp:DataGrid ID="dgUploadFile" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                    AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
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
                            <HeaderStyle CssClass="titleTableSales" Width="20%"></HeaderStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Path")%>'>
                                    <asp:Label ID="lblFileName" runat="server" alt="Download"></asp:Label>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <input id="UploadFile" onkeydown="return false;" style="width: 267px; height: 20px" type="file" size="25" name="File1" runat="server">
                                <asp:Label ID="lblFileUpload" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Keterangan">
                            <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FileDescription")%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                            <FooterTemplate>
                                <asp:TextBox ID="txtKeterangan" runat="server" Width="350px" TabIndex="12" />
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
                    var txtAlasanBatal = document.getElementById("txtAlasanBatal");
                    var hdnDSFLeasingClaimID = document.getElementById("hdnDSFLeasingClaimID");
                    
                    var isUpload = '1';
                    alert('Simpan dokumen berhasil');
                    window.returnValue = isUpload + ';' + hdnDSFLeasingClaimID.value + ';' +  txtAlasanBatal.value;
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
