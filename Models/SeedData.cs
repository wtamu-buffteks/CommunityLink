using CommunityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityLink.Models {
    public static class SeedData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new CommunityLinkDbContext( 
                serviceProvider.GetRequiredService<DbContextOptions<CommunityLinkDbContext>>())) {
                    if (context.Users.Any()) {
                        return; // No need to seed database
                    }
                    var nosubUser = new User { //No subclasses
                        Username = "Hero"
                    };
                    nosubUser.SetPassword("Hero123");//Password can't be accessed directly
                    var nosubUserStats = new Stat {
                        User = nosubUser,
                        DonationStats = new List<DonationStat>{},
                        RequestStats = new List<RequestStat> {}
                    };

                    var volunteerUser = new User {// Volunteer only
                        Username = "Spider",
                        Email = "ILoveSpiders@example.com",
                        PhoneNumber = "(806) 555-1234",
                        FullName = "Susan Green",
                        UserStatus = "Active",
                        UserLocation = "1234 Spider Dr. Amarillo, TX 79109",
                        StartDate = new DateTime(2024, 6, 9)
                    };
                    volunteerUser.SetPassword("Spider123");
                    var volunteerUservol = new Volunteer {
                        User = volunteerUser
                    };
                    var volunteerUserStats = new Stat {
                        User = volunteerUser,
                        DonationStats = new List<DonationStat>{},
                        RequestStats = new List<RequestStat> {}
                    };
                    volunteerUserStats.RequestStats.Add(
                        new RequestStat{
                            Stat = volunteerUserStats,
                            RequestorUsername = "James43",
                            OldRequestID = 997,
                            AmountDonated = 50.00f,
                            DonationDate = new DateTime(2024, 6, 10),
                            RequestTitle = "Broken Tire"
                        }
                    );

                    var employeeUser = new User {//employee, recently went inactive
                        Username = "Overlord",
                        Email = "Overlord@example.com",
                        PhoneNumber = "(777) 555-7777",
                        FullName = "Over Lord",
                        UserStatus = "Inactive",
                        UserLocation = "7777 Over st. Lord, OK, 77777",
                        StartDate = new DateTime(2001, 5, 18),
                        EndDate = new DateTime(2024, 6, 9)
                    };
                    employeeUser.SetPassword("Overlord123");
                    var employeeUseremp = new Employee {
                        User = employeeUser,
                        Role = "Admin",
                        HoursWorked = 47840
                    };
                    var employeeUserStats = new Stat {
                        User = employeeUser,
                        DonationStats = new List<DonationStat>{},
                        RequestStats = new List<RequestStat> {}
                    };

                    var requestorUser = new User { //Requestor
                        Username = "Danger",
                        Email = "Dangerous@example.com",
                        PhoneNumber = "(123) 555-4567",
                        FullName = "Danger Red",
                        UserStatus = "Active",
                        UserLocation = "3453 Red st. Rojo, NM 12345",
                        StartDate = new DateTime(2023, 9, 22)
                    };
                    requestorUser.SetPassword("Danger123");
                    var requestorUserreq = new Requestor {
                        User = requestorUser,
                        Requests = new List<Request> {}//Because Request requires Requestor, you have to establish the requests after the requestor exsists
                    };
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 5000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "School Supplies",
                            RequestDeadline = new DateTime(2024, 9, 1),
                            RequestDescription = "Raising funds to purchase school supplies for underprivileged children.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Office Renovation",
                            RequestDeadline = new DateTime(2024, 10, 1),
                            RequestDescription = "Volunteers needed to help with office renovation.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 3000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Community Center Fund",
                            RequestDeadline = new DateTime(2024, 11, 15),
                            RequestDescription = "Raising funds for the community center construction.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Art Supplies Donation",
                            RequestDeadline = new DateTime(2024, 7, 25),
                            RequestDescription = "We need donations of art supplies for the community art classes.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 7000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Library Fund",
                            RequestDeadline = new DateTime(2024, 8, 20),
                            RequestDescription = "Raising funds to expand the local library.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Park Cleanup Volunteers",
                            RequestDeadline = new DateTime(2024, 9, 10),
                            RequestDescription = "Looking for volunteers to help clean up the local park.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 2000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Sports Equipment",
                            RequestDeadline = new DateTime(2024, 9, 30),
                            RequestDescription = "Raising funds to purchase sports equipment for the youth center.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Computer Donation",
                            RequestDeadline = new DateTime(2024, 10, 5),
                            RequestDescription = "We need donations of computers for the community center.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 4000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Medical Supplies Fund",
                            RequestDeadline = new DateTime(2024, 8, 15),
                            RequestDescription = "Raising funds to purchase medical supplies for the health clinic.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Furniture Donations",
                            RequestDeadline = new DateTime(2024, 9, 20),
                            RequestDescription = "Requesting donations of furniture for the new community hall.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 6000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Scholarship Fund",
                            RequestDeadline = new DateTime(2024, 12, 1),
                            RequestDescription = "Raising funds to provide scholarships to deserving students.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Community Garden Volunteers",
                            RequestDeadline = new DateTime(2024, 8, 30),
                            RequestDescription = "Looking for volunteers to help maintain the community garden.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 4500,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Youth Program Fund",
                            RequestDeadline = new DateTime(2024, 11, 10),
                            RequestDescription = "Raising funds to support youth programs in the community.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Educational Materials Donation",
                            RequestDeadline = new DateTime(2024, 9, 5),
                            RequestDescription = "We need donations of educational materials for the after-school programs.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 3500,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Elderly Care Fund",
                            RequestDeadline = new DateTime(2024, 10, 20),
                            RequestDescription = "Raising funds to provide better care for the elderly in our community.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Volunteer Drivers",
                            RequestDeadline = new DateTime(2024, 8, 25),
                            RequestDescription = "Looking for volunteer drivers to help transport community members.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 10000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Animal Shelter Fund",
                            RequestDeadline = new DateTime(2024, 9, 15),
                            RequestDescription = "Raising funds to support the local animal shelter.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Cleaning Supplies Donation",
                            RequestDeadline = new DateTime(2024, 7, 30),
                            RequestDescription = "We need donations of cleaning supplies for the community center.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 8000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Emergency Relief Fund",
                            RequestDeadline = new DateTime(2024, 11, 1),
                            RequestDescription = "Raising funds for emergency relief efforts in the community.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = requestorUserreq
                        }
                    );
                    requestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Volunteer Coordinators",
                            RequestDeadline = new DateTime(2024, 9, 25),
                            RequestDescription = "Looking for volunteer coordinators to help organize community events.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = requestorUserreq
                        }
                    );
                    var requestorUserStats = new Stat {
                        User = requestorUser,
                        DonationStats = new List<DonationStat>{},
                        RequestStats = new List<RequestStat> {}
                    };

                    var volunteerEmployeeUser = new User { //volunteer and employee
                        Username = "Frank",
                        Email = "Franerjhnogieqrjkblue@example.com",
                        // no phone
                        FullName = "Frank Blue",
                        UserStatus = "Active",
                        //no location
                        StartDate = new DateTime(2022, 1, 12)
                    };
                    volunteerEmployeeUser.SetPassword("Frank123");
                    var volunteerEmployeeUservol = new Volunteer {
                        User = volunteerEmployeeUser,
                        HoursWorked = 507
                    };
                    var volunteerEmployeeUseremp = new Employee {
                        User = volunteerEmployeeUser,
                        Role = "Worker",
                        HoursWorked = 4160
                    };
                    var volunteerEmployeeUserStats = new Stat {
                        User = requestorUser,
                        DonationStats = new List<DonationStat>{},
                        RequestStats = new List<RequestStat> {}
                    };
                    var requestStat = new RequestStat {
                            Stat = volunteerEmployeeUserStats,
                            RequestorUsername = "Mark978",
                            OldRequestID = 998,
                            DonationDate = new DateTime(2022, 6, 10),
                            RequestTitle = "Need shoes",
                            DonationStats = new List<DonationStat>{}
                    };
                    volunteerEmployeeUserStats.RequestStats.Add(requestStat);
                    var donationStat = new DonationStat {
                        Stat = volunteerEmployeeUserStats,
                        ItemName = "Shoes",
                        Quantity = 1,
                        DateDonated = new DateTime(2022, 6, 10),
                        DateGiven = new DateTime(2022, 6, 11),
                        RequestStat = requestStat
                    };
                    volunteerEmployeeUserStats.DonationStats.Add(donationStat);
                    requestStat.DonationStats.Add(donationStat);

                    var volunteerRequestorUser = new User { //Volunteer and requestor
                        Username = "Junnie",
                        PhoneNumber = "(806) 555-8901",
                        FullName = "June Warrior",
                        UserStatus = "Active",
                        StartDate = new DateTime(2023, 12, 25)
                    };
                    volunteerRequestorUser.SetPassword("Junnie123");
                    var volunteerRequestorUservol = new Volunteer {
                        User = volunteerRequestorUser,
                        HoursWorked = 123
                    };
                     var volunteerRequestorUserreq = new Requestor {
                        User = volunteerRequestorUser,
                        Requests = new List<Request> {}//Because Request requires Requestor, you have to establish the requests after the requestor exsists
                    };
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Office Cleaning",
                            RequestDeadline = new DateTime(2024, 8, 1),
                            RequestDescription = "We need volunteers to help with office cleaning.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 500,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "New Computers",
                            RequestDeadline = new DateTime(2024, 9, 15),
                            RequestDescription = "Raising funds to purchase new computers for the office.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Printer Paper",
                            RequestDeadline = new DateTime(2024, 7, 20),
                            RequestDescription = "We are running low on printer paper and need more supplies.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 1000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Furniture Upgrade",
                            RequestDeadline = new DateTime(2024, 10, 5),
                            RequestDescription = "Raising funds to upgrade office furniture.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Event Planning Volunteers",
                            RequestDeadline = new DateTime(2024, 8, 30),
                            RequestDescription = "Looking for volunteers to help plan upcoming events.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 750,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Office Supplies Fund",
                            RequestDeadline = new DateTime(2024, 9, 25),
                            RequestDescription = "Raising funds to purchase general office supplies.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Trash Bags",
                            RequestDeadline = new DateTime(2024, 7, 25),
                            RequestDescription = "We need donations of trash bags for the office.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 300,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Break Room Supplies",
                            RequestDeadline = new DateTime(2024, 8, 15),
                            RequestDescription = "Funds needed for break room supplies like coffee and snacks.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Meeting Room Setup",
                            RequestDeadline = new DateTime(2024, 8, 5),
                            RequestDescription = "Volunteers needed to help set up the meeting room.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 1200,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Software Licenses",
                            RequestDeadline = new DateTime(2024, 10, 1),
                            RequestDescription = "Funds needed to purchase new software licenses.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Bookshelf Donations",
                            RequestDeadline = new DateTime(2024, 7, 30),
                            RequestDescription = "We need donations of bookshelves for the office.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 600,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Training Program Fund",
                            RequestDeadline = new DateTime(2024, 9, 10),
                            RequestDescription = "Raising funds for employee training programs.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Stationery Supplies",
                            RequestDeadline = new DateTime(2024, 8, 10),
                            RequestDescription = "Requesting donations of stationery supplies.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 2000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Conference Attendance",
                            RequestDeadline = new DateTime(2024, 9, 20),
                            RequestDescription = "Funds needed for attending industry conferences.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = volunteerRequestorUserreq
                        }
                    );
                    volunteerRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Kitchen Cleanup Volunteers",
                            RequestDeadline = new DateTime(2024, 8, 22),
                            RequestDescription = "Looking for volunteers to help clean up the office kitchen.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = volunteerRequestorUserreq
                        }
                    );

                    var volunteerRequestorUserStats = new Stat {
                        User = volunteerRequestorUser,
                        DonationStats = new List<DonationStat>{},
                        RequestStats = new List<RequestStat> {}
                    };
                    volunteerRequestorUserStats.RequestStats.Add(
                        new RequestStat{
                            Stat = volunteerUserStats,
                            RequestorUsername = "Susan777",
                            OldRequestID = 999,
                            AmountDonated = 5000.00f,
                            DonationDate = new DateTime(2023, 12, 25),
                            RequestTitle = "Broken Tire"
                        }
                    );

                    var employeeRequestorUser = new User { //Employee and requestor
                        Username = "Ninja",
                        PhoneNumber = "(894) 555-1342",
                        FullName = "James Brown",
                        UserStatus = "Active",
                        UserLocation = "2394 Dark St. Stimly NY, 94783",
                        StartDate = new DateTime(2013, 2, 08)
                    };
                    employeeRequestorUser.SetPassword("Ninja123");
                    var employeeRequestorUseremp = new Employee {
                        User = employeeRequestorUser,
                        Role = "Worker",
                        HoursWorked = 3862
                    };
                     var employeeRequestorUserreq = new Requestor {
                        User = employeeRequestorUser,
                        Requests = new List<Request> {}//Because Request requires Requestor, you have to establish the requests after the requestor exsists
                    };
                    employeeRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 2000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Cat Food Donation",
                            RequestDeadline = new DateTime(2024, 9, 1),
                            RequestDescription = "We are running low on cat food and need donations to keep our cats well-fed.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = employeeRequestorUserreq
                        }
                    );
                    employeeRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Volunteer for Cat Grooming",
                            RequestDeadline = new DateTime(2024, 8, 25),
                            RequestDescription = "We need volunteers to help groom the cats. No prior experience needed!",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = employeeRequestorUserreq
                        }
                    );
                    employeeRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 1500,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Cat Medical Supplies",
                            RequestDeadline = new DateTime(2024, 10, 5),
                            RequestDescription = "Funds needed to purchase medical supplies for sick and injured cats.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = employeeRequestorUserreq
                        }
                    );
                    employeeRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Cat Adoption Event Volunteers",
                            RequestDeadline = new DateTime(2024, 8, 30),
                            RequestDescription = "Looking for volunteers to help out at our cat adoption event.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = employeeRequestorUserreq
                        }
                    );
                    employeeRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 3000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Cat Shelter Renovations",
                            RequestDeadline = new DateTime(2024, 11, 15),
                            RequestDescription = "Raising funds to renovate and expand our cat shelter.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = employeeRequestorUserreq
                        }
                    );
                    employeeRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Cat Toy Donations",
                            RequestDeadline = new DateTime(2024, 9, 20),
                            RequestDescription = "We need donations of cat toys to keep our feline friends entertained.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = employeeRequestorUserreq
                        }
                    );
                    employeeRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 4000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Winter Heating for Cat Shelter",
                            RequestDeadline = new DateTime(2024, 12, 1),
                            RequestDescription = "Funds needed to ensure the shelter stays warm during the winter months.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = employeeRequestorUserreq
                        }
                    );
                    employeeRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Cat Litter Supply",
                            RequestDeadline = new DateTime(2024, 10, 10),
                            RequestDescription = "Requesting donations of cat litter for our shelter.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = employeeRequestorUserreq
                        }
                    );
                    employeeRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 2500,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Cat Enrichment Activities",
                            RequestDeadline = new DateTime(2024, 9, 15),
                            RequestDescription = "Funds needed for enrichment activities and supplies for the cats.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = employeeRequestorUserreq
                        }
                    );
                    employeeRequestorUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Cat Rescue Volunteers",
                            RequestDeadline = new DateTime(2024, 8, 28),
                            RequestDescription = "Looking for volunteers to assist in rescuing and transporting cats in need.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = employeeRequestorUserreq
                        }
                    );
                    var employeeRequestorUserStats = new Stat {
                        User = employeeRequestorUser,
                        DonationStats = new List<DonationStat>{},
                        RequestStats = new List<RequestStat> {}
                    };

                    var allUser = new User { //all three
                        Username = "Yea-Yea",
                        Email = "Hazel@example.com",
                        PhoneNumber = "(123) 555-0987",
                        FullName = "Hazel Lambert",
                        UserStatus = "Active",
                        UserLocation = "3803 Bread Ave. Amarillo, Texas, 79810",
                        StartDate = new DateTime(2001, 5, 25)
                    };
                    allUser.SetPassword("Yea-Yea123");
                    var allUservol = new Volunteer {
                        User = allUser,
                        HoursWorked = 99999
                    };
                    var allUseremp = new Employee {
                        User = allUser,
                        Role = "Admin",
                        HoursWorked = 999999
                    };
                    var allUserreq = new Requestor {
                        User = allUser,
                        Requests = new List<Request> {}
                    };
                    allUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Food Drive",
                            RequestDeadline = new DateTime(2024, 6, 20),
                            RequestDescription = "We need additional hands at the food drive on the 20th, any help is needed!",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = allUserreq
                        }
                    );
                    allUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Office Supplies",
                            RequestDeadline = new DateTime(2024, 7, 15),
                            RequestDescription = "We need additional office supplies, including pens, paper, and staplers.",
                            RequestStatus = "Active",
                            Category = "Item",
                            Requestor = allUserreq
                        }
                    );
                    allUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 500,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Charity Fundraiser",
                            RequestDeadline = new DateTime(2024, 8, 1),
                            RequestDescription = "Raising funds for the annual charity event to support local shelters.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = allUserreq
                        }
                    );
                    allUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Community Clean-Up",
                            RequestDeadline = new DateTime(2024, 9, 10),
                            RequestDescription = "Volunteers needed for a community clean-up event. All help is appreciated.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = allUserreq
                        }
                    );
                    allUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 1000,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Medical Supplies Donation",
                            RequestDeadline = new DateTime(2024, 7, 30),
                            RequestDescription = "We are in need of funds to purchase medical supplies for the local clinic.",
                            RequestStatus = "Active",
                            Category = "Monetary",
                            Requestor = allUserreq
                        }
                    );
                    allUserreq.Requests.Add(
                        new Request {
                            AmountRequested = 0,
                            AmountRecieved = 0,
                            RequestDate = DateTime.Now,
                            RequestTitle = "Event Setup Crew",
                            RequestDeadline = new DateTime(2024, 8, 25),
                            RequestDescription = "Looking for volunteers to help set up for the upcoming event. Any assistance is welcome.",
                            RequestStatus = "Active",
                            Category = "Labor",
                            Requestor = allUserreq
                        }
                    );
                    var allUserStats = new Stat {
                        User = allUser,
                        DonationStats = new List<DonationStat>{},
                        RequestStats = new List<RequestStat> {}
                    };
                    

                    context.Users.AddRange(nosubUser, volunteerUser, employeeUser, requestorUser, volunteerEmployeeUser, volunteerRequestorUser, employeeRequestorUser, allUser);
                    context.Volunteers.AddRange(volunteerUservol, volunteerEmployeeUservol, volunteerRequestorUservol, allUservol);
                    context.Employees.AddRange(employeeUseremp, volunteerEmployeeUseremp, employeeRequestorUseremp, allUseremp);
                    context.Requestors.AddRange(requestorUserreq, volunteerRequestorUserreq, employeeRequestorUserreq, allUserreq);
                    context.Stats.AddRange(nosubUserStats, volunteerUserStats, employeeUserStats, requestorUserStats, volunteerEmployeeUserStats, volunteerRequestorUserStats, employeeRequestorUserStats, allUserStats);
                    context.SaveChanges();
                }
        }
    }
}