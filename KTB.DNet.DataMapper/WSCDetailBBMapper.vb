#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCDetailBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 24/10/2005 - 16:24:01
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

    Public Class WSCDetailBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWSCDetailBB"
        Private m_UpdateStatement As String = "up_UpdateWSCDetailBB"
        Private m_RetrieveStatement As String = "up_RetrieveWSCDetailBB"
        Private m_RetrieveListStatement As String = "up_RetrieveWSCDetailBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWSCDetailBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim WSCDetailBB As WSCDetailBB = Nothing
            While dr.Read

                WSCDetailBB = Me.CreateObject(dr)

            End While

            Return WSCDetailBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim WSCDetailBBList As ArrayList = New ArrayList

            While dr.Read
                Dim WSCDetailBB As WSCDetailBB = Me.CreateObject(dr)
                WSCDetailBBList.Add(WSCDetailBB)
            End While

            Return WSCDetailBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WSCDetailBB As WSCDetailBB = CType(obj, WSCDetailBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, WSCDetailBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WSCDetailBB As WSCDetailBB = CType(obj, WSCDetailBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@WSCType", DbType.AnsiString, WSCDetailBB.WSCType)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Decimal, WSCDetailBB.Quantity)
            DbCommandWrapper.AddInParameter("@PartPrice", DbType.Currency, WSCDetailBB.PartPrice)
            DBCommandWrapper.AddInParameter("@MainPart", DbType.Int16, WSCDetailBB.MainPart)
            DBCommandWrapper.AddInParameter("@QuantityReceived", DbType.Decimal, WSCDetailBB.QuantityReceived)
            DBCommandWrapper.AddInParameter("@ReceivedBy", DbType.AnsiString, WSCDetailBB.ReceivedBy)
            DbCommandWrapper.AddInParameter("@ReceivedDate", DbType.DateTime, WSCDetailBB.ReceivedDate)
            DbCommandWrapper.AddInParameter("@PositionCode", DbType.AnsiString, WSCDetailBB.PositionCode)
            DbCommandWrapper.AddInParameter("@WorkCode", DbType.AnsiString, WSCDetailBB.WorkCode)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, WSCDetailBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, WSCDetailBB.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LaborMasterID", DbType.Int32, Me.GetRefObject(WSCDetailBB.LaborMaster))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(WSCDetailBB.SparePartMaster))
            DBCommandWrapper.AddInParameter("@WSCHeaderBBID", DbType.Int32, Me.GetRefObject(WSCDetailBB.WSCHeaderBB))

            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, WSCDetailBB.Status)
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

            Dim WSCDetailBB As WSCDetailBB = CType(obj, WSCDetailBB)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, WSCDetailBB.ID)
            DBCommandWrapper.AddInParameter("@WSCType", DbType.AnsiString, WSCDetailBB.WSCType)
            DBCommandWrapper.AddInParameter("@Quantity", DbType.Decimal, WSCDetailBB.Quantity)
            DBCommandWrapper.AddInParameter("@PartPrice", DbType.Currency, WSCDetailBB.PartPrice)
            DBCommandWrapper.AddInParameter("@MainPart", DbType.Int16, WSCDetailBB.MainPart)
            DBCommandWrapper.AddInParameter("@QuantityReceived", DbType.Decimal, WSCDetailBB.QuantityReceived)
            DBCommandWrapper.AddInParameter("@ReceivedBy", DbType.AnsiString, WSCDetailBB.ReceivedBy)
            DbCommandWrapper.AddInParameter("@ReceivedDate", DbType.DateTime, WSCDetailBB.ReceivedDate)
            DbCommandWrapper.AddInParameter("@PositionCode", DbType.AnsiString, WSCDetailBB.PositionCode)
            DbCommandWrapper.AddInParameter("@WorkCode", DbType.AnsiString, WSCDetailBB.WorkCode)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, WSCDetailBB.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, WSCDetailBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@LaborMasterID", DbType.Int32, Me.GetRefObject(WSCDetailBB.LaborMaster))
            DBCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(WSCDetailBB.SparePartMaster))
            DBCommandWrapper.AddInParameter("@WSCHeaderBBID", DbType.Int32, Me.GetRefObject(WSCDetailBB.WSCHeaderBB))

            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, WSCDetailBB.Status)
            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WSCDetailBB

            Dim WSCDetailBB As WSCDetailBB = New WSCDetailBB

            WSCDetailBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("WSCType")) Then WSCDetailBB.WSCType = dr("WSCType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then WSCDetailBB.Quantity = CType(dr("Quantity"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartPrice")) Then WSCDetailBB.PartPrice = CType(dr("PartPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("MainPart")) Then WSCDetailBB.MainPart = CType(dr("MainPart"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("QuantityReceived")) Then WSCDetailBB.QuantityReceived = CType(dr("QuantityReceived"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceivedBy")) Then WSCDetailBB.ReceivedBy = dr("ReceivedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceivedDate")) Then WSCDetailBB.ReceivedDate = CType(dr("ReceivedDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("PositionCode")) Then WSCDetailBB.PositionCode = dr("PositionCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkCode")) Then WSCDetailBB.WorkCode = dr("WorkCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then WSCDetailBB.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then WSCDetailBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then WSCDetailBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then WSCDetailBB.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then WSCDetailBB.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then WSCDetailBB.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LaborMasterID")) Then
                WSCDetailBB.LaborMaster = New LaborMaster(CType(dr("LaborMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                WSCDetailBB.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("WSCHeaderBBID")) Then
                WSCDetailBB.WSCHeaderBB = New WSCHeaderBB(CType(dr("WSCHeaderBBID"), Integer))
            End If

            Return WSCDetailBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(WSCDetailBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WSCDetailBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WSCDetailBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

