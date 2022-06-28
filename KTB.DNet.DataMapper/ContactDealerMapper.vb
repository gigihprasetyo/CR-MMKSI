
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ContactDealer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 10/27/2020 - 1:27:49 PM
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

    Public Class ContactDealerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertContactDealer"
        Private m_UpdateStatement As String = "up_UpdateContactDealer"
        Private m_RetrieveStatement As String = "up_RetrieveContactDealer"
        Private m_RetrieveListStatement As String = "up_RetrieveContactDealerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteContactDealer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim contactDealer As ContactDealer = Nothing
            While dr.Read

                contactDealer = Me.CreateObject(dr)

            End While

            Return contactDealer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim contactDealerList As ArrayList = New ArrayList

            While dr.Read
                Dim contactDealer As ContactDealer = Me.CreateObject(dr)
                contactDealerList.Add(contactDealer)
            End While

            Return contactDealerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim contactDealer As ContactDealer = CType(obj, ContactDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, contactDealer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim contactDealer As ContactDealer = CType(obj, ContactDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.AnsiString, contactDealer.Tipe)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, contactDealer.UserName)
            DbCommandWrapper.AddInParameter("@Email1", DbType.AnsiString, contactDealer.Email1)
            DbCommandWrapper.AddInParameter("@Email2", DbType.AnsiString, contactDealer.Email2)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, contactDealer.Phone)
            DbCommandWrapper.AddInParameter("@HP", DbType.AnsiString, contactDealer.HP)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, contactDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, contactDealer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(contactDealer.Dealer))

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

            Dim contactDealer As ContactDealer = CType(obj, ContactDealer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, contactDealer.ID)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.AnsiString, contactDealer.Tipe)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, contactDealer.UserName)
            DbCommandWrapper.AddInParameter("@Email1", DbType.AnsiString, contactDealer.Email1)
            DbCommandWrapper.AddInParameter("@Email2", DbType.AnsiString, contactDealer.Email2)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, contactDealer.Phone)
            DbCommandWrapper.AddInParameter("@HP", DbType.AnsiString, contactDealer.HP)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, contactDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, contactDealer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(contactDealer.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ContactDealer

            Dim contactDealer As ContactDealer = New ContactDealer

            contactDealer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Tipe")) Then contactDealer.Tipe = dr("Tipe").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UserName")) Then contactDealer.UserName = dr("UserName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email1")) Then contactDealer.Email1 = dr("Email1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email2")) Then contactDealer.Email2 = dr("Email2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then contactDealer.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HP")) Then contactDealer.HP = dr("HP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then contactDealer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then contactDealer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then contactDealer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then contactDealer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then contactDealer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                contactDealer.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return contactDealer

        End Function

        Private Sub SetTableName()

            If Not (GetType(ContactDealer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ContactDealer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ContactDealer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

