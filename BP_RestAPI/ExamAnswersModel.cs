using System.ComponentModel.DataAnnotations;

namespace BP_RestAPI
{
    public class ExamAnswersModel
    {
        [Key]
        public int AnswerID { get; set; }
        public int UserID { get; set; }
        public int ExamID { get; set; }
        public int Result { get; set; }
        public int MaxPossible { get; set; }
    }
}
