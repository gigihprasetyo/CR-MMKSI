
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_GetTokenExpired Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 12/23/2009 - 11:16:22 AM
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

    Public Class v_GetTokenExpiredMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_GetTokenExpired"
        Private m_UpdateStatement As String = "up_Updatev_GetTokenExpired"
        Private m_RetrieveStatement As String = "up_Retrievev_GetTokenExpired"
        Private m_RetrieveListStatement As String = "up_Retrievev_GetTokenExpiredList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_GetTokenExpired"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_GetTokenExpired As v_GetTokenExpired = Nothing
            While dr.Read

                v_GetTokenExpired = Me.CreateObject(dr)

            End While

            Return v_GetTokenExpired

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_GetTokenExpiredList As ArrayList = New ArrayList

            While dr.Read
                Dim v_GetTokenExpired As v_GetTokenExpired = Me.CreateObject(dr)
                v_GetTokenExpiredList.Add(v_GetTokenExpired)
            End While

            Return v_GetTokenExpiredList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_GetTokenExpired As v_GetTokenExpired = CType(obj, v_GetTokenExpired)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, v_GetTokenExpired.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_GetTokenExpired As v_GetTokenExpired = CType(obj, v_GetTokenExpired)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, v_GetTokenExpired.UserName)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, v_GetTokenExpired.Name)
            DbCommandWrapper.AddInParameter("@Handphone", DbType.AnsiString, v_GetTokenExpired.Handphone)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, v_GetTokenExpired.Email)
            DbCommandWrapper.AddInParameter("@ActivationCode", DbType.AnsiString, v_GetTokenExpired.ActivationCode)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_GetTokenExpired.DealerCode)
            DbCommandWrapper.AddInParameter("@DayRemind", DbType.Int32, v_GetTokenExpired.DayRemind)
            DbCommandWrapper.AddInParameter("@DateToday", DbType.DateTime, v_GetTokenExpired.DateToday)
            DbCommandWrapper.AddInParameter("@DateRemind", DbType.DateTime, v_GetTokenExpired.DateRemind)
            DbCommandWrapper.AddInParameter("@DayAlertStatus", DbType.Int32, v_GetTokenExpired.DayAlertStatus)
            DbCommandWrapper.AddInParameter("@TokenAlertTime", DbType.DateTime, v_GetTokenExpired.TokenAlertTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_GetTokenExpired.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_GetTokenExpired.LastUpdateBy)


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

            Dim v_GetTokenExpired As v_GetTokenExpired = CType(obj, v_GetTokenExpired)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, v_GetTokenExpired.ID)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, v_GetTokenExpired.UserName)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, v_GetTokenExpired.Name)
            DbCommandWrapper.AddInParameter("@Handphone", DbType.AnsiString, v_GetTokenExpired.Handphone)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, v_GetTokenExpired.Email)
            DbCommandWrapper.AddInParameter("@ActivationCode", DbType.AnsiString, v_GetTokenExpired.ActivationCode)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_GetTokenExpired.DealerCode)
            DbCommandWrapper.AddInParameter("@DayRemind", DbType.Int32, v_GetTokenExpired.DayRemind)
            DbCommandWrapper.AddInParameter("@DateToday", DbType.DateTime, v_GetTokenExpired.DateToday)
            DbCommandWrapper.AddInParameter("@DateRemind", DbType.DateTime, v_GetTokenExpired.DateRemind)
            DbCommandWrapper.AddInParameter("@DayAlertStatus", DbType.Int32, v_GetTokenExpired.DayAlertStatus)
            DbCommandWrapper.AddInParameter("@TokenAlertTime", DbType.DateTime, v_GetTokenExpired.TokenAlertTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_GetTokenExpired.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_GetTokenExpired.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_GetTokenExpired

            Dim v_GetTokenExpired As v_GetTokenExpired = New v_GetTokenExpired

            v_GetTokenExpired.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("UserName")) Then v_GetTokenExpired.UserName = dr("UserName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then v_GetTokenExpired.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Handphone")) Then v_GetTokenExpired.Handphone = dr("Handphone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then v_GetTokenExpired.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActivationCode")) Then v_GetTokenExpired.ActivationCode = dr("ActivationCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_GetTokenExpired.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DayRemind")) Then v_GetTokenExpired.DayRemind = CType(dr("DayRemind"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DateToday")) Then v_GetTokenExpired.DateToday = CType(dr("DateToday"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DateRemind")) Then v_GetTokenExpired.DateRemind = CType(dr("DateRemind"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DayAlertStatus")) Then v_GetTokenExpired.DayAlertStatus = CType(dr("DayAlertStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TokenAlertTime")) Then v_GetTokenExpired.TokenAlertTime = CType(dr("TokenAlertTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_GetTokenExpired.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_GetTokenExpired.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_GetTokenExpired.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_GetTokenExpired.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_GetTokenExpired.LastUpdateBy = dr("LastUpdateBy").ToString

            Return v_GetTokenExpired

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_GetTokenExpired) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_GetTokenExpired), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_GetTokenExpired).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

