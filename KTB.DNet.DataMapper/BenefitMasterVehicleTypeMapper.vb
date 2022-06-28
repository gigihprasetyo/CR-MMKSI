
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitMasterVehicleType Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/11/2015 - 8:53:41 AM
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

    Public Class BenefitMasterVehicleTypeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitMasterVehicleType"
        Private m_UpdateStatement As String = "up_UpdateBenefitMasterVehicleType"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitMasterVehicleType"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitMasterVehicleTypeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitMasterVehicleType"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitMasterVehicleType As BenefitMasterVehicleType = Nothing
            While dr.Read

                benefitMasterVehicleType = Me.CreateObject(dr)

            End While

            Return benefitMasterVehicleType

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitMasterVehicleTypeList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitMasterVehicleType As BenefitMasterVehicleType = Me.CreateObject(dr)
                benefitMasterVehicleTypeList.Add(benefitMasterVehicleType)
            End While

            Return benefitMasterVehicleTypeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitMasterVehicleType As BenefitMasterVehicleType = CType(obj, BenefitMasterVehicleType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitMasterVehicleType.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitMasterVehicleType As BenefitMasterVehicleType = CType(obj, BenefitMasterVehicleType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitMasterVehicleType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitMasterVehicleType.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(benefitMasterVehicleType.VechileType))
            DbCommandWrapper.AddInParameter("@BenefitMasterDetailID", DbType.Int32, Me.GetRefObject(benefitMasterVehicleType.BenefitMasterDetail))

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

            Dim benefitMasterVehicleType As BenefitMasterVehicleType = CType(obj, BenefitMasterVehicleType)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitMasterVehicleType.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitMasterVehicleType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, benefitMasterVehicleType.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(benefitMasterVehicleType.VechileType))
            DbCommandWrapper.AddInParameter("@BenefitMasterDetailID", DbType.Int32, Me.GetRefObject(benefitMasterVehicleType.BenefitMasterDetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitMasterVehicleType

            Dim benefitMasterVehicleType As BenefitMasterVehicleType = New BenefitMasterVehicleType

            benefitMasterVehicleType.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitMasterVehicleType.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitMasterVehicleType.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitMasterVehicleType.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitMasterVehicleType.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitMasterVehicleType.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                benefitMasterVehicleType.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitMasterDetailID")) Then
                benefitMasterVehicleType.BenefitMasterDetail = New BenefitMasterDetail(CType(dr("BenefitMasterDetailID"), Integer))
            End If

            Return benefitMasterVehicleType

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitMasterVehicleType) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitMasterVehicleType), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitMasterVehicleType).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

