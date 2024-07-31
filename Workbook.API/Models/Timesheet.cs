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

    public record TimesheetCreate
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public bool Billable { get; set; }
        public float Hours { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; }
    }

    public record TimesheetComplete
    {
        public int ResourceId { get; set; }
        public string RegistrationDate { get; set; }
        public string RegistrationEndDate { get; set; }
        public bool ConfirmPolicy { get; set; }
        public string ApproveDescription { get; set; }
    }
}
