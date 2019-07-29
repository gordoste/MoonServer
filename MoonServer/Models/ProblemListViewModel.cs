namespace MoonServer.Models
{
    public class ProblemListViewModel
    {
        public long Id;
        public string Name;
        public int Count;

        public ProblemListViewModel() { }

        public ProblemListViewModel(ProblemList p)
        {
            Id = p.Id;
            Name = p.Name;
            Count = p.ProblemListEntries.Count;
        }
    }
}