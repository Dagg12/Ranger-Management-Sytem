# The Ranger - completed fixes

This build contains the requested fixes for the ASP.NET Web Forms / MySQL safari system.

## Main changes

- Landing-page cards now use the supplied images:
  - Safari Accommodation: A1.jpg, A2.jpg, A3.jpg slideshow.
  - Guided Game Drives: D1.jpg, D2.jpg, D3.jpg slideshow.
  - Easy Booking: rotating safari images from the supplied image set.
- Accommodation and game-drive cards now use the database `image_path` when it points to an existing image, with name-based fallbacks for older records.
- Seeded starter records are assigned to the corresponding supplied images.
- Accommodation filtering now has a real location dropdown populated from available database locations.
- Accommodation guest-capacity filtering uses a typed integer parameter and supports 1+, 2+, 4+, 6+, and 8+ guests.
- Game-drive filtering now also has a database-backed location dropdown.
- Owners can select an existing image or upload a new JPG/JPEG/PNG/WEBP/AVIF image when adding or editing accommodation or game drives.
- Owner location controls offer existing locations plus a new-location field.
- Owner bookings now support search, type, location, status, and booking-date filters.
- Owners can generate a CSV booking report using the active filters.
- Client dashboard booking cards no longer use `Eval("booking_type")` or `Eval` for the total amount; values are assigned through explicit controls in `ItemDataBound`.
- Client money values are explicitly formatted with `N2`, and dashboard counters use `N0` so values are not accidentally truncated or displayed as only the first digit.
- Booking pages now load the selected item reliably on every request, use the database `image_path`, validate the client session, and redirect with `CompleteRequest()` after a successful booking.
- The supplied database schema already contains the required `image_path` columns and location indexes, so no destructive schema rewrite is required.

## Database

The supplied schema already defines `image_path` on both `Accommodation` and `GameDrive`. `DatabaseUpgrade-Optional.sql` is included only for an older database that is missing those columns/indexes. Do not run the full original schema again on a live database because that schema drops and recreates tables.

## Run

1. Open `The Ranger.sln` in Visual Studio.
2. Restore NuGet packages.
3. Confirm MySQL is running and update the connection string in `Web.config` if necessary.
4. Build/Rebuild the solution.
5. Run the site and test as Client and Owner.
6. If an older database has no `image_path` columns, run `DatabaseUpgrade-Optional.sql` first.
