<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmChassisMasterDocInput.aspx.vb" Inherits=".frmChassisMasterDocInput" %>
 <%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Pengajuan Diskon</title>
     
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link href="../WebResources/ocr/bootstrap.min.css" rel="stylesheet" />
    <link href="../WebResources/css/Modal.css" rel="stylesheet" />
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
     <script type="text/javascript" src="../WebResources/ocr/jquery-1.12.4.min.js"></script>
      <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 7]>
      <script src="../WebResources/ocr/html5shiv.min.js"></script>
      <script src="../WebResources/ocr/respond.min.js"></script>
    <![endif]-->

        <!--[if lt IE 8]>
      <script src="../WebResources/ocr/html5shiv.min.js"></script>
      <script src="../WebResources/ocr/respond.min.js"></script>
    <![endif]-->

    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" type="text/javascript">
       

        function showLoadingPage() {

            try {
                var hdnProgress = document.getElementById('hdnProgress');

                if (hdnProgress.value == "1") {
                    $("#Table2").find("input,button,textarea,select").attr("disabled", "disabled");


                    $('#pleaseWaitDialog').modal();
                } else {
                    $('#pleaseWaitDialog').modal('hide');
                    $("#Table2").find("input,button,textarea,select").removeAttr("disabled");

                }
            } catch (e) {

            }


            return true;
        }


        function addEventListeners() {

            try {
                document.getElementById('photoViewChassis').addEventListener('click', imageOnChassisClick);
            } catch (e) {

            }
            try {
                document.getElementById('photoViewEngine').addEventListener('click', imageOnEngineClick);
            } catch (e) {

            }
        }

        function DealerSelection(selectedDealer) {
           
        }

        function imageOnChassisClick(evt) {
            var photoViewChassis = document.getElementById('photoViewChassis');
            var imageSource = photoViewChassis.getAttribute('src');
            var lblAttachmentChassis = document.getElementById('lblAttachmentChassis').innerHTML;


            if (lblAttachmentChassis == null || lblAttachmentChassis == '' || lblAttachmentChassis == undefined) {

                return false;
            }


            showPopUp('../PopUp/PopUpImage.aspx?url=' + lblAttachmentChassis + '&type=Chassis', '', 500, 760, DealerSelection);
            return false;
        }


        function imageOnEngineClick(evt) {
            var photoViewEngine = document.getElementById('photoViewEngine');
            var imageSource = photoViewEngine.getAttribute('src');
            var lblAttachmentEngine = document.getElementById('lblAttachmentEngine').innerHTML;


            if (lblAttachmentEngine == null || lblAttachmentEngine == '' || lblAttachmentEngine == undefined) {

                return false;
            }


            showPopUp('../PopUp/PopUpImage.aspx?url=' + lblAttachmentEngine + '&type=Chassis', '', 500, 760, DealerSelection);
            return false;
        }
    </script>
    
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">Sales&nbsp;- Upload No Rangka</td>
            </tr>
            <tr>
                 <td style="border-bottom: dotted thin; height: 1px" colspan="6">
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        
                        <tr valign="top">
                            <td class="titleField" style="" width="24%">Kode Dealer</td>
                            <td style="" width="1%">:</td>
                            <td style="" width="25%">
                                <asp:Label runat="server" ID="lblDealerCode"></asp:Label>

                            </td>
                            <td class="titleField" style="" width="24%">Nama Dealer</td>
                            <td style="" width="1%">:</td>
                            <td style="" width="25%">
                                <asp:Label runat="server" ID="lblDealerName"></asp:Label></td>
                        </tr>

                          <tr valign="top">
                            <td class="titleField" style="" width="24%">No Rangka</td>
                            <td style="" width="1%">:</td>
                            <td style="" width="25%">
                                <asp:Label runat="server" ID="lblChassisNumber"></asp:Label>

                            </td>
                            <td class="titleField" style="" width="24%">No Mesin</td>
                            <td style="" width="1%">:</td>
                            <td style="" width="25%">
                                <asp:Label runat="server" ID="lblEngineNumber"></asp:Label></td>
                        </tr>


                           <tr valign="top">
                            <td class="titleField" style="" width="24%">No SPK</td>
                            <td style="" width="1%">:</td>
                            <td style="" width="25%">
                                <asp:Label runat="server" ID="lblSPKNumber"></asp:Label>

                            </td>
                            <td class="titleField" style="" width="24%">Nama Konsumen</td>
                            <td style="" width="1%">:</td>
                            <td style="" width="25%">
                                <asp:Label runat="server" ID="lblEndCustomerName"></asp:Label></td>
                        </tr>

                         <tr>
                <td style="border-bottom: dotted thin; height: 1px" colspan="6">
                      <asp:HiddenField ID="hdnProgress" runat="server"  value="0"/>
                </td>
                             
            </tr>
                        <tr valign="top">
                            <td class="titleField" style="height: 20px" width="24%">Pilih Lokasi File Rangka<br /> <span style="font-weight:lighter; font-style:italic;"> (Max 200KB)</span></td>
                            <td style="height: 20px" width="1%">:</td>
                            <td style="height: 20px" width="25%">
                                
                                   <input id="fileUploadChassis" onkeydown="return false;" style="width: 240px" type="file" name="File1"
                                    runat="server">
                                <br />
                                <asp:Label ID="lblAttachmentChassis" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" style="" width="20%" colspan="3">
                                <asp:Image ID="photoViewChassis" onclick="imageOnChassisClick()" runat="server" Width="300px" Height="150px" CssClass="ShowControl" ImageUrl="../DataFile/PPT/NotFound.png"></asp:Image>

                            </td>
                           
                        </tr>


                        <tr valign="top">
                            <td class="titleField" style="height: 20px" width="24%">Pilih Lokasi File Mesin<br /> <span style="font-weight:lighter; font-style:italic;"> (Max 200KB)</span></td>
                            <td style="height: 20px" width="1%">:</td>
                            <td style="height: 20px" width="25%">

                                   <input id="fileUploadEngine" onkeydown="return false;" style="width: 240px" type="file" name="File2"
                                    runat="server">
                                <br />
                                <asp:Label ID="lblAttachmentEngine" runat="server"></asp:Label>
                            </td>
                            <td class="titleField" style="" width="20%" colspan="3">
                                <asp:Image ID="photoViewEngine" onclick="imageOnEngineClick()" runat="server" Width="300px" Height="150px" CssClass="ShowControl" ImageUrl="../DataFile/PPT/NotFound.png"></asp:Image>

                            </td>
                           
                        </tr>


                        <tr>
                            <td class="titleField" style="height: 20px" width="24%">&nbsp;</td>
                            <td style="height: 20px" width="1%">&nbsp;</td>
                            <td style="height: 20px" width="25%">&nbsp;</td>
                            <td class="titleField" style="height: 20px" width="20%">&nbsp;</td>
                            <td style="height: 20px" width="1%">&nbsp;</td>
                            <td style="height: 20px" width="29%">&nbsp;</td>
                        </tr>


                        <tr runat="server" id="trDiscountProposalDetailOwnership" visible="false">
                            <td class="titleField" colspan="6">
                                <span class="titlePanel">
                                    <b> </b>
                                </span>
                                <br />



                            </td>
                        </tr>

 

                      

                        <tr>
                            <td class="titleField" style="height: 11px" colspan="6">
                        </tr>

                        <tr>
                            <td class="titleField" style="height: 11px" colspan="6">
                                <input type="button" id="htmButtonSave" runat="server" value="Smmpan" onclick="UploadFile()" style="display:none" />
                                <asp:Button ID="btnSave" runat="server" Width="78px" Text="Simpan" Visible="True" CausesValidation="false"></asp:Button>

                                <asp:Button ID="btnBack" runat="server" Width="60px" Text="Kembali" CausesValidation="False"></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

           <!-- Modal -->
        <div id="pleaseWaitDialog" class="modal" style="display: none" data-backdrop="static">
            <div class="center  ">
                <img alt="" src="../Images/ajax-loader.gif" class="img center-block" style="position: fixed; top: 50%; left: 50%; margin-top: -50px; margin-left: -100px;" />

            </div>
        </div>
        <!--end: Modal -->

    </form>
        <script type="text/javascript" src="../WebResources/ocr/bootstrap.min.js"></script>
    <script language="javascript" type="text/jscript">



        try {
            showLoadingPage();
        } catch (e) {

        }

       
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

        try {
            addEventListeners();
        } catch (e) {

        }

        function UploadFile() {
            try {
                alert('aa');
                var btnSave = document.getElementById('btnSave');
                var hdnProgress = document.getElementById('hdnProgress');
                hdnProgress.value = "1";

                showLoadingPage();

                if (btnSave) btnSave.click();
                return false;
            } catch (e) {
                alert(e.message);
            }


        }
    </script>
</body>
</html>
