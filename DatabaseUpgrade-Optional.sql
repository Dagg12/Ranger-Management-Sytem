-- The current supplied schema already contains image_path on Accommodation and GameDrive.
-- Run this only against an older database that does not have those columns.
SET @has_accommodation_image = (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME='Accommodation' AND COLUMN_NAME='image_path');
SET @sql = IF(@has_accommodation_image=0, 'ALTER TABLE Accommodation ADD COLUMN image_path VARCHAR(500) NULL', 'SELECT 1');
PREPARE stmt FROM @sql; EXECUTE stmt; DEALLOCATE PREPARE stmt;
SET @has_drive_image = (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME='GameDrive' AND COLUMN_NAME='image_path');
SET @sql = IF(@has_drive_image=0, 'ALTER TABLE GameDrive ADD COLUMN image_path VARCHAR(500) NULL', 'SELECT 1');
PREPARE stmt FROM @sql; EXECUTE stmt; DEALLOCATE PREPARE stmt;

-- Optional indexes for the location filters.
SET @has_acc_loc = (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME='Accommodation' AND INDEX_NAME='idx_accommodation_location');
SET @sql = IF(@has_acc_loc=0, 'CREATE INDEX idx_accommodation_location ON Accommodation(location)', 'SELECT 1');
PREPARE stmt FROM @sql; EXECUTE stmt; DEALLOCATE PREPARE stmt;
SET @has_drive_loc = (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME='GameDrive' AND INDEX_NAME='idx_game_drive_location');
SET @sql = IF(@has_drive_loc=0, 'CREATE INDEX idx_game_drive_location ON GameDrive(location)', 'SELECT 1');
PREPARE stmt FROM @sql; EXECUTE stmt; DEALLOCATE PREPARE stmt;
