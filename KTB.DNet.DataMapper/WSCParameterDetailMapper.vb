
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCParameterDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 4/23/2018 - 9:46:59 AM
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

    Public Class WSCParameterDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWSCParameterDetail"
        Private m_UpdateStatement As String = "up_UpdateWSCParameterDetail"
        Private m_RetrieveStatement As String = "up_RetrieveWSCParameterDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveWSCParameterDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWSCParameterDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim wSCParameterDetail As WSCParameterDetail = Nothing
            While dr.Read

                wSCParameterDetail = Me.CreateObject(dr)

            End While

            Return wSCParameterDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim wSCParameterDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim wSCParameterDetail As WSCParameterDetail = Me.CreateObject(dr)
                wSCParameterDetailList.Add(wSCParameterDetail)
            End While

            Return wSCParameterDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wSCParameterDetail As WSCParameterDetail = CType(obj, WSCParameterDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wSCParameterDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wSCParameterDetail As WSCParameterDetail = CType(obj, WSCParameterDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@WSCParameterHeaderID", DbType.Int32, Me.GetRefObject(wSCParameterDetail.WSCParameterHeader))
            DbCommandWrapper.AddInParameter("@Kind", DbType.Int32, wSCParameterDetail.Kind)
            DbCommandWrapper.AddInParameter("@Operator", DbType.Int32, wSCParameterDetail.Operators)
            DbCommandWrapper.AddInParameter("@Value", DbType.AnsiString, wSCParameterDetail.Value)
            DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, wSCParameterDetail.ReasonCode)
            DbCommandWrapper.AddInParameter("@WSCParameterConditionID", DbType.Int32, Me.GetRefObject(wSCParameterDetail.WSCParameterCondition))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wSCParameterDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, wSCParameterDetail.LastUpdateBy)
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

            Dim wSCParameterDetail As WSCParameterDetail = CType(obj, WSCParameterDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wSCParameterDetail.ID)
            DbCommandWrapper.AddInParameter("@Kind", DbType.Int32, wSCParameterDetail.Kind)
            DbCommandWrapper.AddInParameter("@Operator", DbType.Int32, wSCParameterDetail.Operators)
            DbCommandWrapper.AddInParameter("@Value", DbType.AnsiString, wSCParameterDetail.Value)
            DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, wSCParameterDetail.ReasonCode)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wSCParameterDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, wSCParameterDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@WSCParameterHeaderID", DbType.Int32, Me.GetRefObject(wSCParameterDetail.WSCParameterHeader))
            DbCommandWrapper.AddInParameter("@WSCParameterConditionID", DbType.Int32, Me.GetRefObject(wSCParameterDetail.WSCParameterCondition))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WSCParameterDetail

            Dim wSCParameterDetail As WSCParameterDetail = New WSCParameterDetail

            wSCParameterDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Kind")) Then wSCParameterDetail.Kind = CType(dr("Kind"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Operator")) Then wSCParameterDetail.Operators = CType(dr("Operator"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Value")) Then wSCParameterDetail.Value = dr("Value").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReasonCode")) Then wSCParameterDetail.ReasonCode = dr("ReasonCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then wSCParameterDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then wSCParameterDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then wSCParameterDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then wSCParameterDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then wSCParameterDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("WSCParameterConditionID")) Then
                wSCParameterDetail.WSCParameterCondition = New WSCParameterCondition(CType(dr("WSCParameterConditionID"), Integer))
            End If

            Return wSCParameterDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(WSCParameterDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WSCParameterDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WSCParameterDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

