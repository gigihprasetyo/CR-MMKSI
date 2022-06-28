<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpMSPCertificate.aspx.vb" Inherits=".PopUpMSPCertificate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
		<title>MSP - Cetak Sertifikat</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">
		    function PrintDocument() {
		        window.print();
		    }
         </script>
        <%--<style type="text/css">
            @media print{@page {size: landscape}}

            @media print {
                body:before {
                    content: url(../images/Copy.png);
                }
            }
        </style>--%>
        <style type="text/css" media="print">
 
            /* @page {size:landscape}  */   
            body {
                page-break-before: avoid;
                width:100%;
                height:100%;
                -webkit-transform: rotate(-90deg) scale(.68,.68); 
                -moz-transform:rotate(-90deg) scale(.58,.58);
                zoom: 100%    }

        </style>
      
	</head>
	<body>
		<form id="form1" runat="server">
			<div id="divCertificate" align="center">
				<table border="0">
					<tr class="hideTrOnPrint">
						<td align="center" colSpan="3">
                            <input class="hideButtonOnPrint" id="btnCetak" onclick="PrintDocument()" type="button" value="Cetak" runat="server"> 
                            <input class="hideButtonOnPrint" id="btnClose" onclick="window.close()" type="button" value="Tutup" name="btnClose">
						</td>
					</tr>
				</table>
			</div>
			<div align="center">
				<table width="100%" runat="server" id="tblMain">
				<%--<table background="../images/Copy.png" width="100%" style="background-repeat: no-repeat;background-position: center;" runat="server" id="tblMain">--%>
	                <tr>
		                <td align="center">
			                <table>
				                <tr>
					                <td align="center" colspan="2">
						                <%--<p class="MsoNormal" style="text-align: center;" align="center"><span lang="IN">&nbsp;</span></p>--%>
						
						                <p class="MsoNormal" style="margin-bottom: .0001pt; text-align: center; line-height: normal;" align="center">
							                <strong><em><span lang="IN" style="font-size: 20.0pt;">SERTIFIKAT</span></em></strong>
						                </p>
						                <p class="MsoNormal" style="margin-bottom: .0001pt; text-align: center; line-height: normal;" align="center">
							                <strong><em><span lang="IN" style="font-size: 20.0pt;">MITSUBISHI PAKET SMART</span></em></strong>
						                </p>

						                <p class="MsoNormal" style="margin-bottom: .0001pt; text-align: center; line-height: normal;" align="center">
							                <strong><em><span lang="IN" style="font-size: 12.0pt;">Diberikan kepada :</span></em></strong>
						                </p>
					                </td>
				                </tr>
				                <tr>
					                <td align="right" width="50%">
						                <p class="MsoNormal" style="margin-bottom: 0.0001pt; line-height: normal;">
							                <strong><em><span lang="IN" style="font-size: 12.0pt;">Tipe Kendaraan&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : </span></em></strong>
						                </p>
						                <p class="MsoNormal" style="margin-bottom: 0.0001pt; line-height: normal;">
							                <strong><em><span lang="IN" style="font-size: 12.0pt;">Nomor Chassis&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : </span></em></strong>
						                </p>
						                <p class="MsoNormal" style="margin-bottom: 0.0001pt; line-height: normal;">
							                <strong><em><span lang="IN" style="font-size: 12.0pt;">Nomor Engine&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : </span></em></strong>
						                </p>						                
					                </td>
					                <td width="50%">
						                <p class="MsoNormal" style="margin-bottom: 0.0001pt; line-height: normal;">
							                <strong><em><span lang="IN" style="font-size: 12.0pt;">&nbsp; <asp:label runat="server" ID="lblVehicleType"></asp:label></span></em></strong>
						                </p>
						                <p class="MsoNormal" style="margin-bottom: 0.0001pt; line-height: normal;">
							                <strong><em><span lang="IN" style="font-size: 12.0pt;">&nbsp; <asp:label runat="server" ID="lblChassisNumber"></asp:label></span></em></strong>
						                </p>
						                <p class="MsoNormal" style="margin-bottom: 0.0001pt; line-height: normal;">
							                <strong><em><span lang="IN" style="font-size: 12.0pt;">
                                                &nbsp; <asp:label runat="server" ID="lblEngineNumber"></asp:label>
                                            </span></em></strong>
						                </p>	
					                </td>
				                </tr>
				                <tr>
					                <td colspan="2">
						                <p class="MsoNormal" style="margin-bottom: .0001pt; text-align: center; line-height: normal;" align="center">
							                <strong><em> <span lang="IN">Berhak mendapatkan pelayanan Perawatan Berkala <asp:label runat="server" ID="lblDuration"></asp:label> Tahun/<asp:label runat="server" ID="lblMSPKM"></asp:label> KM Paket <asp:label runat="server" ID="lblMSPtypeDescription"></asp:label> terhitung sejak</span> </em></strong>
                                            <br />
                                            <strong><em> <span lang="IN">Tanggal penerimaan kendaraan, dan mengikuti syarat dan ketentuan yang berlaku.</span> </em></strong>
						                </p>
						               </td>
				                </tr>
				                <tr>
					                <td width="50%">
						                &nbsp;
					                </td>
					                <td width="50%">
						                <p class="MsoNormal" style="margin-bottom: 0.0001pt; line-height: normal; text-align: right;" align="center">
							                <strong><span lang="IN" style="font-size: 12.0pt;">&nbsp;</span></strong><strong><span lang="IN"><asp:label runat="server" ID="lblCityDealer"></asp:label>, <asp:label runat="server" ID="lblRequestDate"></asp:label></span></strong>
							                <em><span lang="IN">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></em>
						                </p>
						                <p class="MsoNormal" style="text-indent: .5in; line-height: normal; margin: 0in 0in .0001pt 4.5in;"><strong><span lang="IN">&nbsp;</span></strong></p>

					                </td>
				                </tr>
				                <tr>
					                <td width="50%">
						                <p class="MsoNormal" style="margin-bottom: 0.0001pt; line-height: normal; text-align: left;">
						                <em><span lang="IN" style="font-size: 10.0pt;">Kode Dealer : <asp:label runat="server" ID="lblDealerCode"></asp:label></span></em> <br>
						                <em><span lang="IN" style="font-size: 10.0pt;">Tanggal penjualan : <asp:label runat="server" ID="lblPKTDate"></asp:label></span></em>
						                </p>
						                <br/><br/><br/><br/>
					                </td>
					                <td width="50%">
						                &nbsp;
					                </td>
				                </tr>
				                <tr>
					                <td width="50%">
						                &nbsp;
					                </td>
					                <td width="50%" align="right">
					
					                <p class="MsoNormal" style="margin-bottom: 0.0001pt; text-indent: 0.5in; line-height: normal;">
						                <strong><span lang="IN"><em>(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)</em> </span></strong>
						                <strong>
						                <em><span lang="IN">&nbsp;&nbsp;&nbsp;</span></em></strong> 
						                <br>
						                <strong><em><span lang="IN" style="font-size: 10.0pt;">Branch Manager </span></em></strong>
						                <em><span lang="IN">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></em></strong>
					                </p>
					                </td>
				                </tr>
			                </table>
		                </td>
	                </tr>
                </table>
			</div>
		</form>
	</body>
</html>
