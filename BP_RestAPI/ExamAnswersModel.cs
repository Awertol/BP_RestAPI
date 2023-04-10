namespace BP_RestAPI
{
    public class ExamAnswersModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExamId { get; set; }
        public int NumCorrect { get; set; }
        public int NumMax { get; set; }
    }
}
