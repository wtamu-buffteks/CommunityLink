# Changelog


All notable changes to this project will be documented in this file.


The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).
Here’s a changelog message for the `requestorServices` page that aligns with your previous entries:

---

## [0.5.3] - 2024-09-23
### Added
- added /docs folder to contain the information for the github pages

## [0.5.2] - 2024-09-11
### Fixed
- Fixed links from "#" to "/Events" (Index Page).
- Fixed request and volunteer pages for non-signed-in users (requestorServices Page & Volunteer Page).
    - Request Page displays information for non-signed in users directing them to sign in (requestorServices.cshtml & requestorServices.cshtml.cs)
    - Volunteer Page displays all Requests(to donate to) for non-signed in users and redirects to Sign in if they try to "donate" to a request. (Volunteer.csthml & Volunteer.cshtml.cs)
- Removed unnecessary count from myStat Page (MyStatistics.cshtml)
- Replaced lorem ipsum text with relevant content (About.cshtml & Donate.cshtml)

## [0.5.1] - 2024-08-26
### Fixed
- Fixed the error where employees who don't have stats couldn't access the overall statistics page

## [0.5.0] - 2024-08-25
### Added
- New Statistics page where employees can view the statistics for the whole organization
- Added an option in the employee dropdown where employees can access the new page
- Uses the same layout as the MyStats page

## [0.4.7] - 2024-08-23
### Added
- New "Events" page accessible from the main navigation bar.
- Users can view all upcoming events, with the ability to click on event names to see detailed information in a pop up.
- Users can RSVP to events directly from the event details.
 - RSVP will count signed-in users only once per event
 - Non signed-in users can RSVP without limit
- Introduced the ability for users to view events they have RSVP'd to, with a dedicated "View My RSVP Events" button.
- Users can sort events by name or date, and navigate through events using pagination controls.

### Changed
- "Events" tab on nav bar in _layout, making it easier for users to find and engage with upcoming community events.

## [0.4.6] - 2024-08-23
### Added
- New Employee action: Event Management
- Employees can add new events
- Once an event is created, they can assign inventory to it provided that the inventory isn't already assigned to a request
- They can also remove events
## Changed
- Added the event management tab to the employee dropdown

## [0.4.5] - 2024-08-21
### Added
- Introduced a new `My Stats` page where users can view their contribution statistics, including total donations, monetary and other as well as some functionality for volunteer hours.
- Implemented detailed tables for monetary donations, items donated, and volunteer hours.
- Integrated sorting functionality for the statistics, allowing users to sort by Title, Amount Donated, and Donation Date.
- Visual charts were added using `Chart.js` to represent monetary and item donations. (Used script src)

### Changed
- Enhanced the navbar to include a dropdown for "My Account" with options for "My Profile" and "My Stats."
- Decreased Navbar font size from 24px to 22px to better layout options.
- Updated CSS to ensure the dropdown matches the style of other navbar items, removing the border and making the background transparent. (Height/Position may need to be adjusted)
- Adjusted the navbar font style and size for consistency and readability.

### Note
- Implemented resizable containers for tables and charts on the `My Stats` page to improve responsiveness for users.
- Added scrollable tables with a fixed height to ensure data visibility without disrupting the page layout.
- Tested functionality/database as Overlord (Made monetary and other donations)

## [0.4.4] - 2024-8-18
### Added
- Messages page where users can send and view messages. Admins can send action needed messages.
- The messages icon. I initially was going to make the background white when there were no messages, but it blended in with the background so I made it blue. The Icon turns into the back-button color when there is an unread message and it turns red when there is an action needed message. There is also a count on the message that shows how many new messages you have.
- Made messages service that's injected into the _Layout so that it will run the check on every page.

## [0.4.3] - 2024-08-13
### Added
- Added "Home" to navBar in _layout.cshtml
- Used Template (author referenced in index.html)
    - "carosel" greeting
    - "Marketing" bubbles
    - "featurettes" with placeholder images
- Added CSS required for template 
- Added 9 PNG images including CommunityLink Logo
- Added buttons for if the user is not signed in: SignIn, Create Account, About, Donate, Events(index)
- Added buttons for if the user is signed in: About, Donate, Events(index) Request, Volunteer

### Changed
- #givingFood in site.css to better display on the About page

### Note
- I tried to implement lazy loading with the images. I also went ahead and made a simple quick png of the logo, really just for seeing how it might look when used on the page.
- Links for "Events" currently redirect to index.html, these will need to be altered after Events page is made

## [0.4.2] - 2024-14-24
### Added
- Added admin actions to Inventory management page
- Added the user management page, only admins have access to it. They can edit or remove users and filter by user types.

## [0.4.1] - 2024-08-08
### Added
- requestorServices page now allows requestors to add, update, and inactivate requests directly from their personal dashboard.
- Implemented modal form for adding new requests, including fields for title, deadline, category, description, amount, and status.
- Pagination and sorting functionality on requestorServices page to navigate through and sort requests based on title, date, deadline, and status.

### Changed
- Requests associated with the logged-in requestor are now properly displayed and managed through the requestorServices page.
- If a user is not a requestor/does not have requests, then adding a request will make them a requestor and display their requests.
- The database is now correctly updated when a request is added, updated, or inactivated.
- "Requests" tab in _layout NavBar linked to requestorServices Page

### Note
- Delete request could be harmful if a user is not a requestor, and adds a request(becomes requestor), then deletes their (only) request. 
- It can be harmful if a requestor updates their profile to no longer be a requestor as the user would still be associated with requests.

## [0.4.0] - 2024-8-6
### Added
- Inventory management page
- Employees can manage existing inventory such as taking them in and assigning a user, assigning the item to a specific request if necessary, and editing existing requests. Because changing the associated user, request, or location could have destructive affects or cause confusion, I’ll only give this ability to admins down the line. Employees will still be able to assign these values on initial creation. When an item is donated, the user’s stats are also updated to include the donation. 

### Changed
-  Modified the database setup so that inventory can be associated with a request and Inventory no longer suffers from the oversight that caused inventory to need to exist at a warehouse and event at the same time. 

### Note
-  When I finished this pull request, I was very tired, so I may have missed a bug or failed to catch a problem in my testing, so don’t be afraid to let me know if you see any problems or mistakes so I can fix them before it’s merged with the development branch.


## [0.3.1] - 2024-7-31
### Added
- Location Management Page. Admins can Add, edit, view, and remove locations. Any non-Admin will be redirected to home. Trying to access the page while not signed in will bring you to the sign-in page.
- Location Management Page to the employee Actions dropdown. It will only appear if the User is an employee that is an admin.

### Changed
- The middleware now checks if the user is admin

## [0.3.0] - 2024-7-28
### Added
- The employee actions dropdown
- The employee request management page
- Middleware that reestablishes the Session variables if they are signed in by the cookie

### notes
- attempting to access the page as a non-employee will redirect you to home

## [0.2.7] - 2024-7-24
### Notes
- Reset the database

### Added
- Volunteer Services page. Users can now visit the page, choose a request, read about it, and make a donation to that request. If a user attempts to access the page without being signed in, they are redirected to the sign-in page.
- The Volunteer Services page handles requests based on what category they fall under.
- More requests for the sake of testing sorting options

### Changed
- Modified seed data to make more reqeusts by default
- Modified the Nav bar to include the new page.
- The donation page now defaults back to the Volunteer page if someone attempts to access it without having a request defined

## [0.2.6] - 2024-7-17

### Notes
- This branch was created before the branch fixing the changelog was approved and merged. To minimize conflicts, I have also included the changelog fix in this branch. Both files are now correct and consistent, with this one including the latest notes.

- I set the donation page to always donate to the first request in this version. When I implement the volunteer page next week, I'll set each request to go to the volunteer page.

- The donations page was set to: "$10 is the minimum online donation. All donations are tax deductible." I went ahead and kept this, but since this application is meant for general useage, should we keep this? The Red Cross may have that minimum, but not every organization will.

### Added
- The application now automatically signs you in upon creating an account, so the user does not need to sign in right after creating an account.

- If someone accesses the donation page, but isn't signed in, it will redirect them to the sign-in page as we can't update their stats if we don't know who they are and the organization would need to keep track of donations for legal purposes. When I implement the volunteer page, I'll make the "Donate Now" button link to it so users can choose what specifically they'd like to donate to.

- If the user makes a donation, and isn't a volunteer, it will automatically check them in as one now that they have now volunteered resources to a request.

- The user can now donate to requests. When I implement the volunteer page next week, the user will be able to choose which one to donate to

### changed

- RequestStats now also holds onto the id of the original request to avoid confusion if two requestors make a request with the same name

### Fixed
- Fixed an oversight that caused the phone number not to be saved when a user creates an account.

## [0.2.5] - 2024-7-10

### Added
#### My Profile page
- It will not allow a non-signed in user to access the page
- The user can change their Username, Password(if they confirm it first), Email, Phone Number, Full Name, Address. They can check themselves in or out of being a Volunteer or Requestor.

## [0.2.4] - 2024-6-27

### Added

- The application will now remember if you are signed in
- The cookie will last up to 7 days, but if you block the cookie, it will still remember you for the session
- Gave the ability to sign out

### Changes
- Applied lazy loading to the images to improve performance

### Removed
- Removed placeholderIndex.svg from the index page as it significantly slowed down the website's performance due to its large size and reliance on external resources. It is simply too large and complex to be called with a single image tag. The file remains in the images folder for reference. We can use its design elements and incorporate them into the index page in a way that maintains performance and SEO.

## [0.2.3] - 2024-6-25

### Added
- About was given css elements
- image file "GivingFood" added

### Changes
- changes to About.cshtml css to match other pages

## [0.2.2] - 2024-6-24


### Added
- bin and obj added to gitignore
- About, Donate and Donation page
- Donate was given css elements
- image folder and files
- translucent, fixed navigation bar


### Changes
- layout cshtml updated to reflect pages in XD layout
- changes to index and sign-in css to look correct after fixed nav bar implemented


## [0.2.1] - 2024-6-21

### Changed
- Quick update and Changelog fix


### Known Bugs
- If you don't type in the correct sign in info on the sign in page, it will show errors for into in both forms rather than just the one you pressed

## [0.2.0] - 2024-6-21

### Added
- Ability to sign in
- Ability to add a user

### Fixed
- Corrected the model namespace to have the correct name for the project

## [0.1.0] - 2024-06-10

### Added
- Added a changelog
- Added a gitignore to prevent version control cluttering

### Changed

- Updated README.md on coding procedures

