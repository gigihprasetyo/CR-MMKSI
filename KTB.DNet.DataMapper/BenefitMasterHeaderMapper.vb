
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitMasterHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/11/2015 - 8:51:29 AM
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

    Public Class BenefitMasterHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitMasterHeader"
        Private m_UpdateStatement As String = "up_UpdateBenefitMasterHeader"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitMasterHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitMasterHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitMasterHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitMasterHeader As BenefitMasterHeader = Nothing
            While dr.Read

                benefitMasterHeader = Me.CreateObject(dr)

            End While

            Return benefitMasterHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitMasterHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitMasterHeader As BenefitMasterHeader = Me.CreateObject(dr)
                benefitMasterHeaderList.Add(benefitMasterHeader)
            End While

            Return benefitMasterHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitMasterHeader As BenefitMasterHeader = CType(obj, BenefitMasterHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitMasterHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitMasterHeader As BenefitMasterHeader = CType(obj, BenefitMasterHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@NomorSurat", DbType.AnsiString, benefitMasterHeader.NomorSurat)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, benefitMasterHeader.Status)
            DbCommandWrapper.AddInParameter("@BenefitRegNo", DbType.AnsiString, benefitMasterHeader.BenefitRegNo)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, benefitMasterHeader.Remarks)
            DbCommandWrapper.AddInParameter("@Formula", DbType.String, benefitMasterHeader.Formula)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitMasterHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitMasterHeader.LastUpdateBy)
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

            Dim benefitMasterHeader As BenefitMasterHeader = CType(obj, BenefitMasterHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitMasterHeader.ID)
            DbCommandWrapper.AddInParameter("@NomorSurat", DbType.AnsiString, benefitMasterHeader.NomorSurat)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, benefitMasterHeader.Status)
            DbCommandWrapper.AddInParameter("@BenefitRegNo", DbType.AnsiString, benefitMasterHeader.BenefitRegNo)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, benefitMasterHeader.Remarks)
            DbCommandWrapper.AddInParameter("@Formula", DbType.String, benefitMasterHeader.Formula)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitMasterHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, benefitMasterHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitMasterHeader

            Dim benefitMasterHeader As BenefitMasterHeader = New BenefitMasterHeader

            benefitMasterHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NomorSurat")) Then benefitMasterHeader.NomorSurat = dr("NomorSurat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then benefitMasterHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitRegNo")) Then benefitMasterHeader.BenefitRegNo = dr("BenefitRegNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Remarks")) Then benefitMasterHeader.Remarks = dr("Remarks").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Formula")) Then benefitMasterHeader.Formula = dr("Formula").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitMasterHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitMasterHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitMasterHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitMasterHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitMasterHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return benefitMasterHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitMasterHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitMasterHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitMasterHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

