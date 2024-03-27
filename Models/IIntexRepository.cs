﻿using ToaPro.Models;

namespace ToaPro
{
    public interface IIntexRepository
    {
        IEnumerable<Student> Students { get; }

        IEnumerable<Submission> Submissions { get; }

        IEnumerable<JudgeAvailability> JudgeAvailabilities { get; }

        public void RequestAvailability(JudgeAvailability availabilities);
    }
}
