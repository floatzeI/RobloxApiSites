namespace Roblox.Administration.Partials
{
    public class LookupModel
    {
        public string fieldName { get; set; }
        public int minLength { get; set; } = 1;
        public int maxLength { get; set; } = 255;
        public string fieldId { get; set; }
        public string fieldType { get; set; }
    }
}