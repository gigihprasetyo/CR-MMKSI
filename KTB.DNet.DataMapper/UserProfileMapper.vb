#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UserProfile Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 1/16/2007 - 10:56:40 AM
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

    Public Class UserProfileMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

        Public Sub New(ByVal instanceName As String)
            Db = DatabaseFactory.CreateDatabase(instanceName)
            SetTableName()
        End Sub
#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertUserProfile"
        Private m_UpdateStatement As String = "up_UpdateUserProfile"
        Private m_RetrieveStatement As String = "up_RetrieveUserProfile"
        Private m_RetrieveListStatement As String = "up_RetrieveUserProfileList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteUserProfile"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim userProfile As UserProfile = Nothing
            While dr.Read

                userProfile = Me.CreateObject(dr)

            End While

            Return userProfile

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim userProfileList As ArrayList = New ArrayList

            While dr.Read
                Dim userProfile As UserProfile = Me.CreateObject(dr)
                userProfileList.Add(userProfile)
            End While

            Return userProfileList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim userProfile As UserProfile = CType(obj, UserProfile)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, userProfile.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim userProfile As UserProfile = CType(obj, UserProfile)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RegistrationStatus", DbType.Int16, userProfile.RegistrationStatus)
            DbCommandWrapper.AddInParameter("@ActivationCode", DbType.AnsiString, userProfile.ActivationCode)
            DbCommandWrapper.AddInParameter("@TempActivationCode", DbType.AnsiString, userProfile.TempActivationCode)
            DbCommandWrapper.AddInParameter("@ActivationSentTime", DbType.DateTime, userProfile.ActivationSentTime)
            DbCommandWrapper.AddInParameter("@SessionID", DbType.AnsiString, userProfile.SessionID)
            DbCommandWrapper.AddInParameter("@ImageDescription", DbType.AnsiString, userProfile.ImageDescription)
            DbCommandWrapper.AddInParameter("@ActivationStatus", DbType.Int16, userProfile.ActivationStatus)
            DbCommandWrapper.AddInParameter("@UserLockStatus", DbType.Int16, userProfile.UserLockStatus)
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.DateTime, userProfile.BirthDate)
            DbCommandWrapper.AddInParameter("@MotherName", DbType.AnsiString, userProfile.MotherName)
            DbCommandWrapper.AddInParameter("@LoginCount", DbType.Int32, userProfile.LoginCount)
            DbCommandWrapper.AddInParameter("@Question1", DbType.AnsiString, userProfile.Question1)
            DbCommandWrapper.AddInParameter("@Question2", DbType.AnsiString, userProfile.Question2)
            DbCommandWrapper.AddInParameter("@Question3", DbType.AnsiString, userProfile.Question3)
            DbCommandWrapper.AddInParameter("@Question4", DbType.AnsiString, userProfile.Question4)
            DbCommandWrapper.AddInParameter("@Question5", DbType.AnsiString, userProfile.Question5)
            DbCommandWrapper.AddInParameter("@Answer1", DbType.AnsiString, userProfile.Answer1)
            DbCommandWrapper.AddInParameter("@Answer2", DbType.AnsiString, userProfile.Answer2)
            DbCommandWrapper.AddInParameter("@Answer3", DbType.AnsiString, userProfile.Answer3)
            DbCommandWrapper.AddInParameter("@Answer4", DbType.AnsiString, userProfile.Answer4)
            DbCommandWrapper.AddInParameter("@Answer5", DbType.AnsiString, userProfile.Answer5)
            DBCommandWrapper.AddInParameter("@LastLoginIPAddress", DbType.AnsiString, userProfile.LastLoginIPAddress)

            DBCommandWrapper.AddInParameter("@TransitionHP", DbType.AnsiString, userProfile.TransitionHP)
            DBCommandWrapper.AddInParameter("@TransitionActivationCode", DbType.AnsiString, userProfile.TransitionActivationCode)
            DBCommandWrapper.AddInParameter("@TransitionProcessDate", DbType.DateTime, userProfile.TransitionProcessDate)


            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, userProfile.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, userProfile.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@UserID", DbType.Int32, Me.GetRefObject(userProfile.UserInfo))
            DbCommandWrapper.AddInParameter("@ImageID", DbType.Int32, userProfile.ImageID)
            DbCommandWrapper.AddInParameter("@BingoID", DbType.Int32, Me.GetRefObject(userProfile.Bingo))

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

            Dim userProfile As UserProfile = CType(obj, UserProfile)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, userProfile.ID)
            DbCommandWrapper.AddInParameter("@RegistrationStatus", DbType.Int16, userProfile.RegistrationStatus)
            DbCommandWrapper.AddInParameter("@ActivationCode", DbType.AnsiString, userProfile.ActivationCode)
            DbCommandWrapper.AddInParameter("@TempActivationCode", DbType.AnsiString, userProfile.TempActivationCode)
            DbCommandWrapper.AddInParameter("@ActivationSentTime", DbType.DateTime, userProfile.ActivationSentTime)
            DbCommandWrapper.AddInParameter("@SessionID", DbType.AnsiString, userProfile.SessionID)
            DbCommandWrapper.AddInParameter("@ImageDescription", DbType.AnsiString, userProfile.ImageDescription)
            DbCommandWrapper.AddInParameter("@ActivationStatus", DbType.Int16, userProfile.ActivationStatus)
            DbCommandWrapper.AddInParameter("@UserLockStatus", DbType.Int16, userProfile.UserLockStatus)
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.DateTime, userProfile.BirthDate)
            DbCommandWrapper.AddInParameter("@MotherName", DbType.AnsiString, userProfile.MotherName)
            DbCommandWrapper.AddInParameter("@LoginCount", DbType.Int32, userProfile.LoginCount)
            DbCommandWrapper.AddInParameter("@Question1", DbType.AnsiString, userProfile.Question1)
            DbCommandWrapper.AddInParameter("@Question2", DbType.AnsiString, userProfile.Question2)
            DbCommandWrapper.AddInParameter("@Question3", DbType.AnsiString, userProfile.Question3)
            DbCommandWrapper.AddInParameter("@Question4", DbType.AnsiString, userProfile.Question4)
            DbCommandWrapper.AddInParameter("@Question5", DbType.AnsiString, userProfile.Question5)
            DbCommandWrapper.AddInParameter("@Answer1", DbType.AnsiString, userProfile.Answer1)
            DbCommandWrapper.AddInParameter("@Answer2", DbType.AnsiString, userProfile.Answer2)
            DbCommandWrapper.AddInParameter("@Answer3", DbType.AnsiString, userProfile.Answer3)
            DbCommandWrapper.AddInParameter("@Answer4", DbType.AnsiString, userProfile.Answer4)
            DbCommandWrapper.AddInParameter("@Answer5", DbType.AnsiString, userProfile.Answer5)
            DbCommandWrapper.AddInParameter("@LastLoginIPAddress", DbType.AnsiString, userProfile.LastLoginIPAddress)
            DBCommandWrapper.AddInParameter("@TransitionHP", DbType.AnsiString, userProfile.TransitionHP)
            DBCommandWrapper.AddInParameter("@TransitionActivationCode", DbType.AnsiString, userProfile.TransitionActivationCode)
            DBCommandWrapper.AddInParameter("@TransitionProcessDate", DbType.DateTime, userProfile.TransitionProcessDate)

            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, userProfile.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, userProfile.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@UserID", DbType.Int32, Me.GetRefObject(userProfile.UserInfo))
            DbCommandWrapper.AddInParameter("@ImageID", DbType.Int32, userProfile.ImageID)
            DbCommandWrapper.AddInParameter("@BingoID", DbType.Int32, Me.GetRefObject(userProfile.Bingo))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As UserProfile

            Dim userProfile As UserProfile = New UserProfile

            userProfile.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegistrationStatus")) Then userProfile.RegistrationStatus = CType(dr("RegistrationStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivationCode")) Then userProfile.ActivationCode = dr("ActivationCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TempActivationCode")) Then userProfile.TempActivationCode = dr("TempActivationCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActivationSentTime")) Then userProfile.ActivationSentTime = CType(dr("ActivationSentTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SessionID")) Then userProfile.SessionID = dr("SessionID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ImageDescription")) Then userProfile.ImageDescription = dr("ImageDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActivationStatus")) Then userProfile.ActivationStatus = CType(dr("ActivationStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("UserLockStatus")) Then userProfile.UserLockStatus = CType(dr("UserLockStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("BirthDate")) Then userProfile.BirthDate = CType(dr("BirthDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MotherName")) Then userProfile.MotherName = dr("MotherName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LoginCount")) Then userProfile.LoginCount = CType(dr("LoginCount"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Question1")) Then userProfile.Question1 = dr("Question1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Question2")) Then userProfile.Question2 = dr("Question2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Question3")) Then userProfile.Question3 = dr("Question3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Question4")) Then userProfile.Question4 = dr("Question4").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Question5")) Then userProfile.Question5 = dr("Question5").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Answer1")) Then userProfile.Answer1 = dr("Answer1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Answer2")) Then userProfile.Answer2 = dr("Answer2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Answer3")) Then userProfile.Answer3 = dr("Answer3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Answer4")) Then userProfile.Answer4 = dr("Answer4").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Answer5")) Then userProfile.Answer5 = dr("Answer5").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastLoginIPAddress")) Then userProfile.LastLoginIPAddress = dr("LastLoginIPAddress").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("TransitionHP")) Then userProfile.TransitionHP = dr("TransitionHP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransitionActivationCode")) Then userProfile.TransitionActivationCode = dr("TransitionActivationCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransitionProcessDate")) Then userProfile.TransitionProcessDate = CType(dr("TransitionProcessDate"), DateTime)


            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then userProfile.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then userProfile.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then userProfile.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then userProfile.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then userProfile.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UserID")) Then
                userProfile.UserInfo = New UserInfo(CType(dr("UserID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ImageID")) Then
                userProfile.ImageID = CType(dr("ImageID"), Integer)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BingoID")) Then
                userProfile.Bingo = New Bingo(CType(dr("BingoID"), Integer))
            End If

            Return userProfile

        End Function

        Private Sub SetTableName()

            If Not (GetType(UserProfile) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(UserProfile), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(UserProfile).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

