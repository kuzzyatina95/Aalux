﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AaluxWeb.Models
{
    public class TabsViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<License> Licenses { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }

    public class NavDriverViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public Driver Driver { get; set; }
    }

    public class TabOrdersViewModel
    {
        public IEnumerable<Order> OrdersNew { get; set; }
        public IEnumerable<Order> OrdersInProgress { get; set; }
        public IEnumerable<Order> OrdersFinished { get; set; }
        public IEnumerable<Order> OrdersCanceled { get; set; }
    }

    public class DriverOrdersViewModel
    {
        public IEnumerable<Order> AllOrders { get; set; }
        public IEnumerable<Order> CurrentOrders { get; set; }
    }

    public class EditOrderViewModel
    {
        [Display(Name = "ID")]
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

        public Driver Driver { get; set; }

        [Display(Name = "Order status")]
        public int OrderStatusId { get; set; }

        [Display(Name = "Payment")]
        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }

        [Display(Name = "Post date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatePost { get; set; }

        [Display(Name = "Post time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH-MM}", ApplyFormatInEditMode = true)]
        public DateTime TimePost { get; set; }

        [Display(Name = "Begin date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateBegin { get; set; }

        [Display(Name = "Begin time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH-MM}", ApplyFormatInEditMode = true)]
        public DateTime TimeBegin { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateEnd { get; set; }

        [Display(Name = "End time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH-MM}", ApplyFormatInEditMode = true)]
        public DateTime? TimeEnd { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
    }

    public class DirectionViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "From")]
        public string AddressOrigin { get; set; }
        public string LatOrigin { get; set; }
        public string LngOrigin { get; set; }
        [Required]
        [Display(Name = "To")]
        public string AddressDestination { get; set; }
        public string LatDestination { get; set; }
        public string LngDestination { get; set; }
    }

    public class NewOrderViewModel
    {
        public int ClientId { get; set; }
        [Required]
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public int DirectionId { get; set; }
        [Required]
        [ForeignKey("DirectionId")]
        public DirectionViewModel Direction { get; set; }
        [Required]
        public int ClassCarId { get; set; }

        [Required]
        public int PaymentId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime DateBegin { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Time")]
        public DateTime TimeBegin { get; set; }
        public string Price { get; set; }
    }

    public class CarViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Class car")]
        public int ClassCarId { get; set; }
        [ForeignKey("ClassCarId")]
        public ClassCar ClassCar { get; set; }

        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }


        [Display(Name = "Body type")]
        public string BodyType { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Year of release")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime YearOfRelease { get; set; }

        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Display(Name = "Short character")]
        public string ShortCharacter { get; set; }

        public string ImageLink { get; set; }
    }

    public class DriverWithCarViewModel
    {
        public Driver Driver { get; set; }
        public Car Car { get; set; }
    }
}