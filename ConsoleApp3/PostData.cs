using System;



    public class PostData
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }

        public string body { get; set; }

    public override string ToString()
        {
            return $"userId:{userId},\nid: {id},\ntitle: {title},\nbody:{body}";
        }


}

