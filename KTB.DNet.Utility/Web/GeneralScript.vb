#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/11/2005 - 10:29:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Web.UI.Page
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Security.Principal

Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
#End Region

Namespace KTB.DNet.Utility

    Public Class GeneralScript

        Public Shared Sub InitPopUp(ByVal Page As System.Web.UI.Page)
            Page.RegisterClientScriptBlock("ScriptPopUp", "<script language=""JavaScript"" type=""text/javascript"" src=""resources.axd?type=Javascript&name=PopUp"" ></script>")
        End Sub

        Public Shared Function GetPopUpEventReference(ByVal PopUpAddress As String, ByVal Paramater As String, ByVal Height As Integer, ByVal Width As Integer, ByVal CallBackFunction As String) As String
            Dim petik As String = "'"
            Dim sb As New StringBuilder
            Dim str As String = String.Empty

            sb.Append("showPopUp(")
            sb.Append(petik)
            sb.Append(PopUpAddress)
            sb.Append(petik + "," + petik)
            sb.Append(Paramater)
            sb.Append(petik + ",")
            sb.Append(Height)
            sb.Append(",")
            sb.Append(Width)
            sb.Append(",")
            sb.Append(CallBackFunction)
            sb.Append(")")
            str = sb.ToString()

            Return str

        End Function

        Public Shared Sub InitCallBack(ByVal Page As System.Web.UI.Page)
            Page.RegisterClientScriptBlock("CallbackScript", "<script language=""JavaScript"" type=""text/javascript"" src=""resources.axd?type=Javascript&name=Callback"" ></script>")

            Page.RegisterStartupScript("CallbackInit", "<script type=""text/javascript""><!--var pageUrl='" + Page.Request.RawUrl + "'; WebForm_InitCallback(document.getElementsByTagName(""Form"")[0]) //--></script>")
        End Sub

        Public Shared Function IsCallback(ByVal Page As System.Web.UI.Page, ByVal EventTarget As String, ByRef CallBackParam As String) As Boolean
            If Not IsNothing(Page.Request.QueryString("__CALLBACKID")) Then
                If (Page.Request.QueryString("__CALLBACKID") = EventTarget) Then
                    Dim param As String = Page.Request.QueryString("__CALLBACKPARAM").ToString
                    CallBackParam = param
                    Return True
                Else
                    Return False
                End If
            ElseIf Not IsNothing(Page.Request.Form("__CALLBACKID")) Then
                If Page.Request.Form("__CALLBACKID") = EventTarget Then
                    Dim param As String = Page.Request.Form("__CALLBACKPARAM").ToString
                    CallBackParam = param
                    Return True
                Else
                    Return False
                End If
            End If

            Return False

        End Function

        Public Shared Function GetCallbackEventReference(ByVal EventTarget As String, ByVal EventArgument As String, ByVal CallbackFunction As String, ByVal ContextObject As String, ByVal ErrorCallbackFunction As String) As String
            Return "javascript:WebForm_DoCallback('" + (IIf(EventTarget = Nothing, "__Page", EventTarget)) + "'," + EventArgument + ", " + CallbackFunction + ", " + ContextObject + ", " + ErrorCallbackFunction + ")"
        End Function

        Public Shared Function GetCallbackEventReference(ByVal EventTarget As String, ByVal EventArgument As String, ByVal CallbackFunction As String, ByVal ContextObject As String, ByVal ErrorCallbackFunction As String, ByVal WithJavascriptIdentifier As Boolean) As String
            Return (IIf(WithJavascriptIdentifier, "javascript:", "")) + "WebForm_DoCallback('" + (IIf(EventTarget = Nothing, "__Page", EventTarget)) + "'," + EventArgument + ", " + CallbackFunction + ", " + ContextObject + ", " + ErrorCallbackFunction + ")"
        End Function

        Public Shared Sub BindPaymentMethod(ByRef ddl As DropDownList, Optional ByVal IsIncludeAll As Boolean = False)
            Dim oPCFac As New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim aPCs As ArrayList = oPCFac.RetrieveActiveList()

            ddl.Items.Clear()
            If IsIncludeAll Then
                ddl.Items.Add(New ListItem("Semua", -1))
            End If
            ddl.Items.Add(New ListItem("Gyro", 0))
            ddl.Items.Add(New ListItem("Transfer", 1))
        End Sub

        Public Shared Sub BindProductCategoryDdl(ByRef ddlProductCategory As DropDownList, Optional ByVal IsIncludeAcc As Boolean = False, Optional ByVal IsIncludeAll As Boolean = False, Optional ByVal companyCode As String = "")
            Dim oPCFac As New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim aPCs As ArrayList = oPCFac.RetrieveActiveList()

            ddlProductCategory.Items.Clear()
            If IsIncludeAll Then
                ddlProductCategory.Items.Add(New ListItem("Semua", -1))
            End If
            If IsIncludeAcc Then
                ddlProductCategory.Items.Add(New ListItem("MMC+MFTBC", 0))
            End If
            For Each oPC As ProductCategory In aPCs
                If oPC.Code.Trim.ToUpper() = "ALL" Then
                Else
                    If companyCode.Trim <> "" Then
                        If oPC.Code.Trim.ToUpper = companyCode.Trim.ToUpper Then
                            ddlProductCategory.Items.Add(New ListItem(oPC.Code, oPC.ID))
                        End If
                    Else
                        ddlProductCategory.Items.Add(New ListItem(oPC.Code, oPC.ID))
                    End If
                End If
            Next
        End Sub

        Public Shared Sub BindProductCategoryDdlDeposit(ByRef ddlProductCategory As DropDownList, Optional ByVal all As Boolean = True, Optional ByVal companyCode As String = "")
            Dim oPCFac As New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim aPCs As ArrayList = oPCFac.RetrieveActiveList()

            ddlProductCategory.Items.Clear()

            For Each oPC As ProductCategory In aPCs
                If oPC.Code.Trim.ToUpper() = "ALL" And Not all Then

                Else
                    If companyCode.Trim <> "" Then
                        If oPC.Code.Trim.ToUpper = companyCode.Trim.ToUpper Then
                            ddlProductCategory.Items.Add(New ListItem(oPC.Code, oPC.ID))
                        End If
                    Else
                        ddlProductCategory.Items.Add(New ListItem(oPC.Code, oPC.ID))
                    End If
                End If
            Next
        End Sub


    End Class

End Namespace