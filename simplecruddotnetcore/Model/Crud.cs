namespace crudoperation.Model
{
    public class Crud
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email {  get; set; }
        public string country {  get; set; }
    }


    //++++++++++++++++ audit log +++++++++++++++++++++++++++++++++++
    public class Audit
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string TableName { get; set; } 
        public DateTime ActionTimestamp { get; set; }
        public string ActionData { get; set; } 
        public int UserId { get; set; } 
    }

}
