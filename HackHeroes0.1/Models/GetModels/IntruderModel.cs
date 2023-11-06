namespace HackHeroes0._1.Models.GetModels
{
    public class IntruderModel
    {
        public IntruderModel(string? url)
        {
            unrecognizedFaceUrl = url;
        }
        public string? unrecognizedFaceUrl { get; set; }
    }
}
