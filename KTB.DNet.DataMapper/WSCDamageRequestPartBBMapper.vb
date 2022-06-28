#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCDamageRequestPartBB Objects Mapper.
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

    Public Class WSCDamageRequestPartBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWSCDamageRequestPartBB"
        Private m_UpdateStatement As String = "up_UpdateWSCDamageRequestPartBB"
        Private m_RetrieveStatement As String = "up_RetrieveWSCDamageRequestPartBB"
        Private m_RetrieveListStatement As String = "up_RetrieveWSCDamageRequestPartBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWSCDamageRequestPartBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim WSCDamageRequestPartBB As WSCDamageRequestPartBB = Nothing
            While dr.Read

                WSCDamageRequestPartBB = Me.CreateObject(dr)

            End While

            Return WSCDamageRequestPartBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim WSCDamageRequestPartBBList As ArrayList = New ArrayList

            While dr.Read
                Dim WSCDamageRequestPartBB As WSCDamageRequestPartBB = Me.CreateObject(dr)
                WSCDamageRequestPartBBList.Add(WSCDamageRequestPartBB)
            End While

            Return WSCDamageRequestPartBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WSCDamageRequestPartBB As WSCDamageRequestPartBB = CType(obj, WSCDamageRequestPartBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, WSCDamageRequestPartBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WSCDamageRequestPartBB As WSCDamageRequestPartBB = CType(obj, WSCDamageRequestPartBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Sender", DbType.AnsiString, WSCDamageRequestPartBB.Sender)
            DbCommandWrapper.AddInParameter("@CCSendTo", DbType.AnsiString, WSCDamageRequestPartBB.CCSendTo)
            DbCommandWrapper.AddInParameter("@SendTo", DbType.AnsiString, WSCDamageRequestPartBB.SendTo)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, WSCDamageRequestPartBB.Subject)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, WSCDamageRequestPartBB.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, WSCDamageRequestPartBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, WSCDamageRequestPartBB.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@WSCHeaderBBID", DbType.Int32, Me.GetRefObject(WSCDamageRequestPartBB.WSCHeaderBB))

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

            Dim WSCDamageRequestPartBB As WSCDamageRequestPartBB = CType(obj, WSCDamageRequestPartBB)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, WSCDamageRequestPartBB.ID)
            DBCommandWrapper.AddInParameter("@Sender", DbType.AnsiString, WSCDamageRequestPartBB.Sender)
            DBCommandWrapper.AddInParameter("@CCSendTo", DbType.AnsiString, WSCDamageRequestPartBB.CCSendTo)
            DBCommandWrapper.AddInParameter("@SendTo", DbType.AnsiString, WSCDamageRequestPartBB.SendTo)
            DBCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, WSCDamageRequestPartBB.Subject)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, WSCDamageRequestPartBB.Description)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, WSCDamageRequestPartBB.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, WSCDamageRequestPartBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@WSCHeaderBBID", DbType.Int32, Me.GetRefObject(WSCDamageRequestPartBB.WSCHeaderBB))

            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WSCDamageRequestPartBB

            Dim WSCDamageRequestPartBB As WSCDamageRequestPartBB = New WSCDamageRequestPartBB

            WSCDamageRequestPartBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Sender")) Then WSCDamageRequestPartBB.Sender = dr("Sender").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CCSendTo")) Then WSCDamageRequestPartBB.CCSendTo = dr("CCSendTo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SendTo")) Then WSCDamageRequestPartBB.SendTo = dr("SendTo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Subject")) Then WSCDamageRequestPartBB.Subject = dr("Subject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then WSCDamageRequestPartBB.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then WSCDamageRequestPartBB.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then WSCDamageRequestPartBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then WSCDamageRequestPartBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then WSCDamageRequestPartBB.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then WSCDamageRequestPartBB.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WSCHeaderBBID")) Then
                WSCDamageRequestPartBB.WSCHeaderBB = New WSCHeaderBB(CType(dr("WSCHeaderBBID"), Integer))
            End If

            Return WSCDamageRequestPartBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(WSCDamageRequestPartBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WSCDamageRequestPartBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WSCDamageRequestPartBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

