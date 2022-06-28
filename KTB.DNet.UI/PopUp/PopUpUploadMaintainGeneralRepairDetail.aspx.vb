#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.AfterSales
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.WebCC
Imports MMKSI.DNetUpload.Utility
Imports System.Collections.Generic
Imports OfficeOpenXml
Imports GlobalExtensions
Imports KTB.DNet.BusinessValidation
Imports System.Linq
Imports System.Security.Principal


#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Reflection

#End Region

Public Class PopUpUploadMaintainGeneralRepairDetail
    Inherits System.Web.UI.Page
#Region " Private fields "
    Private sessHelper As SessionHelper = New SessionHelper
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private m_bInputPrivilege As Boolean = False
    Private stdFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private vechileModelFacade As VechileModelFacade = New VechileModelFacade(User)
    Private fSKindFacade As FSKindFacade = New FSKindFacade(User)
    Private pMKindFacade As PMKindFacade = New PMKindFacade(User)
    Private recallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)
    Private serviceBookingFacade As ServiceBookingFacade = New ServiceBookingFacade(User)
    Private freeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
    Private recallServiceFacade As RecallServiceFacade = New RecallServiceFacade(User)
    Private pmHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
    Private vechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
    Private chassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
    Private dealerFacade As DealerFacade = New DealerFacade(User)
    Private stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
    Private assistServiceTypeFacade As AssistServiceTypeFacade = New AssistServiceTypeFacade(User)
    Dim isDealerDMS As Boolean = False
    Private isDealerPiloting As Boolean = False
    Private _arlServStdTime As ArrayList = New ArrayList
    Private arrSST As ArrayList = New ArrayList
    Private objSrvST As New ServiceStandardTime
#End Region
#Region "Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim id As String = Request.QueryString("id")
        Dim IdGRGabor As Integer = CType(id, Integer)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRPartDetail), "ServiceTemplateGRLaborID", MatchType.Exact, IdGRGabor))
        Dim arrGRGaborPartDetail As ArrayList = New ServiceTemplateGRPartDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
        Dim arlDetails As New ArrayList
        For Each itemDetail As ServiceTemplateGRPartDetail In arrGRGaborPartDetail
            Dim dataBindGRGaborPartDetail As New GRGaborPartDetail
            dataBindGRGaborPartDetail.ServiceTemplateGRLaborID = itemDetail.ServiceTemplateGRLaborID
            dataBindGRGaborPartDetail.SparePartMasterID = itemDetail.SparePartMasterID
            criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartMaster), "ID", MatchType.Exact, itemDetail.SparePartMasterID))
            Dim arrsparePart As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If arrsparePart.Count > 0 Then
                Dim sparePart As SparePartMaster = arrsparePart.Item(0)
                dataBindGRGaborPartDetail.KodeSparePart = sparePart.PartNumber
                dataBindGRGaborPartDetail.NamaSparePart = sparePart.PartName
                dataBindGRGaborPartDetail.HargaSatuan = sparePart.RetalPrice
            End If
            dataBindGRGaborPartDetail.JumlahUnit = itemDetail.PartQuantity
            arlDetails.Add(dataBindGRGaborPartDetail)
        Next

        dtgServiceStandardTime.DataSource = arlDetails
        dtgServiceStandardTime.DataBind()

    End Sub

    

    Private Sub dtgServiceStandardTime_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgServiceStandardTime.PageIndexChanged
        '-- Change datagrid page

        dtgServiceStandardTime.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)

    End Sub
    Private Sub dtgServiceStandardTime_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceStandardTime.ItemDataBound
        
    End Sub

    
#End Region

#Region "Custom Method"
   
    Private Function SortArraylist(ByVal ArlToSort As ArrayList, ByVal ObjType As Type, ByVal SortColumn As String, ByVal SortDirection As Sort.SortDirection) As ArrayList

        Dim isDeepSort As Boolean = (SortColumn.IndexOf(".") <> -1)

        Dim i As Integer
        Dim x, y As Object
        Dim currentValue, prevValue As Object
        For i = 0 To ArlToSort.Count - 1
            If i >= 1 Then
                If isDeepSort Then 'Only for 2 level max
                    Dim Properties() As String = SortColumn.Split((".").ToCharArray())
                    Dim dummyType As Type = ObjType
                    Dim currentDummyObject As Object
                    Dim prevDummyObject As Object

                    For z As Integer = 0 To Properties.Length - 2
                        currentDummyObject = dummyType.GetProperty(Properties(z)).GetValue(ArlToSort(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                        prevDummyObject = dummyType.GetProperty(Properties(z)).GetValue(ArlToSort(i - 1), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)

                        dummyType = dummyType.GetProperty(Properties(z)).PropertyType
                    Next
                    Dim prop As PropertyInfo
                    prop = dummyType.GetProperty(Properties(Properties.Length - 1))

                    currentValue = dummyType.GetProperty(Properties(Properties.Length - 1)).GetValue(currentDummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                    prevValue = dummyType.GetProperty(Properties(Properties.Length - 1)).GetValue(prevDummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                Else
                    currentValue = ObjType.GetProperty(SortColumn).GetValue(ArlToSort(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                    prevValue = ObjType.GetProperty(SortColumn).GetValue(ArlToSort(i - 1), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                End If

                If SortDirection = Sort.SortDirection.ASC Then
                    If currentValue < prevValue Then
                        x = ArlToSort(i)
                        y = ArlToSort(i - 1)
                        ArlToSort(i) = y
                        ArlToSort(i - 1) = x
                        i = 0
                    End If
                Else
                    If currentValue > prevValue Then
                        x = ArlToSort(i)
                        y = ArlToSort(i - 1)
                        ArlToSort(i) = y
                        ArlToSort(i - 1) = x
                        i = 0
                    End If
                End If
            End If
        Next

        Return ArlToSort

    End Function
    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrServiceStandard As ArrayList = CType(sessHelper.GetSession("AllocationRealTimeService"), ArrayList)
        Dim aStatus As New ArrayList
        If arrServiceStandard.Count <> 0 Then
            ' SortListControl(arrStallMaster, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrServiceStandard, pageIndex, dtgServiceStandardTime.PageSize)
            dtgServiceStandardTime.DataSource = PagedList
            dtgServiceStandardTime.VirtualItemCount = arrServiceStandard.Count()
            dtgServiceStandardTime.DataBind()
        Else
            dtgServiceStandardTime.DataSource = New ArrayList
            dtgServiceStandardTime.VirtualItemCount = 0
            dtgServiceStandardTime.CurrentPageIndex = 0
            dtgServiceStandardTime.DataBind()
        End If
    End Sub

#End Region



    Protected Sub dtgServiceStandardTime_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
 
    End Sub



    Private Class GRGaborPartDetail
        Private _serviceTemplateGRLaborID As Integer
        Public Property ServiceTemplateGRLaborID As Integer
            Get
                Return _serviceTemplateGRLaborID
            End Get
            Set(ByVal value As Integer)
                _serviceTemplateGRLaborID = value
            End Set
        End Property
        Private _sparePartMasterID As Integer
        Public Property SparePartMasterID As Integer
            Get
                Return _sparePartMasterID
            End Get
            Set(ByVal value As Integer)
                _sparePartMasterID = value
            End Set
        End Property
        Private _NamaSparePart As String = String.Empty
        Public Property NamaSparePart() As String
            Get
                Return _NamaSparePart
            End Get
            Set(ByVal Value As String)
                _NamaSparePart = Value
            End Set
        End Property

        Private _KodeSparePart As String = String.Empty
        Public Property KodeSparePart() As String
            Get
                Return _KodeSparePart
            End Get
            Set(ByVal Value As String)
                _KodeSparePart = Value
            End Set
        End Property

        Private _HargaSatuan As Decimal
        Public Property HargaSatuan() As String
            Get
                Return _HargaSatuan
            End Get
            Set(ByVal Value As String)
                _HargaSatuan = Value
            End Set
        End Property

        Private _JumlahUnit As Decimal
        Public Property JumlahUnit() As Decimal
            Get
                Return _JumlahUnit
            End Get
            Set(ByVal Value As Decimal)
                _JumlahUnit = Value
            End Set
        End Property
    End Class

    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        RegisterStartupScript("Close", "<script>onClose();</script>")
    End Sub
End Class
