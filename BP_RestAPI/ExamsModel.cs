using System.ComponentModel.DataAnnotations;

namespace BP_RestAPI
{
    public class ExamsModel
    {
        [Key]
        public int Id { get; set; }
        public int ClassID { get; set; }
        public int TeacherID { get; set; }
        public string? ExamName { get; set; }
        public string PIN { get; set; }
        public DateTime? Creation { get; set; }
        public string Problem1 { get; set; }
        public string? Problem2 { get; set; }
        public string? Problem3 { get; set; }
        public string? Problem4 { get; set; }
        public string? Problem5 { get; set; }
        public string? Problem6 { get; set; }
        public string? Problem7 { get; set; }
        public string? Problem8 { get; set; }
    }
}
