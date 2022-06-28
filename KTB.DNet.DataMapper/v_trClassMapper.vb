
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_trClass Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 6/11/2009 - 2:00:42 PM
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

    Public Class v_trClassMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_trClass"
        Private m_UpdateStatement As String = "up_Updatev_trClass"
        Private m_RetrieveStatement As String = "up_Retrievev_trClass"
        Private m_RetrieveListStatement As String = "up_Retrievev_trClassList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_trClass"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_trClass As v_trClass = Nothing
            While dr.Read

                v_trClass = Me.CreateObject(dr)

            End While

            Return v_trClass

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_trClassList As ArrayList = New ArrayList

            While dr.Read
                Dim v_trClass As v_trClass = Me.CreateObject(dr)
                v_trClassList.Add(v_trClass)
            End While

            Return v_trClassList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_trClass As v_trClass = CType(obj, v_trClass)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_trClass.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_trClass As v_trClass = CType(obj, v_trClass)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ClassID", DbType.Int32, v_trClass.ClassID)
            DbCommandWrapper.AddInParameter("@ClassCode", DbType.AnsiString, v_trClass.ClassCode)
            DbCommandWrapper.AddInParameter("@ClassName", DbType.AnsiString, v_trClass.ClassName)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, v_trClass.StartDate)
            DbCommandWrapper.AddInParameter("@FinishDate", DbType.DateTime, v_trClass.FinishDate)
            DbCommandWrapper.AddInParameter("@Capacity", DbType.Int32, v_trClass.Capacity)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_trClass.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_trClass.DealerCode)
            DbCommandWrapper.AddInParameter("@Allocated", DbType.Int32, v_trClass.Allocated)
            DbCommandWrapper.AddInParameter("@NumOfTrainee", DbType.Int32, v_trClass.NumOfTrainee)
            DbCommandWrapper.AddInParameter("@AreaID", DbType.Int16, v_trClass.AreaID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_trClass.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_trClass.LastUpdateBy)
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

            Dim v_trClass As v_trClass = CType(obj, v_trClass)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_trClass.ID)
            DbCommandWrapper.AddInParameter("@ClassID", DbType.Int32, v_trClass.ClassID)
            DbCommandWrapper.AddInParameter("@ClassCode", DbType.AnsiString, v_trClass.ClassCode)
            DbCommandWrapper.AddInParameter("@ClassName", DbType.AnsiString, v_trClass.ClassName)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, v_trClass.StartDate)
            DbCommandWrapper.AddInParameter("@FinishDate", DbType.DateTime, v_trClass.FinishDate)
            DbCommandWrapper.AddInParameter("@Capacity", DbType.Int32, v_trClass.Capacity)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_trClass.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_trClass.DealerCode)
            DbCommandWrapper.AddInParameter("@Allocated", DbType.Int32, v_trClass.Allocated)
            DbCommandWrapper.AddInParameter("@NumOfTrainee", DbType.Int32, v_trClass.NumOfTrainee)
            DbCommandWrapper.AddInParameter("@AreaID", DbType.Int16, v_trClass.AreaID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_trClass.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_trClass.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_trClass

            Dim v_trClass As v_trClass = New v_trClass

            v_trClass.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ClassID")) Then v_trClass.ClassID = CType(dr("ClassID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ClassCode")) Then v_trClass.ClassCode = dr("ClassCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClassName")) Then v_trClass.ClassName = dr("ClassName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartDate")) Then v_trClass.StartDate = CType(dr("StartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FinishDate")) Then v_trClass.FinishDate = CType(dr("FinishDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Capacity")) Then v_trClass.Capacity = CType(dr("Capacity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_trClass.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_trClass.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Allocated")) Then v_trClass.Allocated = CType(dr("Allocated"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NumOfTrainee")) Then v_trClass.NumOfTrainee = CType(dr("NumOfTrainee"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AreaID")) Then v_trClass.AreaID = CType(dr("AreaID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_trClass.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_trClass.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_trClass.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_trClass.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_trClass.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_trClass

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_trClass) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_trClass), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_trClass).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

