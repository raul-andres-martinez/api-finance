namespace Finance.Domain.Models.Entities
{
    public class Entity
    {
        public Entity()
        {
            Uid = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Uid { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
