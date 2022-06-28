#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2007 - 10:49:57 AM
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

    Public Class ClaimHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertClaimHeader"
        Private m_UpdateStatement As String = "up_UpdateClaimHeader"
        Private m_RetrieveStatement As String = "up_RetrieveClaimHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveClaimHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteClaimHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim claimHeader As ClaimHeader = Nothing
            While dr.Read

                claimHeader = Me.CreateObject(dr)

            End While

            Return claimHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim claimHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim claimHeader As ClaimHeader = Me.CreateObject(dr)
                claimHeaderList.Add(claimHeader)
            End While

            Return claimHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimHeader As ClaimHeader = CType(obj, ClaimHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimHeader As ClaimHeader = CType(obj, ClaimHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ClaimNo", DbType.AnsiString, claimHeader.ClaimNo)
            DbCommandWrapper.AddInParameter("@ClaimDate", DbType.DateTime, claimHeader.ClaimDate)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, claimHeader.Description)
            DbCommandWrapper.AddInParameter("@UploadFileName", DbType.AnsiString, claimHeader.UploadFileName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, claimHeader.Status)
            DbCommandWrapper.AddInParameter("@StatusKTB", DbType.Byte, claimHeader.StatusKTB)
            DbCommandWrapper.AddInParameter("@KTBNote", DbType.AnsiString, claimHeader.KTBNote)
            DbCommandWrapper.AddInParameter("@FakturRetur", DbType.AnsiString, claimHeader.FakturRetur)
            DbCommandWrapper.AddInParameter("@FakturReturDate", DbType.DateTime, claimHeader.FakturReturDate)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, claimHeader.DONumber)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, claimHeader.DeliveryDate)
            DbCommandWrapper.AddInParameter("@ReceivedDate", DbType.DateTime, claimHeader.ReceivedDate)
            DbCommandWrapper.AddInParameter("@ReceivedDescription", DbType.AnsiString, claimHeader.ReceivedDescription)

            DbCommandWrapper.AddInParameter("@SORetur", DbType.AnsiString, claimHeader.SORetur)
            DbCommandWrapper.AddInParameter("@SOReturDate", DbType.DateTime, claimHeader.SOReturDate)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, claimHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ReceivedGoodsConditionID", DbType.Int32, Me.GetRefObject(claimHeader.ClaimGoodCondition))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(claimHeader.Dealer))
            DbCommandWrapper.AddInParameter("@SparePartPOStatusID", DbType.Int32, Me.GetRefObject(claimHeader.SparePartPOStatus))
            DbCommandWrapper.AddInParameter("@ClaimReasonHeaderID", DbType.Int32, Me.GetRefObject(claimHeader.ClaimReason))
            DbCommandWrapper.AddInParameter("@ClaimProgressID", DbType.Int32, Me.GetRefObject(claimHeader.ClaimProgress))

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

            Dim claimHeader As ClaimHeader = CType(obj, ClaimHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimHeader.ID)
            DbCommandWrapper.AddInParameter("@ClaimNo", DbType.AnsiString, claimHeader.ClaimNo)
            DbCommandWrapper.AddInParameter("@ClaimDate", DbType.DateTime, claimHeader.ClaimDate)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, claimHeader.Description)
            DbCommandWrapper.AddInParameter("@UploadFileName", DbType.AnsiString, claimHeader.UploadFileName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, claimHeader.Status)
            DbCommandWrapper.AddInParameter("@StatusKTB", DbType.Byte, claimHeader.StatusKTB)
            DbCommandWrapper.AddInParameter("@KTBNote", DbType.AnsiString, claimHeader.KTBNote)
            DbCommandWrapper.AddInParameter("@FakturRetur", DbType.AnsiString, claimHeader.FakturRetur)
            DbCommandWrapper.AddInParameter("@FakturReturDate", DbType.DateTime, claimHeader.FakturReturDate)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, claimHeader.DONumber)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, claimHeader.DeliveryDate)
            DbCommandWrapper.AddInParameter("@ReceivedDate", DbType.DateTime, claimHeader.ReceivedDate)
            DbCommandWrapper.AddInParameter("@ReceivedDescription", DbType.AnsiString, claimHeader.ReceivedDescription)

            DbCommandWrapper.AddInParameter("@SORetur", DbType.AnsiString, claimHeader.SORetur)
            DbCommandWrapper.AddInParameter("@SOReturDate", DbType.DateTime, claimHeader.SOReturDate)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, claimHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ReceivedGoodsConditionID", DbType.Int32, Me.GetRefObject(claimHeader.ClaimGoodCondition))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(claimHeader.Dealer))
            DbCommandWrapper.AddInParameter("@SparePartPOStatusID", DbType.Int32, Me.GetRefObject(claimHeader.SparePartPOStatus))
            DbCommandWrapper.AddInParameter("@ClaimReasonHeaderID", DbType.Int32, Me.GetRefObject(claimHeader.ClaimReason))
            DbCommandWrapper.AddInParameter("@ClaimProgressID", DbType.Int32, Me.GetRefObject(claimHeader.ClaimProgress))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ClaimHeader

            Dim claimHeader As ClaimHeader = New ClaimHeader

            claimHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimNo")) Then claimHeader.ClaimNo = dr("ClaimNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimDate")) Then claimHeader.ClaimDate = CType(dr("ClaimDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then claimHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadFileName")) Then claimHeader.UploadFileName = dr("UploadFileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then claimHeader.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusKTB")) Then claimHeader.StatusKTB = CType(dr("StatusKTB"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("KTBNote")) Then claimHeader.KTBNote = dr("KTBNote").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturRetur")) Then claimHeader.FakturRetur = dr("FakturRetur").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturReturDate")) Then claimHeader.FakturReturDate = CType(dr("FakturReturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then claimHeader.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryDate")) Then claimHeader.DeliveryDate = CType(dr("DeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceivedDate")) Then claimHeader.ReceivedDate = CType(dr("ReceivedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceivedDescription")) Then claimHeader.ReceivedDescription = dr("ReceivedDescription").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("SORetur")) Then claimHeader.SORetur = dr("SORetur").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SOReturDate")) Then claimHeader.SOReturDate = CType(dr("SOReturDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then claimHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then claimHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then claimHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then claimHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then claimHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceivedGoodsConditionID")) Then
                claimHeader.ClaimGoodCondition = New ClaimGoodCondition(CType(dr("ReceivedGoodsConditionID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                claimHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOStatusID")) Then
                claimHeader.SparePartPOStatus = New SparePartPOStatus(CType(dr("SparePartPOStatusID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimReasonHeaderID")) Then
                claimHeader.ClaimReason = New ClaimReason(CType(dr("ClaimReasonHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimProgressID")) Then
                claimHeader.ClaimProgress = New ClaimProgress(CType(dr("ClaimProgressID"), Integer))
            End If

            Return claimHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(ClaimHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ClaimHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ClaimHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

