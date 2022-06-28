#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : BusinessArea Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/29/2005 - 6:55:05 PM
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

    Public Class BusinessAreaMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBusinessArea"
        Private m_UpdateStatement As String = "up_UpdateBusinessArea"
        Private m_RetrieveStatement As String = "up_RetrieveBusinessArea"
        Private m_RetrieveListStatement As String = "up_RetrieveBusinessAreaList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBusinessArea"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim businessArea As BusinessArea = Nothing
            While dr.Read

                businessArea = Me.CreateObject(dr)

            End While

            Return businessArea

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim businessAreaList As ArrayList = New ArrayList

            While dr.Read
                Dim businessArea As BusinessArea = Me.CreateObject(dr)
                businessAreaList.Add(businessArea)
            End While

            Return businessAreaList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim businessArea As BusinessArea = CType(obj, BusinessArea)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, businessArea.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim businessArea As BusinessArea = CType(obj, BusinessArea)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Kind", DbType.AnsiString, businessArea.Kind)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, businessArea.Title)
            DbCommandWrapper.AddInParameter("@Position", DbType.Int16, businessArea.Position)
            DbCommandWrapper.AddInParameter("@ContactPerson", DbType.AnsiString, businessArea.ContactPerson)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, businessArea.Email)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, businessArea.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, businessArea.Fax)
            DbCommandWrapper.AddInParameter("@HP", DbType.AnsiString, businessArea.HP)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, businessArea.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, businessArea.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, iif(businessArea.Dealer Is Nothing, CType(DBNull.Value, Object), CType(businessArea.Dealer.ID, Object)))

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

            Dim businessArea As BusinessArea = CType(obj, BusinessArea)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, businessArea.ID)
            DbCommandWrapper.AddInParameter("@Kind", DbType.AnsiString, businessArea.Kind)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, businessArea.Title)
            DbCommandWrapper.AddInParameter("@ContactPerson", DbType.AnsiString, businessArea.ContactPerson)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, businessArea.Email)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, businessArea.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, businessArea.Fax)
            DbCommandWrapper.AddInParameter("@HP", DbType.AnsiString, businessArea.HP)
            DbCommandWrapper.AddInParameter("@Position", DbType.Int16, businessArea.Position)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, businessArea.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, businessArea.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, iif(businessArea.Dealer Is Nothing, CType(DBNull.Value, Object), CType(businessArea.Dealer.ID, Object)))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BusinessArea

            Dim businessArea As BusinessArea = New BusinessArea

            businessArea.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Kind")) Then businessArea.Kind = dr("Kind").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Title")) Then businessArea.Title = dr("Title").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContactPerson")) Then businessArea.ContactPerson = dr("ContactPerson").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then businessArea.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then businessArea.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Fax")) Then businessArea.Fax = dr("Fax").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HP")) Then businessArea.HP = dr("HP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Position")) Then businessArea.Position = CType(dr("Position"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then businessArea.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then businessArea.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then businessArea.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then businessArea.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then businessArea.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                businessArea.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return businessArea

        End Function

        Private Sub SetTableName()

            If Not (GetType(BusinessArea) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BusinessArea), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BusinessArea).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

