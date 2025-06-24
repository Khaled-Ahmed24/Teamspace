using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teamspace.Models
{
    public class LevelSchedule
    {
        [Key]
        public int Level { get; set; }

        
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
        public string ScheduleData { get; set; } = "{}";

    }
}





