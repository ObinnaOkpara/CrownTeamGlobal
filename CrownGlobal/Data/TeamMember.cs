namespace CrownGlobal.Data
{
    public class TeamMember
    {
        public int Id { get; set; }
        public int RankId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Story { get; set; }
        public string Image { get; set; }

        public Rank Rank { get; set; }
    }
}