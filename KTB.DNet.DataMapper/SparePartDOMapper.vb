
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDO Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2016 - 11:38:39 AM
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

    Public Class SparePartDOMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartDO"
        Private m_UpdateStatement As String = "up_UpdateSparePartDO"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartDO"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartDOList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartDO"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartDO As SparePartDO = Nothing
            While dr.Read

                sparePartDO = Me.CreateObject(dr)

            End While

            Return sparePartDO

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartDOList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartDO As SparePartDO = Me.CreateObject(dr)
                sparePartDOList.Add(sparePartDO)
            End While

            Return sparePartDOList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDO As SparePartDO = CType(obj, SparePartDO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDO.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDO As SparePartDO = CType(obj, SparePartDO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, sparePartDO.DONumber)
            DbCommandWrapper.AddInParameter("@DoDate", DbType.DateTime, sparePartDO.DoDate)
            DbCommandWrapper.AddInParameter("@EstmationDeliveryDate", DbType.DateTime, sparePartDO.EstmationDeliveryDate)
            DbCommandWrapper.AddInParameter("@PickingDate", DbType.DateTime, sparePartDO.PickingDate)
            DbCommandWrapper.AddInParameter("@PackingDate", DbType.DateTime, sparePartDO.PackingDate)
            DbCommandWrapper.AddInParameter("@GoodIssueDate", DbType.DateTime, sparePartDO.GoodIssueDate)
            DbCommandWrapper.AddInParameter("@PaymentDate", DbType.DateTime, sparePartDO.PaymentDate)
            DbCommandWrapper.AddInParameter("@ReadyForDeliveryDate", DbType.DateTime, sparePartDO.ReadyForDeliveryDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDO.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartDO.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sparePartDO.Dealer))

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

            Dim sparePartDO As SparePartDO = CType(obj, SparePartDO)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDO.ID)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, sparePartDO.DONumber)
            DbCommandWrapper.AddInParameter("@DoDate", DbType.DateTime, sparePartDO.DoDate)
            DbCommandWrapper.AddInParameter("@EstmationDeliveryDate", DbType.DateTime, sparePartDO.EstmationDeliveryDate)
            DbCommandWrapper.AddInParameter("@PickingDate", DbType.DateTime, sparePartDO.PickingDate)
            DbCommandWrapper.AddInParameter("@PackingDate", DbType.DateTime, sparePartDO.PackingDate)
            DbCommandWrapper.AddInParameter("@GoodIssueDate", DbType.DateTime, sparePartDO.GoodIssueDate)
            DbCommandWrapper.AddInParameter("@PaymentDate", DbType.DateTime, sparePartDO.PaymentDate)
            DbCommandWrapper.AddInParameter("@ReadyForDeliveryDate", DbType.DateTime, sparePartDO.ReadyForDeliveryDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDO.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartDO.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sparePartDO.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartDO

            Dim sparePartDO As SparePartDO = New SparePartDO

            sparePartDO.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then sparePartDO.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DoDate")) Then sparePartDO.DoDate = CType(dr("DoDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EstmationDeliveryDate")) Then sparePartDO.EstmationDeliveryDate = CType(dr("EstmationDeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PickingDate")) Then sparePartDO.PickingDate = CType(dr("PickingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PackingDate")) Then sparePartDO.PackingDate = CType(dr("PackingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GoodIssueDate")) Then sparePartDO.GoodIssueDate = CType(dr("GoodIssueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentDate")) Then sparePartDO.PaymentDate = CType(dr("PaymentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReadyForDeliveryDate")) Then sparePartDO.ReadyForDeliveryDate = CType(dr("ReadyForDeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartDO.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartDO.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartDO.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartDO.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartDO.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sparePartDO.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return sparePartDO

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartDO) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartDO), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartDO).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

