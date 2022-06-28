Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.WebApi.Models
Imports KTB.DNet.Domain.Search
Imports System.Text.RegularExpressions
Imports System
Imports System.Text
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Collections.Generic

Public Class BabitInterfaceHelper
    Inherits System.Web.UI.Page

    Public Function RetrieveBabitMarbox(dealercode As String, babittype As String) As Boolean

        Try

            KTB.DNet.WebApi.Models.BabitInterFace2.GetFilesFolders(dealercode, babittype)

        Catch ex As Exception

            Dim data = ex.Message.ToString()
        End Try


        Return True
    End Function


End Class
