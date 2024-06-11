namespace Finance.Domain.Models
{
    public class Entity
    {
        public Entity()
        {
            Uid = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid Uid { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
