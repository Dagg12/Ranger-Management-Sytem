# The Ranger - Safari Management System

ASP.NET Web Forms / C# / MySQL university project for CMPG213/CMPG223.

## Database
The application uses the existing `SafariManagement` database and these six tables only:

- Client
- Owner
- Accommodation
- GameDrive
- Bookings
- DriveBookings

No tables are created or altered by the application.

## MySQL connection
`Web.config` uses the local MySQL root account supplied for development. Make sure MySQL is running and the database exists before starting the project.

## Development seed data
On application start, if there are no owners, The Ranger creates a development owner account:

- Email: `owner@theranger.co.za`
- Password: `Ranger123`

If `GameDrive` is empty, five real starter game drives are inserted and linked to the first owner. They are fully editable/deletable from the Owner Game Drives page.

If you already have an Owner row, the starter drives are linked to that first owner instead.

## Main pages

### Public / Client
- Default.aspx - landing page
- Login.aspx - client/owner login
- Register.aspx - client registration
- Accommodation.aspx - searchable accommodation inventory
- AccommodationBooking.aspx - accommodation booking
- GameDrives.aspx - searchable game-drive inventory
- GameDriveBooking.aspx - game-drive booking
- ClientDashboard.aspx - client bookings and cancellation

### Owner
- OwnerDashboard.aspx
- OwnerAccommodation.aspx
- OwnerGameDrives.aspx
- OwnerBookings.aspx

## Booking rules implemented
- Only logged-in clients can create bookings.
- Accommodation bookings require valid check-in/check-out dates.
- Accommodation bookings reject dates already occupied by pending/confirmed bookings.
- Guest counts cannot exceed accommodation capacity.
- Game-drive bookings reject dates whose pending/confirmed guest capacity is full.
- Clients can cancel their pending/confirmed bookings.
- Owners can update booking status and manage only their own accommodation and drives.
- Owner registration is not available.


## Fixed in this version

- Fixed ASP.NET Web Forms `Eval(...)` binding crashes by changing all Repeater data-model fields to public properties.
- Fixed Accommodation and Game Drive listing pages so they render instead of throwing `DataBinding` exceptions.
- Fixed Owner Accommodation, Owner Game Drives and Owner Bookings Repeaters for the same binding issue.
- Added automatic starter accommodation inventory as well as starter game drives.
- Added local safari/accommodation images from the supplied image pack.
- Added image selection for accommodation and game-drive cards and booking-detail pages.
- Starter inventory is inserted only when an item with the same name does not already exist, so existing records are not duplicated.
- Added `ImageHelper.cs` to keep image selection out of the database schema.
- Existing database tables and columns remain unchanged.
