namespace BP_RestAPI
{
    public class UserModel : UserBase
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int? ChosenClassID { get; set; }
        public int isTeacher { get; set; }
        public int BronzeMedals { get; set; }
        public int SilverMedals { get; set; }
        public int GoldMedals { get; set; }
        public int Avatar { get; set; }
    }
}
