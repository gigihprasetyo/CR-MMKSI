
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_Customer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 17/05/2018 - 10:34:05
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

    Public Class VWI_CustomerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_Customer"
        Private m_UpdateStatement As String = "up_UpdateVWI_Customer"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_Customer"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_CustomerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_Customer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_Customer As VWI_Customer = Nothing
            While dr.Read

                VWI_Customer = Me.CreateObject(dr)

            End While

            Return VWI_Customer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_CustomerList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_Customer As VWI_Customer = Me.CreateObject(dr)
                VWI_CustomerList.Add(VWI_Customer)
            End While

            Return VWI_CustomerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_Customer As VWI_Customer = CType(obj, VWI_Customer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, VWI_Customer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_Customer As VWI_Customer = CType(obj, VWI_Customer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, VWI_Customer.CustomerCode)
            DbCommandWrapper.AddInParameter("@CustomerRequestId", DbType.Int32, VWI_Customer.CustomerRequestId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_Customer.DealerCode)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, VWI_Customer.SPKNumber)
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

            Dim VWI_Customer As VWI_Customer = CType(obj, VWI_Customer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, VWI_Customer.ID)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, VWI_Customer.CustomerCode)
            DbCommandWrapper.AddInParameter("@CustomerRequestId", DbType.Int32, VWI_Customer.CustomerRequestId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_Customer.DealerCode)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, VWI_Customer.SPKNumber)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_Customer

            Dim VWI_Customer As VWI_Customer = New VWI_Customer

            If Not dr.IsDBNull(dr.GetOrdinal("CustomerCode")) Then VWI_Customer.CustomerCode = dr("CustomerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerRequestId")) Then VWI_Customer.CustomerRequestId = CType(dr("CustomerRequestId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_Customer.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then VWI_Customer.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_Customer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return VWI_Customer

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_Customer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_Customer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_Customer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

