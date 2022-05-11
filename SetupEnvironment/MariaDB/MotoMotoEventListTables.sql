CREATE TABLE Post (
    postID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    postUsername VARCHAR(25) NOT NULL,
    feedName VARCHAR(25) NOT NULL UNIQUE,
    postTitle VARCHAR(100) NOT NULL,
    -- imageID INT UNSIGNED NOT NULL,
    submitUTC TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Post_PK PRIMARY KEY (postID, postTitle),
    -- CONSTRAINT Image_FK FOREIGN KEY (imageID) REFERENCES Image (imageID),
    CONSTRAINT Profile_Post_FK FOREIGN KEY (postUsername) REFERENCES Profile (username),
    CONSTRAINT Feed_FK FOREIGN KEY (feedName) REFERENCES Feed (feedName)
);

CREATE TABLE EventDetails (
    eventID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    --eventLocation VARCHAR(75) NOT NULL,
    eventTime VARCHAR(75) NOT NULL, -- Have to store in UTC Time
    eventDate VARCHAR(75) NOT NULL,
    registeredUsers VARCHAR(75) NOT NULL, -- Should I be storing username? 
    eventStreetAddress VARCHAR(75) NOT NULL,
    eventCity VARCHAR(75) NOT NULL,
    eventState VARCHAR(75) NOT NULL,
    eventCountry VARCHAR(75) NOT NULL,
    eventZipCode VARCHAR(75) NOT NULL,
    postID INT UNSIGNED NOT NULL,
    CONSTRAINT EventDetails_PK PRIMARY KEY (eventID),
    CONSTRAINT Post_FK FOREIGN KEY (postID) REFERENCES Post (postID)
);

-- If I need a separate table for people who registered for an Event
CREATE TABLE Registerers (
    registeredUsers VARCHAR(75) NOT NULL,

    -- CONSTRAINT Registerers_PK PRIMARY KEY (), -- PK would be the postID and postTitle?
    CONSTRAINT Post_FK FOREIGN KEY (postID, postTitle), REFERENCES (postID, postTitle)
)

CREATE TABLE Image (
    postID INT UNSIGNED NOT NULL,
    imageID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    imagePath VARCHAR(100) NOT NULL,
    CONSTRAINT Image_PK PRIMARY KEY (imageID),
    CONSTRAINT Post_Image_FK FOREIGN KEY (postID) REFERENCES Post (postID)
);

-- Unsure if needed for Event List
-- CREATE TABLE Feed (
--     feedID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
--     feedName VARCHAR(25) NOT NULL UNIQUE,
--     CONSTRAINT Feed_PK PRIMARY KEY (feedName)
-- );

-- -- Feed dummy data
-- INSERT INTO Feed
-- VALUES (NULL, 'test');
-- -- Post dummy data
-- INSERT INTO Post
-- VALUES (1, 'test', 'test', 'test', 'test', NULL);
-- -- Image dummy data
-- INSERT INTO Image
-- VALUES (1, NULL, '/Desktop/');