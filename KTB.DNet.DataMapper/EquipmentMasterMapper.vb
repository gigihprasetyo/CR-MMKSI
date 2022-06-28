#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EquipmentMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/08/2005 - 1:39:32 PM
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

    Public Class EquipmentMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEquipmentMaster"
        Private m_UpdateStatement As String = "up_UpdateEquipmentMaster"
        Private m_RetrieveStatement As String = "up_RetrieveEquipmentMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveEquipmentMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEquipmentMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim equipmentMaster As EquipmentMaster = Nothing
            While dr.Read

                equipmentMaster = Me.CreateObject(dr)

            End While

            Return equipmentMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim equipmentMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim equipmentMaster As EquipmentMaster = Me.CreateObject(dr)
                equipmentMasterList.Add(equipmentMaster)
            End While

            Return equipmentMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim equipmentMaster As EquipmentMaster = CType(obj, EquipmentMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, equipmentMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim equipmentMaster As EquipmentMaster = CType(obj, EquipmentMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Kind", DbType.Int32, equipmentMaster.Kind)
            DbCommandWrapper.AddInParameter("@EquipmentNumber", DbType.AnsiString, equipmentMaster.EquipmentNumber)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, equipmentMaster.Description)
            DbCommandWrapper.AddInParameter("@Specification", DbType.AnsiString, equipmentMaster.Specification)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, equipmentMaster.Status)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, equipmentMaster.Price)
            DbCommandWrapper.AddInParameter("@PhotoFileName", DbType.AnsiString, equipmentMaster.PhotoFileName)
            DbCommandWrapper.AddInParameter("@PhotoPath", DbType.AnsiString, equipmentMaster.PhotoPath)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, equipmentMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, equipmentMaster.LastUpdateBy)
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

            Dim equipmentMaster As EquipmentMaster = CType(obj, EquipmentMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, equipmentMaster.ID)
            DbCommandWrapper.AddInParameter("@Kind", DbType.Int32, equipmentMaster.Kind)
            DbCommandWrapper.AddInParameter("@EquipmentNumber", DbType.AnsiString, equipmentMaster.EquipmentNumber)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, equipmentMaster.Description)
            DbCommandWrapper.AddInParameter("@Specification", DbType.AnsiString, equipmentMaster.Specification)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, equipmentMaster.Status)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, equipmentMaster.Price)
            DbCommandWrapper.AddInParameter("@PhotoFileName", DbType.AnsiString, equipmentMaster.PhotoFileName)
            DbCommandWrapper.AddInParameter("@PhotoPath", DbType.AnsiString, equipmentMaster.PhotoPath)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, equipmentMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, equipmentMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EquipmentMaster

            Dim equipmentMaster As EquipmentMaster = New EquipmentMaster

            equipmentMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Kind")) Then equipmentMaster.Kind = CType(dr("Kind"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EquipmentNumber")) Then equipmentMaster.EquipmentNumber = dr("EquipmentNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then equipmentMaster.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Specification")) Then equipmentMaster.Specification = dr("Specification").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then equipmentMaster.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Price")) Then equipmentMaster.Price = CType(dr("Price"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PhotoFileName")) Then equipmentMaster.PhotoFileName = dr("PhotoFileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhotoPath")) Then equipmentMaster.PhotoPath = dr("PhotoPath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then equipmentMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then equipmentMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then equipmentMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then equipmentMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then equipmentMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return equipmentMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(EquipmentMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EquipmentMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EquipmentMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

