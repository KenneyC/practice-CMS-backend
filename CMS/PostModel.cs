using System;

namespace practice_CMS_backend.CMS
{
    public class PostModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public string MainBody { get; set; }
    }
}