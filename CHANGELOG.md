# Changelog


All notable changes to this project will be documented in this file.


The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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

