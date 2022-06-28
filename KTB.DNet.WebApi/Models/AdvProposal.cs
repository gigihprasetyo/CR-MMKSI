using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using KTB.DNet.Domain;

namespace KTB.DNet.WebApi.Models
{
    //[Serializable]
    public abstract class AdvProposal
    {
        //code proposal
        [Key]
        public string ProposalCode { get; set; }
        //nama dealer
        [Display(Name = "Dealer Name")]
        public string DealerName { get; set; }
        //kode dealer
        [Display(Name = "Dealer Code")]
        public string DealerCode { get; set; }
        //BabitHeader.BabitDealerGroup
        public List<string> CollaborateDealer { get; set; }
        //biaya yang diajukan
        [Display(Name = "Proposed Budget")]
        public decimal ProposedBudget { get; set; }
        //biaya yang disetujui
        [Display(Name = "Approved Budget")]
        public decimal ApprovedBudget { get; set; }
        //Babit Dealer Allocation Category Amount
        public List<ApprovedBudgetDetails> ApprovedBudgetDetails { get; set; }
        //keterangan
        [Display(Name = "Description")]
        public string Description { get; set; }
        //status
        [Display(Name = "Status")]
        public string Status { get; set; }
        //ApprovalNumber
        [Display(Name = "ApprovalNumber")]
        public string ApprovalNumber { get; set; }

        //attachment links
        //public IList<string> Attachments { get; set; }
        public List<Attachments> Attachments { get; set; }
    }

    //[Serializable]
    public abstract class BABITBase : AdvProposal
    {
        // no surat dealer
        public string LetterNo { get; set; }
    }
    //[Serializable]
    public class BABITEvtProposal : BABITBase
    {
        // dealer induk
        public string ParentDealer { get; set; }
        // tempat kegiatan
        public string Venue { get; set; }
        // jadwal kegiatan mulai
        public DateTime? ScheduleBegin { get; set; }
        // jadwal kegiatan berakhir
        public DateTime? ScheduleEnd { get; set; }
        // Target SPK
        public string TargetedSPK { get; set; }
        // Tipe Kegiatan
        public string ActivityType { get; set; }
        // Alokasi BABIT yang digunakan
        public string BABITAllocationUsed { get; set; }
    }

    //[Serializable]
    public class BABITAdvProposal : BABITBase
    {
        //media dan periode tayang
        public DateTime? MediaBroadcastPeriodBegin { get; set; }
        //media dan periode tayang
        public DateTime? MediaBroadcastPeriodEnd { get; set; }

        public List<MediaDetail> MediaDetails { get; set; }
    }

    //[Serializable]
    public class EventProposal : AdvProposal
    {
        //alamat dealer
        [Display(Name = "Dealer Address")]
        public string DealerAddress { get; set; }
        //kabupaten/kota dealer
        [Display(Name = "City / District")]
        public string DealerCityDistrict { get; set; }
        //tanggal event
        [Display(Name = "Event Date Begin")]
        public DateTime? EventDateBegin { get; set; }
        //tanggal event
        [Display(Name = "Event Date End")]
        public DateTime? EventDateEnd { get; set; }
        //lokasi event
        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }
        public string TOCode { get; set; }
        public string LetterNo { get; set; }
        public string BabitAllocationUsed { get; set; }
        public string EventCategory { get; set; }
        public int TargetedSPK { get; set; }
    }

    //[Serializable]
    public class MasterBabit
    {
        //event
        public List<EventProposal> Event { get; set; }
        //iklan
        public List<BABITAdvProposal> Iklan { get; set; }
        //pameran
        public List<BABITEvtProposal> Pameran { get; set; }        
    }

    public class UpdateStatus
    {
        public List<BabitData> BabitData { get; set; }
    }
    
    public class MasterEvent
    {
        public string EventRegNumber { get; set; }
        public string EventProposalName { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public string EventStatus { get; set; }
        public string Notes { get; set; }
        public string DealerAddress { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string DealerName { get; set; }
        public string DealerCode { get; set; }
        public List<string> CollaborateDealer { get; set; }
        public string ApprovalNumber { get; set; }
        //biaya yang diajukan
        [Display(Name = "Proposed Budget")]
        public decimal ProposedBudget { get; set; }
        //biaya yang disetujui
        [Display(Name = "Approved Budget")]
        public decimal ApprovedBudget { get; set; }
        public List<Attachments> Attachments { get; set; }
    }

    public class MediaDetail
    {
        public string Media { get; set; }
        public string MediaName { get; set; }
    }

    public class Attachments
    {
        public string Filename { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
    }

    public class BabitData
    {
        public string RegNumber { get; set; }
        public string FolioNumber { get; set; }
    }

    public class ApprovedBudgetDetails
    {
        public string DealerBudget { get; set; }
        public string isEdit { get; set; }
        public int PC { get; set; }
        public int LCV { get; set; }
        public int XPANDER { get; set; }
        public int SPESIAL { get; set; }
    }
}
