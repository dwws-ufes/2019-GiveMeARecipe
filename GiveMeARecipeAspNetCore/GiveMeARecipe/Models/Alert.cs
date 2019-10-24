namespace GiveMeARecipe.Models
{
    public class Alert
    {
        public string Message { get; set; }
        public string Title { get; set; }
        
        public enum AlertType
        {
            Error,
            Success
        }
        public AlertType Type { get; set; }
    }
}
