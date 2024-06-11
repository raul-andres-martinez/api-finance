namespace Finance.Domain.Models.Entities
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
