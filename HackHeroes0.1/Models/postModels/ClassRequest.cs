namespace HackHeroes0._1.Models.postModels
{
    public class ClassRequest
    {
        public ClassRequest(string encodedName)
        {
            klasa = encodedName;
        }
        public string klasa { get; set; }
    }
}
