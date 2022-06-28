#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrClassRegistration Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 2/1/2006 - 9:26:07 AM
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

    Public Class TrClassRegistrationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrClassRegistration"
        Private m_UpdateStatement As String = "up_UpdateTrClassRegistration"
        Private m_RetrieveStatement As String = "up_RetrieveTrClassRegistration"
        Private m_RetrieveListStatement As String = "up_RetrieveTrClassRegistrationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrClassRegistration"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trClassRegistration As TrClassRegistration = Nothing
            While dr.Read

                trClassRegistration = Me.CreateObject(dr)

            End While

            Return trClassRegistration

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trClassRegistrationList As ArrayList = New ArrayList

            While dr.Read
                Dim trClassRegistration As TrClassRegistration = Me.CreateObject(dr)
                trClassRegistrationList.Add(trClassRegistration)
            End While

            Return trClassRegistrationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trClassRegistration As TrClassRegistration = CType(obj, TrClassRegistration)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trClassRegistration.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trClassRegistration As TrClassRegistration = CType(obj, TrClassRegistration)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RegistrationCode", DbType.AnsiString, trClassRegistration.RegistrationCode)
            DbCommandWrapper.AddInParameter("@RegistrationDate", DbType.DateTime, trClassRegistration.RegistrationDate)
            DbCommandWrapper.AddInParameter("@CertificateNo", DbType.AnsiString, trClassRegistration.CertificateNo)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trClassRegistration.Status)
            DbCommandWrapper.AddInParameter("@InitialTest", DbType.Decimal, trClassRegistration.InitialTest)
            DbCommandWrapper.AddInParameter("@FinalTest", DbType.Decimal, trClassRegistration.FinalTest)
            DbCommandWrapper.AddInParameter("@Avarage", DbType.Decimal, trClassRegistration.Avarage)
            DbCommandWrapper.AddInParameter("@Rank", DbType.Int32, trClassRegistration.Rank)
            DBCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, trClassRegistration.Notes)
            DbCommandWrapper.AddInParameter("@EntryType", DbType.Int16, trClassRegistration.EntryType)
            DbCommandWrapper.AddInParameter("@IsManualCheck", DbType.Boolean, trClassRegistration.IsManualCheck)
            DbCommandWrapper.AddInParameter("@IsManualBy", DbType.AnsiString, trClassRegistration.IsManualBy)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trClassRegistration.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trClassRegistration.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@TraineeID", DbType.Int32, Me.GetRefObject(trClassRegistration.TrTrainee))
            DBCommandWrapper.AddInParameter("@ClassID", DbType.Int32, Me.GetRefObject(trClassRegistration.TrClass))
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(trClassRegistration.Dealer))

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

            Dim trClassRegistration As TrClassRegistration = CType(obj, TrClassRegistration)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trClassRegistration.ID)
            DbCommandWrapper.AddInParameter("@RegistrationCode", DbType.AnsiString, trClassRegistration.RegistrationCode)
            DbCommandWrapper.AddInParameter("@RegistrationDate", DbType.DateTime, trClassRegistration.RegistrationDate)
            DbCommandWrapper.AddInParameter("@CertificateNo", DbType.AnsiString, trClassRegistration.CertificateNo)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trClassRegistration.Status)
            DbCommandWrapper.AddInParameter("@InitialTest", DbType.Decimal, trClassRegistration.InitialTest)
            DbCommandWrapper.AddInParameter("@FinalTest", DbType.Decimal, trClassRegistration.FinalTest)
            DbCommandWrapper.AddInParameter("@Avarage", DbType.Decimal, trClassRegistration.Avarage)
            DbCommandWrapper.AddInParameter("@Rank", DbType.Int32, trClassRegistration.Rank)
            DBCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, trClassRegistration.Notes)
            DbCommandWrapper.AddInParameter("@EntryType", DbType.Int16, trClassRegistration.EntryType)
            DbCommandWrapper.AddInParameter("@IsManualCheck", DbType.Boolean, trClassRegistration.IsManualCheck)
            DbCommandWrapper.AddInParameter("@IsManualBy", DbType.AnsiString, trClassRegistration.IsManualBy)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trClassRegistration.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trClassRegistration.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@TraineeID", DbType.Int32, Me.GetRefObject(trClassRegistration.TrTrainee))
            DBCommandWrapper.AddInParameter("@ClassID", DbType.Int32, Me.GetRefObject(trClassRegistration.TrClass))
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(trClassRegistration.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrClassRegistration

            Dim trClassRegistration As TrClassRegistration = New TrClassRegistration

            trClassRegistration.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegistrationCode")) Then trClassRegistration.RegistrationCode = dr("RegistrationCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegistrationDate")) Then trClassRegistration.RegistrationDate = CType(dr("RegistrationDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CertificateNo")) Then trClassRegistration.CertificateNo = dr("CertificateNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trClassRegistration.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InitialTest")) Then trClassRegistration.InitialTest = CType(dr("InitialTest"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FinalTest")) Then trClassRegistration.FinalTest = CType(dr("FinalTest"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Avarage")) Then trClassRegistration.Avarage = CType(dr("Avarage"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Rank")) Then trClassRegistration.Rank = CType(dr("Rank"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then trClassRegistration.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EntryType")) Then trClassRegistration.EntryType = CType(dr("EntryType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsManualCheck")) Then trClassRegistration.IsManualCheck = CType(dr("IsManualCheck"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("IsManualBy")) Then trClassRegistration.IsManualBy = CType(dr("IsManualBy"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trClassRegistration.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trClassRegistration.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trClassRegistration.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trClassRegistration.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trClassRegistration.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TraineeID")) Then
                trClassRegistration.TrTrainee = New TrTrainee(CType(dr("TraineeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ClassID")) Then
                trClassRegistration.TrClass = New TrClass(CType(dr("ClassID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                trClassRegistration.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return trClassRegistration

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrClassRegistration) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrClassRegistration), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrClassRegistration).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

