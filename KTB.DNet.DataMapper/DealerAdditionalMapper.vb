#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerAdditional Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 22/08/2007 - 16:09:31
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

    Public Class DealerAdditionalMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerAdditional"
        Private m_UpdateStatement As String = "up_UpdateDealerAdditional"
        Private m_RetrieveStatement As String = "up_RetrieveDealerAdditional"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerAdditionalList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerAdditional"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerAdditional As DealerAdditional = Nothing
            While dr.Read

                dealerAdditional = Me.CreateObject(dr)

            End While

            Return dealerAdditional

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerAdditionalList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerAdditional As DealerAdditional = Me.CreateObject(dr)
                dealerAdditionalList.Add(dealerAdditional)
            End While

            Return dealerAdditionalList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerAdditional As DealerAdditional = CType(obj, DealerAdditional)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerAdditional.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerAdditional As DealerAdditional = CType(obj, DealerAdditional)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@ClaimETA", DbType.Int32, dealerAdditional.ClaimETA)
            DbCommandWrapper.AddInParameter("@ShowroomFile", DbType.AnsiString, dealerAdditional.ShowroomFile)
            DbCommandWrapper.AddInParameter("@StuctureFile", DbType.AnsiString, dealerAdditional.StuctureFile)
            DbCommandWrapper.AddInParameter("@SalesForceFile", DbType.AnsiString, dealerAdditional.SalesForceFile)
            DbCommandWrapper.AddInParameter("@Classification", DbType.AnsiString, dealerAdditional.Classification)
            DBCommandWrapper.AddInParameter("@HeldYear", DbType.Int32, dealerAdditional.HeldYear)
            DBCommandWrapper.AddInParameter("@SparePartGrade", DbType.AnsiString, dealerAdditional.SparePartGrade)
            DbCommandWrapper.AddInParameter("@EquipmentClass", DbType.Int16, dealerAdditional.EquipmentClass)
            DbCommandWrapper.AddInParameter("@DealerFacility", DbType.Int16, dealerAdditional.DealerFacility)
            DbCommandWrapper.AddInParameter("@DealerStallEquipment", DbType.Int16, dealerAdditional.DealerStallEquipment)
            DbCommandWrapper.AddInParameter("@ServiceGrade", DbType.Int16, dealerAdditional.ServiceGrade)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerAdditional.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerAdditional.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerAdditional.Dealer))

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

            Dim dealerAdditional As DealerAdditional = CType(obj, DealerAdditional)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerAdditional.ID)
            DBCommandWrapper.AddInParameter("@ClaimETA", DbType.Int32, dealerAdditional.ClaimETA)
            DbCommandWrapper.AddInParameter("@ShowroomFile", DbType.AnsiString, dealerAdditional.ShowroomFile)
            DbCommandWrapper.AddInParameter("@StuctureFile", DbType.AnsiString, dealerAdditional.StuctureFile)
            DbCommandWrapper.AddInParameter("@SalesForceFile", DbType.AnsiString, dealerAdditional.SalesForceFile)
            DbCommandWrapper.AddInParameter("@Classification", DbType.AnsiString, dealerAdditional.Classification)
            DBCommandWrapper.AddInParameter("@HeldYear", DbType.Int32, dealerAdditional.HeldYear)
            DbCommandWrapper.AddInParameter("@SparePartGrade", DbType.AnsiString, dealerAdditional.SparePartGrade)
            DbCommandWrapper.AddInParameter("@EquipmentClass", DbType.Int16, dealerAdditional.EquipmentClass)
            DbCommandWrapper.AddInParameter("@DealerFacility", DbType.Int16, dealerAdditional.DealerFacility)
            DbCommandWrapper.AddInParameter("@DealerStallEquipment", DbType.Int16, dealerAdditional.DealerStallEquipment)
            DbCommandWrapper.AddInParameter("@ServiceGrade", DbType.Int16, dealerAdditional.ServiceGrade)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerAdditional.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerAdditional.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerAdditional.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerAdditional

            Dim dealerAdditional As DealerAdditional = New DealerAdditional

            dealerAdditional.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimETA")) Then dealerAdditional.ClaimETA = CType(dr("ClaimETA"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ShowroomFile")) Then dealerAdditional.ShowroomFile = dr("ShowroomFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StuctureFile")) Then dealerAdditional.StuctureFile = dr("StuctureFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesForceFile")) Then dealerAdditional.SalesForceFile = dr("SalesForceFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Classification")) Then dealerAdditional.Classification = dr("Classification").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HeldYear")) Then dealerAdditional.HeldYear = CType(dr("HeldYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartGrade")) Then dealerAdditional.SparePartGrade = dr("SparePartGrade").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EquipmentClass")) Then dealerAdditional.EquipmentClass = CType(dr("EquipmentClass"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerFacility")) Then dealerAdditional.DealerFacility = CType(dr("DealerFacility"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerStallEquipment")) Then dealerAdditional.DealerStallEquipment = CType(dr("DealerStallEquipment"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceGrade")) Then dealerAdditional.ServiceGrade = CType(dr("ServiceGrade"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerAdditional.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerAdditional.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerAdditional.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerAdditional.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerAdditional.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerAdditional.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return dealerAdditional

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerAdditional) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerAdditional), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerAdditional).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

