#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PPQty Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/11/2005 - 11:50:35
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

    Public Class PPQtyMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPPQty"
        Private m_UpdateStatement As String = "up_UpdatePPQty"
        Private m_RetrieveStatement As String = "up_RetrievePPQty"
        Private m_RetrieveListStatement As String = "up_RetrievePPQtyList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePPQty"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pPQty As PPQty = Nothing
            While dr.Read

                pPQty = Me.CreateObject(dr)

            End While

            Return pPQty

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pPQtyList As ArrayList = New ArrayList

            While dr.Read
                Dim pPQty As PPQty = Me.CreateObject(dr)
                pPQtyList.Add(pPQty)
            End While

            Return pPQtyList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pPQty As PPQty = CType(obj, PPQty)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pPQty.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pPQty As PPQty = CType(obj, PPQty)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PeriodeDate", DbType.Int16, pPQty.PeriodeDate)
            DbCommandWrapper.AddInParameter("@PeriodeMonth", DbType.Int16, pPQty.PeriodeMonth)
            DbCommandWrapper.AddInParameter("@PeriodeYear", DbType.Int16, pPQty.PeriodeYear)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, pPQty.ProductionYear)
            DbCommandWrapper.AddInParameter("@AllocationQty", DbType.Int32, pPQty.AllocationQty)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, pPQty.DealerCode)
            DbCommandWrapper.AddInParameter("@UnAllocatedQty", DbType.Int32, pPQty.UnAllocatedQty)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, pPQty.MaterialNumber)
            DBCommandWrapper.AddInParameter("@ValidatedTime", DbType.DateTime, pPQty.ValidatedTime)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pPQty.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pPQty.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim pPQty As PPQty = CType(obj, PPQty)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pPQty.ID)
            DbCommandWrapper.AddInParameter("@PeriodeDate", DbType.Int16, pPQty.PeriodeDate)
            DbCommandWrapper.AddInParameter("@PeriodeMonth", DbType.Int16, pPQty.PeriodeMonth)
            DbCommandWrapper.AddInParameter("@PeriodeYear", DbType.Int16, pPQty.PeriodeYear)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, pPQty.ProductionYear)
            DbCommandWrapper.AddInParameter("@AllocationQty", DbType.Int32, pPQty.AllocationQty)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, pPQty.DealerCode)
            DbCommandWrapper.AddInParameter("@UnAllocatedQty", DbType.Int32, pPQty.UnAllocatedQty)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, pPQty.MaterialNumber)
            DBCommandWrapper.AddInParameter("@ValidatedTime", DbType.DateTime, pPQty.ValidatedTime)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pPQty.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pPQty.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PPQty

            Dim pPQty As PPQty = New PPQty

            pPQty.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeDate")) Then pPQty.PeriodeDate = CType(dr("PeriodeDate"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeMonth")) Then pPQty.PeriodeMonth = CType(dr("PeriodeMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeYear")) Then pPQty.PeriodeYear = CType(dr("PeriodeYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then pPQty.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationQty")) Then pPQty.AllocationQty = CType(dr("AllocationQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then pPQty.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UnAllocatedQty")) Then pPQty.UnAllocatedQty = CType(dr("UnAllocatedQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialNumber")) Then pPQty.MaterialNumber = dr("MaterialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidatedTime")) Then pPQty.ValidatedTime = CType(dr("ValidatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pPQty.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pPQty.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pPQty.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pPQty.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pPQty.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return pPQty

        End Function

        Private Sub SetTableName()

            If Not (GetType(PPQty) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PPQty), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PPQty).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

