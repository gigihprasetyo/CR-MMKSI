<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpPajakLogisticCost.aspx.vb" Inherits="PopUpPajakLogisticCost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Penalty Parkir - Bukti Pemotongan PPh 23</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript">
        function PrintDocument() {
            var kantor = document.getElementById('lblKantorPajak');
            var kota = document.getElementById('lblKota');
            var pejabat = document.getElementById('lblPejabat');
            var jabatan = document.getElementById('lblJabatan');
            if (kantor.innerHTML == "") {
                alert('Kantor Pajak harap diisi!');
                return;
            }
            if (kota.innerHTML == "") {
                alert('Kota dealer harap diisi!');
                return;
            }
            if (pejabat.innerHTML == "") {
                alert('Pejabat harap diisi!');
                return;
            }
            if (jabatan.innerHTML == "") {
                alert('Jabatan harap diisi!');
                return;
            }
            document.getElementById('btnSimpan').click();
            document.getElementById('divpajak').style.display = 'none';

            var printContents = document.getElementById('printed').innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;


        }

        function Kantor(obj) {
            obj.value = obj.value.toUpperCase();
            var ktr = document.getElementById('lblKantorPajak');
            ktr.innerHTML = document.getElementById('txtKtrPajak').value;
        }

        function Kota(obj) {
            //obj.value=obj.value.toUpperCase();
            var kota = document.getElementById('lblKota');
            kota.innerHTML = document.getElementById('txtKota').value;
        }

        function Pejabat(obj) {
            //obj.value=obj.value.toUpperCase();
            var ktr = document.getElementById('lblPejabat');
            ktr.innerHTML = document.getElementById('txtPejabat').value;
        }

        function Jabatan(obj) {
            //obj.value=obj.value.toUpperCase();
            var ktr = document.getElementById('lblJabatan');
            ktr.innerHTML = document.getElementById('txtJabatan').value;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divpajak" align="center">
            <table border="0">
                <tr class="hideTrOnPrint">
                    <td>Kantor Pajak</td>
                    <td>:</td>
                    <td nowrap align="left"><span style="font-weight: bold; font-size: 9pt">
                        <input id="txtKtrPajak" onkeyup="Kantor(this)" style="align: center" type="text" runat="server">
                    </span>
                    </td>
                </tr>
                <tr class="hideTrOnPrint">
                    <td>Nama Kota</td>
                    <td>:</td>
                    <td nowrap align="left"><span style="font-weight: bold; font-size: 9pt">
                        <input id="txtKota" onkeyup="Kota(this)" style="align: center" type="text" runat="server">
                    </span>
                    </td>
                </tr>
                <tr class="hideTrOnPrint">
                    <td>Pejabat</td>
                    <td>:</td>
                    <td nowrap align="left"><span style="font-weight: bold; font-size: 9pt">
                        <input id="txtPejabat" onkeyup="Pejabat(this)" style="align: center" type="text" runat="server">
                    </span>
                    </td>
                </tr>
                <tr class="hideTrOnPrint">
                    <td>Jabatan</td>
                    <td>:</td>
                    <td nowrap align="left"><span style="font-weight: bold; font-size: 9pt">
                        <input id="txtJabatan" onkeyup="Jabatan(this)" style="align: center" type="text" runat="server">
                    </span>
                    </td>
                </tr>
                <tr class="hideTrOnPrint">
                    <td align="center" colspan="3">
                        <input class="hideButtonOnPrint" id="btnSimpan" type="button" value="Simpan" runat="server"
                            style="display: none">
                        <input class="hideButtonOnPrint" id="btnCetak" onclick="PrintDocument()" type="button"
                            value="Cetak" runat="server">
                        <input class="hideButtonOnPrint" id="btnClose" onclick="window.close()" type="button" value="Tutup"
                            name="btnClose">
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" id="printed">
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                    <td style="width: 0.45cm">&nbsp;</td>
                </tr>
                <tr align="center">
                    <td align="center" colspan="40">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td style="width: 8%" valign="middle" align="center">
                                    <img src="../images/logo_pajak.jpg">
                                </td>
                                <td style="width: 72%" align="left">
                                    <table cellspacing="0" cellpadding="0" width="50%">
                                        <tr>
                                            <td nowrap align="center"><span style="font-weight: bold; font-size: 9pt">DEPARTEMEN 
														KEUANGAN REPUBLIK INDONESIA</span></td>
                                        </tr>
                                        <tr>
                                            <td nowrap align="center"><span style="font-weight: bold; font-size: 9pt">DIREKTORAT 
														JENDERAL PAJAK</span></td>
                                        </tr>
                                        <tr>
                                            <td nowrap align="center"><span style="font-weight: bold; font-size: 9pt">KANTOR 
														PELAYANAN PAJAK</span></td>
                                        </tr>
                                        <tr>
                                            <td nowrap align="center"><span style="font-weight: bold; font-size: 9pt">
                                                <asp:Label ID="lblKantorPajak" runat="server"></asp:Label></span></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20%" align="right">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 70%"></td>
                                            <td style="width: 30%" nowrap align="left"><span style="font-size: 9pt; font-style: normal">Lembar 
											ke-1 untuk : wajib pajak</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 70%"></td>
                                            <td style="width: 30%" nowrap align="left"><span style="font-size: 9pt; font-style: normal">Lembar 
											ke-2 untuk : Kantor Pelayanan Pajak</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 70%"></td>
                                            <td style="width: 30%" nowrap align="left"><span style="font-size: 9pt; font-style: normal">Lembar 
											ke-3 untuk : Pemotong Pajak</span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="40">&nbsp;</td>
                </tr>
                <tr height="22">
                    <td colspan="40">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td style="width: 20%"></td>
                                <td style="width: 60%" align="center">
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                                nowrap align="center"><span style="font-weight: bold; font-size: 9pt">BUKTI 
														PEMOTONGAN PPh PASAL 23</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; background-color: gray"
                                                nowrap align="center"><span style="font-weight: bold; font-size: 9pt">
                                                    <asp:Label ID="lblNomor" runat="server"></asp:Label></span></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20%"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="40">&nbsp;</td>
                </tr>
                <tr height="22">
                    <td align="left" colspan="9"><span style="font-weight: bold; font-size: 9pt">&nbsp;&nbsp;NPWP</span></td>
                    <td><span style="font-weight: bold; font-size: 9pt">:&nbsp;</span></td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">3</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">1</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">.</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">6</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">7</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">7</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">.</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">1</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">8</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">7</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">.</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">2</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">-</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">0</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">0</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">3</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">.</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">0</td>
                    <td style="border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid; border-right-width: thin; border-right-color: #000000"
                        align="center">0</td>
                    <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-bottom: #000000 thin solid"
                        align="center">0</td>
                    <td colspan="10"></td>
                </tr>
                <tr height="22">
                    <td align="left" colspan="9"><span style="font-weight: bold; font-size: 9pt">&nbsp;&nbsp;Nama</span></td>
                    <td><span style="font-weight: bold; font-size: 9pt">:&nbsp;</span></td>
                    <td align="left" colspan="30">PT MITSUBISHI MOTORS KRAMA YUDHA SALES INDONESIA</td>
                </tr>
                <tr height="22">
                    <td align="left" colspan="9"><span style="font-weight: bold; font-size: 9pt">&nbsp;&nbsp;Alamat</span></td>
                    <td><span style="font-weight: bold; font-size: 9pt">:&nbsp;</span></td>
                    <td align="left" colspan="30">Jl Jendral Ahmad Yani Pulomas Pulomas Kayu Putih Pulo Gadung Jakarta Timur DKI Jakarta</td>
                </tr>

                <tr>
                    <td colspan="40">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="40">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <!--bordercolorlight="black" bordercolor="black"-->
                            <tr height="30">
                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="center"><span style="font-weight: bold; font-size: 9pt">No</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="center"><span style="font-weight: bold; font-size: 9pt">Jenis Penghasilan</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="center" colspan="14"><span style="font-weight: bold; font-size: 9pt">Jumlah Penghasilan
											Bruto
                                        <br />
                                        (Rp) </span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="center"><span style="font-weight: bold; font-size: 9pt">Tarif Lebih Tinggi</span><br />
                                    <span style="font-weight: bold; font-size: 9pt">100% (Tdk ber NPWP)</span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000; background-color: gray"
                                    align="center" colspan="6"><span style="font-weight: bold; font-size: 9pt">Tarif<br />
                                        %</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000; background-color: gray"
                                    align="center" colspan="16"><span style="font-weight: bold; font-size: 9pt">PPh 
											yang dipotong<br />
                                        %</span></td>
                            </tr>
                            <tr height="20">
                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="center"><span style="font-weight: bold; font-size: 9pt">(1)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="center"><span style="font-weight: bold; font-size: 9pt">(2)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="center" colspan="14"><span style="font-weight: bold; font-size: 9pt">(3)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="center"><span style="font-weight: bold; font-size: 9pt">(4)</span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000; background-color: gray"
                                    align="center" colspan="6"><span style="font-weight: bold; font-size: 9pt">(5)</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000; background-color: gray"
                                    align="center" colspan="16"><span style="font-weight: bold; font-size: 9pt">(6)</span></td>
                            </tr>
                            <tr height="22">
                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt">1</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-weight: bold; font-size: 9pt">Dividen *)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">0,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>


                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt">2</span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-weight: bold; font-size: 9pt">Bunga *)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">0,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>



                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt">3</span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-weight: bold; font-size: 9pt">Royalti *)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">0,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>




                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt">4</span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-weight: bold; font-size: 9pt">Hadiah dan penghargaan *)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">0,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>

                            <tr height="22">
                                <td style="border-bottom-width: thin; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt">5</span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-weight: bold; font-size: 9pt">Sewa dan Penghasilan lain
                                        <br />
                                        sehubungan dengan</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000; background-color: gray"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt"></span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000; background-color: gray"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt"></span></td>
                            </tr>




                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-weight: bold; font-size: 9pt">penggunaan harta ***)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">0,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>

                            <tr height="22">
                                <td style="border-bottom-width: thin; border-left: #000000 thin solid;"
                                    align="center" valign="top"><span style="font-weight: bold; font-size: 9pt">6</span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-weight: bold; font-size: 9pt">Jasa Teknik,Jasa Manajemen,<br />
                                        Jasa Konsultasi dan jasa lain<br />
                                        sesuai PMK-244/PMK.03/2008 : </span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000; background-color: gray"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt"></span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000; background-color: gray"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt"></span></td>
                            </tr>


                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-weight: bold; font-size: 9pt">a.Jasa Teknik</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">0,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>


                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-weight: bold; font-size: 9pt">b.Jasa Manajemen</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">0,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>


                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-weight: bold; font-size: 9pt">c.Jasa Konsultan</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">0,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>

                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-weight: bold; font-size: 9pt">d.Jasa Lain :</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; background-color: gray"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000; background-color: gray"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt"></span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000; background-color: gray"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt"></span></td>
                            </tr>

                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-size: 8pt">1) Jasa Logistic</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">
                                        <asp:Label runat="server" ID="lblpphreturn"></asp:Label></span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">2,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">
                                        <asp:Label runat="server" ID="lblPhhdiPotong"></asp:Label>
                                    </span></td>
                            </tr>


                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-size: 8pt">2)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">2,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>

                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-size: 8pt">3) </span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">2,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>


                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-size: 8pt">4)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">2,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>


                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-size: 8pt">5)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">2,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>


                            <tr height="22">
                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="left"><span style="font-size: 8pt">6)
                                        <br />
                                        <br />
                                        <br />
                                        ****)</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">0</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">2,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">0</span></td>
                            </tr>

                            <tr height="22">
                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="center" colspan="2"><span style="font-weight: bold; font-size: 9pt">Jumlah</span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right" colspan="14"><span style="font-weight: bold; font-size: 9pt">
                                        <asp:Label ID="lblNilai1" runat="server"></asp:Label></span></td>

                                <td style="border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid;"
                                    align="right"><span style="font-weight: bold; font-size: 9pt"></span></td>

                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="6"><span style="font-weight: bold; font-size: 9pt">2,00%</span></td>
                                <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-left-width: thin; border-left-color: #000000; border-bottom-width: thin; border-bottom-color: #000000;"
                                    align="right" colspan="16"><span style="font-weight: bold; font-size: 9pt">
                                        <asp:Label ID="lblNilai2" runat="server"></asp:Label></span></td>
                            </tr>






                            <tr height="22">
                                <td style="border-top: #000000 thin solid; border-right: #000000 thin solid; border-left: #000000 thin solid; border-top-color: #000000; border-bottom: #000000 thin solid"
                                    align="left" colspan="40"><span style="font-weight: bold; font-size: 9pt">&nbsp;&nbsp;Terbilang 
											:</span> <span style="font-size: 9pt">
                                                <asp:Label ID="lblTerbilang" runat="server"></asp:Label></span></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="40">&nbsp;</td>
                </tr>
                <tr height="22">
                    <td colspan="15"></td>
                    <td align="center" colspan="25">

                        <span style="font-weight: bold; font-size: 9pt">
                            <asp:Label ID="lblKota" runat="server"></asp:Label><asp:Label ID="lblTanggal" runat="server"></asp:Label></span>


                    </td>
                </tr>
                <tr>
                    <td style="border-right: #000000 thin solid; border-top: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid"
                        align="left" colspan="15"><span style="font-weight: bold; font-size: 8pt">&nbsp;&nbsp;Perhatian 
								:</span></td>
                    <td align="center" colspan="25"></td>


                </tr>
                <tr>
                    <td style="border-top-width: thin; border-right: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; border-top-color: #000000"
                        align="left" colspan="15"><span style="font-weight: bold; font-size: 8pt">&nbsp;&nbsp;1. 
								Jumlah Pajak Penghasilan Pasal 23</span></td>
                    <td align="center" colspan="25"><span style="font-weight: bold; font-size: 8pt"></span></td>
                </tr>
                <tr>
                    <td style="border-top-width: thin; border-right: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; border-top-color: #000000"
                        align="left" colspan="15"><span style="font-weight: bold; font-size: 8pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;yang
								dipotong diatas merupakan</span></td>
                    <td colspan="25" align="center">

                        <span style="font-weight: bold; font-size: 8pt">Pemotong 
								Pajak</span>

                    </td>
                </tr>
                <tr>
                    <td style="border-top-width: thin; border-right: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; border-top-color: #000000"
                        align="left" colspan="15"><span style="font-weight: bold; font-size: 8pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;angsuran
								atas Pajak Penghasilan yang</span></td>
                    <td></td>

                    <td align="left" colspan="3"><span style="font-weight: bold; font-size: 8pt">NPWP</span></td>
                    <td><span style="font-weight: bold; font-size: 8pt">:</span></td>
                    <asp:Literal ID="ltlNPWP2" runat="server"></asp:Literal>

                </tr>
                <tr>
                    <td style="border-top-width: thin; border-right: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; border-top-color: #000000"
                        align="left" colspan="15"><span style="font-weight: bold; font-size: 8pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;terutang 
								untuk tahun pajak yang</span></td>
                    <td></td>
                    <td valign="middle" align="left" colspan="3"><span style="font-weight: bold; font-size: 8pt">Nama</span></td>
                    <td valign="middle"><span style="font-weight: bold; font-size: 8pt">:</span></td>
                    <td valign="middle" align="left" colspan="20">
                        <asp:Label ID="lblNama2" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="border-top-width: thin; border-right: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; border-top-color: #000000"
                        align="left" colspan="15"><span style="font-weight: bold; font-size: 8pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bersangkutan. 
								Simpanlah bukti</span></td>
                    <td colspan="25"></td>
                </tr>
                <tr>
                    <td style="border-top-width: thin; border-right: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; border-top-color: #000000"
                        align="left" colspan="15"><span style="font-weight: bold; font-size: 8pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pemotongan ini 
								baik-baik untuk</span></td>
                    <td colspan="25"></td>
                </tr>
                <tr>
                    <td style="border-top-width: thin; border-right: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; border-top-color: #000000"
                        align="left" colspan="15"><span style="font-weight: bold; font-size: 8pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;diperhitungkan sebagai
								kredit pajak</span></td>
                    <td colspan="25"></td>
                </tr>
                <tr>
                    <td style="border-top-width: thin; border-right: #000000 thin solid; border-bottom-width: thin; border-bottom-color: #000000; border-left: #000000 thin solid; border-top-color: #000000"
                        align="left" colspan="15"><span style="font-weight: bold; font-size: 8pt">&nbsp;&nbsp;2. 
								Bukti Pemotongan ini dianggap sah apabila </span>
                    </td>
                    <td colspan="25"></td>
                </tr>
                <tr>
                    <td style="border-top-width: thin; border-right: #000000 thin solid; border-left: #000000 thin solid; border-top-color: #000000; border-bottom: #000000 thin solid"
                        align="left" colspan="15"><span style="font-weight: bold; font-size: 8pt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;diisi 
								dengan lengkap dan benar.</span></td>
                    <td align="center" colspan="25"><span style="font-weight: bold; font-size: 8pt">
                        <asp:Label ID="lblPejabat" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td colspan="15"></td>
                    <td align="center" colspan="25"><span style="font-weight: bold; font-size: 8pt">
                        <asp:Label ID="lblJabatan" runat="server"></asp:Label></span></td>
                </tr>
                <tr>
                    <td colspan="40" align="left" style="font-weight: bold; font-size: 8pt; font-style: italic;">*) Tidak termasuk dividen kepada WP Orang Pribadi dalam negri</td>



                </tr>
                <tr>
                    <td colspan="40" align="left" style="font-weight: bold; font-size: 8pt; font-style: italic;">**) Tidak termasuk bunga simpanan yang dibayarkan oleh koperasi
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; kepada WP Orang pribadi</td>
                </tr>
                <tr>

                    <td colspan="40" align="left" style="font-weight: bold; font-size: 8pt; font-style: italic;">***) Kecuali sewa tanah dan bangunan</td>
                </tr>
                <tr>
                    <td colspan="40" align="left" style="font-weight: bold; font-size: 8pt; font-style: italic;">****) Apabila kurang harap diisi sendiri</td>
                </tr>
                <tr>
                    <td align="left" colspan="15"><span style="font-weight: bold; font-size: 8pt">&nbsp;&nbsp;F.1.1.33.06</span></td>
                    <td colspan="25"></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
