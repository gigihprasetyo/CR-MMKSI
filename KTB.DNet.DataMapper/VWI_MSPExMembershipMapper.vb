
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : MSPExMembership Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/03/2018 - 14:31:49
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

    Public Class VWI_MSPExMembershipMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMSPExMembership"
        Private m_UpdateStatement As String = "up_UpdateMSPExMembership"
        Private m_RetrieveStatement As String = "up_RetrieveMSPExMembership"
        Private m_RetrieveListStatement As String = "up_RetrieveMSPExMembershipList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMSPExMembership"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mSPExMembership As VWI_MSPExMembership = Nothing
            While dr.Read

                mSPExMembership = Me.CreateObject(dr)

            End While

            Return mSPExMembership

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mSPExMembershipList As ArrayList = New ArrayList

            While dr.Read
                Dim mSPExMembership As VWI_MSPExMembership = Me.CreateObject(dr)
                mSPExMembershipList.Add(mSPExMembership)
            End While

            Return mSPExMembershipList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExMembership As VWI_MSPExMembership = CType(obj, VWI_MSPExMembership)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, mSPExMembership.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExMembership As VWI_MSPExMembership = CType(obj, VWI_MSPExMembership)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DbCommandWrapper.AddInParameter("@MSPCustomerID", DbType.Int32, mSPExMembership.MSPCustomerID)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int32, mSPExMembership.DealerId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, mSPExMembership.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, mSPExMembership.DealerName)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, mSPExMembership.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@MSPCode", DbType.AnsiString, mSPExMembership.MSPCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, mSPExMembership.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, mSPExMembership.ColorCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, mSPExMembership.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, mSPExMembership.VehicleTypeDesc)
            DbCommandWrapper.AddInParameter("@MSPKm", DbType.Int32, mSPExMembership.MSPKm)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, mSPExMembership.Duration)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, mSPExMembership.Description)
            DbCommandWrapper.AddInParameter("@ValidUntil", DbType.DateTime, mSPExMembership.ValidUntil)
            DbCommandWrapper.AddInParameter("@RegistrationDate", DbType.DateTime, mSPExMembership.RegistrationDate)


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

            Dim mSPExMembership As VWI_MSPExMembership = CType(obj, VWI_MSPExMembership)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, mSPExMembership.ID)
            DbCommandWrapper.AddInParameter("@MSPCustomerID", DbType.Int32, mSPExMembership.MSPCustomerID)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int32, mSPExMembership.DealerId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, mSPExMembership.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, mSPExMembership.DealerName)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, mSPExMembership.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@MSPCode", DbType.AnsiString, mSPExMembership.MSPCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, mSPExMembership.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, mSPExMembership.ColorCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, mSPExMembership.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, mSPExMembership.VehicleTypeDesc)
            DbCommandWrapper.AddInParameter("@MSPKm", DbType.Int32, mSPExMembership.MSPKm)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, mSPExMembership.Duration)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, mSPExMembership.Description)
            DbCommandWrapper.AddInParameter("@ValidUntil", DbType.DateTime, mSPExMembership.ValidUntil)
            DbCommandWrapper.AddInParameter("@RegistrationDate", DbType.DateTime, mSPExMembership.RegistrationDate)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_MSPExMembership

            Dim mSPExMembership As VWI_MSPExMembership = New VWI_MSPExMembership

            If Not dr.IsDBNull(dr.GetOrdinal("MSPCustomerID")) Then mSPExMembership.MSPCustomerID = CType(dr("MSPCustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then mSPExMembership.DealerId = CType(dr("DealerId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then mSPExMembership.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then mSPExMembership.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then mSPExMembership.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MSPCode")) Then mSPExMembership.MSPCode = dr("MSPCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then mSPExMembership.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorCode")) Then mSPExMembership.ColorCode = dr("ColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then mSPExMembership.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeDesc")) Then mSPExMembership.VehicleTypeDesc = dr("VehicleTypeDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MSPKm")) Then mSPExMembership.MSPKm = CType(dr("MSPKm"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Duration")) Then mSPExMembership.Duration = CType(dr("Duration"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then mSPExMembership.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidUntil")) Then mSPExMembership.ValidUntil = CType(dr("ValidUntil"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RegistrationDate")) Then mSPExMembership.RegistrationDate = CType(dr("RegistrationDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mSPExMembership.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return mSPExMembership

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_MSPExMembership) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_MSPExMembership), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_MSPExMembership).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

