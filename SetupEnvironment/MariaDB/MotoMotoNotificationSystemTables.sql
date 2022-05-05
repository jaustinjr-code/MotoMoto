CREATE TABLE InAppNotificationSystem (
    inAppNotificationID int(50) NOT NULL AUTO_INCREMENT,
    username VARCHAR(25) NOT NULL,
    isNotificationSent BOOL NOT NULL DEFAULT FALSE,
    eventID INT(10) UNSIGNED NOT NULL,
    CONSTRAINT InAppNotificationSystem_PK PRIMARY KEY (inAppNotificationID),
    CONSTRAINT User_FK FOREIGN KEY (username) REFERENCES Profile (username),
    CONSTRAINT EventDetails_FK FOREIGN KEY (eventID) REFERENCES EventDetails (eventID)
);

CREATE TABLE InAppEventDetails (
    eventID INT NOT NULL AUTO_INCREMENT,
    eventTime VARCHAR(75) NOT NULL, 
    eventDate DATE NOT NULL,
    registeredUsers VARCHAR(75) NOT NULL, 
    eventStreetAddress VARCHAR(75) NOT NULL,
    eventCity VARCHAR(75) NOT NULL,
    eventState VARCHAR(75) NOT NULL,
    eventCountry VARCHAR(75) NOT NULL,
    eventZipCode VARCHAR(75) NOT NULL,
    postID INT NOT NULL,
    CONSTRAINT InAppEventDetails_PK PRIMARY KEY (eventID),
    CONSTRAINT InAppEventDetailsPost_FK FOREIGN KEY (postID) REFERENCES Post (postID)
);