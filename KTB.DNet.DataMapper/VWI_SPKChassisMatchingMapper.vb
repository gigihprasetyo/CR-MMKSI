
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_SPKChassisMatching Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 17/07/2018 - 8:55:52
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

    Public Class VWI_SPKChassisMatchingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_SPKChassisMatching"
        Private m_UpdateStatement As String = "up_UpdateVWI_SPKChassisMatching"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_SPKChassisMatching"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_SPKChassisMatchingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_SPKChassisMatching"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_SPKChassisMatching As VWI_SPKChassisMatching = Nothing
            While dr.Read

                vWI_SPKChassisMatching = Me.CreateObject(dr)

            End While

            Return vWI_SPKChassisMatching

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_SPKChassisMatchingList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_SPKChassisMatching As VWI_SPKChassisMatching = Me.CreateObject(dr)
                vWI_SPKChassisMatchingList.Add(vWI_SPKChassisMatching)
            End While

            Return vWI_SPKChassisMatchingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_SPKChassisMatching As VWI_SPKChassisMatching = CType(obj, VWI_SPKChassisMatching)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_SPKChassisMatching.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_SPKChassisMatching As VWI_SPKChassisMatching = CType(obj, VWI_SPKChassisMatching)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_SPKChassisMatching.DealerCode)
            DbCommandWrapper.AddInParameter("@MatchingDate", DbType.DateTime, vWI_SPKChassisMatching.MatchingDate)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, vWI_SPKChassisMatching.CustomerCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, vWI_SPKChassisMatching.Name)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, vWI_SPKChassisMatching.SPKNumber)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vWI_SPKChassisMatching.ChassisNumber)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, vWI_SPKChassisMatching.EngineNumber)
            DbCommandWrapper.AddInParameter("@KeyNumber", DbType.AnsiString, vWI_SPKChassisMatching.KeyNumber)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, vWI_SPKChassisMatching.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, vWI_SPKChassisMatching.ColorCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vWI_SPKChassisMatching.Description)
            DbCommandWrapper.AddInParameter("@MatchingNumber", DbType.AnsiString, vWI_SPKChassisMatching.MatchingNumber)
            DbCommandWrapper.AddInParameter("@MatchingType", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.AnsiString, vWI_SPKChassisMatching.ReferenceNumber)


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

            Dim vWI_SPKChassisMatching As VWI_SPKChassisMatching = CType(obj, VWI_SPKChassisMatching)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_SPKChassisMatching.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_SPKChassisMatching.DealerCode)
            DbCommandWrapper.AddInParameter("@MatchingDate", DbType.DateTime, vWI_SPKChassisMatching.MatchingDate)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, vWI_SPKChassisMatching.CustomerCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, vWI_SPKChassisMatching.Name)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, vWI_SPKChassisMatching.SPKNumber)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vWI_SPKChassisMatching.ChassisNumber)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, vWI_SPKChassisMatching.EngineNumber)
            DbCommandWrapper.AddInParameter("@KeyNumber", DbType.AnsiString, vWI_SPKChassisMatching.KeyNumber)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, vWI_SPKChassisMatching.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, vWI_SPKChassisMatching.ColorCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vWI_SPKChassisMatching.Description)
            DbCommandWrapper.AddInParameter("@MatchingNumber", DbType.AnsiString, vWI_SPKChassisMatching.MatchingNumber)
            DbCommandWrapper.AddInParameter("@MatchingType", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.AnsiString, vWI_SPKChassisMatching.ReferenceNumber)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_SPKChassisMatching

            Dim vWI_SPKChassisMatching As VWI_SPKChassisMatching = New VWI_SPKChassisMatching

            vWI_SPKChassisMatching.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_SPKChassisMatching.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MatchingDate")) Then vWI_SPKChassisMatching.MatchingDate = CType(dr("MatchingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerCode")) Then vWI_SPKChassisMatching.CustomerCode = dr("CustomerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then vWI_SPKChassisMatching.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then vWI_SPKChassisMatching.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then vWI_SPKChassisMatching.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EngineNumber")) Then vWI_SPKChassisMatching.EngineNumber = dr("EngineNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KeyNumber")) Then vWI_SPKChassisMatching.KeyNumber = dr("KeyNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then vWI_SPKChassisMatching.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorCode")) Then vWI_SPKChassisMatching.ColorCode = dr("ColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then vWI_SPKChassisMatching.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MatchingNumber")) Then vWI_SPKChassisMatching.MatchingNumber = dr("MatchingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MatchingType")) Then vWI_SPKChassisMatching.MatchingType = CType(dr("MatchingType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNumber")) Then vWI_SPKChassisMatching.ReferenceNumber = dr("ReferenceNumber").ToString

            Return vWI_SPKChassisMatching

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_SPKChassisMatching) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_SPKChassisMatching), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_SPKChassisMatching).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

