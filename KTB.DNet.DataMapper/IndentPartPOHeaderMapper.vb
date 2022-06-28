#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : IndentPartPOHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/10/2007 - 12:54:27 PM
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

    Public Class IndentPartPOHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertIndentPartPOHeader"
        Private m_UpdateStatement As String = "up_UpdateIndentPartPOHeader"
        Private m_RetrieveStatement As String = "up_RetrieveIndentPartPOHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveIndentPartPOHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteIndentPartPOHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim indentPartPOHeader As IndentPartPOHeader = Nothing
            While dr.Read

                indentPartPOHeader = Me.CreateObject(dr)

            End While

            Return indentPartPOHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim indentPartPOHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim indentPartPOHeader As IndentPartPOHeader = Me.CreateObject(dr)
                indentPartPOHeaderList.Add(indentPartPOHeader)
            End While

            Return indentPartPOHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim indentPartPOHeader As IndentPartPOHeader = CType(obj, IndentPartPOHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, indentPartPOHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim indentPartPOHeader As IndentPartPOHeader = CType(obj, IndentPartPOHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, indentPartPOHeader.PONumber)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, indentPartPOHeader.PODate)
            DbCommandWrapper.AddInParameter("@IsCancelled", DbType.Byte, indentPartPOHeader.IsCancelled)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, indentPartPOHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, indentPartPOHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(indentPartPOHeader.Dealer))

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

            Dim indentPartPOHeader As IndentPartPOHeader = CType(obj, IndentPartPOHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, indentPartPOHeader.ID)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, indentPartPOHeader.PONumber)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, indentPartPOHeader.PODate)
            DbCommandWrapper.AddInParameter("@IsCancelled", DbType.Byte, indentPartPOHeader.IsCancelled)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, indentPartPOHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, indentPartPOHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(indentPartPOHeader.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As IndentPartPOHeader

            Dim indentPartPOHeader As IndentPartPOHeader = New IndentPartPOHeader

            indentPartPOHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then indentPartPOHeader.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PODate")) Then indentPartPOHeader.PODate = CType(dr("PODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsCancelled")) Then indentPartPOHeader.IsCancelled = CType(dr("IsCancelled"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then indentPartPOHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then indentPartPOHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then indentPartPOHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then indentPartPOHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then indentPartPOHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                indentPartPOHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return indentPartPOHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(IndentPartPOHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(IndentPartPOHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(IndentPartPOHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

