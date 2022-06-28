#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UserInfo Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 1/4/2006 - 9:54:07 AM
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

    Public Class UserInfoMapper
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

        Private m_InsertStatement As String = "up_InsertUserInfo"
        Private m_UpdateStatement As String = "up_UpdateUserInfo"
        Private m_RetrieveStatement As String = "up_RetrieveUserInfo"
        Private m_RetrieveListStatement As String = "up_RetrieveUserInfoList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteUserInfo"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim userInfo As UserInfo = Nothing
            While dr.Read

                userInfo = Me.CreateObject(dr)

            End While

            Return userInfo

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim userInfoList As ArrayList = New ArrayList

            While dr.Read
                Dim userInfo As UserInfo = Me.CreateObject(dr)
                userInfoList.Add(userInfo)
            End While

            Return userInfoList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim userInfo As UserInfo = CType(obj, UserInfo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, userInfo.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim userInfo As UserInfo = CType(obj, UserInfo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)

            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, userInfo.UserName)
            DBCommandWrapper.AddInParameter("@Password", DbType.Binary, userInfo.Password)
            DbCommandWrapper.AddInParameter("@FirstName", DbType.AnsiString, userInfo.FirstName)
            DBCommandWrapper.AddInParameter("@LastName", DbType.AnsiString, userInfo.LastName)
            DBCommandWrapper.AddInParameter("@Birthday", DbType.DateTime, userInfo.Birthday)
            DBCommandWrapper.AddInParameter("@JobPositionOld", DbType.AnsiString, userInfo.JobPositionOld)
            DBCommandWrapper.AddInParameter("@Email", DbType.AnsiString, userInfo.Email)
            DBCommandWrapper.AddInParameter("@Picture", DbType.Binary, userInfo.Picture)
            DBCommandWrapper.AddInParameter("@Picture", DbType.Binary, userInfo.Picture)
            DBCommandWrapper.AddInParameter("@HandPhone", DbType.AnsiString, userInfo.HandPhone)
            'DBCommandWrapper.AddInParameter("@TokenAlertTime", DbType.DateTime, userInfo.TokenAlertTime)

            DBCommandWrapper.AddInParameter("@Telephone", DbType.AnsiString, userInfo.Telephone)
            DBCommandWrapper.AddInParameter("@Question", DbType.AnsiString, userInfo.Question)
            DBCommandWrapper.AddInParameter("@Answer", DbType.AnsiString, userInfo.Answer)
            DBCommandWrapper.AddInParameter("@UserStatus", DbType.Byte, userInfo.UserStatus)
            DBCommandWrapper.AddInParameter("@LoginFlag", DbType.AnsiString, userInfo.LoginFlag)
            DBCommandWrapper.AddInParameter("@LastLogin", DbType.DateTime, userInfo.LastLogin)
            DBCommandWrapper.AddInParameter("@MessageNotification", DbType.Int16, userInfo.MessageNotification)
            DBCommandWrapper.AddInParameter("@CCKey", DbType.AnsiString, userInfo.CCKey)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, userInfo.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, userInfo.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@OrganizationID", DbType.Int32, Me.GetRefObject(userInfo.Dealer))
            If userInfo.IsAlreadyBranch() Then
                DbCommandWrapper.AddInParameter("@SubOrganizationID", DbType.Int32, Me.GetRefObject(userInfo.DealerBranch))
            Else
                DbCommandWrapper.AddInParameter("@SubOrganizationID", DbType.Int32, DBNull.Value)
            End If

            DBCommandWrapper.AddInParameter("@JobPositionID", DbType.Int32, Me.GetRefObject(userInfo.JobPosition))

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

            Dim userInfo As userInfo = CType(obj, userInfo)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, userInfo.ID)

            DBCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, userInfo.UserName)
            'DBCommandWrapper.AddInParameter("@Password", DbType.Binary, userInfo.Password)
            DBCommandWrapper.AddInParameter("@FirstName", DbType.AnsiString, userInfo.FirstName)
            DBCommandWrapper.AddInParameter("@LastName", DbType.AnsiString, userInfo.LastName)
            DBCommandWrapper.AddInParameter("@Birthday", DbType.DateTime, userInfo.Birthday)
            DBCommandWrapper.AddInParameter("@JobPositionOld", DbType.AnsiString, userInfo.JobPositionOld)
            DBCommandWrapper.AddInParameter("@Email", DbType.AnsiString, userInfo.Email)
            DBCommandWrapper.AddInParameter("@EmailValidation", DbType.Int16, userInfo.EmailValidation)
            DBCommandWrapper.AddInParameter("@Picture", DbType.Binary, userInfo.Picture)
            DBCommandWrapper.AddInParameter("@HandPhone", DbType.AnsiString, userInfo.HandPhone)

            DBCommandWrapper.AddInParameter("@TokenAlertTime", DbType.DateTime, userInfo.TokenAlertTime)
            DBCommandWrapper.AddInParameter("@Telephone", DbType.AnsiString, userInfo.Telephone)
            DBCommandWrapper.AddInParameter("@Question", DbType.AnsiString, userInfo.Question)
            DBCommandWrapper.AddInParameter("@Answer", DbType.AnsiString, userInfo.Answer)
            DBCommandWrapper.AddInParameter("@UserStatus", DbType.Byte, userInfo.UserStatus)
            DBCommandWrapper.AddInParameter("@LoginFlag", DbType.AnsiString, userInfo.LoginFlag)
            DBCommandWrapper.AddInParameter("@LastLogin", DbType.DateTime, userInfo.LastLogin)
            DBCommandWrapper.AddInParameter("@MessageNotification", DbType.Int16, userInfo.MessageNotification)
            DBCommandWrapper.AddInParameter("@CCKey", DbType.AnsiString, userInfo.CCKey)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, userInfo.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, userInfo.CreatedBy)
            'DBCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, userInfo.CreatedTime)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@OrganizationID", DbType.Int32, Me.GetRefObject(userInfo.Dealer))
            If userInfo.IsAlreadyBranch() Then
                DbCommandWrapper.AddInParameter("@SubOrganizationID", DbType.Int32, Me.GetRefObject(userInfo.DealerBranch))
            Else
                DbCommandWrapper.AddInParameter("@SubOrganizationID", DbType.Int32, DBNull.Value)
            End If
            DBCommandWrapper.AddInParameter("@JobPositionID", DbType.Int32, Me.GetRefObject(userInfo.JobPosition))

            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As UserInfo

            Dim userInfo As userInfo = New userInfo

            userInfo.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("UserName")) Then userInfo.UserName = dr("UserName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Password")) Then userInfo.Password = CType(dr("Password"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("FirstName")) Then userInfo.FirstName = dr("FirstName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastName")) Then userInfo.LastName = dr("LastName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionOld")) Then userInfo.JobPositionOld = dr("JobPositionOld").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then userInfo.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EmailValidation")) Then userInfo.EmailValidation = CType(dr("EmailValidation"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Picture")) Then userInfo.Picture = CType(dr("Picture"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("HandPhone")) Then userInfo.HandPhone = dr("HandPhone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TokenAlertTime")) Then userInfo.TokenAlertTime = CType(dr("TokenAlertTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("Telephone")) Then userInfo.Telephone = dr("Telephone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Question")) Then userInfo.Question = dr("Question").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Answer")) Then userInfo.Answer = dr("Answer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UserStatus")) Then userInfo.UserStatus = CType(dr("UserStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("LoginFlag")) Then userInfo.LoginFlag = dr("LoginFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastLogin")) Then userInfo.LastLogin = CType(dr("LastLogin"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MessageNotification")) Then userInfo.MessageNotification = CType(dr("MessageNotification"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CCKey")) Then userInfo.CCKey = dr("CCKey").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then userInfo.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then userInfo.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then userInfo.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then userInfo.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then userInfo.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OrganizationID")) Then
                userInfo.Dealer = New Dealer(CType(dr("OrganizationID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SubOrganizationID")) Then
                userInfo.DealerBranch = New Dealer(CType(dr("SubOrganizationID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionID")) Then
                userInfo.JobPosition = New JobPosition(CType(dr("JobPositionID"), Integer))
            End If

            Return userInfo

        End Function

        Private Sub SetTableName()

            If Not (GetType(UserInfo) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(UserInfo), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(UserInfo).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


