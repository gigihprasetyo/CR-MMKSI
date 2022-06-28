<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmReceipt.aspx.vb" Inherits="FrmReceipt" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>

    <script language="javascript" type="text/javascript">
        function printx() {
            //document.getElementById('divPrint').style.display = 'none';
            window.print();
        }

        function keyUP(txt) {
            if (txt.value.length == txt.maxLength) {
                if (txt.id == "txtNomorFaktur1") {
                    document.getElementById("txtNomorFaktur2").focus()
                } else if (txt.id == "txtNomorFaktur2") {
                    document.getElementById("txtNomorFaktur3").focus()
                } else if (txt.id == "txtNomorFaktur3") {
                    document.getElementById("txtNomorFaktur4").focus()
                }
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:Panel ID="panelInputan" runat="server">
            
          <table id="Table2" cellspacing="5" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px" colspan="2">SALES CAMPAIGN - Input Kuitansi</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" colspan="2" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" colspan="2" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>

            <tr>
                <td class="titleField" width="20%">Kode Dealer&nbsp;</td>
                <td>
                    <asp:Label ID="lblCodeDealer" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="titleField" width="20%">Nama Dealer&nbsp;</td>
                <td>
                    <asp:Label ID="lblNamaDealer" runat="server"></asp:Label>
                </td>

            </tr>

            <tr>
                <td class="titleField" width="20%">Nomor Claim&nbsp;</td>
                <td>
                    <asp:Label ID="lblNoClaim" runat="server"></asp:Label>

                </td>

            </tr>

            <tr>
                <td class="titleField" width="20%">Nomor Kuitansi&nbsp;</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNoKuitansi" onblur="omitSomeCharacter('txtNoKuitansi','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                     
                </td>

            </tr>

            <tr style="display:none">
                <td class="titleField" width="20%">Nomor Faktur Pajak&nbsp;</td>
                <td>

                    <%--<asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNomorFaktur" onblur="omitSomeCharacter('txtNomorFaktur','<>?*%$;')"
                        runat="server" Width="242px" MaxLength="18"></asp:TextBox>--%>
                    <asp:TextBox onkeypress="return NumericOnlyWith(event,',');" ID="txtNomorFaktur1" onblur="omitSomeCharacter('txtNomorFaktur1','<>?*%$;')" onkeyup="keyUP(this)"
                        runat="server" Width="25px" MaxLength="2"></asp:TextBox><asp:Label ID="lblNomorFaktur1" runat="server" Text="." />
                    <asp:TextBox onkeypress="return NumericOnlyWith(event,',');" ID="txtNomorFaktur2" onblur="omitSomeCharacter('txtNomorFaktur2','<>?*%$;')" onkeyup="keyUP(this)"
                        runat="server" Width="25px" MaxLength="3"></asp:TextBox><asp:Label ID="lblNomorFaktur2" runat="server" Text="-" />
                    <asp:TextBox onkeypress="return NumericOnlyWith(event,',');" ID="txtNomorFaktur3" onblur="omitSomeCharacter('txtNomorFaktur3','<>?*%$;')" onkeyup="keyUP(this)"
                        runat="server" Width="20px" MaxLength="2"></asp:TextBox><asp:Label ID="lblNomorFaktur3" runat="server" Text="." />
                    <asp:TextBox onkeypress="return NumericOnlyWith(event,',');" ID="txtNomorFaktur4" onblur="omitSomeCharacter('txtNomorFaktur4','<>?*%$;')" onkeyup="keyUP(this)"
                        runat="server" Width="55px" MaxLength="8"></asp:TextBox>
                    <asp:Label ID="lblNomorFakturOld" runat="server" Visible="false"></asp:Label>
                    <span style="color: red; font-size: 10px"> 1 Digit pertama diabaikan, contoh: 10.XXX-YY.ZZZZZZZZZ</span><br /><br />
                    <span style="color: red; font-size: 10px">Per 1 Mei, Dealer tidak lagi menerbitkan Faktur Pajak untuk PPN ke MMKSI</span>                   
                </td>
            </tr>

            <tr>
                <td class="titleField" width="20%">Tanggal Faktur Pajak&nbsp;</td>
                <td>
                    <table border="0">
                        <tr>
                            <td>
                                <asp:Label ID="lblTanggalFaktur" runat="server" Visible="false"></asp:Label>
                                <cc1:IntiCalendar ID="icTglFaktur" runat="server" TextBoxWidth="70" CanPostBack="true"></cc1:IntiCalendar>
                            </td>
                            <td>
                                <asp:Button ID="btnCalculate" runat="server" Text="Kalkulasi" OnClick="btnCalculate_Click" />   
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td class="titleField" width="20%">Amount Kuitansi &nbsp;</td>
                <td>
                    <%-- <asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtAmount" onblur="omitSomeCharacter('txtAmount','<>?*%$;')"
							            runat="server" Width="242px"></asp:textbox>--%>
                    <asp:TextBox onkeypress="return numericOnlyUniv(event)" Enabled="false" onkeyup="pic(this,this.value,'9999999999','N')" ID="txtAmount" onblur="omitSomeCharacter('txtAmount','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                     
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">Amount Kuitansi setelah Reduksi&nbsp;</td>
                <td>
                    <asp:TextBox onkeypress="return numericOnlyUniv(event)" Enabled="false" onkeyup="pic(this,this.value,'9999999999','N')" ID="txtAmountReduksi" onblur="omitSomeCharacter('txtAmountReduksi','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="titleField" width="20%">Tanggal Kuitansi&nbsp;</td>
                <td>
                    <cc1:IntiCalendar ID="icTglKuitansi" runat="server" TextBoxWidth="70" CanPostBack="true"></cc1:IntiCalendar>
                   
                </td>

            </tr>

            <tr>
                <td class="titleField" width="20%">Nomor Rekening &nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddlDealerBankAccount" runat="server"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td class="titleField" width="20%">Telah Terima Dari &nbsp;</td>
                <td>MITSUBISHI MOTORS KRAMA YUDHA SALES INDONESIA, PT 
                </td>
            </tr>

            <tr>
                <td class="titleField" width="20%">Untuk Pembayaran&nbsp;</td>
                <td>
                    <asp:Label ID="lblKet" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="titleField" width="20%">Nama Tanda Tangan&nbsp;</td>
                <td>
                   <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtName" onblur="omitSomeCharacter('txtName','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td class="titleField" width="20%">Jabatan Tanda Tangan&nbsp;</td>
                <td>
                   <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtTitle" onblur="omitSomeCharacter('txtTitle','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">&nbsp;</td>
                <td>
                    <asp:Button class="hideButtonOnPrint" ID="btnSimpan" runat="server" Text="Simpan" Width="60px"></asp:Button>&nbsp;
					<asp:Button class="hideButtonOnPrint" ID="btnKembali" runat="server" Text="Kembali" Width="60px"></asp:Button>&nbsp;

                    <asp:Button class="hideButtonOnPrint" ID="btnCetak" runat="server" Text="Cetak" Width="60px"></asp:Button>&nbsp;

                    <asp:Button class="hideButtonOnPrint" ID="btnPrint_" OnClientClick="printx()" runat="server" Text="Print" Width="60px" Visible="false"></asp:Button>&nbsp;

                  
                    <asp:HiddenField ID="hfIdHeader" runat="server" />
                    <asp:HiddenField ID="hfPpn" runat="server" />
                    <asp:HiddenField ID="hfPph" runat="server" />


                     <asp:HiddenField ID="hfNoClaimPrint" runat="server" />
                </td>

            </tr>
        </table>
        </asp:Panel>

        <asp:Panel ID="panelCetak" runat="server" Visible="false">
            <table width="640" cellspacing="1" cellpadding="1" border="0" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 640px; POSITION: absolute; TOP: 8px; HEIGHT: 400px" id="Table1">
				<tbody><tr>
					<td align="right" colspan="3"><asp:Label ID="lblCodeDealer1" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td align="right" colspan="3"><asp:Label ID="lblNamaDealer1" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td colspan="3"> <asp:Label ID="lblTglKuitansi" runat="server"></asp:Label>
						<br>
						<br>
						<br>
					</td>
				</tr>
				<tr>
					<td align="center" colspan="3"><u><strong>KUITANSI</strong></u></td>
				</tr>
				<tr>
					<td align="center" colspan="3"><strong> <asp:Label ID="lblNoKuitansi" runat="server"></asp:Label></strong></td>
				</tr>
                  <tr>
					<td style="HEIGHT: 28px"  colspan="3"><strong><u> <asp:Label ID="lblNoClaimPrint" runat="server" style="color:red"></asp:Label></u></strong></td>
					
				</tr>
                 <tr>
					<td style="WIDTH: 147px; HEIGHT: 28px">Telah Terima Dari</td>
					<td style="WIDTH: 6px; HEIGHT: 28px"><strong>:</strong></td>
					<td style="HEIGHT: 28px"><u>PT Mitsubishi Motors Krama Yudha Sales Indonesia</u></td>
				</tr>
				<tr>
					<td style="WIDTH: 147px; HEIGHT: 28px"></td>
					<td style="WIDTH: 6px; HEIGHT: 28px"></td>
					<td style="HEIGHT: 28px">Jl. Jend. Ahmad Yani Jakarta<br>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 147px; HEIGHT: 25px">Uang Sejumlah</td>
					<td style="WIDTH: 6px; HEIGHT: 25px"><strong>:</strong></td>
					<td style="HEIGHT: 25px"><u>
                <asp:Label ID="lblTerbilang" runat="server" Text="Label"></asp:Label>
					                         </u></td>
				</tr>
				<tr>
					<td style="WIDTH: 147px; HEIGHT: 27px">Untuk Pembayaran</td>
					<td style="WIDTH: 6px; HEIGHT: 27px"><strong>:</strong></td>
					<td style="HEIGHT: 27px"><u><asp:Label ID="lblKet1" runat="server"></asp:Label></u></td>
				</tr>

                <tr>
					<td style="WIDTH: 147px; HEIGHT: 27px">Nomor Rekening</td>
					<td style="WIDTH: 6px; HEIGHT: 27px"><strong>:</strong></td>
					<td style="HEIGHT: 27px"><u><asp:Label ID="lblDealerBankAccount" runat="server"></asp:Label></u></td>
				</tr>
                <tr>
					<td style="WIDTH: 147px; HEIGHT: 27px">Remark</td>
					<td style="WIDTH: 6px; HEIGHT: 27px"><strong>:</strong></td>
					<td style="HEIGHT: 27px"><u><asp:Label ID="lblRemarks" runat="server"></asp:Label></u></td>
				</tr>
			
				<tr>
					<td colspan="3" style="WIDTH: 147px; HEIGHT: 13px"></td>
				</tr>
				<tr>
					<td colspan="3" style="WIDTH: 147px; HEIGHT: 50px">
						<table width="300" cellspacing="1" cellpadding="1" border="0" style="WIDTH: 300px; HEIGHT: 41px" id="Table2">
							<tbody><tr>
								<td valign="top"></td>
							</tr>
						</tbody></table>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 147px; HEIGHT: 116px" colspan="3">
						<table width="640" cellspacing="1" cellpadding="1" border="0" style="WIDTH: 640px; HEIGHT: 48px" id="Table3">
							<tbody><tr>
								<td valign="top">
								</td>
								<td>
									
								</td>
								<td>
									<br>
								</td>
							</tr>
							<tr>
								<td>&nbsp;
								</td>
								<td>
									
								</td>
								<td>
									
								</td>
							</tr>
							<tr>
								<td><span id="lblTotal">Total </span>
								</td>
								<td>
									<span id="Label3">:</span>&nbsp;
								</td>
								<td>
									<asp:Label ID="lblAmount" runat="server"></asp:Label>
								</td>
							</tr>
                            <tr style="display:none">
								<td><span id="lblNote">Note </span>
								</td>
								<td>
									<span id="Label4">:</span>&nbsp;
								</td>
								<td>
									<asp:Label ID="lblNoteTxt" runat="server" Font-Italic="true" Text="Per 1 Mei, Dealer tidak lagi menerbitkan Faktur Pajak untuk PPN ke MMKSI"></asp:Label>
								</td>
							</tr>
						</tbody></table>
					</td>
					<%--<td style="WIDTH: 6px; HEIGHT: 116px"></td>--%>
					<%--<td valign="top" align="center" style="HEIGHT: 116px">
                                                 <asp:Label ID="lblCity" runat="server" Text="Label"></asp:Label>,
						<br>
						<asp:Label ID="lblNamaDealer2" runat="server"></asp:Label><br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                        <br>
						<hr width="150">
                         <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
						
                        <br>
					</td>--%>
				</tr>
                <tr>
					<td style="WIDTH: 147px; HEIGHT: 27px"></td>
					<td style="WIDTH: 6px; HEIGHT: 27px"></td>
					<td valign="top" align="center" style="HEIGHT: 116px">
                        <asp:Label ID="lblCity" runat="server" Text="Label"></asp:Label>,
						<br>
						<asp:Label ID="lblNamaDealer2" runat="server"></asp:Label><br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                        <br>
						<hr width="150">
                         <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>						
					</td>
				</tr>
				<tr>
					<td align="left" colspan="3" style="WIDTH: 147px"></td>
				</tr>
				
				<tr>
					<td align="center" colspan="3"><input type="button" style="width:72px;" class="hideButtonOnPrint" id="btnPrint" language="javascript" onclick="window.print();" value="Print" name="btnPrint">

                             <asp:Button  class="hideButtonOnPrint" ID="btnKembali1" runat="server" Text="Kembali" />
             


					</td>
				</tr>
			</tbody></table>
        </asp:Panel>

    </form>
</body>
</html>
