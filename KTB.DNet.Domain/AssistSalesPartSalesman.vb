
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistSparePartSalesman Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/6/2017 - 2:21:46 PM
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

    Public Class AssistSparePartSalesmanMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAssistSparePartSalesman"
        Private m_UpdateStatement As String = "up_UpdateAssistSparePartSalesman"
        Private m_RetrieveStatement As String = "up_RetrieveAssistSparePartSalesman"
        Private m_RetrieveListStatement As String = "up_RetrieveAssistSparePartSalesmanList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAssistSparePartSalesman"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim assistSparePartSalesman As AssistSparePartSalesman = Nothing
            While dr.Read

                assistSparePartSalesman = Me.CreateObject(dr)

            End While

            Return assistSparePartSalesman

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim assistSparePartSalesmanList As ArrayList = New ArrayList

            While dr.Read
                Dim assistSparePartSalesman As AssistSparePartSalesman = Me.CreateObject(dr)
                assistSparePartSalesmanList.Add(assistSparePartSalesman)
            End While

            Return assistSparePartSalesmanList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistSparePartSalesman As AssistSparePartSalesman = CType(obj, AssistSparePartSalesman)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistSparePartSalesman.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistSparePartSalesman As AssistSparePartSalesman = CType(obj, AssistSparePartSalesman)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, assistSparePartSalesman.SalesmanCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, assistSparePartSalesman.Name)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, assistSparePartSalesman.DealerID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, assistSparePartSalesman.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, assistSparePartSalesman.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistSparePartSalesman.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, assistSparePartSalesman.LastUpdateBy)
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

            Dim assistSparePartSalesman As AssistSparePartSalesman = CType(obj, AssistSparePartSalesman)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistSparePartSalesman.ID)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, assistSparePartSalesman.SalesmanCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, assistSparePartSalesman.Name)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, assistSparePartSalesman.DealerID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, assistSparePartSalesman.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, assistSparePartSalesman.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistSparePartSalesman.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, assistSparePartSalesman.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AssistSparePartSalesman

            Dim assistSparePartSalesman As AssistSparePartSalesman = New AssistSparePartSalesman

            assistSparePartSalesman.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then assistSparePartSalesman.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then assistSparePartSalesman.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then assistSparePartSalesman.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then assistSparePartSalesman.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then assistSparePartSalesman.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then assistSparePartSalesman.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then assistSparePartSalesman.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then assistSparePartSalesman.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then assistSparePartSalesman.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then assistSparePartSalesman.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return assistSparePartSalesman

        End Function

        Private Sub SetTableName()

            If Not (GetType(AssistSparePartSalesman) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AssistSparePartSalesman), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AssistSparePartSalesman).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

