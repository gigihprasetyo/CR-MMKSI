#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCDamageRequestPart Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 25/10/2005 - 3:09:55 PM
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

    Public Class WSCDamageRequestPartMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWSCDamageRequestPart"
        Private m_UpdateStatement As String = "up_UpdateWSCDamageRequestPart"
        Private m_RetrieveStatement As String = "up_RetrieveWSCDamageRequestPart"
        Private m_RetrieveListStatement As String = "up_RetrieveWSCDamageRequestPartList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWSCDamageRequestPart"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim wSCDamageRequestPart As WSCDamageRequestPart = Nothing
            While dr.Read

                wSCDamageRequestPart = Me.CreateObject(dr)

            End While

            Return wSCDamageRequestPart

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim wSCDamageRequestPartList As ArrayList = New ArrayList

            While dr.Read
                Dim wSCDamageRequestPart As WSCDamageRequestPart = Me.CreateObject(dr)
                wSCDamageRequestPartList.Add(wSCDamageRequestPart)
            End While

            Return wSCDamageRequestPartList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wSCDamageRequestPart As WSCDamageRequestPart = CType(obj, WSCDamageRequestPart)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wSCDamageRequestPart.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wSCDamageRequestPart As WSCDamageRequestPart = CType(obj, WSCDamageRequestPart)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Sender", DbType.AnsiString, wSCDamageRequestPart.Sender)
            DbCommandWrapper.AddInParameter("@CCSendTo", DbType.AnsiString, wSCDamageRequestPart.CCSendTo)
            DbCommandWrapper.AddInParameter("@SendTo", DbType.AnsiString, wSCDamageRequestPart.SendTo)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, wSCDamageRequestPart.Subject)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, wSCDamageRequestPart.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wSCDamageRequestPart.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, wSCDamageRequestPart.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@WSCHeaderID", DbType.Int32, Me.GetRefObject(wSCDamageRequestPart.WSCHeader))

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

            Dim wSCDamageRequestPart As WSCDamageRequestPart = CType(obj, WSCDamageRequestPart)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wSCDamageRequestPart.ID)
            DbCommandWrapper.AddInParameter("@Sender", DbType.AnsiString, wSCDamageRequestPart.Sender)
            DbCommandWrapper.AddInParameter("@CCSendTo", DbType.AnsiString, wSCDamageRequestPart.CCSendTo)
            DbCommandWrapper.AddInParameter("@SendTo", DbType.AnsiString, wSCDamageRequestPart.SendTo)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, wSCDamageRequestPart.Subject)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, wSCDamageRequestPart.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wSCDamageRequestPart.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, wSCDamageRequestPart.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@WSCHeaderID", DbType.Int32, Me.GetRefObject(wSCDamageRequestPart.WSCHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WSCDamageRequestPart

            Dim wSCDamageRequestPart As WSCDamageRequestPart = New WSCDamageRequestPart

            wSCDamageRequestPart.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Sender")) Then wSCDamageRequestPart.Sender = dr("Sender").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CCSendTo")) Then wSCDamageRequestPart.CCSendTo = dr("CCSendTo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SendTo")) Then wSCDamageRequestPart.SendTo = dr("SendTo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Subject")) Then wSCDamageRequestPart.Subject = dr("Subject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then wSCDamageRequestPart.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then wSCDamageRequestPart.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then wSCDamageRequestPart.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then wSCDamageRequestPart.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then wSCDamageRequestPart.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then wSCDamageRequestPart.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WSCHeaderID")) Then
                wSCDamageRequestPart.WSCHeader = New WSCHeader(CType(dr("WSCHeaderID"), Integer))
            End If

            Return wSCDamageRequestPart

        End Function

        Private Sub SetTableName()

            If Not (GetType(WSCDamageRequestPart) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WSCDamageRequestPart), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WSCDamageRequestPart).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

