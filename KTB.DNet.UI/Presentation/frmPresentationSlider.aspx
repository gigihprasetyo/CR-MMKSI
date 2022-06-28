<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmPresentationSlider.aspx.vb" Inherits=".frmPresentationSlider" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title></title>
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Pragma-directive: no-cache" />
    <meta http-equiv="Cache-directive: no-cache" />
    <meta http-equiv="imagetoolbar" content="no" />
    <%--     <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />--%>
    <%--    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>--%>

    <script src="../WebResources/jquery-1.10.2.js"></script>
    <%-- <script src="../WebResources/jquery-ui.js"></script>--%>
    <link href="../WebResources/css/jquery-ui.css" rel="stylesheet" />
  <%--  <link href="../WebResources/css/w3.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        var isHideLayer = true;

        function curIdx() {
            return $('#idx').val();
        }
        function next() {
            show(1 * curIdx() + 1);
            //$('#watermark').css('opacity', '0.80');
        }
        function prev() {
            show(1 * curIdx() - 1);
            //$('#watermark').css('opacity', '0');
        }

        function show(i) {

            var min = 1; max = 19;
            max = parseInt(document.getElementById("<%=hdnNumber.ClientID%>").value);
            // alert("max : "+max);
            var file;
            var d = new Date();
            if (i < min) i = min;
            if (i > max) i = max;
            $('#idx').val(i);
            var hidSourceID = document.getElementById("<%= hdnKey.ClientID%>").value;
            //file = '../DataTemp/images/Slide' + i + '.PNG';
            //file = '../DataTemp/' + hidSourceID + '/Slide' + i + '.PNG?rand=' + d.getTime();
             $('#slide-title').html("Slide " + i).trigger('create');

             var url = 'frmPresentationSlider.aspx?key=' + hidSourceID + '&page=' + i + '&rand=' + +d.getTime();
             $('#slide').attr('src', url);
             $('#ddPage').val(i);
            // $('#slide').attr('src', file);
             var leftpx = $('#slide').position().left + $('#slide').width() - $('#channel').width() + 40;
             if (leftpx < 1000)
                 leftpx = 1193;

            $('#channel').css({ left: leftpx });




         }

         $(document).bind('keydown keypress', 'ctrl+s', function () {
             $('#save').click();
             return false;
         });

         $(document).keydown(function (e) {
             if (e.keyCode == 37) {
                 prev();
                 return false;
             }

             if (e.keyCode == 39) {
                 next();
                 return false;
             }

             if (e.keyCode == 27) {
                 window.close();
                 return false;
             }
         });


         function confirmLayer() {

             isHideLayer = true;
             //isHideLayer = confirm("Do you want to hide layer covering the material?");
             if (isHideLayer == false)
                 $('#watermark').css('opacity', '0.60');
             else
                 $('#watermark').css('opacity', '0.0');
         }
         function getval(sel) {
             show(sel.value)
             // alert(sel.value);
         }

         var currentZoom = 1.0;

         function btn_ZoomIn() {

             $('#divName').animate({ 'zoom': currentZoom += 0.1 }, 'slow');
             $('#divName').css('zoom', currentZoom);
             return false;
         }

         function btn_ZoomOut() {

             $('#divName').animate({ 'zoom': currentZoom -= 0.1 }, 'slow');
             $('#divName').css('zoom', currentZoom);
             return false;
         }


         function btn_ZoomReset() {
             currentZoom = 1.0
             $('#divName').animate({ 'zoom': 1 }, 'slow');
             $('#divName').css('zoom', currentZoom);
             return false;
         }

         $(document).ready(function () {



             show(1);
         });

    </script>
    <style type="text/css">
        .kimg {
    /*width: 100%;
    height: auto;*/
}

    </style>
</head>
<body onload="confirmLayer();" oncontextmenu="return false;">
    <form id="form1" runat="server">

        <asp:HiddenField ID="hdnPath" runat="server" />
        <asp:HiddenField ID="hdnKey" runat="server" />
        <asp:HiddenField ID="hdnNumber" runat="server" />

    </form>
    <div style="display: none;">
        <input type="text" id="idx" value="1" />
    </div>

    <div id="divName" style="zoom: 1;">

        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="center" colspan="2">
                    <table border="0" style="">
                        <tr>
                            <td>
                                <img src="../WebResources/img/prev.png" onclick="prev();" alt="prev" />
                                <img src="../WebResources/img/next.png" onclick="next();" alt="next" />
                            </td>
                            <td>
                                <select id="ddPage" runat="server" onchange="getval(this);"></select>
                            </td>
                            <td>
                                <%--<button id="btn_ZoomIn" style="" onclick="btn_ZoomIn();">Zoom IN</button>

                                   <button id="btn_ZoomOut" style="" onclick="btn_ZoomOut();">Zoom OUT</button>
                                <button id="btn_ZoomReset" style="" onclick="btn_ZoomReset();">Zoom Reset</button>--%>
                            </td>
                           
                        </tr>

                    </table>

                </td>
                <td align="left"></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <div id="slide-title" style="display: none;">
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <img id="slide" style="" alt="" src="" class="kimg" />
                </td>
            </tr>
        </table>
        <div id="watermark" style="width: 90%; height: 100%; top: 90px; text-align: center; position: absolute; topxxx: 230px; left: 50px; background-color: #e9e9e9; opacity: 0.05; -moz-opacity: 0.05; filter: alpha(opacity=5); min-height:800px; min-width:500px;">
            <table width="100%" border="0" style="display: none;">
                <tr>
                    <td align="right" valign="middle">
                        <img src="../Images/logo.png" width="5px" height="5px;" />
                    </td>
                    <td align="center" width="300px" valign="middle">
                        <br />
                        <h3 style="color: red; font-size: 40;">KTB's Property</h3>
                    </td>
                    <td align="left">
                        <img src="../Images/logo.png" width="5px" height="5px;" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="channel" style="display: none; width: 42px; height: 70px; top: 95px; text-align: center; position: absolute; left: 1193px; background-color: Transparent opacity: 1.0; -moz-opacity: 1.0; filter: alpha(opacity=100);">
            <img src="../Images/logo.png" width="40px" height="40px;" />
        </div>
    </div>

</body>
</html>
