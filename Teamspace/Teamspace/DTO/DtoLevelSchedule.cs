using Teamspace.Models;

namespace Teamspace.DTO
{
    public class DtoLevelSchedule
    {
        public int Level { get; set; }


        public int DepartmentId { get; set; }

        public string ScheduleData { get; set; } = "{}";

    }
}
