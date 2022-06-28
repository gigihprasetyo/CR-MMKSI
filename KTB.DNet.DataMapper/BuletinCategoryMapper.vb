
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BuletinCategory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/5/2008 - 2:24:24 PM
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

    Public Class BuletinCategoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBuletinCategory"
        Private m_UpdateStatement As String = "up_UpdateBuletinCategory"
        Private m_RetrieveStatement As String = "up_RetrieveBuletinCategory"
        Private m_RetrieveListStatement As String = "up_RetrieveBuletinCategoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBuletinCategory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim buletinCategory As BuletinCategory = Nothing
            While dr.Read

                buletinCategory = Me.CreateObject(dr)

            End While

            Return buletinCategory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim buletinCategoryList As ArrayList = New ArrayList

            While dr.Read
                Dim buletinCategory As BuletinCategory = Me.CreateObject(dr)
                buletinCategoryList.Add(buletinCategory)
            End While

            Return buletinCategoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim buletinCategory As BuletinCategory = CType(obj, BuletinCategory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, buletinCategory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim buletinCategory As BuletinCategory = CType(obj, BuletinCategory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, buletinCategory.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, buletinCategory.Description)
            DbCommandWrapper.AddInParameter("@Parent", DbType.Int32, buletinCategory.Parent)
            DbCommandWrapper.AddInParameter("@TopParent", DbType.Int32, buletinCategory.TopParent)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, buletinCategory.Status)
            DbCommandWrapper.AddInParameter("@Leveling", DbType.Byte, buletinCategory.Leveling)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, buletinCategory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, buletinCategory.LastUpdateBy)
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

            Dim buletinCategory As BuletinCategory = CType(obj, BuletinCategory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, buletinCategory.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, buletinCategory.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, buletinCategory.Description)
            DbCommandWrapper.AddInParameter("@Parent", DbType.Int32, buletinCategory.Parent)
            DbCommandWrapper.AddInParameter("@TopParent", DbType.Int32, buletinCategory.TopParent)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, buletinCategory.Status)
            DbCommandWrapper.AddInParameter("@Leveling", DbType.Byte, buletinCategory.Leveling)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, buletinCategory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, buletinCategory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BuletinCategory

            Dim buletinCategory As BuletinCategory = New BuletinCategory

            buletinCategory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then buletinCategory.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then buletinCategory.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Parent")) Then buletinCategory.Parent = CType(dr("Parent"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TopParent")) Then buletinCategory.TopParent = CType(dr("TopParent"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then buletinCategory.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Leveling")) Then buletinCategory.Leveling = CType(dr("Leveling"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then buletinCategory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then buletinCategory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then buletinCategory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then buletinCategory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then buletinCategory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return buletinCategory

        End Function

        Private Sub SetTableName()

            If Not (GetType(BuletinCategory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BuletinCategory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BuletinCategory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

