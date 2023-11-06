namespace HackHeroes0._1.Models.PostResponse
{
    public class FacesRecognized
    {
        public List<string> recognized_faces { get; set; } = default!;
        public string unrecognized_faces { get; set; } = default!;
    }
}
