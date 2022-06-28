
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanDSEAssignment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/7/2020 - 10:19:20 AM
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

    Public Class SalesmanDSEAssignmentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanDSEAssignment"
        Private m_UpdateStatement As String = "up_UpdateSalesmanDSEAssignment"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanDSEAssignment"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanDSEAssignmentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanDSEAssignment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim SalesmanDSEAssignment As SalesmanDSEAssignment = Nothing
            While dr.Read

                SalesmanDSEAssignment = Me.CreateObject(dr)

            End While

            Return SalesmanDSEAssignment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim SalesmanDSEAssignmentList As ArrayList = New ArrayList

            While dr.Read
                Dim SalesmanDSEAssignment As SalesmanDSEAssignment = Me.CreateObject(dr)
                SalesmanDSEAssignmentList.Add(SalesmanDSEAssignment)
            End While

            Return SalesmanDSEAssignmentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim SalesmanDSEAssignment As SalesmanDSEAssignment = CType(obj, SalesmanDSEAssignment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, SalesmanDSEAssignment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim SalesmanDSEAssignment As SalesmanDSEAssignment = CType(obj, SalesmanDSEAssignment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(SalesmanDSEAssignment.Dealer))
            DbCommandWrapper.AddInParameter("@Priority", DbType.Int32, SalesmanDSEAssignment.Priority)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, SalesmanDSEAssignment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, SalesmanDSEAssignment.LastUpdateBy)
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

            Dim SalesmanDSEAssignment As SalesmanDSEAssignment = CType(obj, SalesmanDSEAssignment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, SalesmanDSEAssignment.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(SalesmanDSEAssignment.Dealer))
            DbCommandWrapper.AddInParameter("@Priority", DbType.Int32, SalesmanDSEAssignment.Priority)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, SalesmanDSEAssignment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, SalesmanDSEAssignment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanDSEAssignment

            Dim SalesmanDSEAssignment As SalesmanDSEAssignment = New SalesmanDSEAssignment

            SalesmanDSEAssignment.ID = CType(dr("ID"), Short)
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then SalesmanDSEAssignment.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Priority")) Then SalesmanDSEAssignment.Priority = CType(dr("Priority"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then SalesmanDSEAssignment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then SalesmanDSEAssignment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then SalesmanDSEAssignment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then SalesmanDSEAssignment.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then SalesmanDSEAssignment.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                SalesmanDSEAssignment.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return SalesmanDSEAssignment

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanDSEAssignment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanDSEAssignment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanDSEAssignment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


