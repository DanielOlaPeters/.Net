namespace Lab4P1.Models.ViewModels
{
    public class NewsViewModel
    {
        public IEnumerable<Subscription> Subscriptions { get; set; }
        public IEnumerable<Fan> Fans { get; set; }
        public IEnumerable<SportClub> SportClubs { get; set; }
    }
}
