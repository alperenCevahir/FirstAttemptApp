namespace FirstAttempt.Core
{
    public abstract class BaseEntitiy
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate{ get; set; }


    }
}
