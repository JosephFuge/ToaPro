using ToaPro.Models;

namespace ToaPro
{
    public interface IIntexRepository
    {
        IEnumerable<Student> Students { get; } 
        IEnumerable<Submission> Submissions { get; }
        IQueryable<Judge> Judges { get; }
        IQueryable<Presentation> Presentations { get; }
        IEnumerable<JudgeAvailability> JudgeAvailabilities { get; }
        IQueryable<Ranking> Rankings { get; }
        IQueryable<Group> Groups { get; }

        public void RequestAvailability(JudgeAvailability availabilities);
        public void StudentRequestAvailability(StudentAvailability availabilities);

        public void UpdateRanking(Ranking ranking);

        public void AddRanking(Ranking ranking);

    }
}
