using Newtonsoft.Json;

namespace Workbook.API.Models
{
    public class Timesheet
    {
        public double ExpectedHours { get; set; }
        public double RegisteredHours { get; set; }
        public bool IsCompleted { get; set; }

        public Timesheet() { }

        public Timesheet(TimesheetDto timeSheetDto)
        {
            ExpectedHours = timeSheetDto.ExpectedHours;
            RegisteredHours = timeSheetDto.RegisteredHours;
            IsCompleted = timeSheetDto.IsCompleted;
        }
    }

    public record TimesheetDto
    {
        [JsonProperty("BasicHours")]
        public double ExpectedHours { get; set; }

        [JsonProperty("TimeEntryHours")]
        public double RegisteredHours { get; set; }

        [JsonProperty("TimeEntryIsCompleted")]
        public bool IsCompleted { get; set; }
    }
}
