#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_EquipPO Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/27/2009 - 12:06:22
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

    Public Class v_EquipPOMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_EquipPO"
        Private m_UpdateStatement As String = "up_Updatev_EquipPO"
        Private m_RetrieveStatement As String = "up_Retrievev_EquipPO"
        Private m_RetrieveListStatement As String = "up_Retrievev_EquipPOList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_EquipPO"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_EquipPO As v_EquipPO = Nothing
            While dr.Read

                v_EquipPO = Me.CreateObject(dr)

            End While

            Return v_EquipPO

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_EquipPOList As ArrayList = New ArrayList

            While dr.Read
                Dim v_EquipPO As v_EquipPO = Me.CreateObject(dr)
                v_EquipPOList.Add(v_EquipPO)
            End While

            Return v_EquipPOList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EquipPO As v_EquipPO = CType(obj, v_EquipPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_EquipPO.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EquipPO As v_EquipPO = CType(obj, v_EquipPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@StatusKTB", DbType.AnsiString, v_EquipPO.StatusKTB)
            DBCommandWrapper.AddInParameter("@Status", DbType.AnsiString, v_EquipPO.Status)
            DBCommandWrapper.AddInParameter("@StatusDesc", DbType.AnsiString, v_EquipPO.StatusDesc)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_EquipPO.DealerCode)
            DBCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_EquipPO.DealerName)
            DBCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, v_EquipPO.RequestNo)
            DBCommandWrapper.AddInParameter("@EstimationNumber", DbType.AnsiString, v_EquipPO.EstimationNumber)
            DBCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, v_EquipPO.PaymentType)
            DBCommandWrapper.AddInParameter("@PaymentTypeDesc", DbType.AnsiString, v_EquipPO.PaymentTypeDesc)
            DBCommandWrapper.AddInParameter("@TotalItem", DbType.Int32, v_EquipPO.TotalItem)
            DBCommandWrapper.AddInParameter("@TotalEstimationUnit", DbType.Int32, v_EquipPO.TotalEstimationUnit)
            DbCommandWrapper.AddInParameter("@SisaQty", DbType.Int32, v_EquipPO.SisaQty)
            DbCommandWrapper.AddInParameter("@MaterialType", DbType.Int32, v_EquipPO.MaterialType)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_EquipPO.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_EquipPO.LastUpdateBy)


            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EquipPO As v_EquipPO = CType(obj, v_EquipPO)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, v_EquipPO.ID)
            DBCommandWrapper.AddInParameter("@StatusKTB", DbType.AnsiString, v_EquipPO.StatusKTB)
            DBCommandWrapper.AddInParameter("@Status", DbType.AnsiString, v_EquipPO.Status)
            DBCommandWrapper.AddInParameter("@StatusDesc", DbType.AnsiString, v_EquipPO.StatusDesc)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_EquipPO.DealerCode)
            DBCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_EquipPO.DealerName)
            DBCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, v_EquipPO.RequestNo)
            DBCommandWrapper.AddInParameter("@EstimationNumber", DbType.AnsiString, v_EquipPO.EstimationNumber)
            DBCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, v_EquipPO.PaymentType)
            DBCommandWrapper.AddInParameter("@PaymentTypeDesc", DbType.AnsiString, v_EquipPO.PaymentTypeDesc)
            DBCommandWrapper.AddInParameter("@TotalItem", DbType.Int32, v_EquipPO.TotalItem)
            DBCommandWrapper.AddInParameter("@TotalEstimationUnit", DbType.Int32, v_EquipPO.TotalEstimationUnit)
            DbCommandWrapper.AddInParameter("@SisaQty", DbType.Int32, v_EquipPO.SisaQty)
            DbCommandWrapper.AddInParameter("@MaterialType", DbType.Int32, v_EquipPO.MaterialType)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_EquipPO.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            'DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_EquipPO.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)



            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_EquipPO

            Dim v_EquipPO As v_EquipPO = New v_EquipPO

            v_EquipPO.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_EquipPO.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusKTB")) Then v_EquipPO.StatusKTB = dr("StatusKTB").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDesc")) Then v_EquipPO.StatusDesc = dr("StatusDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_EquipPO.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_EquipPO.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RequestNo")) Then v_EquipPO.RequestNo = dr("RequestNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EstimationNumber")) Then v_EquipPO.EstimationNumber = dr("EstimationNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then v_EquipPO.PaymentType = CType(dr("PaymentType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentTypeDesc")) Then v_EquipPO.PaymentTypeDesc = dr("PaymentTypeDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalItem")) Then v_EquipPO.TotalItem = CType(dr("TotalItem"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalEstimationUnit")) Then v_EquipPO.TotalEstimationUnit = CType(dr("TotalEstimationUnit"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SisaQty")) Then v_EquipPO.SisaQty = CType(dr("SisaQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialType")) Then v_EquipPO.MaterialType = CType(dr("MaterialType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_EquipPO.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_EquipPO.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_EquipPO.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_EquipPO.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_EquipPO.LastUpdateBy = dr("LastUpdateBy").ToString

            Return v_EquipPO

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_EquipPO) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_EquipPO), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_EquipPO).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

