namespace BP_RestAPI
{
    public class UserModel : UserBase
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public int? ChosenClassID { get; set; }
        public int isTeacher { get; set; }
        public int BronzeMedals { get; set; }
        public int SilverMedals { get; set; }
        public int GoldMedals { get; set; }
        public int Avatar { get; set; }
        public int Title1 { get; set; }
        public int Title2 { get; set; }
        public int Title3 { get; set; }
        public int Title4 { get; set; }
        public int Title5 { get; set; }
        public int Title6 { get; set; }
        public int ChosenTitle { get; set; }
    }
}
