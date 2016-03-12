using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AaluxWeb.Models
{
    public class Order
    {
        [Display(Name ="ID")]
        public int Id { get; set; }

        [Display(Name = "Client")]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [Display(Name = "Direction")]
        public int DirectionId { get; set; }
        [ForeignKey("DirectionId")]
        public Direction Direction { get; set; }

        [Display(Name = "Car class")]
        public int ClassCarId { get; set; }
        [ForeignKey("ClassCarId")]
        public ClassCar ClassCar { get; set; }

        [Display(Name = "Driver")]
        public string DriverId { get; set; }
        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }

        [Display(Name = "Order status")]
        public int OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "Payment")]
        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }

        [Display(Name = "Post date")]
        public DateTime DatePost { get; set; }

        [Display(Name = "Post time")]
        public TimeSpan TimePost { get; set; }

        [Display(Name = "Begin date")]
        public DateTime DateBegin { get; set; }

        [Display(Name = "Begin time")]
        public TimeSpan TimeBegin { get; set; }

        [Display(Name = "End")]
        public DateTime DateEnd { get; set; }

        [Display(Name = "End time")]
        public TimeSpan TimeEnd { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }
    }

    public class NewOrder
    {
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public int DirectionId { get; set; }
        [ForeignKey("DirectionId")]
        public Direction Direction { get; set; }
        public int ClassCarId { get; set; }
        [ForeignKey("ClassCarId")]
        public ClassCar ClassCar { get; set; }
        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }
        public DateTime DateBegin { get; set; }
        public TimeSpan TimeBegin { get; set; }
        public double Price { get; set; }
    }


    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }

    public class Driver
    {
        public string Id { get; set; }
        [ForeignKey("Id")]
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsBusy { get; set; }
        public bool IsFired { get; set; }
        public DateTime Birthday { get; set; }


        public ICollection<Car> Cars { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Driver()
        {
            Cars = new List<Car>();
            Orders = new List<Order>();
        }

    }

    public class Car
    {
        public int Id { get; set; }
        public int ClassCarId { get; set; }
        [ForeignKey("ClassCarId")]
        public ClassCar ClassCar { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public Driver Driver { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string BodyType { get; set; }
        public string Color { get; set; }
        public DateTime YearOfRelease { get; set; }
        public int Capacity { get; set; }
        public string ShortCharacter { get; set; }
        public string ImageLink { get; set; }
        public double Price { get; set; }
    }

    public class ClassCar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public ICollection<Car> Cars { get; set; }
        public ClassCar()
        {
            Cars = new List<Car>();
        }
    }

    public class Direction
    {
        public int Id { get; set; }
        public string AddressOrigin { get; set; }
        public string LatOrigin { get; set; }
        public string LngOrigin { get; set; }
        public string AddressDestination { get; set; }
        public string LatDestination { get; set; }
        public string LngDestination { get; set; }
    }

    public class License
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public Driver Driver { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }

    }

    public class Payment
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TabsViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<License> Licenses { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }

    public class EditDriverViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Is available ?")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
    }

}