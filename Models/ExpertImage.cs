namespace HomeTaskkMVC4.Models
{
    public class ExpertImage
    {
       public int Id { get; set; }

        public string ExpertImg { get; set; }

        public bool IsMain { get; set; }

        public int ExpertsId { get; set; }

        public Experts Experts { get; set; }


    }
}
