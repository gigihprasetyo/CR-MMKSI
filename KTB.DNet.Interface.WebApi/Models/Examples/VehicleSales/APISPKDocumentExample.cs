#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APISPKDocumentExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion


using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models
{
    public class APISPKDocumentExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                SPKNumber = "1803000107"
            };
            return obj;
        }
    }
}