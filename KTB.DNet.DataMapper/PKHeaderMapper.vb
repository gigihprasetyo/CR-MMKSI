
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PKHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 6/5/2009 - 3:32:27 PM
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class PKHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPKHeader"
        Private m_UpdateStatement As String = "up_UpdatePKHeader"
        Private m_RetrieveStatement As String = "up_RetrievePKHeader"
        Private m_RetrieveListStatement As String = "up_RetrievePKHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePKHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pKHeader As PKHeader = Nothing
            While dr.Read

                pKHeader = Me.CreateObject(dr)

            End While

            Return pKHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pKHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim pKHeader As PKHeader = Me.CreateObject(dr)
                pKHeaderList.Add(pKHeader)
            End While

            Return pKHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKHeader As PKHeader = CType(obj, PKHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pKHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKHeader As PKHeader = CType(obj, PKHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PKNumber", DbType.AnsiString, pKHeader.PKNumber)
            DbCommandWrapper.AddInParameter("@HeadPKNumber", DbType.Int32, pKHeader.HeadPKNumber)
            DbCommandWrapper.AddInParameter("@PKDate", DbType.DateTime, pKHeader.PKDate)
            DbCommandWrapper.AddInParameter("@PKType", DbType.AnsiStringFixedLength, pKHeader.PKType)
            DbCommandWrapper.AddInParameter("@PKStatus", DbType.AnsiString, pKHeader.PKStatus)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.Byte, pKHeader.OrderType)
            DbCommandWrapper.AddInParameter("@Purpose", DbType.Byte, pKHeader.Purpose)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, pKHeader.ProjectName)
            DbCommandWrapper.AddInParameter("@ProjectDetail", DbType.AnsiString, pKHeader.ProjectDetail)
            DbCommandWrapper.AddInParameter("@DealerPKNumber", DbType.AnsiString, pKHeader.DealerPKNumber)
            DbCommandWrapper.AddInParameter("@RequestPeriodeDay", DbType.Byte, pKHeader.RequestPeriodeDay)
            DbCommandWrapper.AddInParameter("@RequestPeriodeMonth", DbType.Byte, pKHeader.RequestPeriodeMonth)
            DbCommandWrapper.AddInParameter("@RequestPeriodeYear", DbType.Int16, pKHeader.RequestPeriodeYear)
            DbCommandWrapper.AddInParameter("@PricingPeriodeDay", DbType.Byte, pKHeader.PricingPeriodeDay)
            DbCommandWrapper.AddInParameter("@PricingPeriodeMonth", DbType.Byte, pKHeader.PricingPeriodeMonth)
            DbCommandWrapper.AddInParameter("@PricingPeriodeYear", DbType.Int16, pKHeader.PricingPeriodeYear)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, pKHeader.ProductionYear)
            DbCommandWrapper.AddInParameter("@KTBResponse", DbType.AnsiString, pKHeader.KTBResponse)
            DbCommandWrapper.AddInParameter("@ResponseBy", DbType.AnsiString, pKHeader.ResponseBy)
            DbCommandWrapper.AddInParameter("@ResponseTime", DbType.DateTime, pKHeader.ResponseTime)
            DbCommandWrapper.AddInParameter("@AgreeBy", DbType.AnsiString, pKHeader.AgreeBy)
            DbCommandWrapper.AddInParameter("@AgreeTime", DbType.DateTime, pKHeader.AgreeTime)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pKHeader.Description)
            DbCommandWrapper.AddInParameter("@StatusDownload", DbType.Byte, pKHeader.StatusDownload)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pKHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@FreePPh22Indicator", DbType.Byte, pKHeader.FreePPh22Indicator)
            DbCommandWrapper.AddInParameter("@SPLNumber", DbType.AnsiString, pKHeader.SPLNumber)
            DbCommandWrapper.AddInParameter("@MaxTOPDate", DbType.DateTime, pKHeader.MaxTOPDate)
            DbCommandWrapper.AddInParameter("@MaxTopDay", DbType.Int32, pKHeader.MaxTopDay)
            DbCommandWrapper.AddInParameter("@FreeIntIndicator", DbType.Byte, pKHeader.FreeIntIndicator)
            DbCommandWrapper.AddInParameter("@MaxTopIndicator", DbType.Int32, pKHeader.MaxTopIndicator)
            DbCommandWrapper.AddInParameter("@IsAproveRilis", DbType.Byte, pKHeader.IsAproveRilis)
            DbCommandWrapper.AddInParameter("@IsUnlockFreeze", DbType.Byte, pKHeader.IsUnlockFreeze)
            DbCommandWrapper.AddInParameter("@IsFormAConfirmation", DbType.Byte, pKHeader.IsFormAConfirmation)
            DbCommandWrapper.AddInParameter("@JaminanID", DbType.Int32, pKHeader.JaminanID)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pKHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@RejectedReason", DbType.AnsiString, pKHeader.RejectedReason)

            DbCommandWrapper.AddInParameter("@FleetDiscountCode", DbType.AnsiString, pKHeader.FleetDiscountCode)

            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(pKHeader.Category))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(pKHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(pKHeader.DealerBranch))

            DbCommandWrapper.AddInParameter("@EvidencePath", DbType.AnsiString, pKHeader.EvidencePath)
            DbCommandWrapper.AddInParameter("@IsAutoApprovedDealer", DbType.Int16, pKHeader.IsAutoApprovedDealer)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKHeader As PKHeader = CType(obj, PKHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pKHeader.ID)
            DbCommandWrapper.AddInParameter("@PKNumber", DbType.AnsiString, pKHeader.PKNumber)
            DbCommandWrapper.AddInParameter("@HeadPKNumber", DbType.Int32, pKHeader.HeadPKNumber)
            DbCommandWrapper.AddInParameter("@PKDate", DbType.DateTime, pKHeader.PKDate)
            DbCommandWrapper.AddInParameter("@PKType", DbType.AnsiStringFixedLength, pKHeader.PKType)
            DbCommandWrapper.AddInParameter("@PKStatus", DbType.AnsiString, pKHeader.PKStatus)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.Byte, pKHeader.OrderType)
            DbCommandWrapper.AddInParameter("@Purpose", DbType.Byte, pKHeader.Purpose)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, pKHeader.ProjectName)
            DbCommandWrapper.AddInParameter("@ProjectDetail", DbType.AnsiString, pKHeader.ProjectDetail)
            DbCommandWrapper.AddInParameter("@DealerPKNumber", DbType.AnsiString, pKHeader.DealerPKNumber)
            DbCommandWrapper.AddInParameter("@RequestPeriodeDay", DbType.Byte, pKHeader.RequestPeriodeDay)
            DbCommandWrapper.AddInParameter("@RequestPeriodeMonth", DbType.Byte, pKHeader.RequestPeriodeMonth)
            DbCommandWrapper.AddInParameter("@RequestPeriodeYear", DbType.Int16, pKHeader.RequestPeriodeYear)
            DbCommandWrapper.AddInParameter("@PricingPeriodeDay", DbType.Byte, pKHeader.PricingPeriodeDay)
            DbCommandWrapper.AddInParameter("@PricingPeriodeMonth", DbType.Byte, pKHeader.PricingPeriodeMonth)
            DbCommandWrapper.AddInParameter("@PricingPeriodeYear", DbType.Int16, pKHeader.PricingPeriodeYear)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, pKHeader.ProductionYear)
            DbCommandWrapper.AddInParameter("@KTBResponse", DbType.AnsiString, pKHeader.KTBResponse)
            DbCommandWrapper.AddInParameter("@ResponseBy", DbType.AnsiString, pKHeader.ResponseBy)
            DbCommandWrapper.AddInParameter("@ResponseTime", DbType.DateTime, pKHeader.ResponseTime)
            DbCommandWrapper.AddInParameter("@AgreeBy", DbType.AnsiString, pKHeader.AgreeBy)
            DbCommandWrapper.AddInParameter("@AgreeTime", DbType.DateTime, pKHeader.AgreeTime)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pKHeader.Description)
            DbCommandWrapper.AddInParameter("@StatusDownload", DbType.Byte, pKHeader.StatusDownload)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pKHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@FreePPh22Indicator", DbType.Byte, pKHeader.FreePPh22Indicator)
            DbCommandWrapper.AddInParameter("@SPLNumber", DbType.AnsiString, pKHeader.SPLNumber)
            DbCommandWrapper.AddInParameter("@MaxTOPDate", DbType.DateTime, pKHeader.MaxTOPDate)
            DbCommandWrapper.AddInParameter("@MaxTopDay", DbType.Int32, pKHeader.MaxTopDay)
            DbCommandWrapper.AddInParameter("@FreeIntIndicator", DbType.Byte, pKHeader.FreeIntIndicator)
            DbCommandWrapper.AddInParameter("@MaxTopIndicator", DbType.Int32, pKHeader.MaxTopIndicator)
            DbCommandWrapper.AddInParameter("@IsAproveRilis", DbType.Byte, pKHeader.IsAproveRilis)
            DbCommandWrapper.AddInParameter("@IsUnlockFreeze", DbType.Byte, pKHeader.IsUnlockFreeze)
            DbCommandWrapper.AddInParameter("@IsFormAConfirmation", DbType.Byte, pKHeader.IsFormAConfirmation)
            DbCommandWrapper.AddInParameter("@JaminanID", DbType.Int16, pKHeader.JaminanID)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pKHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@RejectedReason", DbType.AnsiString, pKHeader.RejectedReason)

            DbCommandWrapper.AddInParameter("@FleetDiscountCode", DbType.AnsiString, pKHeader.FleetDiscountCode)

            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(pKHeader.Category))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(pKHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(pKHeader.DealerBranch))

            DbCommandWrapper.AddInParameter("@EvidencePath", DbType.AnsiString, pKHeader.EvidencePath)
            DbCommandWrapper.AddInParameter("@IsAutoApprovedDealer", DbType.Int16, pKHeader.IsAutoApprovedDealer)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PKHeader

            Dim pKHeader As PKHeader = New PKHeader

            pKHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PKNumber")) Then pKHeader.PKNumber = dr("PKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HeadPKNumber")) Then pKHeader.HeadPKNumber = CType(dr("HeadPKNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PKDate")) Then pKHeader.PKDate = CType(dr("PKDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PKType")) Then pKHeader.PKType = dr("PKType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PKStatus")) Then pKHeader.PKStatus = dr("PKStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then pKHeader.OrderType = CType(dr("OrderType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Purpose")) Then pKHeader.Purpose = CType(dr("Purpose"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectName")) Then pKHeader.ProjectName = dr("ProjectName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectDetail")) Then pKHeader.ProjectDetail = dr("ProjectDetail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerPKNumber")) Then pKHeader.DealerPKNumber = dr("DealerPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RequestPeriodeDay")) Then pKHeader.RequestPeriodeDay = CType(dr("RequestPeriodeDay"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestPeriodeMonth")) Then pKHeader.RequestPeriodeMonth = CType(dr("RequestPeriodeMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestPeriodeYear")) Then pKHeader.RequestPeriodeYear = CType(dr("RequestPeriodeYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PricingPeriodeDay")) Then pKHeader.PricingPeriodeDay = CType(dr("PricingPeriodeDay"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PricingPeriodeMonth")) Then pKHeader.PricingPeriodeMonth = CType(dr("PricingPeriodeMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PricingPeriodeYear")) Then pKHeader.PricingPeriodeYear = CType(dr("PricingPeriodeYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then pKHeader.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("KTBResponse")) Then pKHeader.KTBResponse = dr("KTBResponse").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseBy")) Then pKHeader.ResponseBy = dr("ResponseBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseTime")) Then pKHeader.ResponseTime = CType(dr("ResponseTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AgreeBy")) Then pKHeader.AgreeBy = dr("AgreeBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AgreeTime")) Then pKHeader.AgreeTime = CType(dr("AgreeTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then pKHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDownload")) Then pKHeader.StatusDownload = CType(dr("StatusDownload"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pKHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FreePPh22Indicator")) Then pKHeader.FreePPh22Indicator = CType(dr("FreePPh22Indicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SPLNumber")) Then pKHeader.SPLNumber = dr("SPLNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDate")) Then pKHeader.MaxTOPDate = CType(dr("MaxTOPDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTopDay")) Then pKHeader.MaxTopDay = CType(dr("MaxTopDay"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FreeIntIndicator")) Then pKHeader.FreeIntIndicator = CType(dr("FreeIntIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTopIndicator")) Then pKHeader.MaxTopIndicator = CType(dr("MaxTopIndicator"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsAproveRilis")) Then pKHeader.IsAproveRilis = CType(dr("IsAproveRilis"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsUnlockFreeze")) Then pKHeader.IsUnlockFreeze = CType(dr("IsUnlockFreeze"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsFormAConfirmation")) Then pKHeader.IsFormAConfirmation = CType(dr("IsFormAConfirmation"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("JaminanID")) Then pKHeader.JaminanID = CType(dr("JaminanID"), Integer)
            'If Not dr.IsDBNull(dr.GetBoolean("JaminanID")) Then
            'End If
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pKHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pKHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pKHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pKHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RejectedReason")) Then pKHeader.RejectedReason = dr("RejectedReason").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("FleetDiscountCode")) Then pKHeader.FleetDiscountCode = dr("FleetDiscountCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                pKHeader.Category = New Category(CType(dr("CategoryID"), Byte))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                pKHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                pKHeader.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("EvidencePath")) Then pKHeader.EvidencePath = dr("EvidencePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsAutoApprovedDealer")) Then pKHeader.IsAutoApprovedDealer = CType(dr("IsAutoApprovedDealer"), Short)

            Return pKHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(PKHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PKHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PKHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

