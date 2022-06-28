
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PPQtyDeleteHelper Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2009 - 9:49:55 AM
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

    Public Class PPQtyDeleteHelperMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPPQtyDeleteHelper"
        Private m_UpdateStatement As String = "up_UpdatePPQtyDeleteHelper"
        Private m_RetrieveStatement As String = "up_RetrievePPQtyDeleteHelper"
        Private m_RetrieveListStatement As String = "up_RetrievePPQtyDeleteHelperList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePPQtyDeleteHelper"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pPQtyDeleteHelper As PPQtyDeleteHelper = Nothing
            While dr.Read

                pPQtyDeleteHelper = Me.CreateObject(dr)

            End While

            Return pPQtyDeleteHelper

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pPQtyDeleteHelperList As ArrayList = New ArrayList

            While dr.Read
                Dim pPQtyDeleteHelper As PPQtyDeleteHelper = Me.CreateObject(dr)
                pPQtyDeleteHelperList.Add(pPQtyDeleteHelper)
            End While

            Return pPQtyDeleteHelperList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pPQtyDeleteHelper As PPQtyDeleteHelper = CType(obj, PPQtyDeleteHelper)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pPQtyDeleteHelper.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pPQtyDeleteHelper As PPQtyDeleteHelper = CType(obj, PPQtyDeleteHelper)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PeriodeDate", DbType.Byte, pPQtyDeleteHelper.PeriodeDate)
            DbCommandWrapper.AddInParameter("@PeriodeMonth", DbType.Byte, pPQtyDeleteHelper.PeriodeMonth)
            DbCommandWrapper.AddInParameter("@PeriodeYear", DbType.Int16, pPQtyDeleteHelper.PeriodeYear)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, pPQtyDeleteHelper.ProductionYear)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, pPQtyDeleteHelper.MaterialNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pPQtyDeleteHelper.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pPQtyDeleteHelper.LastUpdateBy)
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

            Dim pPQtyDeleteHelper As PPQtyDeleteHelper = CType(obj, PPQtyDeleteHelper)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pPQtyDeleteHelper.ID)
            DbCommandWrapper.AddInParameter("@PeriodeDate", DbType.Byte, pPQtyDeleteHelper.PeriodeDate)
            DbCommandWrapper.AddInParameter("@PeriodeMonth", DbType.Byte, pPQtyDeleteHelper.PeriodeMonth)
            DbCommandWrapper.AddInParameter("@PeriodeYear", DbType.Int16, pPQtyDeleteHelper.PeriodeYear)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, pPQtyDeleteHelper.ProductionYear)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, pPQtyDeleteHelper.MaterialNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pPQtyDeleteHelper.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pPQtyDeleteHelper.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PPQtyDeleteHelper

            Dim pPQtyDeleteHelper As PPQtyDeleteHelper = New PPQtyDeleteHelper

            pPQtyDeleteHelper.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeDate")) Then pPQtyDeleteHelper.PeriodeDate = CType(dr("PeriodeDate"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeMonth")) Then pPQtyDeleteHelper.PeriodeMonth = CType(dr("PeriodeMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeYear")) Then pPQtyDeleteHelper.PeriodeYear = CType(dr("PeriodeYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then pPQtyDeleteHelper.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialNumber")) Then pPQtyDeleteHelper.MaterialNumber = dr("MaterialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pPQtyDeleteHelper.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pPQtyDeleteHelper.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pPQtyDeleteHelper.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pPQtyDeleteHelper.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pPQtyDeleteHelper.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return pPQtyDeleteHelper

        End Function

        Private Sub SetTableName()

            If Not (GetType(PPQtyDeleteHelper) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PPQtyDeleteHelper), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PPQtyDeleteHelper).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

