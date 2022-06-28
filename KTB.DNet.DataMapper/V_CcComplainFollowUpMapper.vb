
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_CcComplainFollowUp Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 10/12/2011 - 11:23:38 AM
'//
'// ===========================================================================	
#end region


#region ".NET Base Class Namespace Imports"
imports System
imports System.Data
imports System.Collections
#end region

#region "Custom Namespace Imports"
imports Microsoft.Practices.EnterpriseLibrary.Data
imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
imports Microsoft.Practices.EnterpriseLibrary.Logging
imports KTB.DNet.DataMapper.Framework
imports KTB.DNet.Domain
imports KTB.DNet.Domain.Search
#end region

namespace KTB.DNet.DataMapper

	public class V_CcComplainFollowUpMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertV_CcComplainFollowUp"
		private m_UpdateStatement as string = "up_UpdateV_CcComplainFollowUp"
		private m_RetrieveStatement as string = "up_RetrieveV_CcComplainFollowUp"
		private m_RetrieveListStatement as string = "up_RetrieveV_CcComplainFollowUpList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteV_CcComplainFollowUp"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim V_CcComplainFollowUp as V_CcComplainFollowUp = nothing
			while dr.Read
			
				V_CcComplainFollowUp = me.CreateObject(dr)
			            
			end while        					
			
			return V_CcComplainFollowUp
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim V_CcComplainFollowUpList as ArrayList = new ArrayList
			
			while dr.Read
					dim V_CcComplainFollowUp as V_CcComplainFollowUp = me.CreateObject(dr)
					V_CcComplainFollowUpList.Add(V_CcComplainFollowUp)
			end while
			     
			return V_CcComplainFollowUpList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim V_CcComplainFollowUp as V_CcComplainFollowUp = ctype(obj, V_CcComplainFollowUp)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,V_CcComplainFollowUp.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim V_CcComplainFollowUp as V_CcComplainFollowUp = ctype(obj, V_CcComplainFollowUp)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.AnsiString, V_CcComplainFollowUp.DealerID)
			DbCommandWrapper.AddInParameter("@DealerName",DbType.AnsiString,V_CcComplainFollowUp.DealerName)
			DbCommandWrapper.AddInParameter("@TglSurvey",DbType.AnsiString,V_CcComplainFollowUp.TglSurvey)
			DbCommandWrapper.AddInParameter("@ConsumerName",DbType.AnsiString,V_CcComplainFollowUp.ConsumerName)
			DbCommandWrapper.AddInParameter("@complain",DbType.AnsiString,V_CcComplainFollowUp.complain)
			DbCommandWrapper.AddInParameter("@tanggapan",DbType.AnsiString,V_CcComplainFollowUp.tanggapan)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,V_CcComplainFollowUp.Status)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,V_CcComplainFollowUp.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

						
			return DbCommandWrapper
			
		end function
	
		protected overrides function GetNewID(byval dbCommandWrapper as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) as integer

			return ctype(dbCommandWrapper.GetParameterValue("@ID"), integer)

		end function
		
		protected overrides function GetPagingRetrieveCommand as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_PagingQuery)
			DbCommandWrapper.AddInParameter("@Table",DbType.String,m_TableName)
			DbCommandWrapper.AddInParameter("@PK",DbType.String,"ID")
							
			return DbCommandWrapper
		
		end function
		
		protected overrides function GetRetrieveCommand as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DinamicQuery)
			DbCommandWrapper.AddInParameter("@sqlQuery",DbType.String,"SELECT " + m_TableName + ".* FROM " + m_TableName)
			
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetRetrieveListParameter as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_RetrieveListStatement)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetRetrieveParameter(byval id as integer) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_RetrieveStatement)
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,id)
				
			return DbCommandWrapper
			
		end function

		protected overrides function GetUpdateParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim V_CcComplainFollowUp as V_CcComplainFollowUp = ctype(obj, V_CcComplainFollowUp)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, V_CcComplainFollowUp.ID)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, V_CcComplainFollowUp.DealerID)
			DbCommandWrapper.AddInParameter("@DealerName",DbType.AnsiString,V_CcComplainFollowUp.DealerName)
			DbCommandWrapper.AddInParameter("@TglSurvey",DbType.AnsiString,V_CcComplainFollowUp.TglSurvey)
			DbCommandWrapper.AddInParameter("@ConsumerName",DbType.AnsiString,V_CcComplainFollowUp.ConsumerName)
			DbCommandWrapper.AddInParameter("@complain",DbType.AnsiString,V_CcComplainFollowUp.complain)
			DbCommandWrapper.AddInParameter("@tanggapan",DbType.AnsiString,V_CcComplainFollowUp.tanggapan)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,V_CcComplainFollowUp.Status)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as V_CcComplainFollowUp
		
			dim V_CcComplainFollowUp as V_CcComplainFollowUp = new V_CcComplainFollowUp
			
            V_CcComplainFollowUp.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then V_CcComplainFollowUp.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then V_CcComplainFollowUp.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TglSurvey")) Then V_CcComplainFollowUp.TglSurvey = dr("TglSurvey").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumerName")) Then V_CcComplainFollowUp.ConsumerName = dr("ConsumerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("complain")) Then V_CcComplainFollowUp.complain = dr("complain").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("tanggapan")) Then V_CcComplainFollowUp.tanggapan = dr("tanggapan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then V_CcComplainFollowUp.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then V_CcComplainFollowUp.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then V_CcComplainFollowUp.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return V_CcComplainFollowUp
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (V_CcComplainFollowUp) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(V_CcComplainFollowUp),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(V_CcComplainFollowUp).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

