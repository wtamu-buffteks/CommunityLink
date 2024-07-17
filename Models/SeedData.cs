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
                                AmountRequested = 15000,
                                AmountRecieved = 0,
                                RequestDate = DateTime.Now,
                                RequestTitle = "Medical Bill",
                                RequestDeadline = new DateTime(2024, 8, 1),
                                RequestDescription = "Desc",
                                RequestStatus = "Active",
                                Category = "Monetary",
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
                                RequestTitle = "Low on dogfood",
                                RequestDeadline = new DateTime(2024, 6, 20),
                                RequestDescription = "Our dog shelter is low on dogfood!",
                                RequestStatus = "Active",
                                Category = "Item",
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
                                AmountRequested = 9000,
                                AmountRecieved = 0,
                                RequestDate = DateTime.Now,
                                RequestTitle = "Cat Shelter fund",
                                RequestDeadline = new DateTime(2024, 8, 20),
                                RequestDescription = "Summer fundrasor for cats! If you'd like to volunteer here, we can be found at the cat shelter on 2394 Dark St. Stimly NY, 94783",
                                RequestStatus = "Active",
                                Category = "Ammount",
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
                                RequestDescription = "We need aditional hands at the food drive on the 20th, any help is needed!",
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