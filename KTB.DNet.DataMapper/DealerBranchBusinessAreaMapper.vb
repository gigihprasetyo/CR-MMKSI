
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerBranchBusinessArea Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 08/25/2017 - 1:14:34 PM
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

    Public Class DealerBranchBusinessAreaMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerBranchBusinessArea"
        Private m_UpdateStatement As String = "up_UpdateDealerBranchBusinessArea"
        Private m_RetrieveStatement As String = "up_RetrieveDealerBranchBusinessArea"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerBranchBusinessAreaList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerBranchBusinessArea"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerBranchBusinessArea As DealerBranchBusinessArea = Nothing
            While dr.Read

                dealerBranchBusinessArea = Me.CreateObject(dr)

            End While

            Return dealerBranchBusinessArea

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerBranchBusinessAreaList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerBranchBusinessArea As DealerBranchBusinessArea = Me.CreateObject(dr)
                dealerBranchBusinessAreaList.Add(dealerBranchBusinessArea)
            End While

            Return dealerBranchBusinessAreaList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerBranchBusinessArea As DealerBranchBusinessArea = CType(obj, DealerBranchBusinessArea)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerBranchBusinessArea.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerBranchBusinessArea As DealerBranchBusinessArea = CType(obj, DealerBranchBusinessArea)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(dealerBranchBusinessArea.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(dealerBranchBusinessArea.DealerBranch))
            DbCommandWrapper.AddInParameter("@Kind", DbType.AnsiString, dealerBranchBusinessArea.Kind)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, dealerBranchBusinessArea.Title)
            DbCommandWrapper.AddInParameter("@ContactPerson", DbType.AnsiString, dealerBranchBusinessArea.ContactPerson)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, dealerBranchBusinessArea.Email)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, dealerBranchBusinessArea.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, dealerBranchBusinessArea.Fax)
            DbCommandWrapper.AddInParameter("@HP", DbType.AnsiString, dealerBranchBusinessArea.HP)
            DbCommandWrapper.AddInParameter("@DepHeadPIC", DbType.AnsiString, dealerBranchBusinessArea.DepHeadPIC)
            DbCommandWrapper.AddInParameter("@SectionHeadPIC", DbType.AnsiString, dealerBranchBusinessArea.SectionHeadPIC)
            DbCommandWrapper.AddInParameter("@SalesACPIC", DbType.AnsiString, dealerBranchBusinessArea.SalesACPIC)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerBranchBusinessArea.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerBranchBusinessArea.LastUpdateBy)
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

            Dim dealerBranchBusinessArea As DealerBranchBusinessArea = CType(obj, DealerBranchBusinessArea)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerBranchBusinessArea.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(dealerBranchBusinessArea.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(dealerBranchBusinessArea.DealerBranch))
            DbCommandWrapper.AddInParameter("@Kind", DbType.AnsiString, dealerBranchBusinessArea.Kind)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, dealerBranchBusinessArea.Title)
            DbCommandWrapper.AddInParameter("@ContactPerson", DbType.AnsiString, dealerBranchBusinessArea.ContactPerson)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, dealerBranchBusinessArea.Email)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, dealerBranchBusinessArea.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, dealerBranchBusinessArea.Fax)
            DbCommandWrapper.AddInParameter("@HP", DbType.AnsiString, dealerBranchBusinessArea.HP)
            DbCommandWrapper.AddInParameter("@DepHeadPIC", DbType.AnsiString, dealerBranchBusinessArea.DepHeadPIC)
            DbCommandWrapper.AddInParameter("@SectionHeadPIC", DbType.AnsiString, dealerBranchBusinessArea.SectionHeadPIC)
            DbCommandWrapper.AddInParameter("@SalesACPIC", DbType.AnsiString, dealerBranchBusinessArea.SalesACPIC)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerBranchBusinessArea.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerBranchBusinessArea.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerBranchBusinessArea

            Dim dealerBranchBusinessArea As DealerBranchBusinessArea = New DealerBranchBusinessArea

            dealerBranchBusinessArea.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("Kind")) Then dealerBranchBusinessArea.Kind = dr("Kind").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Title")) Then dealerBranchBusinessArea.Title = dr("Title").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContactPerson")) Then dealerBranchBusinessArea.ContactPerson = dr("ContactPerson").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then dealerBranchBusinessArea.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then dealerBranchBusinessArea.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Fax")) Then dealerBranchBusinessArea.Fax = dr("Fax").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HP")) Then dealerBranchBusinessArea.HP = dr("HP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DepHeadPIC")) Then dealerBranchBusinessArea.DepHeadPIC = dr("DepHeadPIC").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SectionHeadPIC")) Then dealerBranchBusinessArea.SectionHeadPIC = dr("SectionHeadPIC").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesACPIC")) Then dealerBranchBusinessArea.SalesACPIC = dr("SalesACPIC").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerBranchBusinessArea.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerBranchBusinessArea.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerBranchBusinessArea.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerBranchBusinessArea.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerBranchBusinessArea.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)


            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerBranchBusinessArea.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                dealerBranchBusinessArea.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If

            Return dealerBranchBusinessArea

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerBranchBusinessArea) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerBranchBusinessArea), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerBranchBusinessArea).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

