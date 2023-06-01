namespace PriortizedDayBuilder.Data
{
    public class PriorityTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Day { get; set; }
        public int Priority { get; set; }
        public double Hours { get; set; }
        public bool IsDone { get; set; }
    }
}
