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
        public int Id { get; set; }
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public int DirectionId { get; set; }
        [ForeignKey("DirectionId")]
        public Direction Direction { get; set; }
        public int ClassCarId { get; set; }
        [ForeignKey("ClassCarId")]
        public ClassCar ClassCar { get; set; }
        public string DriverId { get; set; }
        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }
        public int OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }
        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }
        public DateTime DatePost { get; set; }
        public TimeSpan TimePost { get; set; }
        public DateTime DateBegin { get; set; }
        public TimeSpan TimeBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public TimeSpan TimeEnd { get; set; }
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
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
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
}