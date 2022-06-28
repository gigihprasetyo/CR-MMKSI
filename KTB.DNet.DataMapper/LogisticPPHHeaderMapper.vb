
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : LogisticPPHHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 9/14/2017 - 9:41:21 AM
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

    Public Class LogisticPPHHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertLogisticPPHHeader"
        Private m_UpdateStatement As String = "up_UpdateLogisticPPHHeader"
        Private m_RetrieveStatement As String = "up_RetrieveLogisticPPHHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveLogisticPPHHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteLogisticPPHHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim logisticPPHHeader As LogisticPPHHeader = Nothing
            While dr.Read

                logisticPPHHeader = Me.CreateObject(dr)

            End While

            Return logisticPPHHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim logisticPPHHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim logisticPPHHeader As LogisticPPHHeader = Me.CreateObject(dr)
                logisticPPHHeaderList.Add(logisticPPHHeader)
            End While

            Return logisticPPHHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim logisticPPHHeader As LogisticPPHHeader = CType(obj, LogisticPPHHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, logisticPPHHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim logisticPPHHeader As LogisticPPHHeader = CType(obj, LogisticPPHHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(logisticPPHHeader.Dealer))
            DbCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, logisticPPHHeader.NoReg)
            DbCommandWrapper.AddInParameter("@BuktiPotongNumber", DbType.AnsiString, logisticPPHHeader.BuktiPotongNumber)
            DbCommandWrapper.AddInParameter("@ReturnAssignNumber", DbType.AnsiString, logisticPPHHeader.ReturnAssignNumber)
            DbCommandWrapper.AddInParameter("@ReturnDate", DbType.DateTime, logisticPPHHeader.ReturnDate)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, logisticPPHHeader.TotalAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, logisticPPHHeader.PPHAmount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, logisticPPHHeader.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, logisticPPHHeader.Status)
            DbCommandWrapper.AddInParameter("@KantorPajak", DbType.AnsiString, logisticPPHHeader.KantorPajak)
            DbCommandWrapper.AddInParameter("@NamaKota", DbType.AnsiString, logisticPPHHeader.NamaKota)
            DbCommandWrapper.AddInParameter("@Pejabat", DbType.AnsiString, logisticPPHHeader.Pejabat)
            DbCommandWrapper.AddInParameter("@Jabatan", DbType.AnsiString, logisticPPHHeader.Jabatan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, logisticPPHHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, logisticPPHHeader.LastUpdateBy)
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

            Dim logisticPPHHeader As LogisticPPHHeader = CType(obj, LogisticPPHHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, logisticPPHHeader.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(logisticPPHHeader.Dealer))
            DbCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, logisticPPHHeader.NoReg)
            DbCommandWrapper.AddInParameter("@BuktiPotongNumber", DbType.AnsiString, logisticPPHHeader.BuktiPotongNumber)
            DbCommandWrapper.AddInParameter("@ReturnAssignNumber", DbType.AnsiString, logisticPPHHeader.ReturnAssignNumber)
            DbCommandWrapper.AddInParameter("@ReturnDate", DbType.DateTime, logisticPPHHeader.ReturnDate)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, logisticPPHHeader.TotalAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, logisticPPHHeader.PPHAmount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, logisticPPHHeader.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, logisticPPHHeader.Status)
            DbCommandWrapper.AddInParameter("@KantorPajak", DbType.AnsiString, logisticPPHHeader.KantorPajak)
            DbCommandWrapper.AddInParameter("@NamaKota", DbType.AnsiString, logisticPPHHeader.NamaKota)
            DbCommandWrapper.AddInParameter("@Pejabat", DbType.AnsiString, logisticPPHHeader.Pejabat)
            DbCommandWrapper.AddInParameter("@Jabatan", DbType.AnsiString, logisticPPHHeader.Jabatan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, logisticPPHHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, logisticPPHHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As LogisticPPHHeader

            Dim logisticPPHHeader As LogisticPPHHeader = New LogisticPPHHeader

            logisticPPHHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoReg")) Then logisticPPHHeader.NoReg = dr("NoReg").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BuktiPotongNumber")) Then logisticPPHHeader.BuktiPotongNumber = dr("BuktiPotongNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReturnAssignNumber")) Then logisticPPHHeader.ReturnAssignNumber = dr("ReturnAssignNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReturnDate")) Then logisticPPHHeader.ReturnDate = CType(dr("ReturnDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then logisticPPHHeader.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPHAmount")) Then logisticPPHHeader.PPHAmount = CType(dr("PPHAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then logisticPPHHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then logisticPPHHeader.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("KantorPajak")) Then logisticPPHHeader.KantorPajak = dr("KantorPajak").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NamaKota")) Then logisticPPHHeader.NamaKota = dr("NamaKota").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Pejabat")) Then logisticPPHHeader.Pejabat = dr("Pejabat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Jabatan")) Then logisticPPHHeader.Jabatan = dr("Jabatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then logisticPPHHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then logisticPPHHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then logisticPPHHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then logisticPPHHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then logisticPPHHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                logisticPPHHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))

            End If
            Return logisticPPHHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(LogisticPPHHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(LogisticPPHHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(LogisticPPHHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.LogisticPPHHeader) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.LogisticPPHHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.LogisticPPHHeader).MarkLoaded()
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

