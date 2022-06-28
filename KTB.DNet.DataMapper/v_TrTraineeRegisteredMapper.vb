
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DN
'// PURPOSE       : v_TrTraineeRegistered Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/6/2009 - 8:33:04 AM
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

    Public Class v_TrTraineeRegisteredMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_TrTraineeRegistered"
        Private m_UpdateStatement As String = "up_Updatev_TrTraineeRegistered"
        Private m_RetrieveStatement As String = "up_Retrievev_TrTraineeRegistered"
        Private m_RetrieveListStatement As String = "up_Retrievev_TrTraineeRegisteredList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_TrTraineeRegistered"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_TrTraineeRegistered As v_TrTraineeRegistered = Nothing
            While dr.Read

                v_TrTraineeRegistered = Me.CreateObject(dr)

            End While

            Return v_TrTraineeRegistered

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_TrTraineeRegisteredList As ArrayList = New ArrayList

            While dr.Read
                Dim v_TrTraineeRegistered As v_TrTraineeRegistered = Me.CreateObject(dr)
                v_TrTraineeRegisteredList.Add(v_TrTraineeRegistered)
            End While

            Return v_TrTraineeRegisteredList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_TrTraineeRegistered As v_TrTraineeRegistered = CType(obj, v_TrTraineeRegistered)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_TrTraineeRegistered.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_TrTraineeRegistered As v_TrTraineeRegistered = CType(obj, v_TrTraineeRegistered)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, v_TrTraineeRegistered.Name)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_TrTraineeRegistered.DealerCode)
            DbCommandWrapper.AddInParameter("@ClassCode", DbType.AnsiString, v_TrTraineeRegistered.ClassCode)
            DbCommandWrapper.AddInParameter("@CourseCode", DbType.AnsiString, v_TrTraineeRegistered.CourseCode)
            DBCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, v_TrTraineeRegistered.Status)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_TrTraineeRegistered.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_TrTraineeRegistered.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim v_TrTraineeRegistered As v_TrTraineeRegistered = CType(obj, v_TrTraineeRegistered)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, v_TrTraineeRegistered.ID)
            DBCommandWrapper.AddInParameter("@Name", DbType.AnsiString, v_TrTraineeRegistered.Name)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_TrTraineeRegistered.DealerCode)
            DBCommandWrapper.AddInParameter("@ClassCode", DbType.AnsiString, v_TrTraineeRegistered.ClassCode)
            DBCommandWrapper.AddInParameter("@CourseCode", DbType.AnsiString, v_TrTraineeRegistered.CourseCode)
            DBCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, v_TrTraineeRegistered.Status)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_TrTraineeRegistered.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_TrTraineeRegistered.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_TrTraineeRegistered

            Dim v_TrTraineeRegistered As v_TrTraineeRegistered = New v_TrTraineeRegistered

            v_TrTraineeRegistered.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then v_TrTraineeRegistered.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_TrTraineeRegistered.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClassCode")) Then v_TrTraineeRegistered.ClassCode = dr("ClassCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CourseCode")) Then v_TrTraineeRegistered.CourseCode = dr("CourseCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_TrTraineeRegistered.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_TrTraineeRegistered.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_TrTraineeRegistered.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_TrTraineeRegistered.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_TrTraineeRegistered.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_TrTraineeRegistered.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_TrTraineeRegistered

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_TrTraineeRegistered) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_TrTraineeRegistered), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_TrTraineeRegistered).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

