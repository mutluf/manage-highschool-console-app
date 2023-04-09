namespace ManageHighSchool.Models
{
    public class Teacher : BaseModel
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string Branch { get; set; }
        public Classroom? Classroom { get; set; }
        
    }
}
