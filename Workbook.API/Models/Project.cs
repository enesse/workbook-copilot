namespace Workbook.API.Models
{
    public record Project
    {
        public int ResourceId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int SequenceNumber { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public int ActivityId { get; set; }
        public string ActivityText { get; set; }
        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
        public DateTime FirstRegDate { get; set; }
        public float TaskHours { get; set; }
        public float TaskHoursTimeRegistration { get; set; }
        public int Id { get; set; }
        public string TaskPhaseName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public bool Access { get; set; }
        public int CompanyId { get; set; }
    }
}