# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.1.0] - 2024-06-10

### Added
- Added a changelog
- Added a gitignore to prevent version control cluttering

### Changed

- Updated README.md on coding procedures

## [0.2.0] - 2024-6-21

### Added
- Ability to sign in
- Ability to add a user

### Fixed
- Corrected the model namespace to have the correct name for the project

## [0.2.1] - 2024-6-21

### Changed
- Quick update and Changelog fix


### Known Bugs
- If you don't type in the correct sign in info on the sign in page, it will show errors for into in both forms rather than just the one you pressed


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

## [0.2.3] - 2024-6-25

### Added
- About was given css elements
- image file "GivingFood" added

### Changes
- changes to About.cshtml css to match other pages

## [0.2.4] - 2024-6-27

### Added

- The application will now remember if you are signed in
- Gave the ability to sign out

### Changes
- Applied lazy loading to the images to improve performance

### Removed
- Removed placeholderIndex.svg from the index page as it significantly slowed down the website's performance due to its large size and reliance on external resources. It is simply too large and complex to be called with a single image tag. The file remains in the images folder for reference. We can use its design elements and incorporate them into the index page in a way that maintains performance and SEO.