using System;

namespace MyShop.Domain.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        public string Name { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        private byte[] profilePicture;// backing field.
        //With lazy Initialization the data is only loades when something request it.
        public byte[] ProfilePicture
        {
            get
            {
                //Load the data and only loaded one, onece the backing field, it´s not Initialized.
                //The biggest problem with lazy initialization and coupling that insider customer entity
                // is that our entity now needs to know about things that it really shouldn´t have
                // to  care about. Entity needs to know about our profileservice and I don´t particularly
                // like that coupling 
                if (profilePicture == null)
                    profilePicture = ProfilePictureService.GetFor(Name);
                return profilePicture;
            }
            set { profilePicture = value; }
        }


        public Customer()
        {
            CustomerId = Guid.NewGuid();
        }
    }
}
