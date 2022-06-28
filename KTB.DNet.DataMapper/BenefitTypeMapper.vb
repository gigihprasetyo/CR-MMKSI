
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitType Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 10:49:13 AM
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

    Public Class BenefitTypeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitType"
        Private m_UpdateStatement As String = "up_UpdateBenefitType"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitType"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitTypeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitType"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitType As BenefitType = Nothing
            While dr.Read

                benefitType = Me.CreateObject(dr)

            End While

            Return benefitType

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitTypeList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitType As BenefitType = Me.CreateObject(dr)
                benefitTypeList.Add(benefitType)
            End While

            Return benefitTypeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitType As BenefitType = CType(obj, BenefitType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, benefitType.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitType As BenefitType = CType(obj, BenefitType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, benefitType.Name)
            DbCommandWrapper.AddInParameter("@LeasingBox", DbType.Int16, benefitType.LeasingBox)
            DbCommandWrapper.AddInParameter("@AssyYearBox", DbType.Int16, benefitType.AssyYearBox)
            DbCommandWrapper.AddInParameter("@ReceiptBox", DbType.Int16, benefitType.ReceiptBox)
            DbCommandWrapper.AddInParameter("@EventValidation", DbType.Int16, benefitType.EventValidation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitType.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@WSDiscount", DbType.Int16, benefitType.WSDiscount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, benefitType.Status)

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

            Dim benefitType As BenefitType = CType(obj, BenefitType)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, benefitType.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, benefitType.Name)
            DbCommandWrapper.AddInParameter("@LeasingBox", DbType.Int16, benefitType.LeasingBox)
            DbCommandWrapper.AddInParameter("@AssyYearBox", DbType.Int16, benefitType.AssyYearBox)
            DbCommandWrapper.AddInParameter("@ReceiptBox", DbType.Int16, benefitType.ReceiptBox)
            DbCommandWrapper.AddInParameter("@EventValidation", DbType.Int16, benefitType.EventValidation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, benefitType.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@WSDiscount", DbType.Int16, benefitType.WSDiscount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, benefitType.Status)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitType

            Dim benefitType As BenefitType = New BenefitType

            benefitType.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then benefitType.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LeasingBox")) Then benefitType.LeasingBox = CType(dr("LeasingBox"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AssyYearBox")) Then benefitType.AssyYearBox = CType(dr("AssyYearBox"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptBox")) Then benefitType.ReceiptBox = CType(dr("ReceiptBox"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("EventValidation")) Then benefitType.EventValidation = CType(dr("EventValidation"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitType.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitType.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitType.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitType.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitType.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("WSDiscount")) Then benefitType.WSDiscount = CType(dr("WSDiscount"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then benefitType.Status = CType(dr("Status"), Short)
            Return benefitType

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitType) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitType), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitType).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

