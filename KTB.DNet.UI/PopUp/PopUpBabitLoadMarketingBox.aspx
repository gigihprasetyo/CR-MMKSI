<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpBabitLoadMarketingBox.aspx.vb" Inherits=".PopUpBabitLoadMarketingBox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pop Up Load Marketing Box</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <style>
        .col-centered {
            float: none;
            margin: 0 auto;
        }

        #fountainTextG {
            width: 255px;
        }

        .fountainTextG {
            color: #000000;
            font-family: Arial;
            font-size: 40px;
            text-decoration: none;
            font-weight: normal;
            font-style: normal;
            float: left;
            -moz-animation-name: bounce_fountainTextG;
            -moz-animation-duration: 2.47s;
            -moz-animation-iteration-count: infinite;
            -moz-animation-direction: linear;
            -moz-transform: scale(.5);
            -webkit-animation-name: bounce_fountainTextG;
            -webkit-animation-duration: 2.47s;
            -webkit-animation-iteration-count: infinite;
            -webkit-animation-direction: linear;
            -webkit-transform: scale(.5);
            -ms-animation-name: bounce_fountainTextG;
            -ms-animation-duration: 2.47s;
            -ms-animation-iteration-count: infinite;
            -ms-animation-direction: linear;
            -ms-transform: scale(.5);
            -o-animation-name: bounce_fountainTextG;
            -o-animation-duration: 2.47s;
            -o-animation-iteration-count: infinite;
            -o-animation-direction: linear;
            -o-transform: scale(.5);
            animation-name: bounce_fountainTextG;
            animation-duration: 2.47s;
            animation-iteration-count: infinite;
            animation-direction: linear;
            transform: scale(.5);
        }

        #fountainTextG_1 {
            -moz-animation-delay: 0.52s;
            -webkit-animation-delay: 0.52s;
            -ms-animation-delay: 0.52s;
            -o-animation-delay: 0.52s;
            animation-delay: 0.52s;
        }

        #fountainTextG_2 {
            -moz-animation-delay: 0.65s;
            -webkit-animation-delay: 0.65s;
            -ms-animation-delay: 0.65s;
            -o-animation-delay: 0.65s;
            animation-delay: 0.65s;
        }

        #fountainTextG_3 {
            -moz-animation-delay: 0.78s;
            -webkit-animation-delay: 0.78s;
            -ms-animation-delay: 0.78s;
            -o-animation-delay: 0.78s;
            animation-delay: 0.78s;
        }

        #fountainTextG_4 {
            -moz-animation-delay: 0.91s;
            -webkit-animation-delay: 0.91s;
            -ms-animation-delay: 0.91s;
            -o-animation-delay: 0.91s;
            animation-delay: 0.91s;
        }

        #fountainTextG_5 {
            -moz-animation-delay: 1.04s;
            -webkit-animation-delay: 1.04s;
            -ms-animation-delay: 1.04s;
            -o-animation-delay: 1.04s;
            animation-delay: 1.04s;
        }

        #fountainTextG_6 {
            -moz-animation-delay: 1.17s;
            -webkit-animation-delay: 1.17s;
            -ms-animation-delay: 1.17s;
            -o-animation-delay: 1.17s;
            animation-delay: 1.17s;
        }

        #fountainTextG_7 {
            -moz-animation-delay: 1.3s;
            -webkit-animation-delay: 1.3s;
            -ms-animation-delay: 1.3s;
            -o-animation-delay: 1.3s;
            animation-delay: 1.3s;
        }

        #fountainTextG_8 {
            -moz-animation-delay: 1.43s;
            -webkit-animation-delay: 1.43s;
            -ms-animation-delay: 1.43s;
            -o-animation-delay: 1.43s;
            animation-delay: 1.43s;
        }

        #fountainTextG_9 {
            -moz-animation-delay: 1.56s;
            -webkit-animation-delay: 1.56s;
            -ms-animation-delay: 1.56s;
            -o-animation-delay: 1.56s;
            animation-delay: 1.56s;
        }

        #fountainTextG_10 {
            -moz-animation-delay: 1.69s;
            -webkit-animation-delay: 1.69s;
            -ms-animation-delay: 1.69s;
            -o-animation-delay: 1.69s;
            animation-delay: 1.69s;
        }

        #fountainTextG_11 {
            -moz-animation-delay: 1.82s;
            -webkit-animation-delay: 1.82s;
            -ms-animation-delay: 1.82s;
            -o-animation-delay: 1.82s;
            animation-delay: 1.82s;
        }

        #fountainTextG_12 {
            -moz-animation-delay: 1.95s;
            -webkit-animation-delay: 1.95s;
            -ms-animation-delay: 1.95s;
            -o-animation-delay: 1.95s;
            animation-delay: 1.95s;
        }

        #fountainTextG_13 {
            -moz-animation-delay: 2.08s;
            -webkit-animation-delay: 2.08s;
            -ms-animation-delay: 2.08s;
            -o-animation-delay: 2.08s;
            animation-delay: 2.08s;
        }

        #fountainTextG_14 {
            -moz-animation-delay: 2.21s;
            -webkit-animation-delay: 2.21s;
            -ms-animation-delay: 2.21s;
            -o-animation-delay: 2.21s;
            animation-delay: 2.21s;
        }

        #fountainTextG_15 {
            -moz-animation-delay: 2.34s;
            -webkit-animation-delay: 2.34s;
            -ms-animation-delay: 2.34s;
            -o-animation-delay: 2.34s;
            animation-delay: 2.34s;
        }

        @-moz-keyframes bounce_fountainTextG {
            0% {
                -moz-transform: scale(1);
                color: #000000;
            }

            100% {
                -moz-transform: scale(.5);
                color: #FFFFFF;
            }
        }

        @-webkit-keyframes bounce_fountainTextG {
            0% {
                -webkit-transform: scale(1);
                color: #000000;
            }

            100% {
                -webkit-transform: scale(.5);
                color: #FFFFFF;
            }
        }

        @-ms-keyframes bounce_fountainTextG {
            0% {
                -ms-transform: scale(1);
                color: #000000;
            }

            100% {
                -ms-transform: scale(.5);
                color: #FFFFFF;
            }
        }

        @-o-keyframes bounce_fountainTextG {
            0% {
                -o-transform: scale(1);
                color: #000000;
            }

            100% {
                -o-transform: scale(.5);
                color: #FFFFFF;
            }
        }

        @keyframes bounce_fountainTextG {
            0% {
                transform: scale(1);
                color: #000000;
            }

            100% {
                transform: scale(.5);
                color: #FFFFFF;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <div class="modal fade" id="myWaitModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title"></h4>

                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div id="fountainTextG" class="col-centered">
                                    <div id="fountainTextG_1" class="fountainTextG">P</div>
                                    <div id="fountainTextG_2" class="fountainTextG">l</div>
                                    <div id="fountainTextG_3" class="fountainTextG">e</div>
                                    <div id="fountainTextG_4" class="fountainTextG">a</div>
                                    <div id="fountainTextG_5" class="fountainTextG">s</div>
                                    <div id="fountainTextG_6" class="fountainTextG">e</div>
                                    <div id="fountainTextG_7" class="fountainTextG">&nbsp;</div>
                                    <div id="fountainTextG_8" class="fountainTextG">w</div>
                                    <div id="fountainTextG_9" class="fountainTextG">a</div>
                                    <div id="fountainTextG_10" class="fountainTextG">i</div>
                                    <div id="fountainTextG_11" class="fountainTextG">t</div>
                                    <div id="fountainTextG_12" class="fountainTextG">&nbsp;</div>
                                    <div id="fountainTextG_13" class="fountainTextG">.</div>
                                    <div id="fountainTextG_14" class="fountainTextG">.</div>
                                    <div id="fountainTextG_15" class="fountainTextG">.</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer"></div>
                </div>
            </div>
        </div>
        <button type="button" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#myWaitModal">Launch demo modal</button>
    </form>
</body>
</html>
