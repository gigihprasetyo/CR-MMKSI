
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 9/5/2013 - 10:06:27 AM
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

    Public Class WSCDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWSCDetail"
        Private m_UpdateStatement As String = "up_UpdateWSCDetail"
        Private m_RetrieveStatement As String = "up_RetrieveWSCDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveWSCDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWSCDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim wSCDetail As WSCDetail = Nothing
            While dr.Read

                wSCDetail = Me.CreateObject(dr)

            End While

            Return wSCDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim wSCDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim wSCDetail As WSCDetail = Me.CreateObject(dr)
                wSCDetailList.Add(wSCDetail)
            End While

            Return wSCDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wSCDetail As WSCDetail = CType(obj, WSCDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wSCDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wSCDetail As WSCDetail = CType(obj, WSCDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@WSCType", DbType.AnsiString, wSCDetail.WSCType)
            DBCommandWrapper.AddInParameter("@Quantity", DbType.Decimal, wSCDetail.Quantity)
            DBCommandWrapper.AddInParameter("@PartPrice", DbType.Currency, wSCDetail.PartPrice)
            DBCommandWrapper.AddInParameter("@MainPart", DbType.Int16, wSCDetail.MainPart)
            DBCommandWrapper.AddInParameter("@QuantityReceived", DbType.Decimal, wSCDetail.QuantityReceived)
            DBCommandWrapper.AddInParameter("@ReceivedBy", DbType.AnsiString, wSCDetail.ReceivedBy)
            DbCommandWrapper.AddInParameter("@ReceivedDate", DbType.DateTime, wSCDetail.ReceivedDate)

            DbCommandWrapper.AddInParameter("@PositionCode", DbType.AnsiString, wSCDetail.PositionCode)
            DbCommandWrapper.AddInParameter("@WorkCode", DbType.AnsiString, wSCDetail.WorkCode)

            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wSCDetail.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, wSCDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@LaborMasterID", DbType.Int32, Me.GetRefObject(wSCDetail.LaborMaster))
            DBCommandWrapper.AddInParameter("@WSCHeaderID", DbType.Int32, Me.GetRefObject(wSCDetail.WSCHeader))
            DBCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(wSCDetail.SparePartMaster))

            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, wSCDetail.Status)
            DbCommandWrapper.AddInParameter("@FakturNumber", DbType.AnsiString, wSCDetail.FakturNumber)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wSCDetail As WSCDetail = CType(obj, WSCDetail)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, wSCDetail.ID)
            DBCommandWrapper.AddInParameter("@WSCType", DbType.AnsiString, wSCDetail.WSCType)
            DBCommandWrapper.AddInParameter("@Quantity", DbType.Decimal, wSCDetail.Quantity)
            DBCommandWrapper.AddInParameter("@PartPrice", DbType.Currency, wSCDetail.PartPrice)
            DBCommandWrapper.AddInParameter("@MainPart", DbType.Int16, wSCDetail.MainPart)
            DBCommandWrapper.AddInParameter("@QuantityReceived", DbType.Decimal, wSCDetail.QuantityReceived)
            DBCommandWrapper.AddInParameter("@ReceivedBy", DbType.AnsiString, wSCDetail.ReceivedBy)
            DbCommandWrapper.AddInParameter("@ReceivedDate", DbType.DateTime, wSCDetail.ReceivedDate)

            DbCommandWrapper.AddInParameter("@PositionCode", DbType.AnsiString, wSCDetail.PositionCode)
            DbCommandWrapper.AddInParameter("@WorkCode", DbType.AnsiString, wSCDetail.WorkCode)

            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wSCDetail.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, wSCDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@LaborMasterID", DbType.Int32, Me.GetRefObject(wSCDetail.LaborMaster))
            DBCommandWrapper.AddInParameter("@WSCHeaderID", DbType.Int32, Me.GetRefObject(wSCDetail.WSCHeader))
            DBCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(wSCDetail.SparePartMaster))

            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, wSCDetail.Status)
            DbCommandWrapper.AddInParameter("@FakturNumber", DbType.AnsiString, wSCDetail.FakturNumber)
            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WSCDetail

            Dim wSCDetail As WSCDetail = New WSCDetail

            wSCDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("WSCType")) Then wSCDetail.WSCType = dr("WSCType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then wSCDetail.Quantity = CType(dr("Quantity"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartPrice")) Then wSCDetail.PartPrice = CType(dr("PartPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("MainPart")) Then wSCDetail.MainPart = CType(dr("MainPart"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("QuantityReceived")) Then wSCDetail.QuantityReceived = CType(dr("QuantityReceived"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceivedBy")) Then wSCDetail.ReceivedBy = dr("ReceivedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceivedDate")) Then wSCDetail.ReceivedDate = CType(dr("ReceivedDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("PositionCode")) Then wSCDetail.PositionCode = dr("PositionCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkCode")) Then wSCDetail.WorkCode = dr("WorkCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then wSCDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then wSCDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then wSCDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then wSCDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then wSCDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then wSCDetail.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturNumber")) Then wSCDetail.FakturNumber = dr("FakturNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LaborMasterID")) Then
                wSCDetail.LaborMaster = New LaborMaster(CType(dr("LaborMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("WSCHeaderID")) Then
                wSCDetail.WSCHeader = New WSCHeader(CType(dr("WSCHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                wSCDetail.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If

            Return wSCDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(WSCDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WSCDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WSCDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

