namespace ToaPro.Models
{
    public class DataSeeder
    {
        public ToaProContext _context { get; set; }

        public DataSeeder(ToaProContext tempContext)
        {
            _context = tempContext;
        }

        public void SeedData()
        {
            Class IS401 = new Class { Code = "401", Description = "Product Management, Project Management, UX/UI, and so much more!" };
            Class IS413 = new Class { Code = "413", Description = "Big Boss Dawg's class about ASP .NET." };
            Class IS414 = new Class { Code = "414", Description = "Cybersecurity; sleep with one eye open, you'll get social engineered out of your first-born child." };
            Class IS455 = new Class { Code = "455", Description = "Machine Learning with Dr. Keith, the man with the biggest heart and the fastest fingers in the west." };

            // Check if data exists
            if (!_context.Classes.ToList().Any())
            {
                _context.Classes.AddRange(
                    IS401,
                    IS413,
                    IS414,
                    IS455
                );

                _context.SaveChanges();
            }

            if (!_context.Semesters.ToList().Any())
            {
                _context.Semesters.AddRange(
                    new Semester { Term = "Winter", Year = 2023 }
                    //new Semester { /* initialize properties */ }
                );

                _context.SaveChanges();
            }

            if (!_context.Requirements.ToList().Any())
            {
                var classes = _context.Classes.ToList();

                _context.Requirements.AddRange(
                    new Requirement
                    {
                        ClassId = classes.FirstOrDefault(x => x.Code == "401").Id,
                        Description = "Figma is fully prototyped with every possible user flow."
                    },
                    new Requirement
                    {
                        ClassId = classes.FirstOrDefault(x => x.Code == "413").Id,
                        Description = "Use the word 'yeet' somewhere in your website."
                    },
                    new Requirement
                    {
                        ClassId = classes.FirstOrDefault(x => x.Code == "413").Id,
                        Description = "Code is clean; excessively commented and with descriptive variable names"
                    }
                );

                
            }

            _context.SaveChanges();

            // Repeat for other tables, respecting foreign key dependencies
        }
    }
}
