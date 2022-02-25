-- NOTE: average duration is calculated at a higher level
-- recieve the current avg, update that avg then store the new avg
CREATE TABLE ViewAnalytics (
    viewID INT NOT NULL AUTO_INCREMENT,
    viewTitle VARCHAR(30) NOT NULL,
    displayTotal INT DEFAULT 0,
    durationAvg INT DEFAULT 0,
    CONSTRAINT ViewAnalytics_PK PRIMARY KEY (viewTitle)
);
-- NOTE: some MariaDB versions don't allow functions to be a default value for DATE
CREATE TABLE AdmissionAnalytics (
    accessID INT NOT NULL AUTO_INCREMENT,
    accessDate DATE NOT NULL DEFAULT CURRENT_DATE,
    loginTotal INT DEFAULT 0,
    registrationTotal INT DEFAULT 0,
    CONSTRAINT AdmissionAnalytics_PK PRIMARY KEY (accessDate)
);
CREATE CommunityBoardAnalytics (
    feedID INT NOT NULL AUTO_INCREMENT,
    feedTitle VARCHAR(20) NOT NULL,
    feedPostTotal INT DEFAULT 0,
    CONSTRAINT CommunityBoardAnalytics_PK PRIMARY KEY (feedTitle)
);
-- NOTE: VARCHAR size for username is subject to change
-- NOTE: can possibly use foreign key for registration total and username
CREATE EventListAnalytics (
    eventID INT NOT NULL AUTO_INCREMENT,
    eventAccountUsername VARCHAR(30) NOT NULL,
    eventRegistrationTotal INT DEFAULT 0,
    CONSTRAINT EventListAnalytics_PK PRIMARY KEY (eventAccountUsername)
);