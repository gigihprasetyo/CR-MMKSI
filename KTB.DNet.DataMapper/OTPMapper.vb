
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : OTP Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 7/24/2018 - 10:51:44 AM
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

    Public Class OTPMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertOTP"
        Private m_UpdateStatement As String = "up_UpdateOTP"
        Private m_RetrieveStatement As String = "up_RetrieveOTP"
        Private m_RetrieveListStatement As String = "up_RetrieveOTPList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteOTP"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim oTP As OTP = Nothing
            While dr.Read

                oTP = Me.CreateObject(dr)

            End While

            Return oTP

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim oTPList As ArrayList = New ArrayList

            While dr.Read
                Dim oTP As OTP = Me.CreateObject(dr)
                oTPList.Add(oTP)
            End While

            Return oTPList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim oTP As OTP = CType(obj, OTP)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, oTP.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim oTP As OTP = CType(obj, OTP)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@UserInfoID", DbType.Int32, oTP.UserInfoID)
            DbCommandWrapper.AddInParameter("@ProcessType", DbType.Int16, oTP.ProcessType)
            DbCommandWrapper.AddInParameter("@NumberDestination", DbType.AnsiString, oTP.NumberDestination)
            DbCommandWrapper.AddInParameter("@ChallengeCode", DbType.AnsiString, oTP.ChallengeCode)
            DbCommandWrapper.AddInParameter("@SMSValue", DbType.AnsiString, oTP.SMSValue)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, oTP.Status)
            'DbCommandWrapper.AddInParameter("@ValidUntil", DbType.DateTime, oTP.ValidUntil)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, oTP.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, oTP.LastUpdateBy)
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

            Dim oTP As OTP = CType(obj, OTP)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, oTP.ID)
            DbCommandWrapper.AddInParameter("@UserInfoID", DbType.Int32, oTP.UserInfoID)
            DbCommandWrapper.AddInParameter("@ProcessType", DbType.Int16, oTP.ProcessType)
            DbCommandWrapper.AddInParameter("@NumberDestination", DbType.AnsiString, oTP.NumberDestination)
            DbCommandWrapper.AddInParameter("@ChallengeCode", DbType.AnsiString, oTP.ChallengeCode)
            DbCommandWrapper.AddInParameter("@SMSValue", DbType.AnsiString, oTP.SMSValue)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, oTP.Status)
            DbCommandWrapper.AddInParameter("@ValidUntil", DbType.DateTime, oTP.ValidUntil)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, oTP.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, oTP.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As OTP

            Dim oTP As OTP = New OTP

            oTP.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UserInfoID")) Then oTP.UserInfoID = CType(dr("UserInfoID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("UserInfoID")) Then
                oTP.UserInfo = New UserInfo(CType(dr("UserInfoID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ProcessType")) Then oTP.ProcessType = CType(dr("ProcessType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("NumberDestination")) Then oTP.NumberDestination = dr("NumberDestination").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChallengeCode")) Then oTP.ChallengeCode = dr("ChallengeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SMSValue")) Then oTP.SMSValue = dr("SMSValue").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then oTP.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidUntil")) Then oTP.ValidUntil = CType(dr("ValidUntil"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then oTP.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then oTP.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then oTP.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then oTP.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then oTP.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return oTP

        End Function

        Private Sub SetTableName()

            If Not (GetType(OTP) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(OTP), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(OTP).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

