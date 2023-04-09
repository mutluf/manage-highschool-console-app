namespace ManageHighSchool.Models
{
    public class Homework : BaseModel
    {
        
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
        public string HomeWork { get; set; }
        public int? Grade { get; set; }
    }
}
