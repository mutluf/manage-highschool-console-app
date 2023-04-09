namespace ManageHighSchool.Models
{
    public class Student
    {
        public string FullName { get; set; }
        public int StudentNumber { get; set; }
        public Classroom? Classroom { get; set; }
        
    }
}
