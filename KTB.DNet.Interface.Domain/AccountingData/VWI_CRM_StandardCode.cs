﻿#region Summary
// ===========================================================================
// AUTHOR        : System
// PURPOSE       : VWI_CRM_StandardCode  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_StandardCode
    {
        public int ID { get; set; }

        public string Category { get; set; }

        public int ValueId { get; set; }

        public string ValueCode { get; set; }

        public string ValueDesc { get; set; }

        public int Sequence { get; set; }

        public int RowStatus { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public string LastUpdateBy { get; set; }

        public DateTime? LastUpdateTime { get; set; }
    }
}
