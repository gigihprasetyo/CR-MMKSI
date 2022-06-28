#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalProgramReguler Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/15/2020 - 2:14:19 PM
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

    Public Class DiscountProposalProgramRegulerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDiscountProposalProgramReguler"
        Private m_UpdateStatement As String = "up_UpdateDiscountProposalProgramReguler"
        Private m_RetrieveStatement As String = "up_RetrieveDiscountProposalProgramReguler"
        Private m_RetrieveListStatement As String = "up_RetrieveDiscountProposalProgramRegulerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDiscountProposalProgramReguler"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim discountProposalProgramReguler As DiscountProposalProgramReguler = Nothing
            While dr.Read

                discountProposalProgramReguler = Me.CreateObject(dr)

            End While

            Return discountProposalProgramReguler

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim discountProposalProgramRegulerList As ArrayList = New ArrayList

            While dr.Read
                Dim discountProposalProgramReguler As DiscountProposalProgramReguler = Me.CreateObject(dr)
                discountProposalProgramRegulerList.Add(discountProposalProgramReguler)
            End While

            Return discountProposalProgramRegulerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalProgramReguler As DiscountProposalProgramReguler = CType(obj, DiscountProposalProgramReguler)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalProgramReguler.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalProgramReguler As DiscountProposalProgramReguler = CType(obj, DiscountProposalProgramReguler)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AssyYear", DbType.Int16, discountProposalProgramReguler.AssyYear)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, discountProposalProgramReguler.ValidFrom)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, discountProposalProgramReguler.DiscountAmount)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, discountProposalProgramReguler.ValidTo)
            DbCommandWrapper.AddInParameter("@ProgramBased", DbType.Int16, discountProposalProgramReguler.ProgramBased)
            DbCommandWrapper.AddInParameter("@ModelYear", DbType.Int16, discountProposalProgramReguler.ModelYear)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalProgramReguler.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, discountProposalProgramReguler.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DiscountProposalParameterID", DbType.Int32, Me.GetRefObject(discountProposalProgramReguler.DiscountProposalParameter))
            DbCommandWrapper.AddInParameter("@VechileTypeGeneralID", DbType.Int16, Me.GetRefObject(discountProposalProgramReguler.VechileTypeGeneral))

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

            Dim discountProposalProgramReguler As DiscountProposalProgramReguler = CType(obj, DiscountProposalProgramReguler)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalProgramReguler.ID)
            DbCommandWrapper.AddInParameter("@AssyYear", DbType.Int16, discountProposalProgramReguler.AssyYear)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, discountProposalProgramReguler.ValidFrom)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, discountProposalProgramReguler.DiscountAmount)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, discountProposalProgramReguler.ValidTo)
            DbCommandWrapper.AddInParameter("@ProgramBased", DbType.Int16, discountProposalProgramReguler.ProgramBased)
            DbCommandWrapper.AddInParameter("@ModelYear", DbType.Int16, discountProposalProgramReguler.ModelYear)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalProgramReguler.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, discountProposalProgramReguler.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DiscountProposalParameterID", DbType.Int32, Me.GetRefObject(discountProposalProgramReguler.DiscountProposalParameter))
            DbCommandWrapper.AddInParameter("@VechileTypeGeneralID", DbType.Int32, Me.GetRefObject(discountProposalProgramReguler.VechileTypeGeneral))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DiscountProposalProgramReguler

            Dim discountProposalProgramReguler As DiscountProposalProgramReguler = New DiscountProposalProgramReguler

            discountProposalProgramReguler.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AssyYear")) Then discountProposalProgramReguler.AssyYear = CType(dr("AssyYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then discountProposalProgramReguler.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then discountProposalProgramReguler.ValidTo = CType(dr("ValidTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountAmount")) Then discountProposalProgramReguler.DiscountAmount = CType(dr("DiscountAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ProgramBased")) Then discountProposalProgramReguler.ProgramBased = CType(dr("ProgramBased"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ModelYear")) Then discountProposalProgramReguler.ModelYear = CType(dr("ModelYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then discountProposalProgramReguler.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then discountProposalProgramReguler.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then discountProposalProgramReguler.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then discountProposalProgramReguler.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then discountProposalProgramReguler.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountProposalParameterID")) Then
                discountProposalProgramReguler.DiscountProposalParameter = New DiscountProposalParameter(CType(dr("DiscountProposalParameterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeGeneralID")) Then
                discountProposalProgramReguler.VechileTypeGeneral = New VechileTypeGeneral(CType(dr("VechileTypeGeneralID"), Short))
            End If

            Return discountProposalProgramReguler

        End Function

        Private Sub SetTableName()

            If Not (GetType(DiscountProposalProgramReguler) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DiscountProposalProgramReguler), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DiscountProposalProgramReguler).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
