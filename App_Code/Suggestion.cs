using System;

namespace WebSite1 {

    public class Suggestion
    {
        public Suggestion(String link, String description)
        {
            this.link= link;
            this.description= description;
        }

        public String link { get; set; }
        public String description { get; set; }
    }

}
