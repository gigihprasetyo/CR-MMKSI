#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_owner  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-13 09:28:00
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
	public class VWI_CRM_owner
	{
		public Int64 IDRow { get; set; }
		public Int64 versionnumber { get; set; }
		public string name { get; set; }
		public Guid ownerid { get; set; }
		public string owneridtype { get; set; }
		public string yominame { get; set; }
	}
}
