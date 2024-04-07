namespace hwApril1.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<QuestionTag> QuestionTags { get; set; }
    }
}
