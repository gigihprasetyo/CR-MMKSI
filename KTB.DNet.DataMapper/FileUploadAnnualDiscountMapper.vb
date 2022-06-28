#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FileUploadAnnualDiscount Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 12/12/2005 - 10:57:04 AM
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

    Public Class FileUploadAnnualDiscountMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFileUploadAnnualDiscount"
        Private m_UpdateStatement As String = "up_UpdateFileUploadAnnualDiscount"
        Private m_RetrieveStatement As String = "up_RetrieveFileUploadAnnualDiscount"
        Private m_RetrieveListStatement As String = "up_RetrieveFileUploadAnnualDiscountList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFileUploadAnnualDiscount"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim fileUploadAnnualDiscount As FileUploadAnnualDiscount = Nothing
            While dr.Read

                fileUploadAnnualDiscount = Me.CreateObject(dr)

            End While

            Return fileUploadAnnualDiscount

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim fileUploadAnnualDiscountList As ArrayList = New ArrayList

            While dr.Read
                Dim fileUploadAnnualDiscount As FileUploadAnnualDiscount = Me.CreateObject(dr)
                fileUploadAnnualDiscountList.Add(fileUploadAnnualDiscount)
            End While

            Return fileUploadAnnualDiscountList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fileUploadAnnualDiscount As FileUploadAnnualDiscount = CType(obj, FileUploadAnnualDiscount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fileUploadAnnualDiscount.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fileUploadAnnualDiscount As FileUploadAnnualDiscount = CType(obj, FileUploadAnnualDiscount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ProgramName", DbType.AnsiString, fileUploadAnnualDiscount.ProgramName)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, fileUploadAnnualDiscount.FileName)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, fileUploadAnnualDiscount.Remark)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.Int16, fileUploadAnnualDiscount.Tipe)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fileUploadAnnualDiscount.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, fileUploadAnnualDiscount.LastUpdateBy)
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

            Dim fileUploadAnnualDiscount As FileUploadAnnualDiscount = CType(obj, FileUploadAnnualDiscount)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fileUploadAnnualDiscount.ID)
            DbCommandWrapper.AddInParameter("@ProgramName", DbType.AnsiString, fileUploadAnnualDiscount.ProgramName)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, fileUploadAnnualDiscount.FileName)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, fileUploadAnnualDiscount.Remark)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.Int16, fileUploadAnnualDiscount.Tipe)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fileUploadAnnualDiscount.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fileUploadAnnualDiscount.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FileUploadAnnualDiscount

            Dim fileUploadAnnualDiscount As FileUploadAnnualDiscount = New FileUploadAnnualDiscount

            fileUploadAnnualDiscount.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProgramName")) Then fileUploadAnnualDiscount.ProgramName = dr("ProgramName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then fileUploadAnnualDiscount.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then fileUploadAnnualDiscount.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Tipe")) Then fileUploadAnnualDiscount.Tipe = CType(dr("Tipe"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fileUploadAnnualDiscount.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fileUploadAnnualDiscount.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fileUploadAnnualDiscount.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then fileUploadAnnualDiscount.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then fileUploadAnnualDiscount.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return fileUploadAnnualDiscount

        End Function

        Private Sub SetTableName()

            If Not (GetType(FileUploadAnnualDiscount) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FileUploadAnnualDiscount), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FileUploadAnnualDiscount).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

