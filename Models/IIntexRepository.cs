using ToaPro.Models;

namespace ToaPro
{
    public interface IIntexRepository
    {
        IEnumerable<Student> Students { get; }

        IEnumerable<Submission> Submissions { get; }
        void AddSubmission(Submission submission);

    }
}
