#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PartIncidentalHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 1/16/2006 - 2:39:56 PM
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

    Public Class PartIncidentalHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPartIncidentalHeader"
        Private m_UpdateStatement As String = "up_UpdatePartIncidentalHeader"
        Private m_RetrieveStatement As String = "up_RetrievePartIncidentalHeader"
        Private m_RetrieveListStatement As String = "up_RetrievePartIncidentalHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePartIncidentalHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim partIncidentalHeader As PartIncidentalHeader = Nothing
            While dr.Read

                partIncidentalHeader = Me.CreateObject(dr)

            End While

            Return partIncidentalHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim partIncidentalHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim partIncidentalHeader As PartIncidentalHeader = Me.CreateObject(dr)
                partIncidentalHeaderList.Add(partIncidentalHeader)
            End While

            Return partIncidentalHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partIncidentalHeader As PartIncidentalHeader = CType(obj, PartIncidentalHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, partIncidentalHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partIncidentalHeader As PartIncidentalHeader = CType(obj, PartIncidentalHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RequestNumber", DbType.AnsiString, partIncidentalHeader.RequestNumber)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiStringFixedLength, partIncidentalHeader.Phone)
            DbCommandWrapper.AddInParameter("@PoliceNumber", DbType.AnsiString, partIncidentalHeader.PoliceNumber)
            DbCommandWrapper.AddInParameter("@WorkOrder", DbType.AnsiString, partIncidentalHeader.WorkOrder)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, partIncidentalHeader.Status)
            DbCommandWrapper.AddInParameter("@PIC", DbType.AnsiString, partIncidentalHeader.PIC)
            DbCommandWrapper.AddInParameter("@EmailStatus", DbType.Int32, partIncidentalHeader.EmailStatus)
            DbCommandWrapper.AddInParameter("@KTBRemark", DbType.AnsiString, partIncidentalHeader.KTBRemark)
            DbCommandWrapper.AddInParameter("@KTBStatus", DbType.Int32, partIncidentalHeader.KTBStatus)
            DbCommandWrapper.AddInParameter("@DealerMailNumber", DbType.AnsiString, partIncidentalHeader.DealerMailNumber)
            DBCommandWrapper.AddInParameter("@IncidentalDate", DbType.DateTime, partIncidentalHeader.IncidentalDate)

            DBCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, partIncidentalHeader.ChassisNumber)
            DBCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, partIncidentalHeader.VehicleType)
            DBCommandWrapper.AddInParameter("@AssemblyYear", DbType.AnsiString, partIncidentalHeader.AssemblyYear)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partIncidentalHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, partIncidentalHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(partIncidentalHeader.Dealer))

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

            Dim partIncidentalHeader As PartIncidentalHeader = CType(obj, PartIncidentalHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, partIncidentalHeader.ID)
            DbCommandWrapper.AddInParameter("@RequestNumber", DbType.AnsiString, partIncidentalHeader.RequestNumber)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiStringFixedLength, partIncidentalHeader.Phone)
            DbCommandWrapper.AddInParameter("@PoliceNumber", DbType.AnsiString, partIncidentalHeader.PoliceNumber)
            DbCommandWrapper.AddInParameter("@WorkOrder", DbType.AnsiString, partIncidentalHeader.WorkOrder)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, partIncidentalHeader.Status)
            DbCommandWrapper.AddInParameter("@PIC", DbType.AnsiString, partIncidentalHeader.PIC)
            DbCommandWrapper.AddInParameter("@EmailStatus", DbType.Int32, partIncidentalHeader.EmailStatus)
            DbCommandWrapper.AddInParameter("@KTBRemark", DbType.AnsiString, partIncidentalHeader.KTBRemark)
            DbCommandWrapper.AddInParameter("@KTBStatus", DbType.Int32, partIncidentalHeader.KTBStatus)
            DbCommandWrapper.AddInParameter("@DealerMailNumber", DbType.AnsiString, partIncidentalHeader.DealerMailNumber)
            DBCommandWrapper.AddInParameter("@IncidentalDate", DbType.DateTime, partIncidentalHeader.IncidentalDate)

            DBCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, partIncidentalHeader.ChassisNumber)
            DBCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, partIncidentalHeader.VehicleType)
            DBCommandWrapper.AddInParameter("@AssemblyYear", DbType.AnsiString, partIncidentalHeader.AssemblyYear)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partIncidentalHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, partIncidentalHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(partIncidentalHeader.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PartIncidentalHeader

            Dim partIncidentalHeader As PartIncidentalHeader = New PartIncidentalHeader

            partIncidentalHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestNumber")) Then partIncidentalHeader.RequestNumber = dr("RequestNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then partIncidentalHeader.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PoliceNumber")) Then partIncidentalHeader.PoliceNumber = dr("PoliceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrder")) Then partIncidentalHeader.WorkOrder = dr("WorkOrder").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then partIncidentalHeader.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PIC")) Then partIncidentalHeader.PIC = dr("PIC").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EmailStatus")) Then partIncidentalHeader.EmailStatus = CType(dr("EmailStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KTBRemark")) Then partIncidentalHeader.KTBRemark = dr("KTBRemark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KTBStatus")) Then partIncidentalHeader.KTBStatus = CType(dr("KTBStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerMailNumber")) Then partIncidentalHeader.DealerMailNumber = dr("DealerMailNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IncidentalDate")) Then partIncidentalHeader.IncidentalDate = CType(dr("IncidentalDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then partIncidentalHeader.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleType")) Then partIncidentalHeader.VehicleType = dr("VehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AssemblyYear")) Then partIncidentalHeader.AssemblyYear = dr("AssemblyYear").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then partIncidentalHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then partIncidentalHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then partIncidentalHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then partIncidentalHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then partIncidentalHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                partIncidentalHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return partIncidentalHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(PartIncidentalHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PartIncidentalHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PartIncidentalHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

