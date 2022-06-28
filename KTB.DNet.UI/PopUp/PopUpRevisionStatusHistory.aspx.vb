#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility

#End Region

Public Class PopUpRevisionStatusHistory
    Inherits System.Web.UI.Page

#Region "Private Variables"
    Private ssHelper As SessionHelper = New SessionHelper
    Private arrListStatusChangeHistory As ArrayList
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            BindHeader()

            RetrieveData()
        End If
    End Sub

    Private Sub BindHeader()
        Dim revisionFaktur As RevisionFaktur = New RevisionFaktur
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RegNumber", MatchType.Exact, CType(Request.QueryString("No"), String).ToString))
        Dim objListRevisionFaktur As ArrayList = New RevisionFakturFacade(User).Retrieve(criterias)
        If objListRevisionFaktur.Count > 0 Then
            revisionFaktur = CType(objListRevisionFaktur(0), RevisionFaktur)
            ssHelper.SetSession("RevisionFaktur", revisionFaktur)
        End If

        lblRegNumberValue.Text = revisionFaktur.RegNumber
        lblRevisionTypeValue.Text = New RevisionTypeFacade(User).Retrieve(revisionFaktur.RevisionTypeID).Description
    End Sub

    Private Sub RetrieveData()
        Dim objRevisionFaktur As RevisionFaktur = CType(ssHelper.GetSession("RevisionFaktur"), RevisionFaktur)
        arrListStatusChangeHistory = New ArrayList

        Dim item1 As RevisionStatusHistory = New RevisionStatusHistory
        item1.OldStatus = String.Empty
        item1.NewStatus = ([Enum].Parse(GetType(EnumDNET.enumFakturKendaraanRev), EnumDNET.enumFakturKendaraanRev.Baru, True)).ToString
        item1.ProcessDate = objRevisionFaktur.CreatedTime
        item1.ProcessBy = objRevisionFaktur.CreatedBy
        arrListStatusChangeHistory.Add(item1)

        If objRevisionFaktur.NewValidationBy <> String.Empty Then
            Dim item2 As RevisionStatusHistory = New RevisionStatusHistory
            item2.OldStatus = ([Enum].Parse(GetType(EnumDNET.enumFakturKendaraanRev), EnumDNET.enumFakturKendaraanRev.Baru, True)).ToString
            item2.NewStatus = ([Enum].Parse(GetType(EnumDNET.enumFakturKendaraanRev), EnumDNET.enumFakturKendaraanRev.Validasi, True)).ToString
            item2.ProcessDate = objRevisionFaktur.NewValidationDate
            item2.ProcessBy = objRevisionFaktur.NewValidationBy
            arrListStatusChangeHistory.Add(item2)
        End If

        If objRevisionFaktur.NewConfirmationBy <> String.Empty Then
            Dim item3 As RevisionStatusHistory = New RevisionStatusHistory
            item3.OldStatus = ([Enum].Parse(GetType(EnumDNET.enumFakturKendaraanRev), EnumDNET.enumFakturKendaraanRev.Validasi, True)).ToString
            item3.NewStatus = ([Enum].Parse(GetType(EnumDNET.enumFakturKendaraanRev), EnumDNET.enumFakturKendaraanRev.Konfirmasi, True)).ToString
            item3.ProcessDate = objRevisionFaktur.NewConfirmationDate
            item3.ProcessBy = objRevisionFaktur.NewConfirmationBy
            arrListStatusChangeHistory.Add(item3)
        End If
        BindToGrid()
    End Sub

    Private Sub BindToGrid()
        dtgStatusChangeHistory.DataSource = arrListStatusChangeHistory
        dtgStatusChangeHistory.DataBind()
    End Sub

    Private Class RevisionStatusHistory

        Private _oldStatus As String
        Private _newStatus As String
        Private _processDate As Date
        Private _processBy As String

        Property OldStatus() As String
            Get
                Return _oldStatus
            End Get
            Set(ByVal Value As String)
                _oldStatus = Value
            End Set
        End Property

        Property NewStatus() As String
            Get
                Return _newStatus
            End Get
            Set(ByVal Value As String)
                _newStatus = Value
            End Set
        End Property

        Property ProcessDate() As Date
            Get
                Return _processDate
            End Get
            Set(ByVal Value As Date)
                _processDate = Value
            End Set
        End Property

        Property ProcessBy() As String
            Get
                Return _processBy
            End Get
            Set(ByVal Value As String)
                _processBy = Value
            End Set
        End Property
    End Class
End Class