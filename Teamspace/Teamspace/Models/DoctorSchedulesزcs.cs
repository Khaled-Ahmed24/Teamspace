using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teamspace.Models
{
    public class DoctorSchedule
    {
        [Key]
        [ForeignKey("Staff")]
        public int StaffId { get; set; }

        public string ScheduleData { get; set; } = "{}";

        public Staff Staff { get; set; }

    }
}





