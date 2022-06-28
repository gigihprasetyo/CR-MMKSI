
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExDebitCharge Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2020 - 4:09:31 PM
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

    Public Class MSPExDebitChargeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMSPExDebitCharge"
        Private m_UpdateStatement As String = "up_UpdateMSPExDebitCharge"
        Private m_RetrieveStatement As String = "up_RetrieveMSPExDebitCharge"
        Private m_RetrieveListStatement As String = "up_RetrieveMSPExDebitChargeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMSPExDebitCharge"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mSPExDebitCharge As MSPExDebitCharge = Nothing
            While dr.Read

                mSPExDebitCharge = Me.CreateObject(dr)

            End While

            Return mSPExDebitCharge

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mSPExDebitChargeList As ArrayList = New ArrayList

            While dr.Read
                Dim mSPExDebitCharge As MSPExDebitCharge = Me.CreateObject(dr)
                mSPExDebitChargeList.Add(mSPExDebitCharge)
            End While

            Return mSPExDebitChargeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExDebitCharge As MSPExDebitCharge = CType(obj, MSPExDebitCharge)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExDebitCharge.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExDebitCharge As MSPExDebitCharge = CType(obj, MSPExDebitCharge)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DebitChargeNo", DbType.AnsiString, mSPExDebitCharge.DebitChargeNo)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, mSPExDebitCharge.Amount)
            DbCommandWrapper.AddInParameter("@TOP", DbType.AnsiString, mSPExDebitCharge.TOP)
            DbCommandWrapper.AddInParameter("@DocumentDate", DbType.DateTime, mSPExDebitCharge.DocumentDate)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, mSPExDebitCharge.FileName)
            DbCommandWrapper.AddInParameter("@Rowstatus", DbType.Int16, mSPExDebitCharge.Rowstatus)
            DbCommandWrapper.AddInParameter("@Createdby", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@Createdtime", DbType.DateTime, mSPExDebitCharge.Createdtime)
            DbCommandWrapper.AddInParameter("@LastUpdatedby", DbType.AnsiString, mSPExDebitCharge.LastUpdatedby)
            'DbCommandWrapper.AddInParameter("@LastUpdatedtime", DbType.DateTime, mSPExDebitCharge.LastUpdatedtime)

            DbCommandWrapper.AddInParameter("@MSPExRegistrationID", DbType.Int32, Me.GetRefObject(mSPExDebitCharge.MSPExRegistration))

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

            Dim mSPExDebitCharge As MSPExDebitCharge = CType(obj, MSPExDebitCharge)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExDebitCharge.ID)
            DbCommandWrapper.AddInParameter("@DebitChargeNo", DbType.AnsiString, mSPExDebitCharge.DebitChargeNo)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, mSPExDebitCharge.Amount)
            DbCommandWrapper.AddInParameter("@TOP", DbType.AnsiString, mSPExDebitCharge.TOP)
            DbCommandWrapper.AddInParameter("@DocumentDate", DbType.DateTime, mSPExDebitCharge.DocumentDate)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, mSPExDebitCharge.FileName)
            DbCommandWrapper.AddInParameter("@Rowstatus", DbType.Int16, mSPExDebitCharge.Rowstatus)
            DbCommandWrapper.AddInParameter("@Createdby", DbType.AnsiString, mSPExDebitCharge.Createdby)
            'DbCommandWrapper.AddInParameter("@Createdtime", DbType.DateTime, mSPExDebitCharge.Createdtime)
            DbCommandWrapper.AddInParameter("@LastUpdatedby", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedtime", DbType.DateTime, mSPExDebitCharge.LastUpdatedtime)


            DbCommandWrapper.AddInParameter("@MSPExRegistrationID", DbType.Int32, Me.GetRefObject(mSPExDebitCharge.MSPExRegistration))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MSPExDebitCharge

            Dim mSPExDebitCharge As MSPExDebitCharge = New MSPExDebitCharge

            mSPExDebitCharge.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitChargeNo")) Then mSPExDebitCharge.DebitChargeNo = dr("DebitChargeNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then mSPExDebitCharge.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TOP")) Then mSPExDebitCharge.TOP = dr("TOP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentDate")) Then mSPExDebitCharge.DocumentDate = CType(dr("DocumentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then mSPExDebitCharge.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Rowstatus")) Then mSPExDebitCharge.Rowstatus = CType(dr("Rowstatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Createdby")) Then mSPExDebitCharge.Createdby = dr("Createdby").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Createdtime")) Then mSPExDebitCharge.Createdtime = CType(dr("Createdtime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedby")) Then mSPExDebitCharge.LastUpdatedby = dr("LastUpdatedby").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedtime")) Then mSPExDebitCharge.LastUpdatedtime = CType(dr("LastUpdatedtime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExRegistrationID")) Then
                mSPExDebitCharge.MSPExRegistration = New MSPExRegistration(CType(dr("MSPExRegistrationID"), Integer))
            End If

            Return mSPExDebitCharge

        End Function

        Private Sub SetTableName()

            If Not (GetType(MSPExDebitCharge) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MSPExDebitCharge), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MSPExDebitCharge).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

