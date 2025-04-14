using Project1.Models;

public class CustomerStores{
        public Guid CustomerId { get; set;}
        public Guid StoreId { get; set;}

        public CustomerStores() {}

        public CustomerStores(Guid custmerId, Guid storeId){
            CustomerId = custmerId;
            StoreId = storeId;
        }

}