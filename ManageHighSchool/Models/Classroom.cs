namespace ManageHighSchool.Models
{
    public class Classroom : BaseModel
    {
        
        public string Name { get; set; }
        public ICollection<Classroom>? Students { get; set; }
        public Teacher Teacher { get; set; }
    }
}
