
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExRegistration Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2020 - 12:14:48 PM
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

    Public Class MSPExRegistrationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMSPExRegistration"
        Private m_UpdateStatement As String = "up_UpdateMSPExRegistration"
        Private m_RetrieveStatement As String = "up_RetrieveMSPExRegistration"
        Private m_RetrieveListStatement As String = "up_RetrieveMSPExRegistrationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMSPExRegistration"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mSPExRegistration As MSPExRegistration = Nothing
            While dr.Read

                mSPExRegistration = Me.CreateObject(dr)

            End While

            Return mSPExRegistration

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mSPExRegistrationList As ArrayList = New ArrayList

            While dr.Read
                Dim mSPExRegistration As MSPExRegistration = Me.CreateObject(dr)
                mSPExRegistrationList.Add(mSPExRegistration)
            End While

            Return mSPExRegistrationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExRegistration As MSPExRegistration = CType(obj, MSPExRegistration)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExRegistration.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExRegistration As MSPExRegistration = CType(obj, MSPExRegistration)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(mSPExRegistration.Dealer))
            DbCommandWrapper.AddInParameter("@MSPCustomerID", DbType.Int32, Me.GetRefObject(mSPExRegistration.MSPCustomer))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(mSPExRegistration.ChassisMaster))
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, mSPExRegistration.DealerID)
            'DbCommandWrapper.AddInParameter("@MSPCustomerID", DbType.Int32, mSPExRegistration.MSPCustomerID)
            'DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, mSPExRegistration.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, mSPExRegistration.MileAge)
            DbCommandWrapper.AddInParameter("@MSPExMasterID", DbType.Int32, Me.GetRefObject(mSPExRegistration.MSPExMaster))
            'DbCommandWrapper.AddInParameter("@MSPExMasterID", DbType.Int32, mSPExRegistration.MSPExMasterID)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, mSPExRegistration.RegNumber)
            DbCommandWrapper.AddInParameter("@ValidDateTo", DbType.DateTime, mSPExRegistration.ValidDateTo)
            DbCommandWrapper.AddInParameter("@ValidKMTo", DbType.Int32, mSPExRegistration.ValidKMTo)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mSPExRegistration.Status)
            DbCommandWrapper.AddInParameter("@IsTransfertoSAP", DbType.Int16, mSPExRegistration.IsTransfertoSAP)
            DbCommandWrapper.AddInParameter("@WarrantyValidDateTo", DbType.DateTime, mSPExRegistration.WarrantyValidDateTo)
            DbCommandWrapper.AddInParameter("@WarrantyValidKMTo", DbType.Int32, mSPExRegistration.WarrantyValidKMTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPExRegistration.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, mSPExRegistration.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@Prefix", DbType.AnsiString, mSPExRegistration.Prefix)

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

            Dim mSPExRegistration As MSPExRegistration = CType(obj, MSPExRegistration)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExRegistration.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(mSPExRegistration.Dealer))
            DbCommandWrapper.AddInParameter("@MSPCustomerID", DbType.Int32, Me.GetRefObject(mSPExRegistration.MSPCustomer))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(mSPExRegistration.ChassisMaster))
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, mSPExRegistration.DealerID)
            'DbCommandWrapper.AddInParameter("@MSPCustomerID", DbType.Int32, mSPExRegistration.MSPCustomerID)
            'DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, mSPExRegistration.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, mSPExRegistration.MileAge)
            DbCommandWrapper.AddInParameter("@MSPExMasterID", DbType.Int32, Me.GetRefObject(mSPExRegistration.MSPExMaster))
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, mSPExRegistration.RegNumber)
            'DbCommandWrapper.AddInParameter("@MSPExMasterID", DbType.Int32, mSPExRegistration.MSPExMasterID)
            DbCommandWrapper.AddInParameter("@ValidDateTo", DbType.DateTime, mSPExRegistration.ValidDateTo)
            DbCommandWrapper.AddInParameter("@ValidKMTo", DbType.Int32, mSPExRegistration.ValidKMTo)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mSPExRegistration.Status)
            DbCommandWrapper.AddInParameter("@IsTransfertoSAP", DbType.Int16, mSPExRegistration.IsTransfertoSAP)
            DbCommandWrapper.AddInParameter("@WarrantyValidDateTo", DbType.DateTime, mSPExRegistration.WarrantyValidDateTo)
            DbCommandWrapper.AddInParameter("@WarrantyValidKMTo", DbType.Int32, mSPExRegistration.WarrantyValidKMTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPExRegistration.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mSPExRegistration.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MSPExRegistration

            Dim mSPExRegistration As MSPExRegistration = New MSPExRegistration

            mSPExRegistration.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then mSPExRegistration.DealerID = CType(dr("DealerID"), Short)
            'If Not dr.IsDBNull(dr.GetOrdinal("MSPCustomerID")) Then mSPExRegistration.MSPCustomerID = CType(dr("MSPCustomerID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then mSPExRegistration.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MileAge")) Then mSPExRegistration.MileAge = CType(dr("MileAge"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("MSPExMasterID")) Then mSPExRegistration.MSPExMasterID = CType(dr("MSPExMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then mSPExRegistration.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidDateTo")) Then mSPExRegistration.ValidDateTo = CType(dr("ValidDateTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidKMTo")) Then mSPExRegistration.ValidKMTo = CType(dr("ValidKMTo"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then mSPExRegistration.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsTransfertoSAP")) Then mSPExRegistration.IsTransfertoSAP = CType(dr("IsTransfertoSAP"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("WarrantyValidDateTo")) Then mSPExRegistration.WarrantyValidDateTo = CType(dr("WarrantyValidDateTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WarrantyValidKMTo")) Then mSPExRegistration.WarrantyValidKMTo = CType(dr("WarrantyValidKMTo"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mSPExRegistration.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mSPExRegistration.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mSPExRegistration.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then mSPExRegistration.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mSPExRegistration.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                mSPExRegistration.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("MSPCustomerID")) Then
                mSPExRegistration.MSPCustomer = New MSPCustomer(CType(dr("MSPCustomerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                mSPExRegistration.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("MSPExMasterID")) Then
                mSPExRegistration.MSPExMaster = New MSPExMaster(CType(dr("MSPExMasterID"), Integer))
            End If


            Return mSPExRegistration

        End Function

        Private Sub SetTableName()

            If Not (GetType(MSPExRegistration) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MSPExRegistration), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MSPExRegistration).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

