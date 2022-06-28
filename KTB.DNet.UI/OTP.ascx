<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="OTP.ascx.vb" Inherits=".OTP" ClassName="OTP"  %>

<title>Form OTP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		
        
            <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" style="align-content:center;align-items:center;align-self:center">
				
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
                    <td valign="middle">
                        <table cellspacing="1" cellpadding="2" width="100%" border="0">

                            <tr>
                                <td width="24%" colspan="3" align="center"><font color="crimson">Verifikasi Kode OTP</font></td>
                            </tr>
                            <tr>
                                <td width="24%" colspan="3" align="center">Silahkan Masukan Kode OTP yang Telah Dikirim ke Nomor HandPhone Anda</td>
                            </tr>
                            <tr>
                                <td width="24%" colspan="3" align="center">
                                    <asp:TextBox ID="txtKodeOTP" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtKodeOTP" ErrorMessage="**"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td  align="center" colspan="3">
                                    <b><asp:label runat="server" ID="lblAlert" style="font-size=12;font-style:normal;font-weight:normal;color:red" ForeColor="Red"></asp:label></b>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Verifikasi" CausesValidation="true" ></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </TR>
			</TABLE>