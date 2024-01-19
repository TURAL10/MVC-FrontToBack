namespace HomeTaskkMVC4.Models
{
    public class Experts
    {
        public int Id { get; set; }

        public string ExpertName { get; set; }

        public string TypeOfJob { get; set; }

        public List<ExpertImage> ExpertImages { get; set; } 
    }
}
