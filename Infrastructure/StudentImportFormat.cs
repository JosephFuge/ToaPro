namespace ToaPro.Infrastructure
{
    // The fields of this class are the expected columns in a student bulk import CSV
    public class StudentImportFormat
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NetID { get; set; }
        public string Email { get; set; }
        public int SectionNumber { get; set; }
        public int GroupNumber { get; set; }
    }
}
