
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FactoringMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 10/2/2010 - 2:33:43 PM
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

    Public Class FactoringMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFactoringMaster"
        Private m_UpdateStatement As String = "up_UpdateFactoringMaster"
        Private m_RetrieveStatement As String = "up_RetrieveFactoringMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveFactoringMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFactoringMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim factoringMaster As FactoringMaster = Nothing
            While dr.Read

                factoringMaster = Me.CreateObject(dr)

            End While

            Return factoringMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim factoringMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim factoringMaster As FactoringMaster = Me.CreateObject(dr)
                factoringMasterList.Add(factoringMaster)
            End While

            Return factoringMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim factoringMaster As FactoringMaster = CType(obj, FactoringMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, factoringMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim factoringMaster As FactoringMaster = CType(obj, FactoringMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DBCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, factoringMaster.CreditAccount)
            DBCommandWrapper.AddInParameter("@TotalCeiling", DbType.Currency, factoringMaster.TotalCeiling)
            DBCommandWrapper.AddInParameter("@StandardCeiling", DbType.Currency, factoringMaster.StandardCeiling)
            DbCommandWrapper.AddInParameter("@FactoringCeiling", DbType.Currency, factoringMaster.FactoringCeiling)
            DbCommandWrapper.AddInParameter("@GiroTolakan", DbType.Currency, factoringMaster.GiroTolakan)
            DBCommandWrapper.AddInParameter("@Outstanding", DbType.Currency, factoringMaster.Outstanding)
            DBCommandWrapper.AddInParameter("@AvailableCeiling", DbType.Currency, factoringMaster.AvailableCeiling)
            DBCommandWrapper.AddInParameter("@MaxTOPDate", DbType.DateTime, factoringMaster.MaxTOPDate)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, factoringMaster.Status)
            DBCommandWrapper.AddInParameter("@LastUploadedBy", DbType.AnsiString, factoringMaster.LastUploadedBy)
            DBCommandWrapper.AddInParameter("@LastUploadedTime", DbType.DateTime, factoringMaster.LastUploadedTime)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, factoringMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, factoringMaster.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTi\me,DateTime.Now)

            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(factoringMaster.ProductCategory))

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

            Dim factoringMaster As FactoringMaster = CType(obj, FactoringMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, factoringMaster.ID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, factoringMaster.CreditAccount)
            DBCommandWrapper.AddInParameter("@TotalCeiling", DbType.Currency, factoringMaster.TotalCeiling)
            DBCommandWrapper.AddInParameter("@StandardCeiling", DbType.Currency, factoringMaster.StandardCeiling)
            DBCommandWrapper.AddInParameter("@FactoringCeiling", DbType.Currency, factoringMaster.FactoringCeiling)
            DbCommandWrapper.AddInParameter("@GiroTolakan", DbType.Currency, factoringMaster.GiroTolakan)
            DBCommandWrapper.AddInParameter("@Outstanding", DbType.Currency, factoringMaster.Outstanding)
            DBCommandWrapper.AddInParameter("@AvailableCeiling", DbType.Currency, factoringMaster.AvailableCeiling)
            DBCommandWrapper.AddInParameter("@MaxTOPDate", DbType.DateTime, factoringMaster.MaxTOPDate)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, factoringMaster.Status)
            DBCommandWrapper.AddInParameter("@LastUploadedBy", DbType.AnsiString, factoringMaster.LastUploadedBy)
            DBCommandWrapper.AddInParameter("@LastUploadedTime", DbType.DateTime, factoringMaster.LastUploadedTime)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, factoringMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, factoringMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(factoringMaster.ProductCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FactoringMaster

            Dim factoringMaster As FactoringMaster = New FactoringMaster

            factoringMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then factoringMaster.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalCeiling")) Then factoringMaster.TotalCeiling = CType(dr("TotalCeiling"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("StandardCeiling")) Then factoringMaster.StandardCeiling = CType(dr("StandardCeiling"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FactoringCeiling")) Then factoringMaster.FactoringCeiling = CType(dr("FactoringCeiling"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("GiroTolakan")) Then factoringMaster.GiroTolakan = CType(dr("GiroTolakan"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Outstanding")) Then factoringMaster.Outstanding = CType(dr("Outstanding"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AvailableCeiling")) Then factoringMaster.AvailableCeiling = CType(dr("AvailableCeiling"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDate")) Then factoringMaster.MaxTOPDate = CType(dr("MaxTOPDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then factoringMaster.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUploadedBy")) Then factoringMaster.LastUploadedBy = dr("LastUploadedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUploadedTime")) Then factoringMaster.LastUploadedTime = CType(dr("LastUploadedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then factoringMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then factoringMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then factoringMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then factoringMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then factoringMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                factoringMaster.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Short))
            End If
            Return factoringMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(FactoringMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FactoringMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FactoringMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

