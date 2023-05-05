using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library3.Models;
using Library3.DB_data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace FrontDeskApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HotelDbContext dx = new();
        private readonly LocalView<Room> Rooms;
        private readonly LocalView<Booking> Bookings;
        private readonly LocalView<Customer> Customers;
        private readonly LocalView<Service> Services;
        public MainWindow()
        {
            InitializeComponent();

            Rooms = dx.Rooms.Local;
            Bookings = dx.Bookings.Local;
            Customers = dx.Customers.Local;
            Services = dx.Services.Local;

            dx.Rooms.Load();
            dx.Bookings.Load();
            dx.Customers.Load();
            dx.Services.Load();

            DateTime checkInDate = DateTime.Now;
            DateTime checkOutDate = DateTime.Now.AddDays(1);

            // Populates the lists with all the relevant entries
            allRooms();
            allBookings();
            allServices();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            dx.Rooms.Load();
            dx.Bookings.Load();
            dx.Customers.Load();
            string searchText = searchBox.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                // If the search field is empty, all the rooms are displayed
                allRooms();
            }
            else if (int.TryParse(searchText, out int roomNumber))
            {

                var filteredList = (from r in Rooms
                                    join b in Bookings on r.Id equals b.RoomId into roomBookings
                                    from rb in roomBookings.DefaultIfEmpty()
                                    join c in Customers on rb?.CustomerId equals c.Id into customerBookings
                                    from cb in customerBookings.DefaultIfEmpty()
                                    where r.RoomNumber.ToString().Contains(searchText)
                                    select new
                                    {
                                        RoomNumber = r.RoomNumber,
                                        RoomType = r.RoomType,
                                        NumBeds = r.NumBeds,
                                        Price = r.Price,
                                        AvailableFrom = r.AvailableFrom,
                                        AvailableTo = r.AvailableTo,
                                        Booked = r.Booked,
                                        CustomerFirstName = cb != null ? cb.FirstName : "",
                                        CustomerLastName = cb != null ? cb.LastName : "",
                                        CheckInDate = rb != null ? rb.CheckInDate : (DateTime?)null
                                    }).ToList();

                roomList.ItemsSource = filteredList;
            }
        }

        private void SearchBooking_Click(object sender, RoutedEventArgs e)
        {
            dx.Rooms.Load();
            dx.Bookings.Load();
            dx.Customers.Load();
            string BookingSearchText = bookingSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(BookingSearchText))
            {
                // If the search field is empty, all the bookings are displayed
                allBookings();
            }
            else
            {
                var filteredList = (from b in Bookings
                                    join c in Customers on b.CustomerId equals c.Id
                                    where c.LastName == bookingSearch.Text
                                    select new
                                    {
                                        LastName = c.LastName,
                                        FirstName = c.FirstName,
                                        Email = c.Email,
                                        RoomNumber = b.Room.RoomNumber,
                                        CheckInDate = b.CheckInDate,
                                        CheckOutDate = b.CheckOutDate
                                    }).ToList();

                bookingList.ItemsSource = filteredList;
            }
        }

        //Class for adding students to a course
        private void changeRoomButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                var customerLastName = deleteCustomerLastNameTextBox.Text;
                var oldRoomNumber = int.Parse(deleteRoomNumberTextBox.Text);
                var newRoomNumber = int.Parse(deleteCheckInDatePicker.Text);

                var oldRoom = context.Rooms.SingleOrDefault(r => r.RoomNumber == oldRoomNumber);
                if (oldRoom == null)
                {
                    // Room not found
                    return;
                }

                var newRoom = context.Rooms.SingleOrDefault(r => r.RoomNumber == newRoomNumber);
                if (newRoom == null)
                {
                    // Room not found
                    return;
                }

                var booking = context.Bookings.Include(b => b.Customer).SingleOrDefault(b => b.RoomId == oldRoom.Id && b.Customer.LastName == customerLastName);
                if (booking == null)
                {
                    // Booking not found
                    return;
                }

                booking.Room = newRoom;
                context.SaveChanges();

                roomChangeResult.Text = "Room Changed Sucessfully";
            }

        }

        private void addReservationButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                var customerFirstName = customerFirstNameTextBox.Text;
                var customerLastName = customerLastNameTextBoxBooking.Text;
                var customerEmail = customerEmailTextBox.Text;

                var checkinDate = (DateTime)checkInDatePicker.SelectedDate;
                var checkoutDate = (DateTime)checkOutDatePicker.SelectedDate;

                var roomNumber = int.Parse(roomNumberTextBox.Text);
                var customerId = 0;

                // Check if a customer with the same first name, last name, and email already exists in the database
                var existingCustomer = context.Customers.FirstOrDefault(c => c.FirstName == customerFirstName &&
                                                                             c.LastName == customerLastName &&
                                                                             c.Email == customerEmail);
                if (existingCustomer != null)
                {
                    customerId = existingCustomer.Id;
                }
                else
                {
                    // Create a new customer entity if one does not exist
                    var newCustomer = new Customer
                    {
                        FirstName = customerFirstName,
                        LastName = customerLastName,
                        Email = customerEmail
                    };
                    context.Customers.Add(newCustomer);
                    context.SaveChanges();

                    customerId = newCustomer.Id;
                }

                // Find the room entity by its RoomNumber property
                var room = context.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room == null)
                {
                    // Room not found
                    addReservationResult.Text = "Room not found.";
                    return;
                }

                // Create a new booking entity with the given room, customer, and checkin/checkout dates
                var newBooking = new Booking
                {
                    RoomId = room.Id, // Set RoomId to the Id of the room entity
                    CustomerId = customerId,
                    CheckInDate = checkinDate,
                    CheckOutDate = checkoutDate
                };
                context.Bookings.Add(newBooking);

                // Set the Booked status of the room to true
                room.Booked = true;

                context.SaveChanges();

                // Display success message
                addReservationResult.Text = "Reservation added successfully!";
            }
        }


        private void deleteReservationButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                var customerLastName = deleteCustomerLastNameTextBox.Text;
                var roomNumber = int.Parse(deleteRoomNumberTextBox.Text);
                var checkinDate = deleteCheckInDatePicker.SelectedDate;

                // Find booking based on customer last name, room number, and check-in date
                var booking = context.Bookings
                    .Include(b => b.Customer)
                    .Include(b => b.Room)
                    .SingleOrDefault(b =>
                        b.Customer.LastName == customerLastName &&
                        b.Room.RoomNumber == roomNumber &&
                        b.CheckInDate == checkinDate);

                if (booking == null)
                {
                    // Booking not found
                    deleteReservationResult.Text = "Booking not found.";
                    return;
                }

                // Get the room associated with the booking and set its Booked status to false
                var room = booking.Room;
                room.Booked = false;

                // Remove booking from context and save changes
                context.Bookings.Remove(booking);
                context.SaveChanges();

                // Update UI
                deleteReservationResult.Text = "Booking deleted successfully.";
            }
        }

        private void CheckinButton_Click(object sender, RoutedEventArgs e)
{
    using (var context = new HotelDbContext())
    {
        var customerLastName = checkinoutCustomerLastNameTextBox.Text;

        // Find the customer entity with the given last name
        var customer = context.Customers.FirstOrDefault(c => c.LastName == customerLastName);
        if (customer == null)
        {
            checkinoutStatusTextBlock.Text = "Customer not found.";
            return;
        }

        // Find the booking entity for the given customer that has not been checked in
        var booking = context.Bookings.FirstOrDefault(b => b.CustomerId == customer.Id);
        if (booking == null)
        {
            checkinoutStatusTextBlock.Text = "No booking found for customer.";
            return;
        }

        // Update the booking entity with the current time for check-in
        booking.CheckInDate = DateTime.Now;

        // Update the booked status of the associated room
        var room = context.Rooms.FirstOrDefault(r => r.Id == booking.RoomId);
        if (room != null)
        {
            room.Booked = true;
        }

        context.SaveChanges();

        checkinoutStatusTextBlock.Text = "Checked in successfully!";
    }
}

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                var customerLastName = checkinoutCustomerLastNameTextBox.Text;

                // Find the customer entity with the given last name
                var customer = context.Customers.FirstOrDefault(c => c.LastName == customerLastName);
                if (customer == null)
                {
                    checkinoutStatusTextBlock.Text = "Customer not found.";
                    return;
                }

                // Find the booking entity for the given customer that has not been checked out
                var booking = context.Bookings.FirstOrDefault(b => b.CustomerId == customer.Id);
                if (booking == null)
                {
                    checkinoutStatusTextBlock.Text = "No active booking found for customer.";
                    return;
                }

                // Update the booking entity with the current time for check-out
                booking.CheckOutDate = DateTime.Now;

                // Update the booked status of the associated room
                var room = context.Rooms.FirstOrDefault(r => r.Id == booking.RoomId);
                if (room != null)
                {
                    room.Booked = false;
                }

                context.SaveChanges();

                checkinoutStatusTextBlock.Text = "Checked out successfully!";
            }
        }

        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                // Parse input values
                if (!int.TryParse(serviceRoomNumberTextBox.Text, out int roomNumber))
                {
                    serviceFeedbackTextBlock.Text = "Invalid room number.";
                    return;
                }
                string serviceType = serviceTypeTextBox.Text.Trim();
                string description = serviceDescriptionTextBox.Text.Trim();

                // Find the room entity with the given room number
                var room = context.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room == null)
                {
                    serviceFeedbackTextBlock.Text = "Room not found.";
                    return;
                }

                // Create a new service request entity and set its properties
                var serviceRequest = new Service
                {
                    RoomId = room.Id,
                    RequestType = serviceType,
                    DateRequested = DateTime.Now,
                    Description = description
                };

                // Add the new service request to the database
                context.Services.Add(serviceRequest);
                context.SaveChanges();

                // Update the data grid to show the new service request
                serviceRequestsDataGrid.ItemsSource = context.Services.ToList();

                serviceFeedbackTextBlock.Text = "Service request added successfully!";
            }
        }

        private void allRooms()
        {
            dx.Rooms.Load();
            dx.Bookings.Load();
            dx.Customers.Load();
            var filteredList = (from r in Rooms
                                join b in Bookings on r.Id equals b.RoomId into roomBookings
                                from rb in roomBookings.DefaultIfEmpty()
                                join c in Customers on rb?.CustomerId equals c.Id into customerBookings
                                from cb in customerBookings.DefaultIfEmpty()
                                select new
                                {
                                    RoomNumber = r.RoomNumber,
                                    RoomType = r.RoomType,
                                    NumBeds = r.NumBeds,
                                    Price = r.Price,
                                    AvailableFrom = r.AvailableFrom,
                                    AvailableTo = r.AvailableTo,
                                    Booked = r.Booked,
                                    CustomerFirstName = cb != null ? cb.FirstName : "",
                                    CustomerLastName = cb != null ? cb.LastName : "",
                                    CheckInDate = rb != null ? rb.CheckInDate : (DateTime?)null

                                }).ToList();
            roomList.ItemsSource = filteredList;
        }

        private void allBookings()
        {
            dx.Rooms.Load();
            dx.Bookings.Load();
            dx.Customers.Load();
            var filteredList = (from b in Bookings
                                join c in Customers on b.CustomerId equals c.Id
                                select new
                                {
                                    LastName = c.LastName,
                                    FirstName = c.FirstName,
                                    Email = c.Email,
                                    RoomNumber = b.Room.RoomNumber,
                                    CheckInDate = b.CheckInDate,
                                    CheckOutDate = b.CheckOutDate
                                }).ToList();

            bookingList.ItemsSource = filteredList;

        }


private void allServices()
            {
                dx.Rooms.Load();
                dx.Services.Load();
                var filteredList = (from s in dx.Services
                                    join r in dx.Rooms on s.RoomId equals r.Id
                                    select new
                                    {
                                        RoomNumber = r.RoomNumber,
                                        ServiceType = s.RequestType,
                                        DateRequested = s.DateRequested,
                                        Description = s.Description,
                                        DateCompleted = s.DateCompleted
                                    }).ToList();

            serviceRequestsDataGrid.ItemsSource = filteredList;
            }

    }
}
