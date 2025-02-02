namespace WebStore.DTOs
{
    public class LinkDTO
    {
        public string Href { get; set; }  // Link URL
        public string Rel { get; set; }   // Relationship (self, update, delete, etc.)
        public string Method { get; set; }  // HTTP Method (GET, POST, DELETE)

        public LinkDTO(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
