namespace OrdersManagementSystem.Entities.Entities
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public int CreatedBy { get; set; }
		public int UpdatedBy { get; set; }
		public bool IsDeleted { get; set; }


	}
}
