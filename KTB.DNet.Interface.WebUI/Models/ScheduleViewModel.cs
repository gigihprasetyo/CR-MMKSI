#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Schedule ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Framework.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class ScheduleViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        public ScheduleType ScheduleType { get; set; }

        //[Required]
        [Display(Name = "Schedule Day")]
        public ScheduleDay? ScheduleDay { get; set; }

        //[Required]
        [Display(Name = "Month Day")]
        //[Range(1, 31)]
        public int? MonthDay { get; set; }

        [Required]
        [Display(Name = "Schedule Time Start")]
        public TimeSpan ScheduleTime { get; set; }

        public string Time
        {
            get
            {
                return ScheduleTime.ToString("c");
            }
        }

        [Required]
        [Display(Name = "Interval (minute)")]
        public int Interval { get; set; }

        [Required]
        [Display(Name = "Dealer Code")]
        public string DealerCode { get; set; }

        /// <summary>
        /// Validation Process
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (ScheduleType == ScheduleType.Monthly && (MonthDay == null || MonthDay <= 0 || (int)MonthDay > 31))
            {
                results.Add(new ValidationResult("The field Month Day must be between 1 and 31."));
            }

            if (ScheduleType == ScheduleType.Weekly && (MonthDay == null || ScheduleDay < 0 || (int)ScheduleDay > 7))
            {
                results.Add(new ValidationResult("The field Schedule Day must be between 1 and 31 is required."));
            }

            // Return if any errors
            if (results.Count > 0)
                return results;

            return results;
        }
    }
}